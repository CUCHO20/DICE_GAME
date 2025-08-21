namespace DiceGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: dotnet run \"face1,face2,...\" \"face1,face2,...\" ...");
                Console.WriteLine("Example: dotnet run \"2,2,4,4,9,9\" \"6,8,1,1,8,6\" \"7,5,3,7,5,3\"");
                return; 
            }

            var parser = new DiceParser();
            var diceList = parser.Parse(args);

            if (diceList == null)
            {
                Console.WriteLine("Invalid input. Please ensure all arguments are comma-separated integers.");
                return;
            }

            var game = new GameEngine(diceList);
            game.Play();
        }
    }
}
