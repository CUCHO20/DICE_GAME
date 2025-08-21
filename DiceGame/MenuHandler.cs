using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    internal class MenuHandler
    {
        public int SelectDice(List<Dice> diceList, string role)
        {
            var prompt = new SelectionPrompt<string>()
                .Title($"\n[bold]{role}, select a dice:[/]")
                .PageSize(10);

            var options = new List<string>();
            for (int i = 0; i < diceList.Count; i++)
            {
                options.Add(diceList[i].Name);
            }

            options.Add("Exit");
            options.Add("Help");

            var result = AnsiConsole.Prompt(prompt.AddChoices(options));

            int index = options.IndexOf(result);
            if (index == options.Count - 2) Environment.Exit(0);
            return index;
        }

        public int RollDice(FairRandomGenerator fairRand, Dice dice)
        {
            int faceIndex = fairRand.Generate(dice.Faces.Count);
            return dice.Roll(faceIndex);
        }
    }
}
