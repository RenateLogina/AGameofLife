namespace GameofLife
{
    using System;
    using System.Threading;
    using Newtonsoft.Json;
    using System.IO;
    public class GameManager
    {
        //BoardSize stores the level of BoardSizeiculty(board BoardSize) entered buy user
        public int BoardSize;
        private string userAction;        
        GameLogic gameLogic = new GameLogic();
        /// <summary>
        /// Prints main game menu
        /// </summary>
        public void GameMenu()
        {
            Console.SetWindowSize(155, 30);
            Console.WriteLine("\n\n\n\n\n                                                    Welcome to\n" +
                              "                                                        the\n" +
                              "                                                   GAME OF LIFE\n" +
                              "\n                                            Enter L to load saved game\n" +
                              "\n\n                               Choose the prefferred board BoardSize by entering a number!\n" +
                              "\n                                                      1. Small" +
                              "\n                                                      2. Medium" +
                              "\n                                                      3. Large" +
                              "\n\n\n                                                  ENTER Q TO QUIT");

            Console.SetCursorPosition(59, 22);
            userAction = Console.ReadLine().ToLower();
            switch (userAction)
            {
                case "1":
                case "2":
                case "3":
                    Console.Clear();
                    BoardSize = Convert.ToInt32(userAction);
                    gameLogic.SetSeed(BoardSize);
                    GameBoard();
                    break;
                case "l":
                    Console.Clear();
                    LoadGame();
                    GameBoard();
                    break;
                default:
                    Console.Clear();
                    break;
                case "q":
                    return;
            }
        }
        /// <summary>
        /// prints gameboard borders and any UI information of the particular game
        /// </summary>
        private void GameBoard()
        {
            Console.CursorVisible = false;
            Console.WriteLine("\n  Live cells:");
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
                Console.SetCursorPosition(50 * BoardSize - 1, height);
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
            //loops through iterations
            while (true)
            {
                
                Console.SetCursorPosition(13, 1);
                Console.Write(gameLogic.LiveCells + "   ");
                Console.SetCursorPosition(2, 3);
                Console.WriteLine("Iteration NR: {0}", gameLogic.Iteration);
                gameLogic.PrintArray();

                Thread.Sleep(1000);

                //checks if user presses any key to pause or save
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.S)
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
                            SaveGame();
                            break;
                        }
                    }
                }
                gameLogic.Iteration++;
                gameLogic.NewGeneration();
            }
            Console.ReadKey();
        }
        /// <summary>
        /// Fills variables with saved values. If moved to separate class, stack overflow exception
        /// </summary>
        private void LoadGame()
        {
            string filePath = @"C:\Users\r.logina\Documents\gameprogress.save";
            GameProgress g = JsonConvert.DeserializeObject<GameProgress>(File.ReadAllText(filePath));
            if (File.Exists(filePath))
            {
                gameLogic.SizeX = g.SizeX;
                gameLogic.SizeY = g.SizeY;
                BoardSize = g.BoardSize;
                gameLogic.Iteration = g.Iteration;
                gameLogic.LiveCells = g.LiveCellCount;
                gameLogic.Generation = g.GenerationArray;
                gameLogic.NewGen = g.GenerationArray;
            }

            //test if values are assigned
            //Console.Clear();
            //Console.WriteLine("Board size is: {0}", _BoardSize);
            //Console.WriteLine("Live cell count is: {0}", gameLogic.LiveCells);
            //Console.WriteLine("Iteration is: {0}", gameLogic.Iteration);
            //Console.WriteLine("Array is: {0}", $@"{{{JsonConvert.SerializeObject(gameLogic.NewGen).Trim('[', ']').Replace("[", "{").Replace("]", "}")}}}");

            //Console.ReadKey();
            //GameBoard();

        }
        /// <summary>
        /// Saves current values to file
        /// </summary>
        private void SaveGame()
        {
            GameProgress gameProgress = new GameProgress
            { GenerationArray = gameLogic.Generation, LiveCellCount = gameLogic.LiveCells, BoardSize = BoardSize, Iteration = gameLogic.Iteration, SizeX = gameLogic.SizeX, SizeY =gameLogic.SizeY };

            string filePath = @"C:\Users\r.logina\Documents\gameprogress.save";
            string result = JsonConvert.SerializeObject(gameProgress);
            if (File.Exists(filePath)) File.Delete(filePath);
            File.WriteAllText(filePath, result);
        }
    }
}
