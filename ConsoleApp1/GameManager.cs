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
        public bool maxGame;        
        private string userAction;
        public GameLogic gameLogic = new GameLogic();
        //private GameUI gameUI = new GameUI();
        //private Serializer serializer = new Serializer();
        private readonly ISerializer _serializer;
        private readonly IGameUI _gameUI;
        public GameManager(ISerializer serializer, IGameUI gameUI)
        {
            _serializer = serializer;
            _gameUI = gameUI;
        }

        /// <summary>
        /// Starts game menu, gathers user input performs actions according to user input.
        /// </summary>
        public void StartGame()
        {            
            _gameUI.GameMenu();
            userAction = _gameUI.UserAction();

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
                switch (_gameUI.ToggleInput().ToString().ToLower())
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
        public void SetFirstIteration()
        {
            maxGame = false;

            if (gameLogic.gameList.Progress.Count < 1000)
            {
                gameLogic.gameProgress.BoardSize = Convert.ToInt32(userAction);
                gameLogic.SetSeed(gameLogic.gameProgress.BoardSize);
            }
            else
            {
                _gameUI.MaxGameList();

                maxGame = true;
            }
        }

        /// <summary>
        /// Prints user commands, starts the game loop of the New, single game and listens for further player input.
        /// </summary>
        private void SetGameBoard()
        {
            _gameUI.Clear();
            SetGameTimer();
            Toggler();
        }

        /// <summary>
        /// Prints user commands, starts the game loop and listens for further player input.
        /// </summary>
        private void SetListBoard()
        {
            _gameUI.Clear();
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
            _gameUI.PrintGame(gameLogic.gameList);

            // Updates last game in the list.
            var gameIndex = gameLogic.gameList.Progress.Count() - 1;
            gameLogic.NewGeneration(gameLogic.gameList.Progress[gameIndex]);
        }

        /// <summary>
        /// Loops a list of games.
        /// </summary>
        public void ListLoop(Object source, ElapsedEventArgs e)
        {
            _gameUI.PrintList(gameLogic.gameList, gameLogic.gamesLoaded);
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
            string input = _gameUI.ToggleInput().ToString().ToLower();

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
            
            _serializer.Serialize(gameLogic.gameList);
            _gameUI.GameIsSaved();
            StartGame();            
        }

        /// <summary>
        /// Loads game file. Checks if it exists.
        /// </summary>
        private void ReadGame()
        {
            gameLogic.gameList = _serializer.Deserialize();
            if (gameLogic.gameList == null)
            {
                _gameUI.NoGameExists();
                StartGame();
            }
        }
        
        /// <summary>
        /// User may choose which games to load to console from file.
        /// </summary>
        private void LoadParticularGame()
        {
            // Clears list in case You return to main menu and choose other games
            gameLogic.gamesLoaded.Clear();
            while(true)
            {
                _gameUI.ChooseGame(gameLogic.gameList, gameLogic.gamesLoaded);
                string action = _gameUI.UserAction();
                bool isActionNUmeric = int.TryParse(action, out int gameId);

                if (gameLogic.gamesLoaded.Count > 8 || ((action == "l") && (gameLogic.gamesLoaded.Count > 0)))
                {
                    break;
                }
                else if ((gameId >= 1) && (gameId <= gameLogic.gameList.Progress.Count))
                {
                    gameLogic.gamesLoaded.Add(gameId);
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
