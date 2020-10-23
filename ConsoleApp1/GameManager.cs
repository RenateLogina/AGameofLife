namespace GameofLife
{
    using System;
    using System.Linq;
    using System.Timers;

    /// <summary>
    /// Manages all functionality. Connecting point between GameLogic and GameUI.
    /// </summary>
    public class GameManager
    {
        // Timer
        public Timer MyTimer;
        private bool maxGame;        
        private string userAction;
        private GameLogic gameLogic = new GameLogic();
        private GameUI gameUI = new GameUI();
        private Serializer serializer = new Serializer();
        public GameManager()
        {

        }

        /// <summary>
        /// Starts game menu, gathers user input performs actions according to user input.
        /// </summary>
        public void StartGame()
        {            
            gameUI.GameMenu();
            userAction = gameUI.UserAction();

            switch (userAction)
            {
                case "1":
                case "2":
                case "3":
                    SetFirstIteration();

                    if(maxGame)
                    {
                        StartGame();
                    }
                    else
                    {
                        SetGameBoard();
                    }

                    break;

                case "l":
                    ReadGame();
                    LoadParticularGame();
                    SetListBoard();

                    break;

                case "q":
                    return;
            }
        }

        /// <summary>
        /// Toggles timer and saves according to user input.
        /// </summary>
        private void Toggler()
        {
            while (MyTimer.Enabled)
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
                        MyTimer.Enabled = false;
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
            maxGame = false;

            if (gameLogic.gameList.Progress.Count < 1000)
            {
                gameLogic.gameProgress.BoardSize = Convert.ToInt32(userAction);
                gameLogic.SetSeed(gameLogic.gameProgress.BoardSize);
            }
            else
            {
                gameUI.MaxGameList();

                maxGame = true;
            }
        }

        /// <summary>
        /// Prints user commands, starts the game loop of the New, single game and listens for further player input.
        /// </summary>
        private void SetGameBoard()
        {
            gameUI.Clear();
            SetGameTimer();
            Toggler();
        }

        /// <summary>
        /// Prints user commands, starts the game loop and listens for further player input.
        /// </summary>
        private void SetListBoard()
        {
            gameUI.Clear();
            SetListTimer();
            Toggler();
        }

        /// <summary>
        /// Sets timer to 1 sec. Loops new game
        /// </summary>
        private void SetGameTimer()
        {
            MyTimer = new System.Timers.Timer(1000);
            MyTimer.Elapsed += NewGameLoop;
            MyTimer.AutoReset = true;
            MyTimer.Enabled = true;
        }

        /// <summary>
        /// Sets timer to 1 sec. Loops List
        /// </summary>
        private void SetListTimer()
        {
            MyTimer = new System.Timers.Timer(1000);
            MyTimer.Elapsed += ListLoop;
            MyTimer.AutoReset = true;
            MyTimer.Enabled = true;
        }

        /// <summary>
        /// Loops a single new game.
        /// </summary>
        public void NewGameLoop(Object source, ElapsedEventArgs e)
        {
            // Prints selected items from list.
            gameUI.PrintGame(gameLogic.gameList);

            // Updates last game in the list.
            var gameIndex = gameLogic.gameList.Progress.Count() - 1;
            gameLogic.NewGeneration(gameLogic.gameList.Progress[gameIndex]);
        }

        /// <summary>
        /// Loops a list of games.
        /// </summary>
        public void ListLoop(Object source, ElapsedEventArgs e)
        {
            gameUI.PrintList(gameLogic.gameList);
            gameLogic.gameList.GamesAlive = 0;
            gameLogic.gameList.CellsAlive = 0;
       
            // Updates All games in the list.
            foreach (GameProgress game in gameLogic.gameList.Progress)
            {
                gameLogic.NewGeneration(game);

                if (game.IsGameAlive == true)
                {
                    gameLogic.gameList.GamesAlive++;
                }
            }
        }

        /// <summary>
        /// Disables Timer, listens for user action to resume timer.
        /// </summary>
        private void PauseGame()
        {
            MyTimer.Enabled = false;
            string input = gameUI.ToggleInput().ToString().ToLower();

            if (input == "p")
            {
                MyTimer.Enabled = true;
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
        /// Stops timer, calls serializer and saves the current state of the game,
        /// listens for user input to go back to start menu.
        /// </summary>
        public void SaveGame()
        {
            MyTimer.Enabled = false;    
            
            serializer.Serialize(gameLogic.gameList);
            gameUI.GameIsSaved();
            
            StartGame();            
        }

        /// <summary>
        /// Loads game file. Checks if it exists.
        /// </summary>
        private void ReadGame()
        {
            gameLogic.gameList = serializer.Deserialize();
            if (gameLogic.gameList == null)
            {
                gameUI.NoGameExists();
                StartGame();
            }
        }
        
        /// <summary>
        /// User may choose which games to load to console from file.
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
