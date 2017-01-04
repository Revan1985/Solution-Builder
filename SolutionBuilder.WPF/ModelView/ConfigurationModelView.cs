using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace SolutionBuilder.WPF.ModelView
{
    /// <summary>
    /// ModelView class for the <c>Configuration</c>
    /// </summary>
    public class ConfigurationModelView : INotifyPropertyChanged
    {
        private InstallationModelView selectedInstallation = null;
        private Object logLock = new Object();
        private StringBuilder log = null;

        /// <summary>
        /// "Native" configuration
        /// </summary>
        internal Configuration configuration = null;

        /// <summary>
        /// Log
        /// </summary>
        public String Log
        {
            get
            {
                lock (logLock)
                {
                    return log.ToString();
                }
            }
            set
            {
                lock (logLock)
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        if (value.EndsWith("\n")) { log.Append(value); }
                        else { log.AppendLine(value); }
                    }
                    OnPropertyChanged("Log");
                }
            }
        }
        /// <summary>
        /// Base Output Path
        /// </summary>
        public String Output
        {
            get { return configuration.Output; }
            set
            {
                if (configuration.Output == null || !configuration.Output.Equals(value, StringComparison.CurrentCulture))
                {
                    configuration.Output = value;
                    OnPropertyChanged("Output");
                }
            }
        }
        /// <summary>
        /// Base Source Path (combined with installation & module paths give the real source path)
        /// </summary>
        public String Source
        {
            get { return configuration.Source; }
            set
            {
                if (configuration.Source == null || !configuration.Source.Equals(value))
                {
                    configuration.Source = value;
                    OnPropertyChanged("Source");
                }
            }
        }
        /// <summary>
        /// Available Installations
        /// </summary>
        public ObservableCollection<InstallationModelView> Installations
        {
            get { return new ObservableCollection<InstallationModelView>(configuration.Installations.Select(c => new InstallationModelView(c, this))); }
            set
            {
                configuration.Installations =
                  value == null ? new List<Installation>() :
                  value.Select(c => c.installation).ToList();
                OnPropertyChanged("Installations");
            }
        }
        /// <summary>
        /// Current Selected Installation
        /// </summary>
        public InstallationModelView SelectedInstallation
        {
            get { return selectedInstallation; }
            set
            {
                if (selectedInstallation == null || !selectedInstallation.Equals(value))
                {
                    selectedInstallation = value;

                    log.Clear();
                    Log = String.Format("Currently selected {0}\n", selectedInstallation == null ? "null" : selectedInstallation.Name);
                    selectedInstallation.SearchAssemblyInfo();
                    OnPropertyChanged("SelectedInstallation");
                }
            }
        }
        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="configuration">"Native" configuration</param>
        public ConfigurationModelView(Configuration configuration)
        {
            this.configuration = configuration;
            this.log = new System.Text.StringBuilder();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = System.Threading.Interlocked.CompareExchange(ref PropertyChanged, PropertyChanged, null);
            if (handler != null) { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }

        #endregion INotifyPropertyChanged

        private ICommand reloadSettingsCommand = null;
        /// <summary>
        /// Command used for Reload the Settings (my "native" self...)
        /// </summary>
        public ICommand ReloadSettingsCommand
        {
            get
            {
                if (reloadSettingsCommand == null)
                {
                    reloadSettingsCommand = new DelegateCommand(ReloadExecute);
                }
                return reloadSettingsCommand;
            }
        }

        private void ReloadExecute()
        {
            String error;
            configuration = Configuration.Load("Config.xml", out error);
            if (configuration == null)
            {
                MessageBox.Show(error, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                return;
            }
            OnPropertyChanged("Installations");
            OnPropertyChanged("Output");
        }

        internal void ClearLog()
        {
            this.log.Clear();
            OnPropertyChanged("Log");
        }
    }
}
