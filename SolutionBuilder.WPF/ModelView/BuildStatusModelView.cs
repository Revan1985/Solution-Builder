using System;
using System.ComponentModel;
using System.Windows.Media;

namespace SolutionBuilder.WPF.ModelView
{
  public class BuildStatusModelView : INotifyPropertyChanged
  {
    /// <summary>
    /// Native Build Status
    /// </summary>
    internal BuildStatus buildStatus = null;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="buildStatus">Native BuildStatus</param>
    public BuildStatusModelView(BuildStatus buildStatus)
    {
      this.buildStatus = buildStatus;
    }
    /// <summary>
    /// Message
    /// </summary>
    public String Message
    {
      get { return buildStatus.Message; }
      set
      {
        if (buildStatus.Message == null || !buildStatus.Message.Equals(value))
        {
          buildStatus.Message = value;
          OnPropertyChanged("Message");
        }
      }
    }
    /// <summary>
    /// Status Message
    /// </summary>
    public String StatusMessage
    {
      get { return buildStatus.StatusMessage; }
    }
    /// <summary>
    /// Current Status
    /// </summary>
    public EBuildModuleStatus Status
    {
      get { return buildStatus.Status; }
      set
      {
        if (!buildStatus.Status.Equals(value))
        {
          buildStatus.Status = value;
          OnPropertyChanged("Status");
          OnPropertyChanged("Message");
          OnPropertyChanged("StatusMessage");
          OnPropertyChanged("BackColor");
          OnPropertyChanged("ForeColor");
        }
      }
    }
    /// <summary>
    /// Background Color
    /// </summary>
    public Brush BackColor
    {
      get { return new SolidColorBrush(Color.FromArgb(buildStatus.BackColor.A, buildStatus.BackColor.R, buildStatus.BackColor.G, buildStatus.BackColor.B)); }
      set
      {
        SolidColorBrush sb = (SolidColorBrush)value;
        if (sb == null) { return; }

        System.Drawing.Color backColor = buildStatus.BackColor;

        if (!backColor.A.Equals(sb.Color.A) ||
          !backColor.R.Equals(sb.Color.R) ||
          !backColor.G.Equals(sb.Color.G) ||
          !backColor.B.Equals(sb.Color.B))
        {
          buildStatus.BackColor = System.Drawing.Color.FromArgb(sb.Color.A, sb.Color.R, sb.Color.G, sb.Color.B);
          OnPropertyChanged("BackColor");
        }
      }
    }
    /// <summary>
    /// Foreground Color
    /// </summary>
    public Brush ForeColor
    {
      get { return new SolidColorBrush(Color.FromArgb(buildStatus.ForeColor.A, buildStatus.ForeColor.R, buildStatus.ForeColor.G, buildStatus.ForeColor.B)); }
      set
      {
        SolidColorBrush sb = (SolidColorBrush)value;
        if (sb == null) { return; }

        System.Drawing.Color foreColor = buildStatus.ForeColor;

        if (!foreColor.A.Equals(sb.Color.A) ||
          !foreColor.R.Equals(sb.Color.R) ||
          !foreColor.G.Equals(sb.Color.G) ||
          !foreColor.B.Equals(sb.Color.B))
        {
          buildStatus.ForeColor = System.Drawing.Color.FromArgb(sb.Color.A, sb.Color.R, sb.Color.G, sb.Color.B);
          OnPropertyChanged("ForeColor");
        }
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(String propertyName)
    {
      PropertyChangedEventHandler handler = System.Threading.Interlocked.CompareExchange(ref PropertyChanged, PropertyChanged, null);
      if (handler != null) { handler(this, new PropertyChangedEventArgs(propertyName)); }
    }
  }
}
