namespace GameofLife
{
    using System;
    using System.Timers;

    /// <summary>
    /// Manages all functionality. Connecting point between GameLogic and GameUI.
    /// </summary>
    public class GameManager
    {
        public Timer myTimer;
        private string userAction;
        private GameLogic gameLogic = new GameLogic();
        private GameUI gameUI = new GameUI();
        private Serializer serializer = new Serializer();

        /// <summary>
        /// Starts game menu, gathers user input performs actions according to user input.
        /// </summary>
        public void StartGame()
        {
            userAction = gameUI.GameMenu();
            switch (userAction)
            {
                case "1":
                case "2":
                case "3":
                    SetFirstIteration();
                    SetGameBoard();
                    break;

                case "l":
                    LoadGame();
                    SetGameBoard();
                    break;             
            }
        }

        /// <summary>
        /// Toggles timer and saves according to user input.
        /// </summary>
        private void Toggler()
        {
            while (myTimer.Enabled)
            {
                switch (gameUI.ToggleInput())
                {
                    case "s":
                        SaveGame(); 
                        break;

                    case "p":
                        PauseGame();
                        break;

                    default:
                        continue;
                }
            }
        }

        /// <summary>
        /// Sets the first randomly filled Generation array according to user input. 
        /// </summary>
        private void SetFirstIteration()
        {
            gameLogic.BoardSize = Convert.ToInt32(userAction);
            gameLogic.SetSeed(gameLogic.BoardSize);
        }

        /// <summary>
        /// Prints user commands, starts the game loop and listens for further player input.
        /// </summary>
        private void SetGameBoard()
        {
            gameUI.GameHeader();
            SetTimer();
            Toggler();
        }

        /// <summary>
        /// Disables Timer, listens for user action to resume timer.
        /// </summary>
        private void PauseGame()
        {
            myTimer.Enabled = false;
            string input = gameUI.ToggleInput();
            if (input == "p")
            {
                myTimer.Enabled = true;
            }
            else if (input == "s")
            {
                SaveGame();
            }
        }

        /// <summary>
        /// Sets timer to 1 sec.
        /// </summary>
        private void SetTimer()
        {
            myTimer = new System.Timers.Timer(1000);
            myTimer.Elapsed += GameLoop;
            myTimer.AutoReset = true;
            myTimer.Enabled = true;
        }

        /// <summary>
        /// Loops game logic.
        /// </summary>
        /// <param name="source"> I honsestly don't know what this is. </param>
        /// <param name="e"> Defines each time elapsed? </param>
        public void GameLoop(Object source, ElapsedEventArgs e)
        {
            gameUI.Cycle(gameLogic.PrintArray());
            gameLogic.Iteration++;
            gameLogic.NewGeneration();
        }

        /// <summary>
        /// Stops timer, calls serializer and saves the current state of the game,
        /// listens for user input to go back to start menu.
        /// </summary>
        public void SaveGame()
        {
            myTimer.Enabled = false;
            
            GameProgress gameProgress = new GameProgress
            {
                GenerationArray = gameLogic.Generation,
                LiveCells = gameLogic.LiveCells,
                BoardSize = gameLogic.BoardSize,
                Iteration = gameLogic.Iteration,
                Columns = gameLogic.Columns,
                Rows = gameLogic.Rows
            };

            serializer.Serialize(gameProgress);

            if (gameUI.GameIsSaved() == "r")
            {
                StartGame();
            }
        }

        /// <summary>
        /// Fills variables with data from file.
        /// </summary>
        private void LoadGame()
        { 
            GameProgress gameProgress = serializer.Deserialize();

            gameLogic.Columns = gameProgress.Columns;
            gameLogic.Rows = gameProgress.Rows;
            gameLogic.BoardSize = gameProgress.BoardSize;
            gameLogic.Iteration = gameProgress.Iteration;
            gameLogic.LiveCells = gameProgress.LiveCells;
            gameLogic.Generation = gameProgress.GenerationArray;
        }

    }
}
