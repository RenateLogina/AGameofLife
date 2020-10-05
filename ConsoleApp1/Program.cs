using System;

namespace GameofLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Game game = new Game();
            while (true)
            {
                Console.Clear();
                game.GameMenu();
                string UserAction = Console.ReadLine().ToLower();
                switch (UserAction)
                {
                    case "1":
                    case "2":
                    case "3":
                        Console.Clear();
                        game.GameBoard(UserAction);
                        game.GameLogic();
                        break;
                    default:
                        Console.Clear();
                        break;
                    case "q":
                        return;
                }

            }
        }
    }
}
