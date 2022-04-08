using System;
using Task3.Help;
using ASCIITableGenerator;
using System.Linq;

namespace Task3
{
    class ProgramGame
    {
        public static bool Controller(string userCommand)
        {
            switch (userCommand)
            {
                case "0":
                    return false;
                case "?":
                    ASCIITable helpTable = Table.Create(Game.moves);
                    Console.WriteLine(helpTable.GetAsString());
                    return true;
                default:
                {
                    Game.MakeUserMove(Convert.ToInt32(userCommand));
                    Console.WriteLine($"Your move: {Game.GetMoveString(Convert.ToInt32(userCommand) - 1)}");
                    return true;
                }
            }
        }
        
        public static bool IsInputCorrect(string input)
        {
            return (int.TryParse(input, out _) && Game.CheckUserMove(Convert.ToInt32(input))) 
                   || (input == "?");
        }
        
        public static bool IsArgumentsCorrect(string[] args)
        {
            return !(
                (args.Length < 3) || 
                (args.Length % 2 == 0) || 
                (args.Distinct().ToArray().Length != args.Length)
            );
        }
        
        public static void Display()
        {
            Console.WriteLine("Available moves:");
            int i = 0;
            foreach (var move in Game.moves)
            {
                Console.WriteLine($"{++i} - {move}");
            }
            Console.WriteLine("0 - Exit");
            Console.WriteLine("? - Help");
        }
        
        static void Main(string[] args)
        {
            if (!IsArgumentsCorrect(args))
            {
                Console.WriteLine("Incorrect command line arguments! It should be more than 2 arguments.");
                return;
            }
            while (true)
            {
            Game.CreateGameRools(args);
            Game.MakeComputerMove();
            Console.WriteLine($"HMAC: {Game.hmac}");
            Display();
                string userInput;
                Console.Write("Enter your move: ");
                while (!IsInputCorrect(userInput = Console.ReadLine()))
                {
                    Console.WriteLine("Incorrect input! Please make your move again.");
                    Console.Write("Enter your move: ");
                }
                if (!Controller(userInput))
                {
                    Console.WriteLine("Bye.");
                    return;
                }
                else if (!userInput.Contains("?"))
                {
                    Console.WriteLine(
                        $"Computer move: {Game.GetMoveString(Convert.ToInt32(Game.computerMoveIndex))}");
                    bool? isUserWin;
                    if ((isUserWin = Game.CheckGameResult()) == null)
                    {
                        Console.WriteLine("Draw!");
                    }
                    else
                    {
                        Console.WriteLine(isUserWin == true ? "You win!" : "You loss!");
                    }
                    Console.WriteLine($"HMAC key: {HMACGenerator.ConvertKeyToHex(Game.secretKey)}");
                    Console.ReadLine();
                }
            }
        }
    }
}
