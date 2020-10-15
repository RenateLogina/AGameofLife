namespace GameofLife
{
    using System;
    using System.Timers;

    public class GameManager
    {
        public static Timer myTimer;
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
                    Toggler();
                    break;

                case "l":
                    LoadGame();
                    gameUI.GameHeader();
                    SetTimer();
                    Toggler();
                    break;

                default:
                    break;

                case "q":
                    return;
            }
        }
        /// <summary>
        /// Toggles timer and saves according to user input
        /// </summary>
        private void Toggler()
        {
            while (myTimer.Enabled)
            {
                switch (gameUI.Toggle())
                {
                    case "s":
                        myTimer.Enabled = false;
                        SaveGame();
                        if(gameUI.GameisSaved() == "r")
                        {
                            gameUI.Clear();
                            StartGame();
                        }
                        break;
                    case "p":
                        myTimer.Enabled = false;
                        if(gameUI.Toggle() == "p")
                        {
                            myTimer.Enabled = true;
                        }
                        if (gameUI.Toggle() == "s")
                        {
                            SaveGame();
                            if (gameUI.GameisSaved() == "r")
                            {
                                gameUI.Clear();
                                StartGame();
                            }
                        }
                        break;
                    case "r":
                        myTimer.Enabled = true;
                        break;
                }
            }
        }
        /// <summary>
        /// Sets timer to 1 sec
        /// </summary>
        private void SetTimer()
        {
            myTimer = new System.Timers.Timer(1000);
            myTimer.Elapsed += GameLoop;
            myTimer.AutoReset = true;
            myTimer.Enabled = true;
        }

        /// <summary>
        /// loops game logic
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
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
