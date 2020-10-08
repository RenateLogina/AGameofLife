namespace GameofLife
{
    using System;
    using System.Threading;
    public class GameUI
    {
        //UserAction should store the string entered when navigating menu
        public string UserAction = "";
        GameLogic logic = new GameLogic();
        //BoardSize stores the level of BoardSizeiculty(board BoardSize) entered buy user
        public int BoardSize;

        public void GameMenu()
        {
            Console.SetWindowSize(155, 30);
            Console.WriteLine("\n\n\n\n\n                                                    Welcome to\n" +
                              "                                                        the\n" +
                              "                                                   GAME OF LIFE\n"+
                              "\n\n                               Choose the prefferred board BoardSize by entering a number!\n" +
                              "\n                                                      1. Small" +
                              "\n                                                      2. Medium" +
                              "\n                                                      3. Large"+
                              "\n\n\n                                                  ENTER Q TO QUIT");

            Console.SetCursorPosition(59, 22);
            UserAction = Console.ReadLine().ToLower();
            switch (UserAction)
            {
                case "1":
                case "2":
                case "3":
                    Console.Clear();
                    BoardSize = Convert.ToInt32(UserAction);
                    GameBoard();
                    break;
                default:
                    Console.Clear();
                    break;
                case "q":
                    return;
            }
        }
        //prints gameboard borders and any UI information of the particular game
        public void GameBoard()
        {
            Console.CursorVisible = false;

            Console.WriteLine("\n  Live cells: {0}", logic.LiveCells); 
            Console.WriteLine("  BoardSize is {0}", BoardSize);
            Console.SetCursorPosition(25, 1);
            Console.WriteLine("Press P to pause and resume game");
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("Press S to save game");

            //draws game board || vertical lines
            for (int height = 5; height < 20 + BoardSize * BoardSize; height++)
            {
                Console.SetCursorPosition(1, height);
                Console.Write("\u2588");
                Console.SetCursorPosition(50 * BoardSize-1, height);
                Console.Write("\u2588");
            }
            //draws game board --- horizontal lines
            for (int width = 1; width < 50 * BoardSize; width++)
            {                
                Console.SetCursorPosition(width, 5);
                Console.Write("\u2584");
                Console.SetCursorPosition(width, 20 + BoardSize * BoardSize);
                Console.Write("\u2580");
            }

            //reads and prints logic.
            int Iteration = 0;
            logic.SetSeed(BoardSize);
            //loops through iterations
            while(true)
            {
                logic.PrintArray();
                Iteration++;
                Console.SetCursorPosition(2, 3);
                Console.WriteLine("Iteration NR: {0}", Iteration);
                logic.NewGeneration();
                Thread.Sleep(1000);

                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.S)
                {
                    Console.Clear();
                    Console.SetCursorPosition(13, 5);
                    Console.WriteLine("Save the game");
                    break;
                }
                else if (Console.KeyAvailable && Console.ReadKey(true).Key != ConsoleKey.S)
                {
                    //You can pause and resume at this point. The following line resumes the loop
                    Console.ReadKey(true);
                }

            }
            Console.ReadKey();

            //stops the script
            //while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.P))
            //{
            //    logic.PrintArray();
            //    Iteration++;
            //    Console.SetCursorPosition(2, 3);
            //    Console.WriteLine("Iteration NR: {0}", Iteration);
            //    logic.NewGeneration();
            //    Thread.Sleep(1000);
            //}


        }

    }
}
