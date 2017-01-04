using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SolutionBuilder
{
  public partial class BackupScriptCommandType : IChildItem<BackupScriptType>
  {
    #region IChildItem<BackupScriptType>

    [XmlIgnore()]
    public BackupScriptType Parent
    {
      get;
      internal set;
    }
    BackupScriptType IChildItem<BackupScriptType>.Parent
    {
      get { return this.Parent; }
      set { this.Parent = value; }
    }

    internal void SetParent(BackupScriptType parent)
    {
      this.Parent = parent;
    }
    void IChildItem<BackupScriptType>.SetParent(BackupScriptType parent)
    {
      SetParent(parent);
    }

    #endregion IChildItem<BackupScriptType>

    [XmlText()]
    public String Value { get; set; }

    /// <summary>
    /// Database Server (-S)
    /// </summary>
    [XmlElement("server")]
    public String Server { get; set; }
    /// <summary>
    /// Database Username (-U)
    /// </summary>
    [XmlElement("username")]
    public String Username { get; set; }
    /// <summary>
    /// Database Password ( -P)
    /// </summary>
    [XmlElement("password")]
    public String Password { get; set; }
    /// <summary>
    /// Database Name ( -D)
    /// </summary>
    [XmlElement("database")]
    public String Database { get; set; }
    /// <summary>
    /// Any Additional Info
    /// </summary>
    [XmlIgnore()]
    public String AdditionalInfo { get; set; }
    /// <summary>
    /// Current Query (-Q)
    /// </summary>
    [XmlElement("query")]
    public String Query { get; set; }
    /// <summary>
    /// The Remote Path for exported Database
    /// </summary>
    [XmlIgnore()]
    public String CompleteRemotePath { get; set; }
    /// <summary>
    /// Is Currently Checked?
    /// </summary>
    [XmlIgnore()]
    public Boolean IsChecked { get; set; }
    /// <summary>
    /// Build Status
    /// </summary>
    [XmlIgnore()]
    public BuildStatus Status { get; set; }

    [XmlElement("export_sp")]
    public Boolean StoredProcedures { get; set; }
    [XmlElement("export_view")]
    public Boolean Views { get; set; }
    [XmlElement("export_table")]
    public Boolean Tables { get; set; }
    [XmlElement("export_synonyms")]
    public Boolean Synonyms { get; set; }
    [XmlElement("export_functions")]
    public Boolean Functions { get; set; }
  }
}
