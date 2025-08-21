using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public class TableRenderer
    {
        public static void RenderProbabilityTable(List<Dice> diceList, double[,] probabilities)
        {
            var table = new Table()
                .RoundedBorder()
                .Title("[green]Probability of the win for the user:[/]");

            table.AddColumn(new TableColumn("[blue]User dice v[/]").Centered());
            foreach (var d in diceList)
            {
                table.AddColumn(new TableColumn($"[yellow]{d.Name}[/]").Centered());
            }

            for (int i = 0; i < diceList.Count; i++)
            {
                var row = new List<string> { $"[cyan]{diceList[i].Name}[/]" };
                for (int j = 0; j < diceList.Count; j++)
                {
                    if (i == j)
                        row.Add("-");
                    else
                    {
                        double p = probabilities[i, j];
                        row.Add($"{p:F4}");
                    }
                }
                table.AddRow(row.ToArray());
            }

            AnsiConsole.Write(table);
        }
    }
}
