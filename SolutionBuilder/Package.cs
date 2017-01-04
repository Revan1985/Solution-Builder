using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionBuilder
{
  /// <summary>
  /// Class used to create package from Installation infos...
  /// </summary>
  public class Package
  {
    /// <summary>
    /// Create the Package
    /// </summary>
    /// <param name="options"></param>
    public void Create(Installation options)
    {
      if (options == null) { throw new ArgumentNullException("options"); }
      foreach (ModuleType module in options.Modules.Where(m => m.IsChecked)) { Create(module); }
      foreach (ModuleType control in options.Controls.Where(c => c.IsChecked)) { Create(control); }
      if (options.Database != null)
      {
        if (options.Database.BackupScripts != null && options.Database.BackupScripts.Count > 0)
        {
          foreach (BackupScriptType script in options.Database.BackupScripts.Where(b => b.IsChecked))
          {
            Create(script, options);
          }
        }
      }

      if (options.ArchivePackage)
      {
        Archive(options);
      }
    }

    /// <summary>
    /// Create a package foreach Module
    /// </summary>
    /// <typeparam name="T">Module</typeparam>
    /// <param name="value">T</param>
    /// <param name="options">Installation</param>
    private void Create(ModuleType value)
    {
      if (value == null) { throw new ArgumentNullException("value"); }

      Installation options = ((IModulesType)value.Parent).Parent;
      if (options == null) { return; }

      String Directory = Path.Combine(options.Parent.Source, options.Source, value.Path);
      FileInfo fi = new FileInfo(Directory);

      if (fi.Exists)
      {
        foreach (Project project in value.Projects.Where(p => p.IsChecked))
        {
          var proj = value.FindProject(project.Value);
          if (proj != null)
          {
            /* Build the Platform/Configuration from the CSProj root directory */
            String Configuration = value.Configuration;
            String Platform = value.Platform == EPlatformType.AnyCPU ? String.Empty : value.Platform.ToString();

            String SourcePath = Path.Combine(Path.GetDirectoryName(Path.Combine(fi.DirectoryName, proj.Path)), "bin", Platform, Configuration);

            if (!System.IO.Directory.Exists(SourcePath))
            {
              options.OnAppendTextBoxLog("Bin folder not found. Skipping module {0}", value.Name);
              return;
            }

            //String Destination = Path.Combine(options.OutputPath, value.Name, proj.Name);
            //String Destination = Path.Combine(options.OutputPath, value.Name, value.Projects.Count > 1 ? project.Value : String.Empty);
            String Destination = Path.Combine(options.CompleteOutputPath,
             ((IModulesType)value.Parent).Output,
              value.GetType().Name.Substring(0, 6),
              value.Name,
              value.Projects.Count > 1 ? project.Value : String.Empty);
            try
            {
              Copy(SourcePath, Destination, options);
            }
            catch (Exception ex) { options.OnAppendTextBoxLog("Error during copy: {0}", ex.ToString()); }
          }
        }
      }
      else
      {
        /* value.Projects are only Directory, seen that root is either a directory, and not a solutions file */
        foreach (Project project in value.Projects.Where(p => p.IsChecked))
        {
          //String Destination = Path.Combine(options.OutputPath, value.Name, value.Projects.Count > 1 ? project.Value : String.Empty);
          String Destination = Path.Combine(options.CompleteOutputPath, value.GetType().Name, value.Projects.Count > 1 ? project.Value : String.Empty);
          try
          {
            Copy(Path.Combine(Directory, project.Value), Destination, options);
          }
          catch (Exception ex) { options.OnAppendTextBoxLog("Error during copy: {0}", ex.ToString()); }
        }
      }
    }

    /// <summary>
    /// This is used to copy the generated script (if exists), in the options.OutputPath (every file should have an unique name)
    /// </summary>
    /// <param name="script">The BackupScript</param>
    /// <param name="options">Installation</param>
    private void Create(BackupScriptType script, Installation options)
    {
      if (script == null) { throw new ArgumentNullException("script"); }
      if (options == null) { throw new ArgumentNullException("options"); }
      try
      {
        foreach (BackupScriptCommandType command in script.Commands.Where(c => c.IsChecked))
        {
          if (Directory.Exists(command.CompleteRemotePath))
          {
            options.OnAppendTextBoxLog("Exporting BackScript {0}", script.Name);
            File.Copy(command.CompleteRemotePath,
              Path.Combine(options.CompleteOutputPath, Path.GetFileName(command.CompleteRemotePath)));
          }
        }
      }
      catch (Exception e)
      {
        options.OnAppendTextBoxLog("Catched exception from Copying BackupScript from remote location.");
        options.OnAppendTextBoxLog("ERROR: {0}", e.ToString());
      }
    }
    /// <summary>
    /// Generate the Archive for the package.
    /// <remarks>The compression level is set in the Installation xml node</remarks>
    /// </summary>
    /// <param name="options">Installation</param>
    private void Archive(Installation options)
    {
      if (options == null) { throw new ArgumentNullException("options"); }
      options.OnAppendTextBoxLog("Generating Package Archive");
      String ZipPackage = String.Format("{0}.zip", options.CompleteOutputPath);
      if (!Zip.CreateFileFromFolder(options.CompleteOutputPath, ZipPackage, false, options.Compression))
      {
        options.OnAppendTextBoxLog("Archive generation failed");
        return;
      }
      options.OnAppendTextBoxLog("Generation completed.");
    }

    /// <summary>
    /// Copy the files in the source directory in the provided options.OutputPath
    /// </summary>
    /// <param name="source">Source directory</param>
    /// <param name="destination">Where the files must be copied</param>
    /// <param name="options">Installation</param>
    private void Copy(String source, String destination, Installation options)
    {
      if (String.IsNullOrEmpty(source)) { throw new ArgumentNullException("source"); }
      if (String.IsNullOrEmpty(destination)) { throw new ArgumentNullException("destination"); }
      if (options == null) { throw new ArgumentNullException("options"); }

      if (!Directory.Exists(source)) { throw new DirectoryNotFoundException(String.Format("Cannot find the required path '{0}'", source)); }
      if (!Directory.Exists(destination)) { Directory.CreateDirectory(destination); }


      String[] files = Directory.GetFiles(source, "*.*", SearchOption.TopDirectoryOnly);
      foreach (String file in files)
      {
        Int32 Count = (from ex in options.Exclusions where file.ToLower().Contains(ex.ToLower()) select ex).Count();
        if (Count == 0)
        {
          options.OnAppendTextBoxLog("Copying: {0}", file);
          File.Copy(file, file.Replace(source, destination), true);
          options.OnAppendTextBoxLog("Copied: {0}", file);
        }
      }
    }
  }
}
