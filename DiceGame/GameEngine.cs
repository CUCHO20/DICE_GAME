using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public class GameEngine
    {
        private readonly List<Dice> _diceList;
        private readonly FairRandomGenerator _fairRand = new();
        private readonly MenuHandler _menuHandler;

        public GameEngine(List<Dice> diceList)
        {
            _diceList = diceList;
            _fairRand = new FairRandomGenerator();
            _menuHandler = new MenuHandler(_fairRand);
        }

        public void Play()
        {
            TableRenderer.RenderProbabilityTable(_diceList, ProbabilityCalculator.CalculatorProbabilities(_diceList));

            bool isComputerFirst = _fairRand.Generate(2) == 1;
            AnsiConsole.MarkupLine(isComputerFirst ? "I make the first move." : "You make the first move.");

            int computerIndex, playerIndex;

            if (isComputerFirst)
            {
                computerIndex = _fairRand.Generate(_diceList.Count);
                AnsiConsole.MarkupLine($"I choose the [green]{_diceList[computerIndex].Name}[/] dice.");
                playerIndex = _menuHandler.SelectDice(_diceList, "Choose your dice:");
            }
            else
            {
                playerIndex = _menuHandler.SelectDice(_diceList, "Choose your dice:");
                computerIndex = (playerIndex + 1) % _diceList.Count;
                AnsiConsole.MarkupLine($"I choose the [green]{_diceList[computerIndex].Name}[/] dice.");
            }

            int computerRoll = _menuHandler.RollDice(_diceList[computerIndex]);
            int playerRoll = _menuHandler.RollDice(_diceList[playerIndex]);

            AnsiConsole.MarkupLine($"\nYour roll result is [green]{playerRoll}[/].");
            AnsiConsole.MarkupLine($"My roll result is [red]{computerRoll}[/].");

            if (playerRoll > computerRoll)
                AnsiConsole.MarkupLine("[bold green]You win![/]");
            else if (computerRoll > playerRoll)
                AnsiConsole.MarkupLine("[bold red]I win![/]");
            else
                AnsiConsole.MarkupLine("It's a tie!");
        }
    }
}
