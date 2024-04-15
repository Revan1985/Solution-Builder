using Microsoft.Build.Construction;
using Microsoft.Build.Definition;
using Microsoft.Build.Evaluation;

namespace SolutionBuilder.Model
{
    public class CommandCopy : Command
    {
        private static readonly Dictionary<string, Project> ProjectsPath = [];

        public override bool Execute(SolutionFile solution, string basePath, Spectre.Console.Progress? progress = null)
        {

            foreach (string ProjectName in ProjectNames)
            {
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

                string path = Directory.GetParent(project!.ProjectFileLocation.LocationString)?.FullName ?? string.Empty;
                if (string.IsNullOrEmpty(path)) { throw new InvalidOperationException(); }

                foreach (KeyValuePair<string, CommandAction> pair in Actions)
                {
                    CommandAction action = pair.Value;
                    string? file = action.File;
                    if (string.IsNullOrEmpty(file)) { throw new InvalidConfigurationException(); }

                    string? complete = Directory.GetFiles(path, file, SearchOption.AllDirectories).FirstOrDefault();
                    if (string.IsNullOrEmpty(complete))
                    {
                        OnCommandExecution(new($"[maroon]Cannot export {Name}, file not found!...[/]"));
                        //throw new FileNotFoundException($"'{file}' was not found! Path: {path}", file); 
                        continue;
                    }

                    string destination = Path.Combine(basePath, ProjectName);

                    if (progress != null)
                    {
                        progress.Start(ctx =>
                        {
                            if (Directory.Exists(destination)) { Installation.DeleteDirectory(destination, ctx); }
                            Installation.CopyDirectory(Path.GetDirectoryName(complete)!, destination, true, ctx);
                        });
                    }
                    else
                    {
                        if (Directory.Exists(destination)) { Installation.DeleteDirectory(destination, null); }
                        Installation.CopyDirectory(Path.GetDirectoryName(complete)!, destination, true, null);
                    }
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
