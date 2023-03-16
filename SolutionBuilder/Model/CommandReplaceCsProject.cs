using Microsoft.Build.Construction;
using Microsoft.Build.Definition;
using Microsoft.Build.Evaluation;

namespace SolutionBuilder.Model
{
    public class CommandReplaceCsProject : Command
    {
        static readonly Dictionary<string, Project> ProjectsPath = new();
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
                        ProjectOptions options = new();
                        options.LoadSettings = ProjectLoadSettings.IgnoreMissingImports;

                        project = Project.FromFile(projectInSolution.AbsolutePath, options);
                        ProjectsPath.Add(projectInSolution.ProjectName, project);
                    }
                }

                if (project == null)
                {
                    OnCommandExecution(new("[maroon]Cannot find required project...[/]"));
                    return false;
                }

                foreach (KeyValuePair<string, CommandAction> pair in Actions.OrderBy(c => c.Value.Order))
                {
                    CommandAction action = pair.Value;

                    int order = action.Order;
                    string value = action.Value;
                    string source = action.Source;

                    ProjectProperty? property = project!.GetProperty(source);
                    if (property != null)
                    {
                        OnCommandExecution(new($"\t[silver]Replacing property \"{property.Name}\" ({property.UnevaluatedValue}) with {value}[/]"));
                        project!.SetProperty(property.Name, value);
                    }
                    else
                    {
                        project.SetProperty(source, value);

                        OnCommandExecution(new($"\t[aqua]Added property {source}![/]"));
                    }
                }

                project!.Save();

                if (ProjectCollection.GlobalProjectCollection.LoadedProjects.Contains(project))
                {
                    ProjectCollection.GlobalProjectCollection.UnloadProject(project);
                }
            }
            return true;
        }
    }
}
