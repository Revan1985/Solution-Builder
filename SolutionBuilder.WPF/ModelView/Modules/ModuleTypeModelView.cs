using Microsoft.Build.BuildEngine;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SolutionBuilder.WPF.ModelView
{ /// <summary>
    /// Model View for ModuleType
    /// </summary>
    public class ModuleTypeModelView : INotifyPropertyChanged
    {
        /// <summary>
        /// Native ModuleType
        /// </summary>
        internal ModuleType moduleType = null;
        /// <summary>
        /// Parent Object (IModuleType)
        /// </summary>
        internal IModulesTypeModelView Parent;

        private Boolean isExpanded = false;

        /// <summary>
        /// Number of items Checked
        /// </summary>
        internal Int32 CheckedItemsCount
        {
            get { return Projects == null ? 0 : Projects.Where(p => p.IsChecked).Count(); }
        }

        /// <summary>
        /// Name of the Module
        /// </summary>
        public String Name
        {
            get { return moduleType.Name; }
            set
            {
                if (moduleType.Name == null || !moduleType.Name.Equals(value))
                {
                    moduleType.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        /// <summary>
        /// Path to Sln File
        /// </summary>
        public String Path
        {
            get { return moduleType.Path; }
            set
            {
                if (moduleType.Path == null || !moduleType.Path.Equals(value))
                {
                    moduleType.Path = value;
                    OnPropertyChanged("Path");
                }
            }
        }
        /// <summary>
        /// Configuration (Debug, Release, ecc.)
        /// </summary>
        public String Configuration
        {
            get { return moduleType.Configuration; }
            set
            {
                if (moduleType.Configuration == null || !moduleType.Configuration.Equals(value))
                {
                    moduleType.Configuration = value;
                    OnPropertyChanged("Configuration");
                }
            }
        }
        /// <summary>
        /// Platform (AnyCPU, Arm, x86, x64)
        /// </summary>
        public EPlatformType Platform
        {
            get { return moduleType.Platform; }
            set
            {
                if (!moduleType.Platform.Equals(value))
                {
                    moduleType.Platform = value;
                    OnPropertyChanged("Platform");
                }
            }
        }
        /// <summary>
        /// Projects to build (single solution can have multiple projects, here the active ones)
        /// </summary>
        public ObservableCollection<ProjectModelView> Projects
        {
            get { return new ObservableCollection<ProjectModelView>(moduleType.Projects.Select(p => new ProjectModelView(p, this))); }
            set
            {
                moduleType.Projects = value == null ? new List<Project>() :
                  value.Select(p => p.project).ToList();
                OnPropertyChanged("Projects");
            }
        }
        /// <summary>
        /// Available platforms for Building
        /// </summary>
        public ObservableCollection<EPlatformType> Platforms
        {
            get { return moduleType.Platforms == null ? new ObservableCollection<EPlatformType>() : new ObservableCollection<EPlatformType>(moduleType.Platforms); }
            set
            {
                moduleType.Platforms = value == null ? new List<EPlatformType>() : value.ToList();
                OnPropertyChanged("Platforms");
            }
        }
        /// <summary>
        /// Available configurations for Building
        /// </summary>
        public ObservableCollection<String> Configurations
        {
            get { return moduleType.Configurations == null ? new ObservableCollection<String>() : new ObservableCollection<String>(moduleType.Configurations); }
            set
            {
                moduleType.Configurations = value == null ? new List<String>() : value.ToList();
                OnPropertyChanged("Configurations");
            }
        }
        /// <summary>
        /// Currently Checked?
        /// </summary>
        public Boolean IsChecked
        {
            get { return moduleType.IsChecked; }
            set
            {
                if (!this.moduleType.IsChecked.Equals(value))
                {
                    this.moduleType.IsChecked = value;
                    OnPropertyChanged("IsChecked");
                    IsExpanded = value;
                }

                foreach (var project in Projects)
                {
                    project.IsChecked = value;
                    project.IsExpanded = value;
                }
                OnPropertyChanged("Projects");
            }
        }
        /// <summary>
        /// Currently Expended?
        /// </summary>
        public Boolean IsExpanded
        {
            get { return isExpanded; }
            set
            {
                if (!isExpanded.Equals(value))
                {
                    isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }
            }
        }
        /// <summary>
        /// Module or Control?
        /// </summary>
        public EModuleType Type
        {
            get { return moduleType.Type; }
        }

        public String Destination
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="moduleType">Native ModuleType</param>
        /// <param name="Parent">ModulesType of ControlsType ModelView?</param>
        public ModuleTypeModelView(ModuleType moduleType, IModulesTypeModelView Parent)
        {
            this.Parent = Parent;
            this.moduleType = moduleType;
        }

        /// <summary>
        /// Check me or the parent, not children
        /// </summary>
        /// <param name="value"></param>
        internal void Check(Boolean value)
        {
            if (value)
            {
                this.moduleType.IsChecked = value;
                OnPropertyChanged("IsChecked");
                ((IModulesTypeModelView)Parent).Check(value);
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = System.Threading.Interlocked.CompareExchange(ref PropertyChanged, PropertyChanged, null);
            if (handler != null) { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }

        #endregion INotifyPropertyChanged

        #region Updating

        internal Boolean Update()
        {
            IModulesTypeModelView Parent = (IModulesTypeModelView)this.Parent;
            InstallationModelView Installation = Parent.Parent;

            FileInfo SourceSln = new FileInfo(System.IO.Path.Combine(Installation.CompleteSourcePath, this.Path));
            if (!SourceSln.Exists)
            {
                Installation.AppendTextBoxLog("Skip Update AssemblyVersion for Solution: {0}", SourceSln.Name);
                Installation.AppendTextBoxLog("File not found.");
                return false;
            }

            Installation.AppendTextBoxLog("AssemblyVersion updating starts for {0}", Name);

            foreach (var project in Projects.Where(p => p.IsChecked))
            {
                var onion_proj = moduleType.FindProject(project.Value);
                if (onion_proj != null)
                {
                    string log;
                    if (!moduleType.UpdateAssemblyInfo(SourceSln, onion_proj, out log,
                           Installation.MajorUpdate ? Installation.MajorVersion : (short)-1,
                           Installation.MinorUpdate ? Installation.MinorVersion : (short)-1,
                           Installation.BuildUpdate ? Installation.BuildVersion : (short)-1,
                           Installation.RevisionUpdate ? Installation.RevisionVersion : (short)-1))
                    {
                        if (Installation.StopCompilationOnError)
                        {
                            return false;
                        }
                    }

                    Installation.AppendTextBoxLog(string.IsNullOrEmpty(log) ? "AssemblyInfo.Build updated." : log);
                    //FileInfo AssemblyInfo = new FileInfo(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.Combine(SourceSln.DirectoryName, onion_proj.Path)), "Properties", "AssemblyInfo.cs"));

                    //String Content = System.IO.File.ReadAllText(AssemblyInfo.FullName);
                    //Regex regex = new Regex(Compiler.AssemblyInfoPattern, RegexOptions.Compiled | RegexOptions.Multiline);
                    //Content = Regex.Replace(Content, Compiler.AssemblyInfoPattern, match =>
                    //  String.Format("[assembly: Assembly{0}Version(\"{1}.{2}.{3}.{4}\")]",
                    //  match.Groups["File"].Value,
                    //  Installation.MajorUpdate ? Installation.MajorVersion.ToString() : match.Groups["Major"].Value,
                    //  Installation.MinorUpdate ? Installation.MinorVersion.ToString() : match.Groups["Minor"].Value,
                    //  Installation.BuildUpdate ? Installation.BuildVersion.ToString() : match.Groups["Build"].Value,
                    //  Installation.RevisionUpdate ? Installation.RevisionVersion.ToString() : match.Groups["Revision"].Value));
                    //File.WriteAllText(AssemblyInfo.FullName, Content);
                    //Installation.AppendTextBoxLog("AssemblyInfo.Build updated.");
                }
                else
                {
                    if (Installation.StopCompilationOnError)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion Updating

        #region Compilation

        /// <summary>
        /// Compile the Module
        /// </summary>
        /// <returns><c>true</c> if ok, <c>false</c> on fail</returns>
        internal Boolean Compile()
        {
            IModulesTypeModelView Parent = (IModulesTypeModelView)this.Parent;
            InstallationModelView Installation = Parent.Parent;

            FileInfo SourceSln = new FileInfo(System.IO.Path.Combine(Installation.CompleteSourcePath, this.Path));
            if (!SourceSln.Exists)
            {
                Installation.AppendTextBoxLog("Skip Compilation for Solution: {0}", SourceSln.Name);
                Installation.AppendTextBoxLog("File not found.");
                return false;
            }

            Installation.AppendTextBoxLog("Compilation started for {0}", Name);

            // In this moment, the VSSolution MUST BE set.
            //if (moduleType.VSSolution == null) { moduleType.VSSolution = Onion.SolutionParser.Parser.SolutionParser.Parse(SourceSln.FullName); }      

            if (Installation.IncrementBuild)
            {
                if (!IncrementAssemblyInfo()) { return false; }
            }

            foreach (var project in Projects.Where(p => p.IsChecked))
            {
                project.BuildStatus.Status = EBuildModuleStatus.Building;
                var onion_proj = moduleType.FindProject(project.Value);
                if (onion_proj != null)
                {
                    FileInfo ProjectPath = new FileInfo(System.IO.Path.Combine(SourceSln.DirectoryName, onion_proj.Path));
                    FileLogger fileLogger = new FileLogger();
                    fileLogger.Parameters = String.Format("LogFile={0}", System.IO.Path.Combine(Installation.CompleteOutputPath, String.Format("{0}.{1}.log", Type, project.Value)));

                    ProjectCollection projectCollection = new ProjectCollection();
                    BuildParameters buildParameters = new BuildParameters();
                    buildParameters.Loggers = new List<ILogger> { fileLogger };
                    IDictionary<String, String> globalProperties = new Dictionary<String, String>();
                    globalProperties.Add("SolutionDir", String.Format("{0}{1}", SourceSln.DirectoryName, SourceSln.DirectoryName.EndsWith("\\") ? String.Empty : "\\"));
                    globalProperties.Add("Configuration", Configuration);
                    globalProperties.Add("Platform", Platform.ToString());

                    if (Installation.AllowMulticoreUsage) { globalProperties.Add("maxcpucount", Environment.ProcessorCount.ToString()); }

                    String[] targetsToBuild = { "Rebuild" };
                    BuildRequestData buildRequestData = new BuildRequestData(ProjectPath.FullName, globalProperties, null, targetsToBuild, null, BuildRequestDataFlags.None);
                    BuildResult buildResult = BuildManager.DefaultBuildManager.Build(buildParameters, buildRequestData);
                    projectCollection.UnregisterAllLoggers();
                    Boolean res = true;
                    String failProj = String.Empty;

                    foreach (String key in buildResult.ResultsByTarget.Keys) //.Where(k => targetsToBuild.Contains(k, EqualityComparer<String>.Default)))
                    {

                        TargetResult result = buildResult.ResultsByTarget[key];
                        String project_value = String.Format("\t - Project {0} [{1}:{2}]", project.Value, Platform.ToString(), Configuration);
                        if (targetsToBuild.Contains(key, EqualityComparer<String>.Default))
                        {
                            Installation.AppendTextBoxLog("{0}{1}Compilation: {2}", project_value, new String(' ', 89 - project_value.Length), result.ResultCode);
                        }
                        else { System.Diagnostics.Debug.WriteLine("{0}{1}Compilation: {2}", project_value, new String(' ', 89 - project_value.Length), result.ResultCode); }
                        if (result.Exception != null)
                        {
                            project.BuildStatus.Message = result.Exception.ToString();
                            project.BuildStatus.Status = EBuildModuleStatus.Failure;
                            Installation.AppendTextBoxLog("\t- Errors: {0}", result.Exception.Message);
                            if (Installation.StopCompilationOnError) { return false; }
                        }
                        switch (result.ResultCode)
                        {
                            case TargetResultCode.Failure: project.BuildStatus.Status = EBuildModuleStatus.Failure;
                                failProj = project.Value;
                                res &= false;
                                break;
                            case TargetResultCode.Skipped:
                            case TargetResultCode.Success: project.BuildStatus.Status = EBuildModuleStatus.Success; res &= true; break;
                        }
                    }

                    project.BuildStatus.OnPropertyChanged("Status");
                    project.BuildStatus.OnPropertyChanged("Message");
                    project.BuildStatus.OnPropertyChanged("StatusMessage");
                    project.BuildStatus.OnPropertyChanged("BackColor");
                    project.BuildStatus.OnPropertyChanged("ForeColor");

                    if (Installation.DeleteLogs && res)
                    {
                        String log = System.IO.Path.Combine(Installation.CompleteOutputPath, String.Format("{0}.{1}.log", Type, project.Value));
                        if (File.Exists(log)) { File.Delete(log); }
                    }

                    if (!res)
                    {
                        throw new InvalidOperationException(String.Format("Compilation fail for solution '{0}' [Project '{1}']", Name, failProj));
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Search for values in AssemblyInfo
        /// </summary>
        internal void SearchAssemblyInfo()
        {
            IModulesTypeModelView Parent = (IModulesTypeModelView)this.Parent;
            InstallationModelView Installation = Parent.Parent;

            FileInfo SourceSln = new FileInfo(System.IO.Path.Combine(Installation.CompleteSourcePath, this.Path));
            if (!SourceSln.Exists)
            {
                foreach (var project in Projects)
                {
                    project.BuildStatus.Message = "Solution not found";
                    project.BuildStatus.Status = EBuildModuleStatus.Failure;
                    project.AllowSelection = false;
                }
                Installation.AppendTextBoxLog("Solution file '{0}' not found.", SourceSln.Name);
                Installation.AllowCompilation = false;
                return;
            }
            if (moduleType.VSSolution == null) { moduleType.VSSolution = Onion.SolutionParser.Parser.SolutionParser.Parse(SourceSln.FullName); }

            foreach (var project in Projects)
            {
                var onion_proj = moduleType.FindProject(project.Value);
                if (onion_proj == null)
                {
                    project.AllowSelection = false;
                    continue;
                }

                //FileInfo assemblyInfo = new FileInfo(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.Combine(SourceSln.DirectoryName, onion_proj.Path)), "Properties", "AssemblyInfo.cs"));
                //if (!assemblyInfo.Exists)
                //{
                //    Installation.AppendTextBoxLog("AssemblyInfo '{0}' not found.", assemblyInfo.FullName);
                //    project.BuildStatus.Message = "AssemblyInfo not found";
                //    project.AllowSelection = false;
                //    continue;
                //}

                string log = "";
                short major = 0;
                short minor = 0;
                short build = 0;
                short revision = 0;

                if (moduleType.ReadAssemblyInfo(SourceSln, onion_proj, out major, out minor, out build, out revision, out log))
                {
                    project.Major = major;
                    project.Minor = minor;
                    project.Build = build;
                    project.Revision = revision;
                    project.BuildStatus.Status = EBuildModuleStatus.Waiting;
                }
                else
                {
                    project.BuildStatus.Message = string.Format("Reading from AssemblyInfo failed:{0}", log);
                    project.AllowSelection = false;
                }

                //String Content = System.IO.File.ReadAllText(assemblyInfo.FullName);
                //Regex regex = new Regex(Compiler.AssemblyInfoPattern, RegexOptions.Compiled | RegexOptions.Multiline);
                //MatchCollection matches = regex.Matches(Content);
                //if (matches != null && matches.Count > 0)
                //{
                //    Match m = matches[0];
                //    project.Major = Convert.ToInt16(m.Groups["Major"].Value);
                //    project.Minor = Convert.ToInt16(m.Groups["Minor"].Value);
                //    project.Build = Convert.ToInt16(m.Groups["Build"].Value);
                //    project.Revision = Convert.ToInt16(m.Groups["Revision"].Value);
                //    project.BuildStatus.Status = EBuildModuleStatus.Waiting;
                //}
                //else
                //{
                //    project.BuildStatus.Message = "Reading from AssemblyInfo failed";
                //    project.AllowSelection = false;
                //}

                List<String> platforms = new List<String>();
                List<String> configurations = new List<String>();
                foreach (var global in moduleType.VSSolution.Global)
                {
                    foreach (var entry in global.Entries.Where(g => g.Key.Contains('|') && !g.Key.Contains('{')))
                    {
                        String[] s = entry.Value.Split('|');
                        if (s.Length > 0) { configurations.Add(s[0]); }
                        if (s.Length > 1) { platforms.Add(s[1]); }

                        //platforms.Add(entry.Value.Split('|')[1]);
                        //configurations.Add(entry.Value.Split('|')[0]);
                    }
                }

                foreach (String s in platforms.Distinct())
                {
                    EPlatformType result;
                    if (Enum.TryParse<EPlatformType>(s.Replace(" ", ""), true, out result))
                    {
                        if (!moduleType.Platforms.Contains(result))
                        {
                            moduleType.Platforms.Add(result);
                        }
                    }
                }
                foreach (String s in configurations.Distinct()) { if (!moduleType.Configurations.Contains(s)) { moduleType.Configurations.Add(s); } }

                Platform = moduleType.Platforms.Contains(EPlatformType.AnyCPU) ? EPlatformType.AnyCPU : EPlatformType.x86;
                Configuration = moduleType.Configurations.Contains("Debug") ? "Debug" : "Release";
            }
            Installation.AllowCompilation = true;
        }
        /// <summary>
        /// Increment Build in the AssemblyInfo.
        /// </summary>
        /// <returns><c>true</c> if ok, <c>false</c> on fail</returns>
        internal Boolean IncrementAssemblyInfo()
        {
            IModulesTypeModelView Parent = (IModulesTypeModelView)this.Parent;
            InstallationModelView Installation = Parent.Parent;

            FileInfo SourceSln = new FileInfo(System.IO.Path.Combine(Installation.CompleteSourcePath, this.Path));
            foreach (var project in Projects.Where(p => p.IsChecked))
            {
                string log = string.Empty;
                var onion_proj = moduleType.FindProject(project.Value);

                Boolean majorUpdate = Installation.MajorUpdate;
                Boolean minorUpdate = Installation.MinorUpdate;
                Boolean buildUpdate = Installation.BuildUpdate;
                Boolean revisionUpdate = Installation.RevisionUpdate;

                if (!majorUpdate && !minorUpdate && !buildUpdate && !revisionUpdate) { buildUpdate = true; }
                bool b = moduleType.UpdateAssemblyInfo(SourceSln, onion_proj, majorUpdate, minorUpdate, buildUpdate, revisionUpdate, out log);


                Installation.AppendTextBoxLog("AssemblyInfo.Build updated: {0}", log);
            }

            return true;
        }

        #endregion Compilation

        #region Packing

        /// <summary>
        /// Pack the Module
        /// </summary>
        /// <returns><c>true</c> if ok, <c>false</c> on fail</returns>
        internal Boolean Pack()
        {
            IModulesTypeModelView Parent = (IModulesTypeModelView)this.Parent;
            InstallationModelView Installation = Parent.Parent;

            FileInfo SourceSln = new FileInfo(System.IO.Path.Combine(Installation.CompleteSourcePath, this.Path));
            if (!SourceSln.Exists)
            {
                Installation.AppendTextBoxLog("Skip Export for Solution: {0}", SourceSln.Name);
                Installation.AppendTextBoxLog("Solution not found.");
                return false;
            }

            Installation.AppendTextBoxLog("Export started for {0}", Name);
            // As for compilation, in this moment, the VSSolution MUST BE set.
            this.Destination = System.IO.Path.Combine(Installation.CompleteOutputPath, Parent.Output, Name);

            foreach (var project in Projects.Where(p => p.IsChecked))
            {
                project.BuildStatus.Status = EBuildModuleStatus.Building;

                var onion_proj = moduleType.FindProject(project.Value);
                if (onion_proj == null)
                {
                    project.BuildStatus.Status = EBuildModuleStatus.Failure;
                    Installation.AppendTextBoxLog("Cannot export project {0}", project.Value);
                    continue;
                }

                Installation.AppendTextBoxLog("\t- {0}: ", onion_proj.Name);

                String configuration = Configuration;
                String platform = Platform == EPlatformType.AnyCPU ? String.Empty : Platform.ToString();
                String sourcePath = System.IO.Path.Combine(SourceSln.DirectoryName, System.IO.Path.GetDirectoryName(onion_proj.Path), "bin", platform, configuration);
                if (!System.IO.Directory.Exists(sourcePath))
                {
                    project.BuildStatus.Status = EBuildModuleStatus.Failure;
                    Installation.AppendTextBoxLog("\t- Bin folder not found. Skipping");
                    continue;
                }

                String destination = System.IO.Path.Combine(this.Destination,
                    Projects.Where(p => p.IsChecked).Count() > 1 ? project.Value : String.Empty);

                try
                {
                    Copy(sourcePath, destination);
                    project.BuildStatus.Status = EBuildModuleStatus.Success;
                }
                catch (Exception e)
                {
                    Installation.AppendTextBoxLog("\t- Error during copy: {0}", e.Message);
                    project.BuildStatus.Status = EBuildModuleStatus.Failure;
                }
            }
            return false;
        }
        /// <summary>
        /// Copy data from source to destination
        /// </summary>
        /// <param name="source">Source Directory</param>
        /// <param name="destination">Destination Directory</param>
        /// <returns><c>true</c> if ok, <c>false</c> on fail</returns>
        private Boolean Copy(String source, String destination)
        {
            IModulesTypeModelView Parent = (IModulesTypeModelView)this.Parent;
            InstallationModelView Installation = Parent.Parent;

            if (String.IsNullOrEmpty(source)) { Installation.AppendTextBoxLog("Source is null"); return false; }
            if (String.IsNullOrEmpty(destination)) { Installation.AppendTextBoxLog("Destination is null"); return false; }
            if (!Directory.Exists(source)) { Installation.AppendTextBoxLog("Source '{0}' does not exists"); return false; }
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
                Installation.AppendTextBoxLog("\t- Destination '{0}' created.", destination);
            }

            String[] files = Directory.GetFiles(source);
            foreach (String file in files)
            {
                Int32 count = Installation.Exclusions.Where(e => file.ToLower().Contains(e)).Count();
                if (count == 0)
                {
                    //Parent.Parent.AppendTextBoxLog("\t- Copying: {0}", file);
                    File.Copy(file, file.Replace(source, destination), true);
                    Installation.AppendTextBoxLog("\t- Copied: {0}", file);
                }
            }
            return true;
        }

        #endregion Packing
    }
}

