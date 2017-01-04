using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace SolutionBuilder.WPF.ModelView
{
  /// <summary>
  /// Model View for ControlsType
  /// </summary>
  public class ControlsTypeModelView : INotifyPropertyChanged, IModulesTypeModelView
  {
    private Boolean isExpanded = false;

    /// <summary>
    /// Native "ControlsType"
    /// </summary>
    internal ControlsType controlsType = null;

    public InstallationModelView Parent { get; private set; }


    /// <summary>
    /// Output Path
    /// </summary>
    public String Output
    {
      get { return controlsType.Output; }
      set
      {
        if (controlsType.Output == null || !controlsType.Equals(value))
        {
          controlsType.Output = value;
          OnPropertyChanged("Output");
        }
      }
    }
    /// <summary>
    /// Collection of Controls
    /// </summary>
    public ObservableCollection<ModuleTypeModelView> Controls
    {
      get { return new ObservableCollection<ModuleTypeModelView>(controlsType.Controls.Select(m => new ModuleTypeModelView(m, this))); }
      set
      {
        controlsType.Controls =
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
      get { return controlsType.IsChecked; }
      set
      {
        if (!controlsType.IsChecked.Equals(value))
        {
          controlsType.IsChecked = value;
          OnPropertyChanged("IsChecked");
          IsExpanded = value;
        }

        foreach (var control in Controls)
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

        foreach (var control in Controls)
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
        return Controls == null ? 0 : Controls.Where(p => p.IsChecked).Count() + Controls.Sum(c => c.CheckedItemsCount);
      }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="controlsType">Native ControlsType</param>
    /// <param name="Parent">Installation</param>
    public ControlsTypeModelView(ControlsType controlsType, InstallationModelView Parent)
    {
      this.Parent = Parent;
      this.controlsType = controlsType;
    }

    /// <summary>
    /// Check me or the parent, not children
    /// </summary>
    /// <param name="value">Check or Uncheck</param>
    public void Check(Boolean value)
    {
      if (value)
      {
        controlsType.IsChecked = value;
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
