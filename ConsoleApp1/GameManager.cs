namespace GameofLife
{
    using System;
    using System.Threading;
    using Newtonsoft.Json;
    using System.IO;
    public class GameManager
    {
        //BoardSize stores the level of BoardSizeiculty(board BoardSize) entered buy user     
        GameLogic gL = new GameLogic();
        GameUI uI = new GameUI();
        Serializer ser = new Serializer();
        GameProgress gP = new GameProgress();
        public void StartGame()
        {
            string userAction = uI.GameMenu();
            switch (userAction)
            {
                case "1":
                case "2":
                case "3":
                    gL.BoardSize = Convert.ToInt32(userAction);
                    gL.SetSeed(gL.BoardSize);
                    uI.GameHeader(gL.BoardSize);
                    GameLoop();
                    break;
                case "l":
                    LoadGame();
                    uI.GameHeader(gL.BoardSize);
                    GameLoop();
                    break;
                default:
                    break;
                case "q":
                    return;
            }
        }

        /// <summary>
        /// prints gameboard borders UI information of the particular game and loops the game :O
        /// </summary>
        private void GameLoop()
        {
            //loops through iterations
            while (true)
            {
                Console.SetCursorPosition(0, 3);
                Console.Write(gL.PrintArray());
                Thread.Sleep(1000);

                //checks if user presses any key to pause or save
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.S)
                    {
                        
                        Console.Clear();
                        ser.SaveGame(gL.Generation, gL.SizeX, gL.SizeY, gL.Iteration, gL.LiveCells, gL.BoardSize);
                        Console.SetCursorPosition(13, 5);
                        
                        Console.WriteLine("The game is saved");
                        Console.WriteLine("         Press R to return to menu");
                        if (Console.ReadKey(true).Key == ConsoleKey.R)
                        {
                            //uI.GameisSaved();// doesn't save properly!!! how to fix?
                            Console.Clear();
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
                            ser.SaveGame(gL.Generation, gL.SizeX, gL.SizeY, gL.Iteration, gL.LiveCells,gL.BoardSize);
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
                gL.BoardSize = g.BoardSize;
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
            { GenerationArray = gL.Generation, LiveCellCount = gL.LiveCells, BoardSize = gL.BoardSize, Iteration = gL.Iteration, SizeX = gL.SizeX, SizeY =gL.SizeY };

            string filePath = @"C:\Users\r.logina\Documents\gameprogress.save";
            string result = JsonConvert.SerializeObject(gameProgress);
            if (File.Exists(filePath)) File.Delete(filePath);
            File.WriteAllText(filePath, result);
        }
    }
}
