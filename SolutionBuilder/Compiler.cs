using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Build.BuildEngine;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Text.RegularExpressions;
using Onion.SolutionParser.Parser.Model;
using Onion.SolutionParser.Parser;


namespace SolutionBuilder
{
  /// <summary>
  /// Code Compiler (C#, VB, C++/Cli)
  /// <remarks>This has been tested only with c#, but should work with the others too...</remarks>
  /// </summary>
  public class Compiler
  {
    /// <summary>
    /// Assembly Info Pattern (Regular Expression)
    /// </summary>
    public const String AssemblyInfoPattern = "\\[assembly[:]\\s*Assembly(?<File>\\w*)Version\\(\"(?<Major>\\d+)\\.(?<Minor>\\d+)\\.(?<Build>\\d+)\\.(?<Revision>\\d+)\"\\)\\]";

    /// <summary>
    /// Compile the solution (async)
    /// </summary>
    /// <param name="options">Installation</param>
    /// <returns>Task</returns>
    async public System.Threading.Tasks.Task CompileAsync(Installation options)
    {
      options.OnAppendTextBoxLog("Compiling (Async)");

      if (options == null) { throw new ArgumentNullException("options"); }
      if (String.IsNullOrEmpty(options.CompleteOutputPath)) { throw new ArgumentNullException("DefaultPath / Module.Path"); }

      foreach (ModuleType module in options.Modules.Where(m => m.IsChecked)) { await CompileAsync(module); }
      foreach (ModuleType control in options.Controls.Where(c => c.IsChecked)) { await CompileAsync(control); }

      options.OnAppendTextBoxLog("Compile (Async) Complete");
    }
    /// <summary>
    /// Find the Assembly Information for provided Installation (async)
    /// </summary>
    /// <param name="options">Installation</param>
    /// <returns>Task</returns>
    async public static System.Threading.Tasks.Task FindAssemblyInfoAsync(Installation options)
    {
      if (options == null) { throw new ArgumentNullException("options"); }
      foreach (ModuleType module in options.Modules) { await SearchAssemblyInfoAsync(module); }
      foreach (ModuleType control in options.Controls) { await SearchAssemblyInfoAsync(control); }
    }

    /// <summary>
    /// Compile the specific Module solution (async)
    /// <remarks>This call the Compile Method with await Task.Run(()=>{ ... });</remarks>
    /// </summary>
    /// <typeparam name="T">Module (or derived)</typeparam>
    /// <param name="value">The <paramref name="T"/> to compile</param>
    /// <param name="options">Installation</param>
    /// <returns>Task</returns>
    async private System.Threading.Tasks.Task CompileAsync(ModuleType value)
    {
      if (value == null) { throw new ArgumentNullException("value"); }
      await System.Threading.Tasks.Task.Run(() => { Compile(value); });
    }
    /// <summary>
    /// Search for the Assembly Info Async
    /// <remarks>This call the SearchAssemblyInfo Method with await Task.Run(()=>{ ... });</remarks>
    /// </summary>
    /// <typeparam name="T">Module (or derived)</typeparam>
    /// <param name="value">The <paramref name="T"/> to compile</param>
    /// <param name="options">Installation</param>
    /// <returns>Task</returns>
    async private static System.Threading.Tasks.Task SearchAssemblyInfoAsync(ModuleType value)
    {
      if (value == null) { throw new ArgumentNullException("value"); }
      await System.Threading.Tasks.Task.Run(() => { SearchAssemblyInfo(value); });
    }
#if false
    /// <summary>
    /// Increment the Assembly Info Build by 1
    /// <remarks>This call the IncrementAssemblyInfo Method with await Task.Run(()=>{ ... });</remarks>
    /// </summary>
    /// <typeparam name="T">Module (or derived)</typeparam>
    /// <param name="value">The <paramref name="T"/> to compile</param>
    /// <param name="options">Installation</param>
    /// <returns>Task</returns>
    async private static System.Threading.Tasks.Task IncrementAssemblyInfoAsync(ModuleType value)
    {
      if (value == null) { throw new ArgumentNullException("value"); }
      await System.Threading.Tasks.Task.Run(() => { IncrementAssemblyInfo(value); });
    }
#endif
    /// <summary>
    /// Update the AssemblyInfo with the Information in the <para>T</para> value (Async)
    /// </summary>
    /// <typeparam name="T">Module</typeparam>
    /// <param name="value">T</param>
    /// <param name="options">Installation</param>
    /// <returns>Task</returns>
    async public static System.Threading.Tasks.Task UpdateAssembyInfoAsync(ModuleType value)
    {
      if (value == null) { throw new ArgumentNullException("value"); }
      await System.Threading.Tasks.Task.Run(() => { IncrementAssemblyInfo(value); });
    }

    /// <summary>
    /// Build the code
    /// </summary>
    /// <param name="options">Installation</param>
    public Boolean Compile(Installation options)
    {
      options.OnAppendTextBoxLog("Compiling");

      if (options == null) { throw new ArgumentNullException("options"); }
      if (String.IsNullOrEmpty(options.CompleteOutputPath)) { throw new ArgumentNullException("DefaultPath / Module.Path"); }

      foreach (ModuleType module in options.Modules.Where(m => m.IsChecked)) { if (!Compile(module) && options.StopCompilationOnError) { return false; } }
      foreach (ModuleType control in options.Controls.Where(c => c.IsChecked)) { if (!Compile(control) && options.StopCompilationOnError) { return false; } }

      options.OnAppendTextBoxLog("Completed");
      return true;
    }
    /// <summary>
    /// Find the Assembly Information for provided Installation
    /// </summary>
    /// <param name="options">Installation</param>
    public static Boolean FindAssemblyInfo(Installation options)
    {
      if (options == null) { throw new ArgumentNullException("options"); }
      if (String.IsNullOrEmpty(options.CompleteOutputPath)) { throw new ArgumentNullException("DefaultPath / Module.Path"); }

      foreach (ModuleType module in options.Modules) { SearchAssemblyInfo(module); }
      foreach (ModuleType control in options.Controls) { SearchAssemblyInfo(control); }
      return true;
    }
    /// <summary>
    /// Compile the CSProj
    /// </summary>
    /// <param name="value">The ModuleType to compile</param>
    /// <param name="options">Installation</param>
    private Boolean Compile(ModuleType value)
    {
      if (value == null) { throw new ArgumentNullException("value"); }
      Installation options = ((IModulesType)value.Parent).Parent;
      if (options == null) { return false; }

      FileInfo fi = new FileInfo(System.IO.Path.Combine(options.Parent.Source, options.Source, value.Path));
      if (!fi.Exists)
      {
        options.OnAppendTextBoxLog("Skipping compiling solution for '{0}'", fi.FullName);
        options.OnAppendTextBoxLog("No file found (directory or deleted file?)");
        return false;
      }

      options.OnAppendTextBoxLog("Starting compiling solution for {0}", value.Name);
      options.OnAppendTextBoxLog("Using parameters: {0}", value.Configuration);

      if (options.IncrementBuild)
      {
        IncrementAssemblyInfo(value);
      }

      if (value.VSSolution == null)
      {
        value.VSSolution = Onion.SolutionParser.Parser.SolutionParser.Parse(System.IO.Path.Combine(
          options.Parent.Source,
          options.Source,
          value.Path));
      }

      foreach (Project project in value.Projects.Where(p => p.IsChecked))
      {
        var proj = value.FindProject(project.Value);
        if (proj != null)
        {
          project.Status.Status = EBuildModuleStatus.Building;
          String ProjectName = Path.Combine(fi.DirectoryName, proj.Path);

          FileLogger fileLogger = new FileLogger();
          fileLogger.Parameters = String.Format("LogFile={0}", System.IO.Path.Combine(options.CompleteOutputPath, String.Format("{0}.{1}.log", value, project.Value)));

          ProjectCollection projectCollection = new ProjectCollection();
          BuildParameters buildParameters = new BuildParameters(projectCollection);
          buildParameters.Loggers = new List<ILogger> { fileLogger };
          IDictionary<String, String> globalProperties = new Dictionary<String, String>();
          globalProperties.Add("SolutionDir", String.Format("{0}{1}", fi.DirectoryName, fi.DirectoryName.EndsWith("\\") ? String.Empty : "\\"));
          globalProperties.Add("Configuration", value.Configuration);
          globalProperties.Add("Platform", value.Platform.ToString());

          String[] targetsToBuild = { "Rebuild" };
          BuildRequestData buildRequestData = new BuildRequestData(ProjectName, globalProperties, null, targetsToBuild, null, BuildRequestDataFlags.None);
          BuildResult buildResult = BuildManager.DefaultBuildManager.Build(buildParameters, buildRequestData);
          projectCollection.UnregisterAllLoggers();

          //options.OnAppendTextBoxLog("\t- Project {0}: {1}", project.Value, buildResult.OverallResult);

          foreach (String key in buildResult.ResultsByTarget.Keys.Where(k => targetsToBuild.Contains(k, EqualityComparer<String>.Default)))
          {
            TargetResult target = buildResult.ResultsByTarget[key];
            options.OnAppendTextBoxLog("\t- Project {0}\t[{1}] Compilation: {2}", project.Value, key, target.ResultCode);

            if (target.Exception != null)
            {
              options.OnAppendTextBoxLog("\t- Errors: {0}", target.Exception.ToString());
              project.Status.Status = EBuildModuleStatus.Failure;
              if (options.StopCompilationOnError) { return false; }
            }
          }
        }
        project.Status.Status = EBuildModuleStatus.Success;
      }

      return true;
    }

    /// <summary>
    /// Search for the Assembly Info
    /// </summary>
    /// <param name="value">The ModuleType to compile</param>
    /// <param name="options">Installation</param>
    private static Boolean SearchAssemblyInfo(ModuleType value)
    {
      if (value == null) { throw new ArgumentNullException("value"); }
      Installation options = ((IModulesType)value.Parent).Parent;
      if (options == null) { return false; }

      // Search for an assembly info.
      FileInfo fi = new FileInfo(Path.Combine(options.Parent.Source, options.Source, value.Path));
      if (!fi.Exists)
      {
        options.OnAppendTextBoxLog("Searching in a dll path? skipping.");
        return false;
      }

      String FullPath = fi.DirectoryName;
      value.Initialize();

      if (value.VSSolution == null)
      {
        value.VSSolution = Onion.SolutionParser.Parser.SolutionParser.Parse(System.IO.Path.Combine(
          options.Parent.Source,
          options.Source,
          value.Path));
      }

      foreach (Project project in value.Projects)
      {
        var proj = value.FindProject(project.Value);
        if (proj != null)
        {
          FileInfo AssemblyInfo = new FileInfo(Path.Combine(Path.GetDirectoryName(Path.Combine(FullPath, proj.Path)), "Properties", "AssemblyInfo.cs"));

          if (!AssemblyInfo.Exists)
          {
            options.OnAppendTextBoxLog("No valid CS AssemblyInfo found.");
            return false;
          }

          String content = File.ReadAllText(AssemblyInfo.FullName);
          Regex regex = new Regex(AssemblyInfoPattern, RegexOptions.Compiled | RegexOptions.Multiline);

          MatchCollection matches = regex.Matches(content);
          if (matches != null && matches.Count > 0)
          {
            project.Major = Int16.Parse(matches[0].Groups["Major"].Value);
            project.Minor = Int16.Parse(matches[0].Groups["Minor"].Value);
            project.Build = Int16.Parse(matches[0].Groups["Build"].Value);
            project.Revision = Int16.Parse(matches[0].Groups["Revision"].Value);
          }
        }
      }
      return true;
    }
    /// <summary>
    /// Increment the Assembly Info Build by 1
    /// </summary>
    /// <typeparam name="T">Module (or derived)</typeparam>
    /// <param name="value">The <paramref name="T"/> to compile</param>
    /// <param name="options">Installation</param>
    private static void IncrementAssemblyInfo(ModuleType value)
    {
      if (value == null) { throw new ArgumentNullException("value"); }
      Installation options = ((IModulesType)value.Parent).Parent;
      if (options == null) { return; }

      FileInfo fi = new FileInfo(Path.Combine(options.Parent.Source, options.Source, value.Path));
      if (!fi.Exists)
      {
        /* Maybe it's not a valid file, but only a dll path... (check later) */
        options.OnAppendTextBoxLog("Searching in a dll path? skipping.");
        return;
      }

      if (value.VSSolution == null)
      {
        value.VSSolution = Onion.SolutionParser.Parser.SolutionParser.Parse(System.IO.Path.Combine(
          options.Parent.Source,
          options.Source,
          value.Path));
      }

      String FullPath = fi.DirectoryName;
      foreach (Project project in value.Projects.Where(p => p.IsChecked))
      {
        var proj = value.FindProject(project.Value);
        FileInfo AssemblyInfo = new FileInfo(Path.Combine(Path.GetDirectoryName(Path.Combine(FullPath, proj.Path)), "Properties", "AssemblyInfo.cs"));

        if (!AssemblyInfo.Exists)
        {
          // If not found, there is something wrong
          options.OnAppendTextBoxLog("No valid CS AssemblyInfo found.");
          return;
        }

        String content = File.ReadAllText(AssemblyInfo.FullName);
        Regex regex = new Regex(AssemblyInfoPattern, RegexOptions.Compiled | RegexOptions.Multiline);
        
        // MPP20150223 - auto increase version increasing only relaease.
        content = Regex.Replace(
          content, AssemblyInfoPattern, match =>
            String.Format("[assembly: Assembly{0}Version(\"{1}.{2}.{3}.{4}\")]",
            match.Groups["File"].Value,
            match.Groups["Major"].Value,
            match.Groups["Minor"].Value,
            match.Groups["Build"].Value,
            (Int32.Parse(match.Groups["Revision"].Value) +1).ToString()
            )
        );
        File.WriteAllText(AssemblyInfo.FullName, content);
        options.OnAppendTextBoxLog("AssemblyInfo.Build Updated");
      }
    }

#if false
    /// <summary>
    /// Update the AssemblyInfo with the Information in the <para>T</para> value
    /// </summary>
    /// <typeparam name="T">Module</typeparam>
    /// <param name="value">T</param>
    /// <param name="options">Installation</param>
    private static void UpdateAssembyInfo(ModuleType value)
    {
      if (value == null) { throw new ArgumentNullException("value"); }
      Installation options =
   value.Type == EModuleType.Control ? ((ControlsType)value.Parent).Parent :
   value.Type == EModuleType.Module ? ((ModulesType)value.Parent).Parent : null;
      if (options == null) { return; }

      options.OnAppendTextBoxLog("Incrementing Assembly Version for {0}", value.Name);

      FileInfo fi = new FileInfo(Path.Combine(options.Source, value.Path));
      if (!fi.Exists)
      {
        /* Maybe it's not a valid file, but only a dll path... (check later) */
        options.OnAppendTextBoxLog("Searching in a dll path? skipping.");
        return;
      }

      String FullPath = fi.DirectoryName;
      foreach (Project project in value.Projects.Where(p => p.IsChecked))
      {
        var proj = value.FindProject(project.Value);
        FileInfo AssemblyInfo = new FileInfo(Path.Combine(Path.GetDirectoryName(Path.Combine(FullPath, proj.Path)), "Properties", "AssemblyInfo.cs"));
        if (!AssemblyInfo.Exists) { AssemblyInfo = new FileInfo(Path.Combine(FullPath, value.Name, project.Value, "Properties", "AssemblyInfo.cs")); }
        if (!AssemblyInfo.Exists)
        {
          // If not found, there is something wrong
          options.OnAppendTextBoxLog("No valid AssemblyInfo found.");
          return;
        }

        String content = File.ReadAllText(AssemblyInfo.FullName);
        Regex regex = new Regex(AssemblyInfoPattern, RegexOptions.Compiled | RegexOptions.Multiline);

        content = Regex.Replace(
          content, AssemblyInfoPattern, match =>
            String.Format("[assembly: Assembly{0}Version(\"{1}.{2}.{3}.{4}\")]",
            match.Groups["File"].Value,
            project.Major,
            project.Minor,
            project.Build,
            project.Revision));
        File.WriteAllText(AssemblyInfo.FullName, content);

        options.OnAppendTextBoxLog("AssemblyInfo.Build Updated");
      }
    }
#endif
    public static void FindAssemblyInfo(ModuleType value)
    {
      SearchAssemblyInfo(value);
    }
  }
}