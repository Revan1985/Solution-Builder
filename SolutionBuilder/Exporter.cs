using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionBuilder
{
  public class Exporter
  {
    /// <summary>
    /// Export the databases using the provided informations
    /// BascupScript.SimpleExport decide the "difficulty" for export.
    /// Simple (CMD) or Advanced (Code)?
    /// </summary>
    /// <param name="options">Installation</param>
    public void Export(Installation options)
    {
      if (options == null) { throw new ArgumentNullException("options"); }
      options.OnAppendTextBoxLog("Starting BackupScript's");

      if (options.Database == null) { return; }
      if (options.Database.BackupScripts == null || options.Database.BackupScripts.Count == 0) { return; }

      foreach (BackupScriptType bs in options.Database.BackupScripts.Where(b => b.IsChecked))
      {
        try
        {
          SimpleExportScript(bs, options.OnAppendTextBoxLog);
          //switch (bs.SimpleExport)
          //{
          //  case true: SimpleExportScript(bs, options.OnAppendTextBoxLog); break;
          //  case false: AdvancedExportScript(bs, options.OnAppendTextBoxLog); break;
          //}
        }
        catch (Exception e)
        {
          options.OnAppendTextBoxLog("Catched an error for BackupScript {0}", bs.Name);
          options.OnAppendTextBoxLog("ERROR: {0}", e.ToString());
        }
      }

      options.OnAppendTextBoxLog("Completed");
    }

    /// <summary>
    /// Simple export:
    /// Build the query as requested (Replace %%DATE_TIME%% and %%DATE%%)
    /// Execute the Command Prompt
    /// </summary>
    /// <param name="script">The DatabaseScript to Execute</param>
    /// <param name="appendTextBoxLog">Delegate</param>
    private void SimpleExportScript(BackupScriptType script, AppendTextBoxLogDelegate appendTextBoxLog)
    {
      if (script == null) { throw new ArgumentNullException("script"); }
      if (appendTextBoxLog == null) { throw new ArgumentNullException("appendTextBoxLog"); }
      appendTextBoxLog(String.Format("Using Simple Export for the BackupScript '{0}'", script.Name));

      foreach (BackupScriptCommandType command in script.Commands.Where(c => c.IsChecked))
      {

        String query = command.Value.Replace("%%DATE_TIME%%", DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss")).Replace("%%DATE%%", DateTime.Now.ToString("yyyy_MM_dd"));
        String[] parts = query.Split(new String[] { " -" }, StringSplitOptions.RemoveEmptyEntries);
        command.Server = parts.Where(p => p[0].Equals('S') || p.Equals('s')).FirstOrDefault();
        if (!String.IsNullOrEmpty(command.Server))
        {
          command.Server = command.Server.Remove(0, 2);
          //"  BACKUP DATABASE [KerryMESDB] TO  DISK = N'D:\MSSQL\BACKUP\KerryMESDB\%%DATE_TIME%%_KerryMESDB.bak' WITH  COPY_ONLY, NOFORMAT, INIT,  NAME = N'KerryMESDB-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"
          //"  BACKUP DATABASE [KerryMESDB] TO  DISK = N'\\UKDCBTMES06\D$\MSSQL\BACKUP\KerryMESDB\%%DATE_TIME%%_KerryMESDB.bak' WITH  COPY_ONLY, NOFORMAT, INIT,  NAME = N'KerryMESDB-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"
          String q = parts.Where(p => p[0].Equals('q') || p[0].Equals('Q')).FirstOrDefault();
          if (!String.IsNullOrEmpty(q))
          {
            Int32 index = q.IndexOf('N');
            String disk = q.Substring(index + 2, 3); /* D:\ */
            String copyQuery = query.Replace(disk, String.Format(@"\\{0}\{1}", command.Server, disk.Replace(':', '$')));
            index = copyQuery.IndexOf('N');
            command.CompleteRemotePath = copyQuery.Substring(index + 1, copyQuery.IndexOf("'", index + 2) - (index + 1));
          }

          appendTextBoxLog("Starting execution");

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

          StringBuilder sb = new StringBuilder();
          while (!process.StandardOutput.EndOfStream)
          {
            sb.AppendLine(process.StandardOutput.ReadLine());
          }

          appendTextBoxLog(String.Format("Output: {0}", sb.ToString()));
          process.WaitForExit();
          appendTextBoxLog(String.Format("ExitCode: {0}", process.ExitCode));
        }
      }
    }

    /// <summary>
    /// Advanced Export:
    /// Extract setting from the script (ServerName, Database, Username, Password, Query)
    /// and execute it with c# code
    /// </summary>
    /// <param name="script">BackupScript to execute</param>
    /// <param name="appendTextBoxLog">Delegate</param>
    private void AdvancedExportScript(BackupScriptType script, AppendTextBoxLogDelegate appendTextBoxLog)
    {
      if (script == null) { throw new ArgumentNullException("script"); }
      if (appendTextBoxLog == null) { throw new ArgumentNullException("appendTextBoxLog"); }
      throw new NotImplementedException();

#if false
      String query = script.Value;
      if (String.IsNullOrEmpty(query)) { return; }
      appendTextBoxLog(String.Format("Using advanced export for the BackupScript '{0}'", script.Name));

      // sqlcmd -U sa -P W0nderware -S UKDCBTMES06 -d KerryMESDB -Q "  BACKUP DATABASE [KerryMESDB] TO  DISK = N'D:\MSSQL\BACKUP\KerryMESDB\%%DATE_TIME%%_KerryMESDB.bak' WITH  COPY_ONLY, NOFORMAT, INIT,  NAME = N'KerryMESDB-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"

      String[] parts = query.Split(new String[] { " -" }, StringSplitOptions.RemoveEmptyEntries);
      foreach (String part in parts)
      {
        switch (part[0])
        {
          case 'u':
          case 'U': script.Username = part.Remove(0, 1); break;
          case 'p':
          case 'P': script.Password = part.Remove(0, 1); break;
          case 's':
          case 'S': script.ServerName = part.Remove(0, 1); break;
          case 'd':
          case 'D': script.Database = part.Remove(0, 1); break;
          case 'q':
          case 'Q':
            {
              script.Query = part.Remove(0, 1);
            }
            break;
        }
      }
#endif
    }
  }
}
