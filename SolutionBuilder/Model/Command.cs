using Microsoft.Build.Construction;

namespace SolutionBuilder.Model
{
    public abstract class Command
    {
        protected readonly string DefaultFilePattern = "*.cs";
        protected internal Installation _parent = new();

        public int Order { get; set; }
        public string? Name { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<string> ProjectNames { get; set; } = [];
        public string? SolutionFile { get; set; }

        public Dictionary<string, CommandAction> Actions { get; set; } = [];

        public event EventHandler<CommandEventArgs>? CommandExecution;

        public abstract bool Execute(SolutionFile solution, string basePath, Spectre.Console.Progress? progress = null);


        protected virtual void OnCommandExecution(CommandEventArgs e) => CommandExecution?.Invoke(this, e);
    }

    public class CommandEventArgs(string text) : EventArgs
    {
        public string Text { get; init; } = text;
    }

    public record class CommandAction(int Order, string Source, string SourceFallback, string Value, string? File = "*.cs");

    public static class CommandExtensions
    {
        public static string ToStringOnlyAction(this CommandAction action, Command cmd, string Key)
        {
            string text = "";

            if (action != null)
            {
                switch (cmd)
                {
                    case CommandReplace:
                        text = $"[{action.Source}] -> [{action.Value}]";
                        break;
                    case CommandReplaceCsProject:
                    case CommandReplaceProperties:
                        text = $"[{action.Source}] = '{action.Value}'";
                        break;
                    case CommandReplaceReferences:
                        text = $"[{action.Source}] -> [{action.File}]";
                        break;
                    case CommandCopy:
                        break;
                    case CommandBuild:
                        break;
                    default:
                        text = $"[{action.Source}] -> [{action.Value}]";
                        break;
                }

            }
            else
                text = "";


            return text;
        }

        public static string ToString(this CommandAction action, Command cmd, string Key)
        {
            string text = "";

            if (action != null)
            {
                switch (cmd)
                {
                    case CommandReplace:
                        text = $"{Key} [{action.Source}] -> [{action.Value}]";
                        break;
                    case CommandReplaceCsProject:
                    case CommandReplaceProperties:
                        text = $"{Key} [{action.Source}] = '{action.Value}'";
                        break;
                    case CommandReplaceReferences:
                        text = $"{Key} [{action.Source}] -> [{action.File}]";
                        break;
                    case CommandCopy:
                        break;
                    case CommandBuild:
                        break;
                    default:
                        text = $"{Key} [{action.Source}] -> [{action.Value}]";
                        break;
                }

            }
            else
                text = "";


            return text;
        }
    }


}
