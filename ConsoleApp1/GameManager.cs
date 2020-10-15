namespace GameofLife
{
    using System;
    using System.Runtime.InteropServices.ComTypes;
    using System.Timers;

    public class GameManager
    {
        private static System.Timers.Timer myTimer;
        GameLogic gameLogic = new GameLogic();
        GameUI gameUI = new GameUI();
        Serializer serializer = new Serializer();
        GameProgress gameProgress = new GameProgress();

        public void StartGame()
        {
            string userializerAction = gameUI.GameMenu();

            switch (userializerAction)
            {
                case "1":
                case "2":
                case "3":
                    gameLogic.BoardSize = Convert.ToInt32(userializerAction);
                    gameLogic.SetSeed(gameLogic.BoardSize);
                    SetTimer();
                    gameUI.GameHeader();

                    Console.Read();

                    //GameLoop();
                    break;

                case "l":
                    LoadGame();
                    gameUI.GameHeader();
                    //GameLoop();
                    break;

                default:
                    break;

                case "q":
                    return;
            }
        }

        private  void SetTimer()
        {
            myTimer = new System.Timers.Timer(1000);
            myTimer.Elapsed += GameLoop;
            myTimer.AutoReset = true;
            myTimer.Enabled = true;
            
        }

        private void GameLoop(Object source, ElapsedEventArgs e)
        {
            Console.SetCursorPosition(0, 3);
            Console.Write(gameLogic.PrintArray());
            gameLogic.Iteration++;
            gameLogic.NewGenerationeration();
        }

        //private void ToggleTimer()
        //{
        //    myTimer.Enabled = false;
        //}

        private void GameLoop2(Object source, ElapsedEventArgs e)
        {
            gameUI.Cycle();
        }

        /// <summary>
        /// prints gameboard borders UI information of the particular game and loops the game :O
        /// </summary>
        private void GameLoop3()
        {
                Console.SetCursorPosition(0, 3);
                Console.Write(gameLogic.PrintArray());

                //checks if userializer presses any key to pause or save
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.S)
                    {
                        Console.Clear();
                        SaveGame();
                        Console.SetCursorPosition(13, 5);
                        Console.WriteLine("The game is saved");
                        Console.WriteLine("         Press R to return to menu");

                        if (Console.ReadKey(true).Key == ConsoleKey.R)
                        {
                            //gameUI.GameisSaved();// doesn't save properly!!! how to fix?
                            Console.Clear();
                            StartGame();
                        }
                    }

                    else
                    {
                        if (Console.ReadKey(true).Key == ConsoleKey.S)
                        {
                            SaveGame();
                            Console.Clear();
                            Console.SetCursorPosition(13, 5);
                            Console.WriteLine("The game is saved");
                            Console.WriteLine("         Press R to return to menu");

                            if (Console.ReadKey(true).Key == ConsoleKey.R)
                            {
                                Console.Clear();
                                StartGame();
                            }
                        }
                    }
                }
                gameLogic.Iteration++;
                gameLogic.NewGenerationeration();
        }
        private void SaveGame()
        {
            GameProgress gameProgress = new GameProgress
            {
                GenerationArray = gameLogic.Generation,
                LiveCells = gameLogic.LiveCells,
                BoardSize = gameLogic.BoardSize,
                Iteration = gameLogic.Iteration,
                Columns = gameLogic.Columns,
                Rows = gameLogic.Rows
            };
            string filePath = @"C:\Users\r.logina\Documents\gameprogress.save";
            serializer.Serialize(gameProgress, filePath);
        }

        /// <summary>
        /// Fills variables with data from file
        /// </summary>
        private void LoadGame()
        { 
            string filePath = @"C:\Users\r.logina\Documents\gameprogress.save";
            GameProgress gameProgress = serializer.Deserialize(filePath);

            gameLogic.Columns = gameProgress.Columns;
            gameLogic.Rows = gameProgress.Rows;
            gameLogic.BoardSize = gameProgress.BoardSize;
            gameLogic.Iteration = gameProgress.Iteration;
            gameLogic.LiveCells = gameProgress.LiveCells;
            gameLogic.Generation = gameProgress.GenerationArray;
        }
    }
}
