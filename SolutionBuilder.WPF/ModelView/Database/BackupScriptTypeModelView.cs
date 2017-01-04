using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace SolutionBuilder.WPF.ModelView
{
  /// <summary>
  /// Type of Backups...
  /// </summary>
  public class BackupScriptTypeModelView : INotifyPropertyChanged
  {
    /// <summary>
    /// Native BackupScript
    /// </summary>
    internal BackupScriptType backupScriptType = null;
    internal DatabaseTypeModelView Parent;

    private Boolean isExpanded = false;

    /// <summary>
    /// Name of the Script
    /// </summary>
    public String Name
    {
      get { return backupScriptType.Name; }
      set
      {
        if (backupScriptType.Name == null || !backupScriptType.Name.Equals(value))
        {
          backupScriptType.Name = value;
          OnPropertyChanged("Name");
        }
      }
    }
    /// <summary>
    /// Collection of binded Commands
    /// </summary>
    public ObservableCollection<BackupScriptCommandTypeModelView> Commands
    {
      get { return new ObservableCollection<BackupScriptCommandTypeModelView>(backupScriptType.Commands.Select(c => new BackupScriptCommandTypeModelView(c, this))); }
      set
      {
        backupScriptType.Commands = value == null ? new List<BackupScriptCommandType>() :
          value.Select(c => c.backupScriptCommandType).ToList();
        OnPropertyChanged("Commands");
      }
    }

    /// <summary>
    /// Currently Checked?
    /// </summary>
    public Boolean IsChecked
    {
      get { return backupScriptType.IsChecked; }
      set
      {
        if (!backupScriptType.IsChecked.Equals(value))
        {
          backupScriptType.IsChecked = value;
          OnPropertyChanged("IsChecked");
          IsExpanded = value;
        }

        for (Int32 i = 0; i < this.Commands.Count; ++i)
        {
          this.Commands[i].IsChecked = value;
        }
        OnPropertyChanged("Commands");
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

        for (Int32 i = 0; i < this.Commands.Count; ++i)
        {
          this.Commands[i].IsExpanded = value;
        }
      }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="backupScriptType">Native Backup Script</param>
    /// <param name="Parent">Database Type Parent</param>
    public BackupScriptTypeModelView(BackupScriptType backupScriptType, DatabaseTypeModelView Parent)
    {
      this.Parent = Parent;
      this.backupScriptType = backupScriptType;
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
      foreach (var command in Commands.Where(c => c.IsChecked))
      {
        Boolean b = command._Scripting();
      }

      return true;
    }

    #endregion Script Execution
    #region Packing

    internal Boolean Pack()
    {
      foreach (var command in Commands.Where(c => c.IsChecked))
      {
        Boolean b = command.Pack();
      }
      return false;
    }

    #endregion Packing
  }
}
