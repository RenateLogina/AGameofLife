namespace GameofLife
{
    using System;
    using System.Threading;
    using Newtonsoft.Json;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;

    public class GameManager
    {
        //BoardSize stores the level of BoardSizeiculty(board BoardSize) entered buy user

        public int boardSize;
        private string UserAction;
        
        GameLogic gameLogic = new GameLogic();

        public void GameMenu()
        {
            Console.SetWindowSize(155, 30);
            Console.WriteLine("\n\n\n\n\n                                                    Welcome to\n" +
                              "                                                        the\n" +
                              "                                                   GAME OF LIFE\n" +
                              "\n                                              Enter R to resume game\n" +
                              "\n\n                               Choose the prefferred board BoardSize by entering a number!\n" +
                              "\n                                                      1. Small" +
                              "\n                                                      2. Medium" +
                              "\n                                                      3. Large" +
                              "\n\n\n                                                  ENTER Q TO QUIT");

            Console.SetCursorPosition(59, 22);
            UserAction = Console.ReadLine().ToLower();
            switch (UserAction)
            {
                case "1":
                case "2":
                case "3":
                    Console.Clear();
                    boardSize = Convert.ToInt32(UserAction);
                    gameLogic.SetSeed(boardSize);
                    GameBoard();
                    break;
                case "r":
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
        //prints gameboard borders and any UI information of the particular game
        private void GameBoard()
        {
            Console.CursorVisible = false;

            Console.WriteLine("\n  Live cells:");
            Console.WriteLine("  BoardSize is {0}", boardSize);
            Console.SetCursorPosition(25, 1);
            Console.WriteLine("Press P to pause and resume game");
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("Press S to save game");

            //draws game board || vertical lines
            for (int height = 5; height < 20 + boardSize * boardSize; height++)
            {
                Console.SetCursorPosition(1, height);
                Console.Write("\u2588");
                Console.SetCursorPosition(50 * boardSize - 1, height);
                Console.Write("\u2588");
            }
            //draws game board --- horizontal lines
            for (int width = 1; width < 50 * boardSize; width++)
            {
                Console.SetCursorPosition(width, 5);
                Console.Write("\u2584");
                Console.SetCursorPosition(width, 20 + boardSize * boardSize);
                Console.Write("\u2580");
            }
            //loops through iterations
            while (true)
            {
                
                Console.SetCursorPosition(13, 1);
                Console.Write(gameLogic._liveCells + "   ");
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
        private void LoadGame()
        {
            string filePath = @"C:\Users\r.logina\Documents\gameprogress.save";
            GameProgress g = JsonConvert.DeserializeObject<GameProgress>(File.ReadAllText(filePath));
            if (File.Exists(filePath))
            {
                
                boardSize = g.BoardSize;
                gameLogic.Iteration = g.Iteration;
                gameLogic._liveCells = g.LiveCellCount;
                gameLogic.Generation = g.GenerationArray;
                gameLogic.NewGen = g.GenerationArray;
            }

            //test if values are assigned
            //Console.Clear();
            //Console.WriteLine("Board size is: {0}", _boardSize);
            //Console.WriteLine("Live cell count is: {0}", gameLogic._liveCells);
            //Console.WriteLine("Iteration is: {0}", gameLogic.Iteration);
            //Console.WriteLine("Array is: {0}", $@"{{{JsonConvert.SerializeObject(gameLogic.NewGen).Trim('[', ']').Replace("[", "{").Replace("]", "}")}}}");

            //Console.ReadKey();
            //GameBoard();

        }
        private void SaveGame()
        {
            GameProgress gameProgress = new GameProgress
            { GenerationArray = gameLogic.Generation, LiveCellCount = gameLogic._liveCells, BoardSize = boardSize, Iteration = gameLogic.Iteration };

            string filePath = @"C:\Users\r.logina\Documents\gameprogress.save";
            string result = JsonConvert.SerializeObject(gameProgress);
            if (File.Exists(filePath)) File.Delete(filePath);
            File.WriteAllText(filePath, result);
        }
    }
}
