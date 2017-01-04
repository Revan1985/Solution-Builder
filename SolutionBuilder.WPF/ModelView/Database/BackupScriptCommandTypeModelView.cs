using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace SolutionBuilder.WPF.ModelView
{
  /// <summary>
  /// Backup Script Command ModelView
  /// </summary>
  public class BackupScriptCommandTypeModelView : INotifyPropertyChanged
  {
    private Boolean isExpanded = false;

    /// <summary>
    /// Native Command
    /// </summary>
    internal BackupScriptCommandType backupScriptCommandType = null;
    internal BackupScriptTypeModelView Parent;

    /// <summary>
    /// String with the complete Command
    /// </summary>
    public String Value
    {
      get { return backupScriptCommandType.Value; }
      set
      {
        if (backupScriptCommandType.Value == null || !backupScriptCommandType.Value.Equals(value))
        {
          backupScriptCommandType.Value = value;
          OnPropertyChanged("Value");
          ExpandScriptValue();
        }
      }
    }

    /// <summary>
    /// Name of te Server
    /// </summary>
    public String Server
    {
      get { return backupScriptCommandType.Server; }
      set
      {
        if (backupScriptCommandType.Server == null || !backupScriptCommandType.Server.Equals(value))
        {
          backupScriptCommandType.Server = value;
          OnPropertyChanged("Server");
        }
      }
    }
    /// <summary>
    /// Username
    /// </summary>
    public String Username
    {
      get { return backupScriptCommandType.Username; }
      set
      {
        if (backupScriptCommandType.Username == null || !backupScriptCommandType.Username.Equals(value))
        {
          backupScriptCommandType.Username = value;
          OnPropertyChanged("Username");
        }
      }
    }
    /// <summary>
    /// Password
    /// </summary>
    public String Password
    {
      get { return backupScriptCommandType.Password; }
      set
      {
        if (backupScriptCommandType.Password == null || !backupScriptCommandType.Password.Equals(value))
        {
          backupScriptCommandType.Password = value;
          OnPropertyChanged("Password");
        }
      }
    }
    /// <summary>
    /// Databasename
    /// </summary>
    public String Database
    {
      get { return backupScriptCommandType.Database; }
      set
      {
        if (backupScriptCommandType.Database == null || !backupScriptCommandType.Database.Equals(value))
        {
          backupScriptCommandType.Database = value;
          OnPropertyChanged("Database");
        }
      }
    }
    /// <summary>
    /// Any additional info
    /// </summary>
    public String AdditionalInfo
    {
      get { return backupScriptCommandType.AdditionalInfo; }
      set
      {
        if (backupScriptCommandType.AdditionalInfo == null || !backupScriptCommandType.AdditionalInfo.Equals(value))
        {
          backupScriptCommandType.AdditionalInfo = value;
          OnPropertyChanged("AdditionalInfo");
        }
      }
    }
    /// <summary>
    /// Query
    /// </summary>
    public String Query
    {
      get { return backupScriptCommandType.Query; }
      set
      {
        if (backupScriptCommandType.Query == null || !backupScriptCommandType.Query.Equals(value))
        {
          backupScriptCommandType.Query = value;
          OnPropertyChanged("Query");
        }
      }
    }
    /// <summary>
    /// The Remote Path for the File Copy
    /// </summary>
    public String CompleteRemotePath
    {
      get { return backupScriptCommandType.CompleteRemotePath; }
      set
      {
        if (backupScriptCommandType.CompleteRemotePath == null || !backupScriptCommandType.CompleteRemotePath.Equals(value))
        {
          backupScriptCommandType.CompleteRemotePath = value;
          OnPropertyChanged("CompleteRemotePath");
        }
      }
    }
    /// <summary>
    /// Currently Checked?
    /// </summary>
    public Boolean IsChecked
    {
      get { return backupScriptCommandType.IsChecked; }
      set
      {
        if (!backupScriptCommandType.IsChecked.Equals(value))
        {
          backupScriptCommandType.IsChecked = value;
          OnPropertyChanged("IsChecked");
          IsExpanded = value;
        }
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
      }
    }

    public Boolean StoredProcedures
    {
      get { return backupScriptCommandType.StoredProcedures; }
      set
      {
        if (!backupScriptCommandType.StoredProcedures.Equals(value))
        {
          backupScriptCommandType.StoredProcedures = value;
          OnPropertyChanged("StoredProcedure");
        }
      }
    }
    public Boolean Views
    {
      get { return backupScriptCommandType.Views; }
      set
      {
        if (!backupScriptCommandType.Views.Equals(value))
        {
          backupScriptCommandType.Views = value;
          OnPropertyChanged("Views");
        }
      }
    }
    public Boolean Tables
    {
      get { return backupScriptCommandType.Tables; }
      set
      {
        if (!backupScriptCommandType.Tables.Equals(value))
        {
          backupScriptCommandType.Tables = value;
          OnPropertyChanged("Tables");
        }
      }
    }
    public Boolean Synonyms
    {
      get { return backupScriptCommandType.Synonyms; }
      set
      {
        if (!backupScriptCommandType.Synonyms.Equals(value))
        {
          backupScriptCommandType.Synonyms = value;
          OnPropertyChanged("Synonyms");
        }
      }
    }
    public Boolean Functions
    {
      get { return backupScriptCommandType.Functions; }
      set
      {
        if (!backupScriptCommandType.Functions.Equals(value))
        {
          backupScriptCommandType.Functions = value;
          OnPropertyChanged("Functions");
        }
      }
    }

    public String Command
    {
      get { return String.Format("sqlcmd -U {0} -P {1} - S {2} -D {3} -Q \"{4}\"", Username, Password, Server, Database, Query); }
    }

    /// <summary>
    /// Build status
    /// </summary>
    public BuildStatusModelView Status
    {
      get { return new BuildStatusModelView(backupScriptCommandType.Status); }
      set
      {
        backupScriptCommandType.Status = value == null ? null : value.buildStatus;
        OnPropertyChanged("Status");
      }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="backupScriptCommandType">Native Command</param>
    /// <param name="Parent">BackupScript Parent</param>
    public BackupScriptCommandTypeModelView(BackupScriptCommandType backupScriptCommandType, BackupScriptTypeModelView Parent)
    {
      this.Parent = Parent;
      this.backupScriptCommandType = backupScriptCommandType;
      this.ExpandScriptValue();
    }

    /// <summary>
    /// Split the Query from single line to multiple values...
    /// </summary>
    private void ExpandScriptValue()
    {
      if (String.IsNullOrEmpty(Value)) { /* Nothing to expand */ return; }
      String[] parts = Value.Split(new[] { " -" }, StringSplitOptions.RemoveEmptyEntries);

      this.Query = parts.Where(p => p[0].Equals('Q') || p[0].Equals('q')).FirstOrDefault().Remove(0, 2);        // Query is under     -Q
      this.Server = parts.
        Where(p => p[0].Equals('S') || p[0].Equals('s') && !p.Equals("sqlcmd", StringComparison.CurrentCultureIgnoreCase)).
        FirstOrDefault().Remove(0, 2);                                                                          // Server is under    -S
      this.Username = parts.Where(p => p[0].Equals('U') || p[0].Equals('u')).FirstOrDefault().Remove(0, 2);     // Username is under  -U
      this.Password = parts.Where(p => p[0].Equals('P') || p[0].Equals('p')).FirstOrDefault().Remove(0, 2);     // Password is under  -P
      this.Database = parts.Where(p => p[0].Equals('D') || p[0].Equals('d')).FirstOrDefault().Remove(0, 2);     // Database is under  -D

      if (!String.IsNullOrEmpty(this.Query))
      {
        Int32 index = this.Query.IndexOf('N');                                                                // Search for <N>' D:\
        String disk = this.Query.Substring(index + 2, 3);                                                     // Split N' <D:\>
        String Query = this.Query.Replace(disk, String.Format(@"\\{0}\{1}", Server, disk.Replace(':', '$'))); // Temporary Query (for Complete remote path)
        index = Query.IndexOf('N');                                                                           // Search for the next <N>
        this.CompleteRemotePath = Query.Substring(index + 1, Query.IndexOf("'", index + 2) - (index + 1));    // Extract the path
      }
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
      Parent.Parent.Parent.AppendTextBoxLog("Executing Query for {0}", Parent.Name);

      String query = Command.Replace("%%DATETIME%%", DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss")).Replace("%%DATE%%", DateTime.Now.ToString("yyyy_MM_dd"));
      System.Diagnostics.Process process = new System.Diagnostics.Process()
      {
        StartInfo = new System.Diagnostics.ProcessStartInfo()
        {
          Arguments = String.Format("/C {0}", query),
          CreateNoWindow = true,
          FileName = "CMD.exe",
          RedirectStandardOutput = true,
          UseShellExecute = false,
        },
      };
      process.Start();
      process.WaitForExit();

      StringBuilder sb = new StringBuilder();
      while (!process.StandardOutput.EndOfStream) { sb.AppendLine(process.StandardOutput.ReadLine()); }
      Parent.Parent.Parent.AppendTextBoxLog("Output: {0}", sb.ToString());
      Parent.Parent.Parent.AppendTextBoxLog("ExitCode: {0}", process.ExitCode);

      return true;
    }
    internal Boolean _Scripting()
    {
      // False if almost one of the needed info is empty
      if (String.IsNullOrEmpty(Server) || String.IsNullOrEmpty(Database) || String.IsNullOrEmpty(Query))
      {
        return false;
      }

      StringBuilder connectionString = new StringBuilder();
      connectionString.AppendFormat("Server = {0}; Database = {1};", Server, Database);
      connectionString.AppendFormat(String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password) ?
        "Trusted_Connection = True;" :
        "Trusted_Connection = False;User Id = {0}; Password = {1};", Username, Password);

      SqlConnection connection = null;
      try
      {
        Parent.Parent.Parent.AppendTextBoxLog("\t- Connection to '{0}' with following parameters:", Parent.Name);
        Parent.Parent.Parent.AppendTextBoxLog("\t- Server: {0}", Server);
        Parent.Parent.Parent.AppendTextBoxLog("\t- Database: {0}", Database);
        Parent.Parent.Parent.AppendTextBoxLog("\t- Username: {0}", Username);
        Parent.Parent.Parent.AppendTextBoxLog("\t- Password: {0}", new String('*', Password.Length));

        connection = new SqlConnection(connectionString.ToString());

        try { connection.Open(); }
        catch (Exception e)
        {
          Parent.Parent.Parent.AppendTextBoxLog("Db Connection Fails: {0}", e.Message);
          return false;
        }

        Parent.Parent.Parent.AppendTextBoxLog("Db Connection success for '{0}'", Parent.Name);
        Parent.Parent.Parent.AppendTextBoxLog("Executing Query for {0}", Parent.Name);

        String query = Value.Replace("%%DATETIME%%", DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss")).Replace("%%DATE%%", DateTime.Now.ToString("yyyy_MM_dd"));
        using (SqlCommand command = new SqlCommand(query, connection))
        {
          Int32 rows = command.ExecuteNonQuery();
          Parent.Parent.Parent.AppendTextBoxLog("?");
          return true; // For now...
        }
      }
      finally
      {
        if (connection != null)
        {
          if (connection.State == ConnectionState.Open) { connection.Close(); }
          connection.Dispose();
          connection = null;
        }
      }
    }

    #endregion Script Execution
    #region Packing

    internal Boolean Pack()
    {
      try
      {
        if (!Directory.Exists(CompleteRemotePath)) { return false; }
        Parent.Parent.Parent.AppendTextBoxLog("Exporting BackupScript: {0}", Parent.Name);
        File.Copy(CompleteRemotePath, Path.Combine(Parent.Parent.Parent.CompleteOutputPath, Path.GetFileName(CompleteRemotePath)));
      }
      catch (FileNotFoundException fnf_ex)
      {
        Parent.Parent.Parent.AppendTextBoxLog("FileNotFound Exception:");
        Parent.Parent.Parent.AppendTextBoxLog(fnf_ex.ToString());
      }
      catch (DirectoryNotFoundException dnf_ex)
      {
        Parent.Parent.Parent.AppendTextBoxLog("DirectoryNotFound Exception:");
        Parent.Parent.Parent.AppendTextBoxLog(dnf_ex.ToString());
      }
      catch (Exception ex)
      {
        Parent.Parent.Parent.AppendTextBoxLog("Generic Exception ({0}):", ex.GetType());
        Parent.Parent.Parent.AppendTextBoxLog(ex.ToString());
      }
      return true;
    }

    #endregion Packing
  }
}
