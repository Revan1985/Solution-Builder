using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SolutionBuilder
{
  /// <summary>
  /// Class that contains Controls (typeof(<typeparamref name="ModuleType"/>))
  /// </summary>
  public class ControlsType : IModulesType
  {
    #region IChildItem<Installation>

    [XmlIgnore()]
    public Installation Parent
    {
      get;
      internal set;
    }
    Installation IChildItem<Installation>.Parent
    {
      get { return this.Parent; }
      set { this.Parent = value; }
    }

    internal void SetParent(Installation parent)
    {
      this.Parent = parent;
      foreach (ModuleType module in Controls) { module.SetParent(this); }
    }
    void IChildItem<Installation>.SetParent(Installation parent)
    {
      SetParent(parent);
    }

    #endregion IChildItem<Installation>

    /// <summary>
    /// Is Currently Checked?
    /// </summary>
    [XmlIgnore()]
    public Boolean IsChecked { get; set; }
    /// <summary>
    /// Output Path
    /// </summary>
    [XmlAttribute("OutputDir")]
    public String Output { get; set; }
    /// <summary>
    /// List of Controls (typeof(<paramref name="ModulesType"/>))
    /// </summary>
    [XmlElement("Control")]
    public List<ModuleType> Controls { get; set; }
  }
}
