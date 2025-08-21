using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public class FairRandomGenerator
    {
        private readonly SHA3_256 _sha3 = SHA3_256.Create();

        public int Generate(int range)
        {
            byte[] secretKey = new byte[32];
            RandomNumberGenerator.Fill(secretKey);

            int computerRoll = Utils.Modulo(Utils.SecureRandomInt(0, int.MaxValue), range);

            string hmac = ComputeHmac(computerRoll, secretKey);

            AnsiConsole.MarkupLine($"I selected a random value in the range [0..{range - 1}] (HMAC={hmac}).");

            int userRoll = ShowMenu(range);

            int finalResult = Utils.Modulo(computerRoll + userRoll, range);
            AnsiConsole.MarkupLine($"My number is [green]{computerRoll}[/] (KEY=[yellow]{BitConverter.ToString(secretKey).Replace("-", "")}[/]).");
            AnsiConsole.MarkupLine($"The fair number generation result is [bold]{computerRoll} + {userRoll} = {finalResult} (mod {range})[/].");

            return finalResult;
        }

        private string ComputeHmac(int computerRoll, byte[] secretKey)
        {
            using (var hmac = new HMACSHA256(secretKey))
            {
                byte[] message = BitConverter.GetBytes(computerRoll);
                byte[] hash = hmac.ComputeHash(message);
                return BitConverter.ToString(hash).Replace("-", "").ToUpper();
            }
        }

        private int ShowMenu(int range)
        {
            while (true)
            {
                var table = new SelectionPrompt<int>()
                    .Title("Add your number modulo [blue]" + range + "[/]:")
                    .PageSize(10)
                    .AddChoices(Enumerable.Range(0, range).ToArray());

                foreach (int i in Enumerable.Range(0, range))
                {
                    table.AddChoice(i);
                }

                table.AddChoices(-1); // exit
                table.AddChoice(-2); // help

                var choice = AnsiConsole.Prompt(table);

                if (choice == -1) Environment.Exit(0);
                if (choice >= 0 && choice < range) return choice;
            }
        }

        // Note: HMACSHA256 is used instead of SHA3-256 for HMAC computation
        // Step 1: Computer generates secret key
        // Step 2: Computer rolls number
        // Step 3: Computer computes HMAC of the rolled number using the secret key
        // Step 4: User chooses their number
        // Step 5: Reveal key and compute final fair roll

    }
}
