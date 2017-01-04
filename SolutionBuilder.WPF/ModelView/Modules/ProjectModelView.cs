using System;
using System.ComponentModel;

namespace SolutionBuilder.WPF.ModelView
{
  /// <summary>
  /// Model View for Project
  /// </summary>
  public class ProjectModelView : INotifyPropertyChanged
  {
    private Boolean isExpanded = false;

    private Boolean allowSelection = true;

    internal ModuleTypeModelView Parent = null;

    /// <summary>
    /// Native Project
    /// </summary>
    internal Project project = null;

    public String FullName
    {
      get
      {
        if (Parent != null)
          return string.Format("{0}\\{1}", Parent.Name, Value);
        else
          return Value;
      }
    }

    /// <summary>
    /// Name of the Project
    /// </summary>
    public String Value
    {
      get { return project.Value; }
      set
      {
        if (project == null || !project.Value.Equals(value))
        {
          project.Value = value;
          OnPropertyChanged("Value");
        }
      }
    }

    /// <summary>
    /// Current Major Value
    /// </summary>
    public Int16 Major
    {
      get { return project.Major; }
      set
      {
        if (!project.Major.Equals(value))
        {
          project.Major = value;
          OnPropertyChanged("Major");
          OnPropertyChanged("Version");
        }
      }
    }
    /// <summary>
    /// Current Minor Value
    /// </summary>
    public Int16 Minor
    {
      get { return project.Minor; }
      set
      {
        if (!project.Minor.Equals(value))
        {
          project.Minor = value;
          OnPropertyChanged("Minor");
          OnPropertyChanged("Version");
        }
      }
    }
    /// <summary>
    /// Current Build Value
    /// </summary>
    public Int16 Build
    {
      get { return project.Build; }
      set
      {
        if (!project.Build.Equals(value))
        {
          project.Build = value;
          OnPropertyChanged("Build");
          OnPropertyChanged("Version");
        }
      }
    }
    /// <summary>
    /// Current Revision Build
    /// </summary>
    public Int16 Revision
    {
      get { return project.Revision; }
      set
      {
        if (!project.Revision.Equals(value))
        {
          project.Revision = value;
          OnPropertyChanged("Revision");
          OnPropertyChanged("Version");
        }
      }
    }
    /// <summary>
    /// Currently Checked?
    /// </summary>
    public Boolean IsChecked
    {
      get { return project.IsChecked; }
      set
      {
        if (!allowSelection && project.IsChecked)
        {
          project.IsChecked = false;
          OnPropertyChanged("IsChecked");
          return;
        }

        if (!project.IsChecked.Equals(value))
        {
          project.IsChecked = value;
          OnPropertyChanged("IsChecked");
        }
        if (Parent != null) Parent.Check(value);
      }
    }
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
    /// Can be selected?
    /// </summary>
    public Boolean AllowSelection
    {
      get { return allowSelection; }
      set
      {
        if (!allowSelection.Equals(value))
        {
          allowSelection = false;
          OnPropertyChanged("AllowSelection");
        }
      }
    }
    /// <summary>
    /// Build Status
    /// </summary>
    public BuildStatusModelView BuildStatus
    {
      get { return new BuildStatusModelView(project.Status); }
      set
      {
        project.Status = value == null ? project.Status : value.buildStatus;
        OnPropertyChanged("BuildStatus");
      }
    }

    public String Version
    {
      get { return String.Format("[{0}.{1}.{2}.{3}]", Major, Minor, Build, Revision); }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="project">Native Project</param>
    /// <param name="Parent">ModuleType Model View</param>
    public ProjectModelView(Project project, ModuleTypeModelView Parent)
    {
      this.Parent = Parent;
      this.project = project;
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
