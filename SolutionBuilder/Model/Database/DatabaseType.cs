using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SolutionBuilder
{
  public partial class DatabaseType : IChildItem<Installation>
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
      foreach (BackupScriptType backup in BackupScripts) { backup.SetParent(this); }
    }
    void IChildItem<Installation>.SetParent(Installation parent)
    {
      SetParent(parent);
    }

    #endregion IChildItem<Installation>

    [XmlElement("BackupScript")]
    public List<BackupScriptType> BackupScripts { get; set; }
    /// <summary>
    /// Is Currently Checked?
    /// </summary>
    [XmlIgnore()]
    public Boolean IsChecked { get; set; }

    public DatabaseType()
    {
      BackupScripts = new List<BackupScriptType>();
    }
  }
}
