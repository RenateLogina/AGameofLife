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
                
        GameLogic gL = new GameLogic();
        GameUI uI = new GameUI();
        public void StartGame()
        {
            string userAction = uI.GameMenu();
            switch (userAction)
            {
                case "1":
                case "2":
                case "3":
                    Console.Clear();
                    BoardSize = Convert.ToInt32(userAction);
                    gL.SetSeed(BoardSize);
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
                GameUI uI = new GameUI();
                
                gL.PrintArray();
                Console.SetCursorPosition(2, 3);
                Console.WriteLine("Iteration NR: {0}", gL.Iteration);
                
                Console.SetCursorPosition(13, 1);
                Console.Write(gL.LiveCells + "   ");

                Thread.Sleep(1000);

                //checks if user presses any key to pause or save
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.S)
                    {
                        
                        Console.Clear();
                        Console.SetCursorPosition(13, 5);
                        Console.WriteLine("The game is saved");
                        Console.WriteLine("         Press R to return to menu");
                        SaveGame();
                        if (Console.ReadKey(true).Key == ConsoleKey.R)
                        {
                            uI.GameisSaved();// doesn't save properly!!! how to fix?
                            StartGame();
                        }
                        break;
                    }
                    else
                    {
                        if (Console.ReadKey(true).Key == ConsoleKey.S)
                        {
                            
                            Console.Clear();
                            Console.SetCursorPosition(13, 5);
                            Console.WriteLine("The game is saved");
                            Console.WriteLine("         Press R to return to menu");
                            SaveGame();
                            if (Console.ReadKey(true).Key == ConsoleKey.R)
                            {
                                Console.Clear();
                                StartGame();
                            }
                            break;
                        }
                    }
                }
                gL.Iteration++;
                gL.NewGeneration();
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
                gL.SizeX = g.SizeX;
                gL.SizeY = g.SizeY;
                BoardSize = g.BoardSize;
                gL.Iteration = g.Iteration;
                gL.LiveCells = g.LiveCellCount;
                gL.Generation = g.GenerationArray;
            }
        }
        /// <summary>
        /// Saves current values to file
        /// </summary>
        public void SaveGame()
        {
            GameProgress gameProgress = new GameProgress
            { GenerationArray = gL.Generation, LiveCellCount = gL.LiveCells, BoardSize = BoardSize, Iteration = gL.Iteration, SizeX = gL.SizeX, SizeY =gL.SizeY };

            string filePath = @"C:\Users\r.logina\Documents\gameprogress.save";
            string result = JsonConvert.SerializeObject(gameProgress);
            if (File.Exists(filePath)) File.Delete(filePath);
            File.WriteAllText(filePath, result);
        }
    }
}
