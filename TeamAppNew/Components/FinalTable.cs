using TeamAppNew.Models;
using Spectre.Console;

namespace TeamAppNew.Components;

public class CustomComponents
{
    public void ShowFinalTable(List<Member> members)
    {
        var table = new Table();

        table.Title = new TableTitle("\n[bold yellow]TEAM MEMBERS:[/]");

        table.AddColumn(new TableColumn("[blue]Member Name[/]").Centered());
        table.AddColumn(new TableColumn("[blue]Member Age[/]").Centered());
        table.AddColumn(new TableColumn("[blue]Member Skills[/]").Centered());
        table.AddColumn(new TableColumn("[blue]Contract Type[/]").Centered());

        for (int i = 0; i < members.Count; i++)
        {
            var languages = string.Join(", ", members[i].ProgramingLanguages);

            if (i % 2 == 0)
            {
                table.AddRow(
                    $"[fuchsia]{members[i].Name}[/]",
                    $"[fuchsia]{members[i].Age}[/]",
                    $"[fuchsia]{languages}[/]",
                    $"[fuchsia]{members[i].ContractType}[/]"
                    );
            }
            else
            {
                table.AddRow(
                    $"[green1]{members[i].Name}[/]",
                    $"[green1]{members[i].Age}[/]",
                    $"[green1]{languages}[/]",
                    $"[green1]{members[i].ContractType}[/]"
                    );
            }

            table.AddEmptyRow();
        }

        table.Columns[0].Width(20);
        table.Columns[1].Width(10);
        table.Columns[2].Width(40);
        table.Columns[3].Width(15);

        table.Border = TableBorder.Double;

        AnsiConsole.Write(table);
    }
}

