
using Microsoft.Build.Construction;
using Newtonsoft.Json;
using Spectre.Console;

namespace SolutionBuilder.Model
{
    public class Installation
    {
        protected internal SolutionConfiguration _parent = new();

        [JsonProperty("Name", Order = 0, Required = Required.Always)]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("SolutionFilename", Order = 1, Required = Required.Always)]
        public string SolutionFilename { get; set; } = string.Empty;

        [JsonProperty("SourcePath", Order = 2, Required = Required.DisallowNull)]
        public string SourcePath { get; set; } = string.Empty;

        [JsonProperty("OutputPath", Order = 3, Required = Required.DisallowNull)]
        public string OutputPath { get; set; } = string.Empty;

        [JsonProperty("Compression", Order = 4, Required = Required.DisallowNull)]
        public int Compression { get; set; } = 0;
        [JsonProperty("Enabled", Order = 5, Required = Required.Always)]
        public bool Enabled { get; set; } = true;

        [JsonProperty("ClearDestination", Order = 6, Required = Required.Always)]
        public bool ClearDestination { get; set; } = false;

        [JsonProperty("Commands", Order = 7)]
        [JsonConverter(typeof(CommandConverter))]
        public List<Command> Commands { get; set; } = new();

        public event EventHandler<ProgressEventArgs>? Progress;

        public bool Execute(Progress? progress)
        {
            //Console.WriteLine("Premi un tasto per continuare:");
            //var x = Console.ReadKey();
            if (!Enabled)
            {
                OnProgress(new("[darkorange]Operation disabled by configuration![/]"));
                return true;
            }

            // source project path
            string sourcePath = Path.Combine(_parent.SourcePath, SourcePath);
            // destination project path (here are copied all dll's)
            string destinationPath = Path.Combine(_parent.OutputPath, OutputPath);
            // temporary support path (here are copied all project files and will be used as modification)
            string temporaryPath = Path.Combine(_parent.TemporaryPath, OutputPath);


            if (progress != null)
            {
                progress.Start(ctx =>
                {
                    if (Directory.Exists(temporaryPath) && ClearDestination)
                    {
                        OnProgress(new("[grey]Temporary directory delete...[/]"));
                        DeleteDirectory(temporaryPath, ctx);
                    }

                    if (!Directory.Exists(temporaryPath) && ClearDestination)
                    {
                        Directory.CreateDirectory(temporaryPath);
                    }
                    else if (!Directory.Exists(temporaryPath) && !ClearDestination)
                    {
                        throw new InvalidConfigurationException("Check configuration, temporary path does not exists and clear destination is not set...");
                    }

                    OnProgress(new("[grey]Temporary directory creation...[/]"));
                    OnProgress(new("[grey]Starting copy[/]"));

                    if (Directory.Exists(temporaryPath) && ClearDestination)
                    {
                        CopyDirectory(sourcePath, temporaryPath, true, ctx);
                    }

                    OnProgress(new("[grey]Copy completed[/]"));
                });
            }
            else
            {
                if (Directory.Exists(temporaryPath) && ClearDestination)
                {
                    OnProgress(new("[grey]Temporary directory delete...[/]"));
                    DeleteDirectory(temporaryPath, null);
                }

                if (!Directory.Exists(temporaryPath) && ClearDestination)
                {
                    Directory.CreateDirectory(temporaryPath);
                }

                OnProgress(new("[grey]Temporary directory creation...[/]"));
                OnProgress(new("[grey]Starting copy[/]"));

                if (Directory.Exists(temporaryPath) && ClearDestination)
                {
                    CopyDirectory(sourcePath, temporaryPath, true, null);
                }

                OnProgress(new("[grey]Copy completed[/]"));
            }


            string solutionFile = Path.Combine(temporaryPath, SolutionFilename);
            if (!File.Exists(solutionFile))
            {
                OnProgress(new($"\t[red]File {SolutionFilename} does not exists in temporary path...[/]"));
                throw new InvalidOperationException($"File {SolutionFilename} does not exists in temporary path...");
            }

            SolutionFile solution = SolutionFile.Parse(solutionFile);
            bool result = false;
            foreach (Command command in Commands.OrderBy(c => c.Order))
            {
                command.CommandExecution += CommandExecution;
                command.SolutionFile = solutionFile;
                if (command is CommandBuild build)
                {
                    build.Options.Add(nameof(_parent.Configuration), _parent.Configuration);
                    build.Options.Add(nameof(_parent.Platform), _parent.Platform);
                }

                result = command switch
                {
                    CommandCopy => command.Execute(solution, _parent.OutputPath, progress),
                    _ => command.Execute(solution, _parent.TemporaryPath, progress),
                };

                if (result == false) { return false; }
            }

            return result;
        }

        private void CommandExecution(object? sender, CommandEventArgs e)
        {
            string text = e.Text;
            OnProgress(new(text));
        }

        protected virtual void OnProgress(ProgressEventArgs e) => Progress?.Invoke(this, e);

        internal static void CopyDirectory(string sourcePath, string destinationPath, bool recursive, ProgressContext? context)
        {
            DirectoryInfo directory = new(sourcePath);
            if (sourcePath.ToLower().EndsWith("resources") == true)
            {

            }
            if (!directory.Exists)
            {
                AnsiConsole.MarkupLine($"\t[red]Source directory not found {directory.FullName}[/]");
                throw new DirectoryNotFoundException($"Source directory not found {directory.FullName}");
            }

            if (sourcePath.StartsWith("C:\\Windows")) { return; }

            if (directory.Name.StartsWith(".")) { return; }
            if (directory.Name.StartsWith("bin")) { return; }
            if (directory.Name.StartsWith("obj")) { return; }

            DirectoryInfo[] directories = directory.GetDirectories();
            Directory.CreateDirectory(destinationPath);

            ProgressTask? task = context?.AddTask(directory.Name, true);

            FileInfo[] files = directory.GetFiles().Where(l => l.Extension != "licx").ToArray();

            for (int i = 0; i < files.Length; ++i)
            {
                FileInfo file = files[i];
                if (task != null) { task.Value = (i - 1) / (double)(files.Length + directories.Length) * 100.0; }
                string target = Path.Combine(destinationPath, file.Name);
                file.CopyTo(target);
            }

#if DEBUG
            context?.Refresh();
#endif

            if (recursive)
            {
                for (int i = 0; i < directories.Length; ++i)
                {
                    DirectoryInfo dir = directories[i];
                    if (task != null) { task.Value = (files.Length + (i - 1)) / (double)(files.Length + directories.Length) * 100.0; }
                    string destination = Path.Combine(destinationPath, dir.Name);
                    CopyDirectory(dir.FullName, destination, recursive, context);
                }
            }

            task?.StopTask();
        }

        internal static void DeleteDirectory(string target_dir, ProgressContext? context)
        {
            ProgressTask? task = context?.AddTask(target_dir, true);

            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            for (int i = 0; i < files.Length; ++i)
            {
                string file = files[i];
                if (task != null) { task.Value = (i - 1) / (double)(files.Length + dirs.Length) * 100.0; }
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            for (int i = 0; i < dirs.Length; ++i)
            {
                string dir = dirs[i];
                if (task != null) { task.Value = (files.Length + (i - 1)) / (double)(files.Length + dirs.Length) * 100.0; }
                DeleteDirectory(dir, context);
            }

            Directory.Delete(target_dir, false);
            task?.StopTask();
        }
    }
    public class ProgressEventArgs : EventArgs
    {
        public string Text { get; init; }

        public ProgressEventArgs(string text)
        {
            Text = text;
        }
    }
}
