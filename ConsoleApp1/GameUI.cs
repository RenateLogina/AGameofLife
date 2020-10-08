namespace GameofLife
{
    using System;
    using System.Threading;
    public class GameUI
    { 
        //BoardSize stores the level of BoardSizeiculty(board BoardSize) entered buy user
        private int _boardSize;
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
            string UserAction = Console.ReadLine().ToLower();
            switch (UserAction)
            {
                case "1":
                case "2":
                case "3":
                    Console.Clear();
                    _boardSize = Convert.ToInt32(UserAction);
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
        private void GameBoard()
        {
            GameLogic gameLogic = new GameLogic();
            Console.CursorVisible = false;

            Console.WriteLine("\n  Live cells: {0}", gameLogic.LiveCells); 
            Console.WriteLine("  BoardSize is {0}", _boardSize);
            Console.SetCursorPosition(25, 1);
            Console.WriteLine("Press P to pause and resume game");
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("Press S to save game");

            //draws game board || vertical lines
            for (int height = 5; height < 20 + _boardSize * _boardSize; height++)
            {
                Console.SetCursorPosition(1, height);
                Console.Write("\u2588");
                Console.SetCursorPosition(50 * _boardSize - 1, height);
                Console.Write("\u2588");
            }
            //draws game board --- horizontal lines
            for (int width = 1; width < 50 * _boardSize; width++)
            {                
                Console.SetCursorPosition(width, 5);
                Console.Write("\u2584");
                Console.SetCursorPosition(width, 20 + _boardSize * _boardSize);
                Console.Write("\u2580");
            }

            //reads and prints logic.
            int Iteration = 0;
            gameLogic.SetSeed(_boardSize);
            //loops through iterations
            while(true)
            {
                gameLogic.PrintArray();
                Iteration++;
                Console.SetCursorPosition(2, 3);
                Console.WriteLine("Iteration NR: {0}", Iteration);
                gameLogic.NewGeneration();
                Thread.Sleep(1000);

                //checks if user presses any key to pause or save
                if (Console.KeyAvailable)
                {
                    if(Console.ReadKey(true).Key == ConsoleKey.S)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(13, 5);
                        Console.WriteLine("Save the game");
                        break;
                    }
                    else
                    {
                        if (Console.ReadKey(true).Key == ConsoleKey.S)
                        {
                            Console.Clear();
                            Console.SetCursorPosition(13, 5);
                            Console.WriteLine("Save the game");
                            break;
                        }
                    }                   
                }
            }
            Console.ReadKey();
        }

    }
}
