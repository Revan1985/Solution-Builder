using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SolutionBuilder
{
  public partial class BackupScriptType : IChildItem<DatabaseType>
  {
    #region IChildItem<DatabaseType>

    [XmlIgnore()]
    public DatabaseType Parent
    {
      get;
      internal set;
    }
    DatabaseType IChildItem<DatabaseType>.Parent
    {
      get { return this.Parent; }
      set { this.Parent = value; }
    }

    internal void SetParent(DatabaseType parent)
    {
      this.Parent = parent;
      foreach (BackupScriptCommandType command in Commands) { command.SetParent(this); }
    }
    void IChildItem<DatabaseType>.SetParent(DatabaseType parent)
    {
      SetParent(parent);
    }

    #endregion IChildItem<DatabaseType>

    [XmlAttribute("Name")]
    public String Name { get; set; }

    [XmlElement("Command")]
    public List<BackupScriptCommandType> Commands { get; set; }

    /// <summary>
    /// Is Currently Checked?
    /// </summary>
    [XmlIgnore()]
    public Boolean IsChecked { get; set; }

    public BackupScriptType()
    {
      Commands = new List<BackupScriptCommandType>();
    }
  }
}
