using Buildalyzer;
using Buildalyzer.Environment;
using Microsoft.Build.Construction;
using Spectre.Console;

namespace SolutionBuilder.Model
{
    public class CommandBuild : Command
    {
        public Dictionary<string, string> Options { get; set; } = [];
       
        public override bool Execute(SolutionFile solution, string _, Progress? progress = null)
        {
            foreach (string ProjectName in ProjectNames)
            {
                OnCommandExecution(new($"Executing [green]{Name}[/] {Type} \"{ProjectName}\""));
                ProjectInSolution? projectInSolution = solution.ProjectsInOrder.FirstOrDefault(p => p.ProjectName == ProjectName);
                if (projectInSolution == null) { return false; }

                StringWriter writer = new();

                AnalyzerManagerOptions options = new()
                {
                    LogWriter = writer
                };

                AnalyzerManager manager = new(SolutionFile, options);
                
                manager.SetEnvironmentVariable("MS_BUILD_PATH", @"C:\Program Files\Microsoft Visual Studio\2022\Professional\Msbuild\Current\Bin\MSBuild.exe");
                manager.SetGlobalProperty("VisualStudioVersion", "15.0");

                IProjectAnalyzer analyzer = manager.GetProject(projectInSolution.AbsolutePath);
                //analyzer.SetGlobalProperty("Configuration", "");
                //analyzer.SetGlobalProperty("", "");
                EnvironmentOptions environmentOptions = new()
                {
                    DesignTime = false,
                    Preference = EnvironmentPreference.Framework,
                    Restore = true,
                };

                BuildEnvironment buildEnvironment = analyzer.EnvironmentFactory.GetBuildEnvironment("v472", environmentOptions);
                IAnalyzerResults results = analyzer.Build(buildEnvironment);

                OnCommandExecution(new("\t\tBuild log:"));
                OnCommandExecution(new($"\t\t{Markup.Escape($"{writer}")}"));


                using StreamWriter fileWriter = new(Path.Combine(Path.GetTempPath(), $"{ProjectName}.log"));
                fileWriter.Write($"{writer}");

                foreach (IAnalyzerResult? result in results)
                {
                    if (result == null) { continue; }
                    string statusColor = result.Succeeded ? "green" : "red";
                    string targetFramework = result.TargetFramework;
                    OnCommandExecution(new($"\t[{statusColor}]Build {result.ProjectFilePath} with {targetFramework} {(result.Succeeded ? "Succedded" : "Failed")}[/]"));
                }
            }
            return true;
        }
    }
}
