using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionBuilder.WPF.ModelView
{
    public class InterfacesTypeModelView : INotifyPropertyChanged, IModulesTypeModelView
    {
        private Boolean isExpanded = false;

        /// <summary>
        /// Native "ControlsType"
        /// </summary>
        internal InterfacesType interfacesType = null;

        public InstallationModelView Parent { get; private set; }


        /// <summary>
        /// Output Path
        /// </summary>
        public String Output
        {
            get { return interfacesType.Output; }
            set
            {
                if (interfacesType.Output == null || !interfacesType.Equals(value))
                {
                    interfacesType.Output = value;
                    OnPropertyChanged("Output");
                }
            }
        }
        /// <summary>
        /// Collection of Controls
        /// </summary>
        public ObservableCollection<ModuleTypeModelView> Interfaces
        {
            get { return new ObservableCollection<ModuleTypeModelView>(interfacesType.Interfaces.Select(m => new ModuleTypeModelView(m, this))); }
            set
            {
                interfacesType.Interfaces =
                  value == null ? new List<ModuleType>() :
                  value.Select(m => m.moduleType).ToList();
                OnPropertyChanged("Controls");
            }
        }
        /// <summary>
        /// Currently Checked?
        /// </summary>
        public Boolean IsChecked
        {
            get { return interfacesType.IsChecked; }
            set
            {
                if (!interfacesType.IsChecked.Equals(value))
                {
                    interfacesType.IsChecked = value;
                    OnPropertyChanged("IsChecked");
                    IsExpanded = value;
                }

                foreach (var control in Interfaces)
                {
                    control.IsChecked = value;
                    control.IsExpanded = value;
                }
                OnPropertyChanged("Controls");
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

                foreach (var control in Interfaces)
                {
                    control.IsExpanded = value;
                }
                OnPropertyChanged("Controls");
            }
        }
        /// <summary>
        /// Number of items Checked
        /// </summary>
        internal Int32 CheckedItemsCount
        {
            get
            {
                return Interfaces == null ? 0 : Interfaces.Where(p => p.IsChecked).Count() + Interfaces.Sum(c => c.CheckedItemsCount);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="controlsType">Native ControlsType</param>
        /// <param name="Parent">Installation</param>
        public InterfacesTypeModelView(InterfacesType interfacesType, InstallationModelView Parent)
        {
            this.Parent = Parent;
            this.interfacesType = interfacesType;
        }

        /// <summary>
        /// Check me or the parent, not children
        /// </summary>
        /// <param name="value">Check or Uncheck</param>
        public void Check(Boolean value)
        {
            if (value)
            {
                interfacesType.IsChecked = value;
                OnPropertyChanged("IsChecked");
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
    }
}
