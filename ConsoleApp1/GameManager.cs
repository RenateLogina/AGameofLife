namespace GameofLife
{
    using System;
    using System.Timers;

    public class GameManager
    {
        public static System.Timers.Timer myTimer;
        GameLogic gameLogic = new GameLogic();
        GameUI gameUI = new GameUI();
        Serializer serializer = new Serializer();

        public void StartGame()
        {
            string userAction = gameUI.GameMenu();

            switch (userAction)
            {
                case "1":
                case "2":
                case "3":
                    gameLogic.BoardSize = Convert.ToInt32(userAction);
                    gameLogic.SetSeed(gameLogic.BoardSize);
                    gameUI.GameHeader();
                    SetTimer();  
                    while(myTimer.Enabled)
                    {
                        gameUI.Toggle(myTimer);
                        if (gameUI.Toggle(myTimer) == "pause")
                        {
                            myTimer.Enabled = false;
                            Console.Read();
                        }
                        if (gameUI.Toggle(myTimer) == "save")
                        {
                            myTimer.Enabled = false;
                            SaveGame();
                            Console.Read();
                        }
                    }

                    

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

        private void SetTimer()
        {
            myTimer = new System.Timers.Timer(1000);
            myTimer.Elapsed += GameLoop;
            myTimer.AutoReset = true;
            myTimer.Enabled = true;
        }

        public void GameLoop(Object source, ElapsedEventArgs e)
        {
            gameUI.Cycle(gameLogic.PrintArray());
            gameLogic.Iteration++;
            gameLogic.NewGenerationeration();
        }

        //private void ToggleTimer()
        //{
        //    myTimer.Enabled = false;
        //}
        public void SaveGame()
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
