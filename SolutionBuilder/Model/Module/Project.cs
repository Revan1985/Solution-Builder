using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SolutionBuilder
{
  /// <summary>
  /// Projects to build in Solution
  /// </summary>
  public class Project : IChildItem<ModuleType>
  {
    #region IChildItem<ModuleType>

    [XmlIgnore()]
    public ModuleType Parent
    {
      get;
      internal set;
    }
    ModuleType IChildItem<ModuleType>.Parent
    {
      get { return this.Parent; }
      set { this.Parent = value; }
    }

    internal void SetParent(ModuleType parent)
    {
      this.Parent = parent;
    }
    void IChildItem<ModuleType>.SetParent(ModuleType parent)
    {
      SetParent(parent);
    }

    #endregion IChildItem<ModuleType>

    /// <summary>
    /// Name of the Project
    /// </summary>
    [XmlText()]
    public String Value { get; set; }
    /// <summary>
    /// Current Major AssemblyInfo Version
    /// </summary>
    [XmlIgnore()]
    public Int16 Major { get; set; }

    /// <summary>
    /// Current Minor AssemblyInfo Version
    /// </summary>
    [XmlIgnore()]
    public Int16 Minor { get; set; }

    /// <summary>
    /// Current Build AssemblyInfo Version
    /// </summary>
    [XmlIgnore()]
    public Int16 Build { get; set; }

    /// <summary>
    /// Current Revision AssemblyInfo Version
    /// </summary>
    [XmlIgnore()]
    public Int16 Revision { get; set; }
    /// <summary>
    /// Is Currently Checked?
    /// </summary>
    [XmlIgnore()]
    public Boolean IsChecked { get; set; }
    /// <summary>
    /// Current Status
    /// </summary>
    [XmlIgnore()]
    public BuildStatus Status { get; set; }

    public Project()
    {
      Status = new BuildStatus();
    }
  }
}
