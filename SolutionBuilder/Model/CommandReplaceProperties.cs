using Microsoft.Build.Construction;
using Microsoft.Build.Definition;
using Microsoft.Build.Evaluation;
using System.Text.RegularExpressions;

namespace SolutionBuilder.Model
{
    public class CommandReplaceProperties : Command
    {
        static readonly string AssemblyFormatReader = "\\[assembly[:]\\s*{0}\\(\"(?<ReplaceValue>.*)\"\\)\\]";
        static readonly string AssemblyFormatWriter = "[assembly: {0}(\"{1}\")]";

        static readonly Dictionary<string, Project> ProjectsPath = [];
        public override bool Execute(SolutionFile solution, string _, Spectre.Console.Progress? progress = null)
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

                string? directoryPath = Directory.GetParent(project!.ProjectFileLocation.LocationString)?.FullName ?? string.Empty;
                string assemblyPath = Path.Combine(directoryPath, "Properties", "AssemblyInfo.cs");
                bool assemblyExists = File.Exists(assemblyPath);
                string assemblyValue = assemblyExists ? File.ReadAllText(assemblyPath) : string.Empty;
                OnCommandExecution(new($"[silver]Replacing \"{(assemblyExists ? assemblyPath : "csproj")}\" properties[/]\t"));

                foreach (KeyValuePair<string, CommandAction> pair in Actions.OrderBy(c => c.Value.Order))
                {
                    CommandAction action = pair.Value;

                    int order = action.Order;
                    string value = action.Value;
                    string source = action.Source;
                    string sourceFallback = action.SourceFallback;

                    if (assemblyExists)
                    {
                        assemblyValue = ReplaceAssemblyInfoFile(assemblyValue, source, value);
                    }

                    ReplaceAssemblyProject(project, source, sourceFallback, value);
                }

                // this is done by default.
                // all builded project must support at least .netstandard2.0 (.net v4.6 minimum)
                project.SetProperty("GenerateResourceUsePreserializedResources", "true");
                if (!project.Items.Any(p => p.UnevaluatedInclude == "System.Resources.Extensions"))
                {
                    string itemType = project.Xml.ToolsVersion == "15.0" ? "Reference" : "PackageReference";

                    // add package reference if not exists
                    project.AddItem(itemType, "System.Resources.Extensions", new KeyValuePair<string, string>[]
                    {
                        new("Version", "6.0.0")
                    });
                }


                if (assemblyExists) { File.WriteAllText(assemblyPath, assemblyValue); }
                else { project.Save(); }


                if (ProjectCollection.GlobalProjectCollection.LoadedProjects.Contains(project))
                {
                    ProjectCollection.GlobalProjectCollection.UnloadProject(project);
                }
                OnCommandExecution(new($"[green]Done![/]"));
            }
            return true;
        }

        private string ReplaceAssemblyInfoFile(string assemblyValue, string source, string value)
        {
            OnCommandExecution(new($"\t[silver]Replacing property \"{source}\" with {value}[/]"));
            string content = Regex.Replace(assemblyValue, string.Format(AssemblyFormatReader, source), match => string.Format(AssemblyFormatWriter, source, value));
            return content;
        }
        private void ReplaceAssemblyProject(Project project, string source, string sourceFallback, string value)
        {
            ProjectProperty? property = project.GetProperty(source);
            if (property != null && !property.IsImported)
            {
                OnCommandExecution(new($"\t[silver]Replacing property \"{property.Name}\" ({property.UnevaluatedValue}) with {value}[/]"));
                property.UnevaluatedValue = value;
            }
            else if ((property = project.GetProperty(sourceFallback)) != null && !property.IsImported)
            {
                OnCommandExecution(new($"\t[silver]Replacing property \"{property.Name}\" ({property.UnevaluatedValue}) with {value}[/]"));
                property.UnevaluatedValue = value;
            }
            else if (property == null && !string.IsNullOrEmpty(sourceFallback))
            {
                project.SetProperty(sourceFallback, value);
            }
            else if (property == null && !string.IsNullOrEmpty(source))
            {
                project.SetProperty(source, value);
            }
        }
    }
}
