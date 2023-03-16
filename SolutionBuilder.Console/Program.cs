// See https://aka.ms/new-console-template for more information
using Microsoft.Build.Locator;
using SolutionBuilder.Model;
using Spectre.Console;

MSBuildLocator.RegisterDefaults();
string? configPath = args.FirstOrDefault(o => o.StartsWith("-F"))?.Substring(2);


if (!SolutionConfiguration.TryParse(File.ReadAllText(configPath ?? "Configuration.json"), out SolutionConfiguration? configuration))
{
    AnsiConsole.MarkupLine("[red]Invalid configuration[/]");
    throw new InvalidOperationException("Invalid configuration");
}

AnsiConsole.MarkupLine("[yellow]Configuration loaded[/]...");
AnsiConsole.MarkupLine($"[yellow]Found[/] [bold]{configuration!.Installations.Count}[/] [yellow]installations.[/]");

bool execution = false;
foreach (Installation installation in configuration.Installations)
{
    Progress? progress = AnsiConsole.Progress()
                .AutoClear(true)
                .HideCompleted(true)
                .Columns(new ProgressColumn[]
                {
                    new TaskDescriptionColumn(),
                    new ProgressBarColumn(),
                    new PercentageColumn(),
                });

    installation.Progress += (sender, e) => AnsiConsole.MarkupLine(e.Text);
    AnsiConsole.MarkupLine($"\t[green]{Markup.Escape($"[{installation.Name}]")}[/]");

    try
    {
        execution = installation.Execute(progress!);
    }
    catch (SolutionBuilder.InvalidConfigurationException iex)
    {
        AnsiConsole.MarkupLine($"[red](Configuration exception) {iex.Message ?? ""}[/]");
    }
    catch (Exception ex)
    {
        AnsiConsole.MarkupLine($"[red](General exception) {ex.Message ?? ""}[/]");
    }
    if (!execution) { break; }
}

AnsiConsole.MarkupLine(execution ? "[green]Done![/]" : "[red]Failed![/]");
Console.ReadLine();

await Task.Delay(5000);
