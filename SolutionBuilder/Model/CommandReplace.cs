using Microsoft.Build.Construction;
using Microsoft.Build.Definition;
using Microsoft.Build.Evaluation;

namespace SolutionBuilder.Model
{
    public class CommandReplace : Command
    {
        static readonly Dictionary<string, Project> ProjectsPath = [];

        public override bool Execute(SolutionFile solution, string _, Spectre.Console.Progress? progress = null)
        {
            foreach (string ProjectName in ProjectNames)
            {
                OnCommandExecution(new($"Executing [green]{Name}[/] {Type} \"{ProjectName}\""));

                Project? project = null;
                ProjectInSolution? projectInSolution = solution.ProjectsInOrder.FirstOrDefault(p => p.ProjectName == ProjectName);
                if (projectInSolution != null)
                {
                    if (!ProjectsPath.TryGetValue(projectInSolution.ProjectName, out project))
                    {
                        ProjectOptions options = new()
                        {
                            LoadSettings = ProjectLoadSettings.IgnoreMissingImports
                        };
                        project = Project.FromFile(projectInSolution.AbsolutePath, options);
                        ProjectsPath.Add(projectInSolution.ProjectName, project);
                    }
                }

                if (project == null)
                {
                    OnCommandExecution(new("[maroon]Cannot find required project...[/]"));
                    return false;
                }

                string? directoryPath = Directory.GetParent(project!.ProjectFileLocation.LocationString)?.FullName ?? string.Empty;
                OnCommandExecution(new($"\t[silver]Searching in path '{directoryPath}'[/]"));
                DirectoryInfo directoryInfo = new(directoryPath);

                foreach (KeyValuePair<string, CommandAction> pair in Actions.OrderBy(a => a.Value.Order))
                {
                    CommandAction action = pair.Value;
                    OnCommandExecution(new($"\t[silver]Replacing {action.Source} for {action.File ?? DefaultFilePattern} search pattern![/]\t"));
                    FileInfo[] files = directoryInfo.GetFiles(action.File ?? DefaultFilePattern, SearchOption.AllDirectories);
                    foreach (FileInfo file in files)
                    {
                        if(file.Extension == ".png" || file.Extension == ".ico")
                        {
                            continue;
                        }
                        string content;
                        using (StreamReader reader = file.OpenText())
                        {
                            content = reader.ReadToEnd();
                        }
                        content = content.Replace(action.Source, action.Value);

                        using StreamWriter writer = file.CreateText();
                        writer.Write(content);
                    }
                    OnCommandExecution(new($"[green]Done![/]"));
                }

                if (ProjectCollection.GlobalProjectCollection.LoadedProjects.Contains(project))
                {
                    ProjectCollection.GlobalProjectCollection.UnloadProject(project);
                }
            }
            return true;
        }
    }
}
