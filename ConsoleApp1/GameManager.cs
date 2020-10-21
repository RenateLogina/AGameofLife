namespace GameofLife
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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
            if(gameLogic.gameList != null)
            {
                gameLogic.gameList.Progress.Clear();
            }
            
            gameUI.GameMenu();
            userAction = gameUI.UserAction();
            switch (userAction)
            {
                case "1":
                case "2":
                case "3":
                    SetFirstIteration();
                    SetGameBoard();
                    break;

                case "l":
                    ReadGame();
                    CheckFile();
                    LoadParticularGame();
                    SetListBoard();
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
                switch (gameUI.ToggleInput().ToString().ToLower())
                {
                    case "s":
                        SaveGame();
                        StartGame();
                        break;

                    case "p":
                        PauseGame();
                        break;

                    case "r":
                        myTimer.Enabled = false;
                        StartGame();
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
            gameLogic.gameProgress.BoardSize = Convert.ToInt32(userAction);
            gameLogic.SetSeed(gameLogic.gameProgress.BoardSize);
        }

        /// <summary>
        /// Prints user commands, starts the game loop and listens for further player input.
        /// </summary>
        private void SetGameBoard()
        {
            gameUI.Clear();
            SetTimer();
            Toggler();
        }

        private void SetListBoard()
        {
            gameUI.Clear();
            SetTimer2();
            Toggler();
        }

        /// <summary>
        /// Disables Timer, listens for user action to resume timer.
        /// </summary>
        private void PauseGame()
        {
            myTimer.Enabled = false;
            string input = gameUI.ToggleInput().ToString().ToLower();
            if (input == "p")
            {
                myTimer.Enabled = true;
            }
            else if (input == "s")
            {
                SaveGame();
            }
            else if (input == "r")
            {
                StartGame();
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
        private void SetTimer2()
        {
            myTimer = new System.Timers.Timer(1000);
            myTimer.Elapsed += ListLoop;
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
            gameUI.Cycle(gameUI.PrintArray(gameLogic.gameProgress));
            //gameLogic.gameProgress.Iteration++;
            gameLogic.NewGeneration();
        }

        public void ListLoop(Object source, ElapsedEventArgs e)
        {
            // Prints selected items from list.
            gameUI.Cycle(gameUI.PrintList(gameLogic.gameList));
            // Updates All games in the list.
            foreach(GameProgress game in gameLogic.gameList.Progress)
            {
                gameLogic.NewGenerationList(game);
            }
        }

        /// <summary>
        /// Stops timer, calls serializer and saves the current state of the game,
        /// listens for user input to go back to start menu.
        /// </summary>
        public void SaveGame()
        {
            myTimer.Enabled = false;
            // List of saved games from file.
            GameList temporaryList = serializer.Deserialize();

            // Create a new list if there is no file (list of saved games).
            if (temporaryList == null)
            {
                gameLogic.gameList = new GameList();
                gameLogic.gameList.Progress.Add(new GameProgress()
                {
                    Generation = gameLogic.gameProgress.Generation,
                    LiveCells = gameLogic.gameProgress.LiveCells,
                    BoardSize = gameLogic.gameProgress.BoardSize,
                    Iteration = gameLogic.gameProgress.Iteration,
                    Columns = gameLogic.gameProgress.Columns,
                    Rows = gameLogic.gameProgress.Rows,
                    ID = 1,
                });
            }

            // Overwrite the save file if it exists.
            else
            {
                // Add a game to file if it's new. Else it should overwrite all games.
                // Always reads game progress!!! If You have started a game in this session, it will be the last game.
                // If You started a new game, the gameProgress will be null and will save null parameters!!!
                if (gameLogic.gameProgress.ID == 0)
                {
                    gameLogic.gameList = temporaryList;
                    gameLogic.gameList.Progress.Add(new GameProgress()
                    {
                        Generation = gameLogic.gameProgress.Generation,
                        LiveCells = gameLogic.gameProgress.LiveCells,
                        BoardSize = gameLogic.gameProgress.BoardSize,
                        Iteration = gameLogic.gameProgress.Iteration,
                        Columns = gameLogic.gameProgress.Columns,
                        Rows = gameLogic.gameProgress.Rows,
                        ID = gameLogic.gameList.Progress.Count + 1,
                    });
                }
                // If loaded file has another ID but 0.
                else
                {
                    gameLogic.gameProgress = null;
                    //foreach(GameProgress game in gameLogic.gameList.Progress)
                    //{
                    //    if(game.ID == temporaryList.Progress.FirstOrDefault().ID)
                    //    {

                    //    }
                    //}
                }
            }




            serializer.Serialize(gameLogic.gameList);

            gameUI.GameIsSaved();
            
            StartGame();
            
        }
        private void ReadGame()
        {
            gameLogic.gameList = serializer.Deserialize();
        }
        private void CheckFile()
        {
            if (gameLogic.gameList == null)
            {
                gameUI.NoGameExists();
                StartGame();
            }
        }
        /// <summary>
        /// Fills variables with data from file. User may choose which games to load from list
        /// </summary>
        private void LoadParticularGame()
        {
           
            // Clears list in case You return to main menu and choose other games
            gameUI.gamesLoaded.Clear();
            while(true)
            {
                gameUI.ChooseGame(gameLogic.gameList);
                string action = gameUI.UserAction();
                bool isActionNUmeric = int.TryParse(action, out int gameId);


                if (gameUI.gamesLoaded.Count > 8 || ((action == "l") && (gameUI.gamesLoaded.Count > 0)))
                {
                    break;
                }
                else if ((gameId >= 1) && (gameId <= gameLogic.gameList.Progress.Count))
                {
                    gameUI.gamesLoaded.Add(gameId);
                }
                
                else if (action == "r")
                {
                    StartGame();
                    break;
                }
            }
        }
    }
}
