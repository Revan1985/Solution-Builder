using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace SolutionBuilder.WPF.ModelView
{
  /// <summary>
  /// Root for Databases Export...
  /// </summary>
  public class DatabaseTypeModelView : INotifyPropertyChanged
  {
    private Boolean isExpanded = false;

    /// <summary>
    /// Native Database Type
    /// </summary>
    internal DatabaseType databaseType = null;
    internal InstallationModelView Parent;

    /// <summary>
    /// Collection to the Scripts to execute
    /// </summary>
    public ObservableCollection<BackupScriptTypeModelView> BackupScripts
    {
      get { return new ObservableCollection<BackupScriptTypeModelView>(databaseType.BackupScripts.Select(b => new BackupScriptTypeModelView(b, this))); }
      set
      {
        databaseType.BackupScripts = value == null ? new List<BackupScriptType>() :
          value.Select(b => b.backupScriptType).ToList();
        OnPropertyChanged("BackupScripts");
      }
    }

    /// <summary>
    /// Currently Checked?
    /// </summary>
    public Boolean IsChecked
    {
      get { return databaseType.IsChecked; }
      set
      {
        if (!databaseType.IsChecked.Equals(value))
        {
          databaseType.IsChecked = value;
          OnPropertyChanged("IsChecked");
          IsExpanded = value;
        }

        for (Int32 i = 0; i < this.BackupScripts.Count; ++i)
        {
          this.BackupScripts[i].IsChecked = value;
        }
        OnPropertyChanged("BackupScripts");
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

        for (Int32 i = 0; i < this.BackupScripts.Count; ++i)
        {
          this.BackupScripts[i].IsExpanded = value;
        }
      }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="databaseType">Native DatabaseType</param>
    /// <param name="Parent">Installation Parent</param>
    public DatabaseTypeModelView(DatabaseType databaseType, InstallationModelView Parent)
    {
      this.Parent = Parent;
      this.databaseType = databaseType;
    }

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(String propertyName)
    {
      PropertyChangedEventHandler handler = System.Threading.Interlocked.CompareExchange(ref PropertyChanged, PropertyChanged, null);
      if (handler != null) { handler(this, new PropertyChangedEventArgs(propertyName)); }
    }

    #endregion INotifyPropertyChanged

    #region Script Execution

    internal Boolean Scripting()
    {
      foreach (var script in BackupScripts.Where(b => b.IsChecked))
      {
        Boolean b = script.Scripting();
      }
      return true;
    }

    #endregion Script Execution
    #region Packing

    internal Boolean Pack()
    {
      foreach (var script in BackupScripts.Where(b => b.IsChecked))
      {
        Boolean b = script.Pack();
      }
      return false;
    }

    #endregion Packing
  }
}
