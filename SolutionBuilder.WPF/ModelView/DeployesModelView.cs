using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionBuilder.WPF.ModelView
{
    public class DeployModelView : INotifyPropertyChanged
    {
        internal Deploy deploy = null;
        private Boolean isChecked = false;

        public DeployClass Class
        {
            get { return deploy.Class; }
            set
            {
                if (deploy.Class != value)
                {
                    deploy.Class = value;
                    OnPropertyChanged("Class");
                }
            }
        }
        public Boolean IsChecked
        {
            get { return isChecked; }
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    OnPropertyChanged("IsChecked");
                }
            }
        }

        public ObservableCollection<AutoDeployModelView> AutoDeployes
        {
            get { return new ObservableCollection<AutoDeployModelView>(deploy.AutoDeployes.Select(a => new AutoDeployModelView(a))); }
            set
            {
                if (value != null)
                {
                    deploy.AutoDeployes = value.Select(a => a.autoDeploy).ToList();
                    OnPropertyChanged("AutoDeployes");
                }
            }
        }

        public DeployModelView(Deploy deploy)
        {
            this.deploy = deploy;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged
    }

    public class AutoDeployModelView : INotifyPropertyChanged
    {
        internal AutoDeploy autoDeploy = null;
        private Boolean isChecked = false;


        public DeployType DeployType
        {
            get { return autoDeploy.Type; }
            set
            {
                if (autoDeploy.Type != value)
                {
                    autoDeploy.Type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public String Machine
        {
            get { return autoDeploy.Machine; }
            set
            {
                if (autoDeploy.Machine != value)
                {
                    autoDeploy.Machine = value;
                    OnPropertyChanged("Machine");
                }
            }
        }
        public String Path
        {
            get { return autoDeploy.Path; }
            set
            {
                if (autoDeploy.Path != value)
                {
                    autoDeploy.Path = value;
                    OnPropertyChanged("Path");
                }
            }
        }
        public Boolean BackupPrevious
        {
            get { return autoDeploy.BackupPrevious; }
            set
            {
                if (autoDeploy.BackupPrevious != value)
                {
                    autoDeploy.BackupPrevious = value;
                    OnPropertyChanged("BackupPrevious");
                }
            }
        }

        public Boolean IsChecked
        {
            get { return isChecked; }
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    OnPropertyChanged("IsChecked");
                }
            }
        }


        public AutoDeployModelView(AutoDeploy autoDeploy)
        {
            this.autoDeploy = autoDeploy;
        }


        public Boolean Deploy(ObservableCollection<ModuleTypeModelView> moduleType)
        {
            IEnumerable<ModuleTypeModelView> modules = null;
            switch (this.DeployType)
            {
                case SolutionBuilder.DeployType.Modules: modules = moduleType.Where(m => m.Type == EModuleType.Module); break;
                case SolutionBuilder.DeployType.Controls: modules = moduleType.Where(m => m.Type == EModuleType.Control); break;
                default: return false; /* should never enter here, if configuration is well built... */
            }

            if (modules == null || modules.Count() == 0) { return false; }


            return false;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged
    }
}
