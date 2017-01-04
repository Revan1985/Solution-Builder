using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace SolutionBuilder.WPF.ModelView
{ /// <summary>
    /// ModelView class for the <c>Installation</c>
    /// </summary>
    public class InstallationModelView : INotifyPropertyChanged, IDisposable
    {
        private String completeSourcePath = null;

        private Boolean majorUpdate = false;
        private Boolean minorUpdate = false;
        private Boolean buildUpdate = false;
        private Boolean revisionUpdate = false;

        private Int16 majorVersion = 0;
        private Int16 minorVersion = 0;
        private Int16 buildVersion = 0;
        private Int16 revisionVersion = 0;

        private Boolean automaticDeploy = false;
        private Boolean allowCompilation = false;

        private Boolean onlyModulesAndClients = false;
        private Boolean openDestinationFolder = false;

        private IList collection = null;
        private String BuildLogFile = null;
        private ICommand executeCommand = null;
        private ICommand updateBuildCommand = null;
        private ICommand incrementBuildCommand = null;
        private BackgroundWorker executeBackgroundWorker = new BackgroundWorker()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true,
        };

        private DeployModelView selectedDeploy = null;

        /// <summary>
        /// "Native" installation
        /// </summary>
        internal Installation installation = null;
        /// <summary>
        /// Configuration Parent
        /// </summary>
        internal ConfigurationModelView Parent;
        /// <summary>
        /// Name
        /// </summary>
        public String Name
        {
            get { return installation.Name; }
            set
            {
                if (installation.Name == null || !installation.Name.Equals(value))
                {
                    installation.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        /// <summary>
        /// Source path
        /// </summary>
        public String Source
        {
            get { return installation.Source; }
            set
            {
                if (installation.Source == null || !installation.Source.Equals(value))
                {
                    installation.Source = value;
                    OnPropertyChanged("Source");
                }
            }
        }
        /// <summary>
        /// Output path
        /// </summary>
        public String Output
        {
            get { return installation.Output; }
            set
            {
                if (installation.Output == null || !installation.Output.Equals(value))
                {
                    installation.Output = value;
                    OnPropertyChanged("Output");
                }
            }
        }

        /// <summary>
        /// String to exclude from copy (|)
        /// </summary>
        public String Exclude
        {
            get { return installation.Exclude; }
            set
            {
                if (installation.Exclude == null || !installation.Exclude.Equals(value))
                {
                    installation.Exclude = value;
                    OnPropertyChanged("Exclude");
                }
            }
        }

        public ObservableCollection<String> ExportList
        {
            get { return new ObservableCollection<String>(installation.ExportList); }
        }
        /// <summary>
        /// Compression level for Archive
        /// </summary>
        public UInt16 Compression
        {
            get { return installation.Compression; }
            set
            {
                if (!installation.Compression.Equals(value))
                {
                    installation.Compression = value;
                    OnPropertyChanged("Compression");
                }
            }
        }
        /// <summary>
        /// Compile the Modules/Controls?
        /// </summary>
        public Boolean Compile
        {
            get { return installation.Compile; }
            set
            {
                if (!installation.Compile.Equals(value))
                {
                    installation.Compile = value;
                    OnPropertyChanged("Compile");
                }
            }
        }
        /// <summary>
        /// Delete Logs if no errors happens (default is true)
        /// </summary>
        public Boolean DeleteLogs
        {
            get { return installation.DeleteLogs; }
            set
            {
                if (installation.DeleteLogs != value)
                {
                    installation.DeleteLogs = value;
                    OnPropertyChanged("DeleteLogs");
                }
            }
        }

        /// <summary>
        /// Allow Compilation (is false if occours problems during load)
        /// </summary>
        public Boolean AllowCompilation
        {
            get { return allowCompilation; }
            set
            {
                if (!allowCompilation.Equals(value))
                {
                    allowCompilation = value;
                    OnPropertyChanged("AllowCompilation");
                }
            }
        }
        /// <summary>
        /// Stop the compilation if any error occours
        /// </summary>
        public Boolean StopCompilationOnError
        {
            get { return installation.StopCompilationOnError; }
            set
            {
                if (!installation.StopCompilationOnError.Equals(value))
                {
                    installation.StopCompilationOnError = value;
                    OnPropertyChanged("StopCompilationOnError");
                }
            }
        }
        /// <summary>
        /// Allow use of more than 1 core for build (can it works with single project build ?)
        /// </summary>
        public Boolean AllowMulticoreUsage
        {
            get { return installation.AllowMulticoreUsage; }
            set
            {
                if (!installation.AllowMulticoreUsage.Equals(value))
                {
                    installation.AllowMulticoreUsage = value;
                    OnPropertyChanged("AllowMulticoreUsage");
                }
            }
        }
        /// <summary>
        /// Increment the Build number
        /// </summary>
        public Boolean IncrementBuild
        {
            get { return installation.IncrementBuild; }
            set
            {
                if (!installation.IncrementBuild.Equals(value))
                {
                    installation.IncrementBuild = value;
                    OnPropertyChanged("IncrementBuild");
                }
            }
        }
        /// <summary>
        /// Execute Database Section Scripts
        /// </summary>
        public Boolean ExecuteScripts
        {
            get { return installation.ExecuteScripts; }
            set
            {
                if (!installation.ExecuteScripts.Equals(value))
                {
                    installation.ExecuteScripts = value;
                    OnPropertyChanged("ExecuteScripts");
                }
            }
        }
        /// <summary>
        /// Create a Package (copy file in CompleteOutputPath)
        /// </summary>
        public Boolean CreatePackage
        {
            get { return installation.CreatePackage; }
            set
            {
                if (!installation.CreatePackage.Equals(value))
                {
                    installation.CreatePackage = value;
                    OnPropertyChanged("CreatePackage");
                }
            }
        }
        /// <summary>
        /// Create an Archive from the Package
        /// </summary>
        public Boolean ArchivePackage
        {
            get { return installation.ArchivePackage; }
            set
            {
                if (!installation.ArchivePackage.Equals(value))
                {
                    installation.ArchivePackage = value;
                    OnPropertyChanged("ArchivePackage");
                }
            }
        }
        /// <summary>
        /// The complete destination path
        /// </summary>
        public String CompleteOutputPath
        {
            get { return installation.CompleteOutputPath; }
            set
            {
                if (installation.CompleteOutputPath == null || !installation.CompleteOutputPath.Equals(value))
                {
                    installation.CompleteOutputPath = value;
                    OnPropertyChanged("CompleteOutputPath");
                }
            }
        }
        /// <summary>
        /// SOurce Path (Configuration.Source, Path)
        /// </summary>
        public String CompleteSourcePath
        {
            get
            {
                if (completeSourcePath == null) { completeSourcePath = System.IO.Path.Combine(Parent.Source, this.Source); }
                return completeSourcePath;
            }
            set
            {
                if (completeSourcePath == null || !completeSourcePath.Equals(value))
                {
                    completeSourcePath = value;
                    OnPropertyChanged("CompleteSourcePath");

                    Parent.ClearLog();
                    this.SearchAssemblyInfo();
                }
            }
        }
        /// <summary>
        ///  Exclude Field already splitted by |
        /// </summary>
        public ObservableCollection<String> Exclusions
        {
            get { return new ObservableCollection<String>(installation.Exclusions); }
        }
        /// <summary>
        /// Items to build count
        /// </summary>
        internal Int32 CheckedItemsBuildCount
        {
            get
            {
                return
                  (Modules == null ? 0 : Modules.Sum(c => c.CheckedItemsCount)) +
                  (Controls == null ? 0 : Controls.Sum(c => c.CheckedItemsCount));
            }
        }
        /// <summary>
        /// Items to script count
        /// </summary>
        internal Int32 CheckedItemsScriptCount
        {
            get { return (Database == null ? 0 : Database.BackupScripts.Sum(b => b.Commands.Where(c => c.IsChecked).Count())); }
        }
        /// <summary>
        /// Update the Major Number?
        /// </summary>
        public Boolean MajorUpdate
        {
            get { return majorUpdate; }
            set
            {
                if (!majorUpdate.Equals(value))
                {
                    majorUpdate = value;
                    OnPropertyChanged("MajorUpdate");
                }
            }
        }
        /// <summary>
        /// Update the Minor Number?
        /// </summary>
        public Boolean MinorUpdate
        {
            get { return minorUpdate; }
            set
            {
                if (!minorUpdate.Equals(value))
                {
                    minorUpdate = value;
                    OnPropertyChanged("MinorUpdate");
                }
            }
        }
        /// <summary>
        /// Update the Build Number?
        /// </summary>
        public Boolean BuildUpdate
        {
            get { return buildUpdate; }
            set
            {
                if (!buildUpdate.Equals(value))
                {
                    buildUpdate = value;
                    OnPropertyChanged("BuildUpdate");
                }
            }
        }
        /// <summary>
        /// Update the Revision Number?
        /// </summary>
        public Boolean RevisionUpdate
        {
            get { return revisionUpdate; }
            set
            {
                if (!revisionUpdate.Equals(value))
                {
                    revisionUpdate = value;
                    OnPropertyChanged("RevisionUpdate");
                }
            }
        }
        /// <summary>
        /// Value to give to Major
        /// </summary>
        public Int16 MajorVersion
        {
            get { return majorVersion; }
            set
            {
                if (!majorVersion.Equals(value))
                {
                    majorVersion = value;
                    OnPropertyChanged("MajorVersion");
                }
            }
        }
        /// <summary>
        /// Value to give to Minor
        /// </summary>
        public Int16 MinorVersion
        {
            get { return minorVersion; }
            set
            {
                if (!minorVersion.Equals(value))
                {
                    minorVersion = value;
                    OnPropertyChanged("MinorVersion");
                }
            }
        }
        /// <summary>
        /// Value to give to Build
        /// </summary>
        public Int16 BuildVersion
        {
            get { return buildVersion; }
            set
            {
                if (!buildVersion.Equals(value))
                {
                    buildVersion = value;
                    OnPropertyChanged("BuildVersion");
                }
            }
        }
        /// <summary>
        /// Value to give to Revision
        /// </summary>
        public Int16 RevisionVersion
        {
            get { return revisionVersion; }
            set
            {
                if (!revisionVersion.Equals(value))
                {
                    revisionVersion = value;
                    OnPropertyChanged("RevisionVersion");
                }
            }
        }
        /// <summary>
        /// ModulesType (<c>ModulesType</c> & <c>ControlsType</c>)
        /// </summary>
        public ObservableCollection<Object> ModuleType
        {
            get
            {
                List<ModulesTypeModelView> modules = new List<ModulesTypeModelView>(installation.ModuleType.OfType<ModulesType>().Select(m => new ModulesTypeModelView(m, this)));
                List<ControlsTypeModelView> controls = new List<ControlsTypeModelView>(installation.ModuleType.OfType<ControlsType>().Select(m => new ControlsTypeModelView(m, this)));
                List<InterfacesTypeModelView> interfaces = new List<InterfacesTypeModelView>(installation.ModuleType.OfType<InterfacesType>().Select(m => new InterfacesTypeModelView(m, this)));

                ObservableCollection<Object> o = new ObservableCollection<Object>();

                foreach (var m in modules) { o.Add(m); }
                foreach (var c in controls) { o.Add(c); }
                foreach (var i in interfaces) { o.Add(i); }
                return o;
            }
            set
            {
                installation.ModuleType = value == null ? new List<Object>() : value.ToList();
                OnPropertyChanged("ModuleType");
            }
        }
        /// <summary>
        /// Modules
        /// </summary>
        public ObservableCollection<ModuleTypeModelView> Modules
        {
            get
            {
                return new ObservableCollection<ModuleTypeModelView>(installation.Modules.Select(m => new ModuleTypeModelView(m,
                  installation.ModuleType
                  .OfType<ModulesType>()
                  .Select(ms => new ModulesTypeModelView(ms, this))
                  .FirstOrDefault())));
            }
            //set;
        }
        /// <summary>
        /// Controls
        /// </summary>
        public ObservableCollection<ModuleTypeModelView> Controls
        {
            get
            {
                return new ObservableCollection<ModuleTypeModelView>(installation.Controls.Select(m => new ModuleTypeModelView(m,
                  installation.ModuleType
                  .OfType<ControlsType>()
                  .Select(c => new ControlsTypeModelView(c, this))
                  .FirstOrDefault())));
            }
            //set;
        }

        public ObservableCollection<ModuleTypeModelView> Interfaces
        {
            get
            {
                if (installation.Interfaces == null) { return new ObservableCollection<ModuleTypeModelView>(); }
                return new ObservableCollection<ModuleTypeModelView>(installation.Interfaces.Select(m => new ModuleTypeModelView(m,
                   installation.ModuleType
                   .OfType<InterfacesType>()
                   .Select(c => new InterfacesTypeModelView(c, this))
                   .FirstOrDefault())));
            }
        }

        /// <summary>
        /// Database Export
        /// </summary>
        public DatabaseTypeModelView Database
        {
            get { return new DatabaseTypeModelView(installation.Database, this); }
            set
            {
                installation.Database = value == null ? null : value.databaseType;
                OnPropertyChanged("Database");
            }
        }

        public Boolean OnlyModulesAndClients
        {
            get { return onlyModulesAndClients; }
            set
            {
                onlyModulesAndClients = value;
                OnPropertyChanged("OnlyModulesAndClients");
            }
        }

        public Boolean OpenDestinationFolder
        {
            get { return openDestinationFolder; }
            set
            {
                openDestinationFolder = value;
                OnPropertyChanged("OpenDestinationFolder");
            }
        }

        public Boolean AutomaticDeploy
        {
            get { return automaticDeploy; }
            set
            {
                if (automaticDeploy != value)
                {
                    automaticDeploy = value;
                    OnPropertyChanged("AutomaticDeploy");
                }
            }
        }

        public ObservableCollection<DeployModelView> Deployes
        {
            get
            {
                if (installation == null || installation.Deployes == null)
                    return new ObservableCollection<DeployModelView>();
                return new ObservableCollection<DeployModelView>(installation.Deployes.Select(d => new DeployModelView(d)));
            }
            set
            {
                if (value != null)
                {
                    installation.Deployes = value.Select(d => d.deploy).ToArray();
                    OnPropertyChanged("Deployes");
                }
            }
        }

        public DeployModelView SelectedDeploy
        {
            get { return selectedDeploy; }
            set
            {
                if (value != selectedDeploy)
                {
                    selectedDeploy = value;
                    OnPropertyChanged("SelectedDeploy");
                }
            }
        }


        /// <summary>
        /// Command to call for the "Build" command
        /// </summary>
        public ICommand ExecuteCommand
        {
            get
            {
                if (executeCommand == null)
                {
                    executeCommand = new DelegateCommand(ExecuteExecute, CanExecuteExecute);
                }
                return executeCommand;
            }
        }
        /// <summary>
        /// Command to call to update the current AssemblyInfo version
        /// </summary>
        public ICommand UpdateBuildCommand
        {
            get
            {
                if (updateBuildCommand == null)
                {
                    updateBuildCommand = new DelegateCommand(ExecuteUpdateBuild, CanExecuteUpdateBuild);
                }
                return updateBuildCommand;
            }
        }
        /// <summary>
        /// Called on event "Increment Build Version IsChecked"
        /// </summary>
        public ICommand IncrementBuildCommand
        {
            get
            {
                if (incrementBuildCommand == null)
                {
                    incrementBuildCommand = new DelegateCommand(() =>
                    {
                        if (!IncrementBuild)
                        {
                            MajorUpdate = false;
                            MinorUpdate = false;
                            BuildUpdate = false;
                            RevisionUpdate = false;
                        }
                    });
                }
                return incrementBuildCommand;
            }
        }

        /// <summary>
        /// Get the Modules & Database in only one list (for treeview binding)
        /// </summary>
        public IList Collection
        {
            get
            {
                if (collection == null)
                {
                    collection = new CompositeCollection()
                    {
                        new CollectionContainer() { Collection = ModuleType },
                        new CollectionContainer() { Collection = new [] { Database } },
                    };
                }
                return collection;
            }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="installation">Native Installation</param>
        /// <param name="Parent">Configuration ModelView</param>
        public InstallationModelView(Installation installation, ConfigurationModelView Parent)
        {
            this.Parent = Parent;
            this.installation = installation;
            this.DeleteLogs = true;
            this.AllowMulticoreUsage = true;
            this.StopCompilationOnError = true;
            this.OnlyModulesAndClients = true;

            this.executeBackgroundWorker.DoWork += executeBackgroundWorker_DoWork;
            this.executeBackgroundWorker.ProgressChanged += executeBackgroundWorker_ProgressChanged;
            this.executeBackgroundWorker.RunWorkerCompleted += executeBackgroundWorker_RunWorkerCompleted;
        }

        #region IDisposable

        /// <summary>
        /// Destructor
        /// </summary>
        ~InstallationModelView()
        {
            Dispose(false);
        }
        /// <summary>
        /// Dispose Method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(Boolean disposeManagedResources)
        {
            if (disposeManagedResources)
            {
                // Remove Background Worker Events
                if (this.executeBackgroundWorker != null)
                {
                    this.executeBackgroundWorker.DoWork -= executeBackgroundWorker_DoWork;
                    this.executeBackgroundWorker.ProgressChanged -= executeBackgroundWorker_ProgressChanged;
                    this.executeBackgroundWorker.RunWorkerCompleted -= executeBackgroundWorker_RunWorkerCompleted;
                    this.executeBackgroundWorker.Dispose();
                    this.executeBackgroundWorker = null;
                }
            }
        }

        #endregion IDisposable

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = System.Threading.Interlocked.CompareExchange(ref PropertyChanged, PropertyChanged, null);
            if (handler != null) { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }

        #endregion INotifyPropertyChanged

        /// <summary>
        /// This method appends log either to TextBoxLog and a Log File names [Opts.DestinationDir]/Log_yyyyMMddHHmmss.log where datetime is set to ButtonExecute Click.
        /// The provided string is appended in format [--Datetime--] {0}\n
        /// </summary>
        /// <param name="value">The line to write</param>
        internal void AppendTextBoxLog(String value)
        {
            Parent.Log = String.Format("[{0:yyyy-MM-dd HH:mm:ss}] {1}{2}", DateTime.Now, value, value.EndsWith("\n") ? String.Empty : Environment.NewLine);

            try
            {
                if (!string.IsNullOrEmpty(BuildLogFile))
                    File.AppendAllText(BuildLogFile, String.Format("[{0:yyyy-MM-dd HH:mm:ss}] {1}{2}", DateTime.Now, value, value.EndsWith("\n") ? String.Empty : Environment.NewLine));
            }
            catch { }
        }
        /// <summary>
        /// This method appends log either to TextBoxLog and a Log File names [Opts.DestinationDir]/Log_yyyyMMddHHmmss.log where datetime is set to ButtonExecute Click.
        /// The provided string is appended in format [--Datetime--] {0}\n
        /// </summary>
        /// <param name="value">The text to write (for String.Format)</param>
        /// <param name="p">Parameters for String.Format</param>
        internal void AppendTextBoxLog(String value, params Object[] p)
        {
            AppendTextBoxLog(String.Format(value, p));
        }

        private void ExecuteExecute()
        {
            if (executeBackgroundWorker.IsBusy)
            {
                MessageBox.Show(System.Windows.Application.Current.MainWindow, "Generating. Please wait, or stop running process.");
                return;
            }

            installation.AppendTextBoxLog += AppendTextBoxLog;

            DateTime dt = DateTime.Now;
            BuildLogFile = Path.Combine(Parent.Output, String.Format("Log{0:yyyyMMddHHmmss}.txt", dt));
            if (String.IsNullOrEmpty(CompleteOutputPath)) { CompleteOutputPath = Path.Combine(Parent.Output, dt.ToString("yyyyMMddHHmmss"), Name); }

            AppendTextBoxLog("Starting process with following options:");
            AppendTextBoxLog("\t- Build Modules:\t\t{0}", Compile ? "Y" : "N");
            AppendTextBoxLog("\t- Incremental Build:\t{0}", IncrementBuild ? "Y" : "N");
            AppendTextBoxLog("\t- Exporting Database:\t{0}", ExecuteScripts ? "Y" : "N");
            AppendTextBoxLog("\t- Creating Package:\t{0}", CreatePackage ? "Y" : "N");
            AppendTextBoxLog("\t- Archiving Package:\t{0}", ArchivePackage ? "Y" : "N");
            AppendTextBoxLog("\t- Log File:\t\t{0}", Path.GetFileName(BuildLogFile));

            executeBackgroundWorker.RunWorkerAsync(this);
        }
        private Boolean CanExecuteExecute()
        {
            return
              (this.CheckedItemsBuildCount > 0 ||
              this.CheckedItemsScriptCount > 0) &&
              (this.Compile ||
              this.CreatePackage ||
              this.ExecuteScripts) &&
              !executeBackgroundWorker.IsBusy;
        }

        private void ExecuteUpdateBuild()
        {
            if (!UpdateModules() && StopCompilationOnError) { }
        }
        private Boolean CanExecuteUpdateBuild()
        {
            return
              ((MajorUpdate && MajorVersion > -1) ||
              (MinorUpdate && MinorVersion > -1) ||
              (BuildUpdate && BuildVersion > -1) ||
              (RevisionUpdate && RevisionVersion > -1)) &&
              CheckedItemsBuildCount > 0 && !executeBackgroundWorker.IsBusy;
        }

        private void executeBackgroundWorker_DoWork(Object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            InstallationModelView options = e.Argument as InstallationModelView;
            if (options == null) { throw new ArgumentException("Argument is not a SolutionBuilder.Installation", "e.Argument"); }

            if (!Directory.Exists(options.CompleteOutputPath)) { Directory.CreateDirectory(options.CompleteOutputPath); }

            if (!CompileModules() && this.StopCompilationOnError) { return; }
            if (!ScriptsExecution()) { return; }
            if (!ExecutePacking()) { return; }
            if (!AutomaticDeploying()) { return; }
        }

        private void executeBackgroundWorker_ProgressChanged(Object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
        }

        private void executeBackgroundWorker_RunWorkerCompleted(Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //CompleteOutputPath = String.Empty;
            installation.AppendTextBoxLog -= AppendTextBoxLog;

            if (e.Cancelled)
            {

                MessageBox.Show(System.Windows.Application.Current.MainWindow, "Export cancelled by user.", "Cancelled", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
            }
            else if (e.Error != null)
            {
                MessageBox.Show(System.Windows.Application.Current.MainWindow, String.Format("An error occours during the process.\n{0}", e.Error.Message), "Exception", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show(System.Windows.Application.Current.MainWindow, "Action completed", "Complete", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }

            this.NotifyAllPropertyChanged();
        }

        private Boolean UpdateModules()
        {
            //if (!IncrementBuild)
            //{
            //    AppendTextBoxLog("Update not required...");
            //    return true;
            //}
            AppendTextBoxLog("Starting Update");

            foreach (var module in this.Modules) { foreach (var project in module.Projects) { project.BuildStatus.Status = EBuildModuleStatus.Waiting; } }
            foreach (var control in this.Controls) { foreach (var project in control.Projects) { project.BuildStatus.Status = EBuildModuleStatus.Waiting; } }
            foreach (var interfaces in this.Interfaces) { foreach (var project in interfaces.Projects) { project.BuildStatus.Status = EBuildModuleStatus.Waiting; } }

            foreach (var module in this.Modules.Where(m => m.IsChecked)) { if (!module.Update() && this.StopCompilationOnError) { return false; } }
            foreach (var control in this.Controls.Where(c => c.IsChecked)) { if (!control.Update() && this.StopCompilationOnError) { return false; } }
            foreach (var interfaces in this.Interfaces.Where(c => c.IsChecked)) { if (!interfaces.Update() && this.StopCompilationOnError) { return false; } }

            AppendTextBoxLog("Update Completed");
            return true;
        }

        /// <summary>
        /// Compile the modules
        /// </summary>
        /// <returns><c>true</c> if ok, <c>false</c> on fail</returns>
        private Boolean CompileModules()
        {
            if (!Compile)
            {
                AppendTextBoxLog("Compile not required");
                return true;
            }
            AppendTextBoxLog("Starting Compiling");

            foreach (var module in this.Modules) { foreach (var project in module.Projects) { project.BuildStatus.Status = EBuildModuleStatus.Waiting; } }
            foreach (var control in this.Controls) { foreach (var project in control.Projects) { project.BuildStatus.Status = EBuildModuleStatus.Waiting; } }
            foreach (var interfaces in this.Interfaces) { foreach (var project in interfaces.Projects) { project.BuildStatus.Status = EBuildModuleStatus.Waiting; } }

            foreach (var module in this.Modules.Where(m => m.IsChecked)) { if (!module.Compile() && this.StopCompilationOnError) { return false; } }
            foreach (var control in this.Controls.Where(c => c.IsChecked)) { if (!control.Compile() && this.StopCompilationOnError) { return false; } }
            foreach (var interfaces in this.Interfaces.Where(c => c.IsChecked)) { if (!interfaces.Compile() && this.StopCompilationOnError) { return false; } }

            AppendTextBoxLog("Compile Completed");
            return true;
        }
        /// <summary>
        /// Execute scripts
        /// </summary>
        /// <returns><c>true</c> if ok, <c>false</c> on fail</returns>
        private Boolean ScriptsExecution()
        {
            if (!ExecuteScripts)
            {
                AppendTextBoxLog("Scripting not required");
                return true;
            }
            return Database.Scripting();
        }
        /// <summary>
        /// Pack the export
        /// </summary>
        /// <returns><c>true</c> if ok, <c>false</c> on fail</returns>
        private Boolean ExecutePacking()
        {
            Boolean result = true;
            if (!this.CreatePackage)
            {
                AppendTextBoxLog("Package creation not required");
                return true;
            }

            if (this.onlyModulesAndClients && ExportList != null && ExportList.Count > 0)
            {
                Dictionary<String, List<String>> exportList = new Dictionary<String, List<String>>();
                foreach (String value in ExportList)
                {
                    String[] values = value.Split('/');
                    if (exportList.ContainsKey(values[0]))
                    {
                        exportList[values[0]].Add(values[1]);
                    }
                    else
                    {
                        exportList.Add(values[0], new List<String> { values[1] });
                    }
                }

                if (exportList.ContainsKey("Modules"))
                {
                    foreach (var module in
                        this.Modules.Where(m => m.IsChecked && (exportList["Modules"].Contains(m.Name) || exportList["Modules"].Contains("*"))))
                    {
                        result &= module.Pack();
                    }
                }
                if (exportList.ContainsKey("Controls"))
                {
                    foreach (var control in
                        this.Controls.Where(c => c.IsChecked && (exportList["Controls"].Contains(c.Name) || exportList["Controls"].Contains("*"))))
                    {
                        result &= control.Pack();
                    }
                }
                if (exportList.ContainsKey("Interfaces"))
                {
                    foreach (var interfaces in
                        this.Interfaces.Where(c => c.IsChecked && (exportList["Interfaces"].Contains(c.Name) || exportList["Interfaces"].Contains("*"))))
                    {
                        result &= interfaces.Pack();
                    }
                }
            }
            else
            {
                foreach (var module in this.Modules.Where(m => m.IsChecked)) { result &= module.Pack(); }
                foreach (var control in this.Controls.Where(c => c.IsChecked)) { result &= control.Pack(); }
                foreach (var interfaces in this.Interfaces.Where(c => c.IsChecked)) { result &= interfaces.Pack(); }
                result &= Database.Pack();
            }

            if (ArchivePackage) { result &= ArchivePackageCreation(); }
            if (OpenDestinationFolder) { System.Diagnostics.Process.Start(CompleteOutputPath); }
            return result;
        }
        /// <summary>
        /// Create an archive (zip or 7z), for the export
        /// </summary>
        /// <returns><c>true</c> if ok, <c>false</c> on fail</returns>
        private Boolean ArchivePackageCreation()
        {
            //String CompleteOutputZipPath = String.Format("{0}.7z", CompleteOutputPath);
            //using (MemoryStream ms = new MemoryStream())
            //{
            //  SevenZip.SDK.Common.OutBuffer buffer = new SevenZip.SDK.Common.OutBuffer(4096);

            //  buffer.Init();
            //  buffer.SetStream(ms);

            //  var fileList = Zip.GenerateFileList(CompleteOutputPath, new[] { ".log" });
            //  foreach (String file in fileList)
            //  {
            //    using (FileStream readStream = File.OpenRead(file))
            //    {
            //      Int32 len = 4096;
            //      Byte[] byteBuffer = new Byte[len];
            //      while ((len = readStream.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
            //      {
            //        for (Int32 i = 0; i < len; ++i)
            //        {
            //          buffer.WriteByte(byteBuffer[i]);
            //        }
            //      }
            //    }
            //  }


            //  using (FileStream fs = File.OpenWrite(CompleteOutputZipPath))
            //  {
            //    SevenZip.SDK.Compress.LZMA.Encoder encoder = new SevenZip.SDK.Compress.LZMA.Encoder();
            //    encoder.WriteCoderProperties(ms);
            //    ms.WriteTo(fs);
            //  }

            //  buffer.CloseStream();
            //}

            return true;
        }
        /// <summary>
        /// Deploy the Modules & Controls Automatically
        /// </summary>
        /// <returns></returns>
        private Boolean AutomaticDeploying()
        {
            if (!AutomaticDeploy) { return false; }
            for (Int32 i = 0; i < this.Deployes.Count; ++i)
            {
                DeployModelView view = this.Deployes[i];
                if (!view.IsChecked) { continue; }
                for (Int32 j = 0; j < view.AutoDeployes.Count; ++j)
                {
                    AutoDeployModelView deploy = view.AutoDeployes[j];
                    if (!deploy.IsChecked) { continue; }

                    if (!deploy.Deploy(this.Modules))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Search for the Assembly Information
        /// </summary>
        async internal void SearchAssemblyInfo()
        {
            await Task.Run(() =>
            {
                foreach (var module in this.Modules) { module.SearchAssemblyInfo(); }
                foreach (var control in this.Controls) { control.SearchAssemblyInfo(); }
                foreach (var interfaces in this.Interfaces) { interfaces.SearchAssemblyInfo(); }
            });
        }

        private void NotifyAllPropertyChanged()
        {
            OnPropertyChanged("Collection");
        }
    }
}
