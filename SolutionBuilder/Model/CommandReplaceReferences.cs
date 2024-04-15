﻿using Microsoft.Build.Construction;
using Microsoft.Build.Definition;
using Microsoft.Build.Evaluation;

namespace SolutionBuilder.Model
{
    public class CommandReplaceReferences : Command
    {
        static readonly Dictionary<string, Project> ProjectsPath = [];

        public override bool Execute(SolutionFile solution, string basePath, Spectre.Console.Progress? progress = null)
        {
            //Console.WriteLine("Premi un tasto per continuare:");
            //var x = Console.ReadKey();
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

                if (Actions == null || Actions.Count == 0)
                {
                    OnCommandExecution(new("[maroon]No replace action defined...[/]"));
                    return false;
                }
                // ProjectReference for new vs template...
                IEnumerable<ProjectItem>? references = project.Items.Where(i => i.ItemType == "ProjectReference");
                // PackageReference for new vs template and Reference for old ones...
                IEnumerable<ProjectItem>? items = project.Items.Where(i => i.ItemType == "PackageReference" || i.ItemType == "Reference");



                foreach (KeyValuePair<string, CommandAction> pair in Actions.OrderBy(p => p.Value.Order))
                {
                    CommandAction action = pair.Value;
                    string source = action.Source;
                    string value = action.Value;
                    string? file = action.File;

                    if (string.IsNullOrEmpty(file)) { throw new InvalidConfigurationException(); }

                    ProjectItem? item = items.FirstOrDefault(i => i.UnevaluatedInclude == source || i.UnevaluatedInclude.StartsWith($"{source},"));
                    if (item != null)
                    {
                        // found as package, remove this and replace with new value
                        project.RemoveItem(item);

                        string? path = Path.GetDirectoryName(Path.Combine(basePath, value));
                        string? complete = Directory.GetFiles(path!, file!, SearchOption.AllDirectories).FirstOrDefault();

                        if (string.IsNullOrEmpty(complete))
                        {
                            project.AddItem(item.ItemType == "PackageReference" ? "ProjectReference" : item.ItemType, Path.Combine(basePath, value));
                        }
                        else
                        {
                            project.AddItem("Reference", Path.GetFileNameWithoutExtension(complete),
                            [
                                new("Include", Path.GetFileName(complete)),
                                new("HintPath",complete)
                            ]);
                        }
                    }
                    else if (references.Any(i => i.UnevaluatedInclude.Contains(source)))
                    {
                        // should only work with my test development (integro use local projects instead of packages)
                        item = references.FirstOrDefault(i => i.UnevaluatedInclude.Contains(source));
                        if (item != null)
                        {
                            string? path = Path.GetDirectoryName(Path.Combine(basePath, value));
                            string? complete = Directory.GetFiles(path!, file!, SearchOption.AllDirectories).FirstOrDefault();

                            if (string.IsNullOrEmpty(complete))
                            {
                                project.AddItem(item.ItemType == "PackageReference" ? "ProjectReference" : item.ItemType, Path.Combine(basePath, value));
                            }
                            else
                            {
                                project.AddItem("Reference", Path.GetFileNameWithoutExtension(complete),
                                [
                                    new("Include", Path.GetFileName(complete)),
                                    new("HintPath",complete)
                                ]);
                            }
                        }
                    }
                    // skip other cases, not needed
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
