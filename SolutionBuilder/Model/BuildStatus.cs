using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SolutionBuilder
{
  /// <summary>
  /// Contains information about the current building status.
  /// </summary>
  public class BuildStatus
  {
    private String message = null;
    private EBuildModuleStatus status = EBuildModuleStatus.Waiting;

    /// <summary>
    /// Message
    /// </summary>
    public String Message
    {
      get { return message; }
      set { message = value; }
    }
    /// <summary>
    /// Status Message
    /// </summary>
    public String StatusMessage
    {
      get { return status.ToString(); }
    }
    /// <summary>
    /// Build Status Enum
    /// </summary>
    public EBuildModuleStatus Status
    {
      get { return status; }
      set
      {
        status = value;
        switch (status)
        {
          case EBuildModuleStatus.Waiting: ForeColor = Color.Black; break;
          case EBuildModuleStatus.Building: ForeColor = Color.LightSalmon; break;
          case EBuildModuleStatus.Failure: ForeColor = Color.Red; break;
          case EBuildModuleStatus.Warning: ForeColor = Color.Orange; break;
          case EBuildModuleStatus.Success: ForeColor = Color.Green; break;
        }
      }
    }
    /// <summary>
    /// BackgroundColor
    /// </summary>
    public System.Drawing.Color BackColor { get; set; }
    /// <summary>
    /// Foreground Color
    /// </summary>
    public System.Drawing.Color ForeColor { get; set; }

    public BuildStatus()
    {
      Status = EBuildModuleStatus.Waiting;
      BackColor = Color.Transparent;
      //ForeColor = Color.Black;
      message = String.Empty;
    }
  }

  /// <summary>
  /// Enum for Building Status
  /// </summary>
  public enum EBuildModuleStatus : byte
  {
    /// <summary>
    /// Waiting for build
    /// </summary>
    Waiting = 0,
    /// <summary>
    /// Currently Building
    /// </summary>
    Building = 1,
    /// <summary>
    /// Succedded
    /// </summary>
    Success = 2,
    /// <summary>
    /// There are been Warning during build
    /// </summary>
    Warning = 3,
    /// <summary>
    /// The build fails.
    /// </summary>
    Failure = 4,
  }
}
