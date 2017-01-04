using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace SolutionBuilder.WPF.ModelView
{
  /// <summary>
  /// Model View for ModulesType
  /// </summary>
  public class ModulesTypeModelView : INotifyPropertyChanged, IModulesTypeModelView
  {
    private Boolean isExpanded = false;

    /// <summary>
    /// Native "ModulesType"
    /// </summary>
    internal ModulesType modulesType = null;
    public InstallationModelView Parent { get; private set; }

    /// <summary>
    /// Output Path
    /// </summary>
    public String Output
    {
      get { return modulesType.Output; }
      set
      {
        if (modulesType.Output == null || !modulesType.Equals(value))
        {
          modulesType.Output = value;
          OnPropertyChanged("Output");
        }
      }
    }
    /// <summary>
    /// Collection of Modules
    /// </summary>
    public ObservableCollection<ModuleTypeModelView> Modules
    {
      get { return new ObservableCollection<ModuleTypeModelView>(modulesType.Modules.Select(m => new ModuleTypeModelView(m, this))); }
      set
      {
        modulesType.Modules =
          value == null ? new List<ModuleType>() :
          value.Select(m => m.moduleType).ToList();
        OnPropertyChanged("Modules");
      }
    }
    /// <summary>
    /// Currently Checked?
    /// </summary>
    public Boolean IsChecked
    {
      get { return modulesType.IsChecked; }
      set
      {
        if (!modulesType.IsChecked.Equals(value))
        {
          modulesType.IsChecked = value;
          OnPropertyChanged("IsChecked");
          IsExpanded = value;
        }

        foreach (var module in Modules)
        {
          module.IsChecked = value;
          module.IsExpanded = value;
        }
        OnPropertyChanged("Modules");
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
        foreach (var module in Modules)
        {
          module.IsExpanded = value;
        }
        OnPropertyChanged("Modules");
      }
    }
    /// <summary>
    /// Number of items Checked
    /// </summary>
    internal Int32 CheckedItemsCount
    {
      get
      {
        return Modules == null ? 0 : Modules.Where(p => p.IsChecked).Count() + Modules.Sum(c => c.CheckedItemsCount);
      }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="modulesType">Native ModulesType</param>
    /// <param name="Parent">Installation</param>
    public ModulesTypeModelView(ModulesType modulesType, InstallationModelView Parent)
    {
      this.Parent = Parent;
      this.modulesType = modulesType;
    }

    /// <summary>
    /// Check me or the parent, not children
    /// </summary>
    /// <param name="value">Check or Uncheck</param>
    public void Check(Boolean value)
    {
      if (value)
      {
        modulesType.IsChecked = value;
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