namespace GameofLife
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Prints console commands and collects user input.
    /// </summary>
    public class GameUI
    {
        public List<int> gamesLoaded = new List<int>();

        /// <summary>
        /// Prints main game menu. 
        /// </summary>
        public void GameMenu()
        {
            Console.Clear();
            Console.SetWindowSize(155, 34);
            Console.WriteLine("\n\n\n\n\n                                                    Welcome to\n" +
                              "                                                        the\n" +
                              "                                                   GAME OF LIFE\n" +
                              "\n                                            Enter L to load saved game\n" +
                              "\n\n                               Choose the preferred board BoardSize by entering a number!\n" +
                              "\n                                                      1. Small" +
                              "\n                                                      2. Medium" +
                              "\n                                                      3. Large" +
                              "\n\n\n                                                  ENTER Q TO QUIT");

            Console.SetCursorPosition(59, 22);
        }

        /// <summary>
        /// Simple console clear in case necessary.
        /// </summary>
        public void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// Prints Generation Array.
        /// </summary>
        /// <param name="generationArray"> This represents the generation array from GameManager. </param>
        public void Cycle(string generationArray)
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            Console.WriteLine("  Press P to pause and resume game");
            Console.WriteLine("  Press S to save game");
            Console.WriteLine("  Press R to return to main menu");
            Console.Write(generationArray);
            Console.WriteLine();
        }

        /// <summary>
        /// Listens for user input during the game.
        /// </summary>
        /// <returns> Returns user input value. </returns>
        public ConsoleKey ToggleInput()
        {
            ConsoleKey input = Console.ReadKey(true).Key;
            return input;
        }

        /// <summary>
        /// Informs user that the game is saved. Informs of a way to return to main menu.
        /// </summary>
        /// <returns> Reads further user input. </returns>
        public void GameIsSaved()
        {
            //call save game method
            Console.Clear();
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("                The game is saved");
            Console.WriteLine("       Press any key to return to main menu");
            Console.CursorVisible = false;
            Console.ReadKey(true);
        }

        /// <summary>
        /// Prints the Generation array of whole list and boarders using StringBuilder.
        /// </summary>
        /// <returns> Appended string of current game iteration. </returns>
        public string PrintList(GameList gameList)
        {
            #region character symbols used
            var boarderTop = "\u2584";
            var boarderLeft = " \u2588";
            var dot = "\u25CF";
            var boarderRight = "\u2588";
            var boarderBottom = "\u2580";
            #endregion

            var sb = new StringBuilder(string.Empty);
            
            for( int index = 0; index < gamesLoaded.Count; index++)
            {
                for( int progress = 0; progress < gameList.Progress.Count; progress++)
                {
                    if (gamesLoaded[index] == progress + 1)
                    {
                        #region stringBuilder

                        sb.AppendLine();
                        sb.AppendFormat("  Game ID: {0}    ", gameList.Progress[progress].ID);
                        sb.AppendLine();
                        sb.AppendFormat("  Board size: {0}", gameList.Progress[progress].BoardSize);
                        sb.AppendLine();
                        sb.AppendFormat("  Live cells: {0}    ", gameList.Progress[progress].LiveCells);
                        sb.AppendLine();
                        sb.AppendFormat("  Iteration NR: {0}    ", gameList.Progress[progress].Iteration);
                        sb.AppendLine();
                        sb.Append(" ");

                        for (int width = 1; width < 50 * gameList.Progress[progress].BoardSize; width++)
                        {
                            sb.Append(boarderTop);
                        }

                        sb.AppendLine();

                        for (var rowIndex = 0; rowIndex < gameList.Progress[progress].Rows; rowIndex++)
                        {
                            sb.Append(boarderLeft);
                            for (var colIndex = 0; colIndex < gameList.Progress[progress].Columns; colIndex++)
                            {
                                if (gameList.Progress[progress].Generation[rowIndex, colIndex])
                                {
                                    sb.Append(dot);
                                }
                                else
                                {
                                    sb.Append(" ");
                                }
                            }

                            sb.Append(boarderRight);
                            sb.AppendLine();
                        }

                        sb.Append(" ");

                        for (int width = 1; width < 50 * gameList.Progress[progress].BoardSize; width++)
                        {
                            sb.Append(boarderBottom);
                        }
                        #endregion
                    }
                }
            }

            var result = sb.ToString();

            return result;
        }

        /// <summary>
        /// Prints the Generation array of a new game and boarders using StringBuilder.
        /// </summary>
        /// <returns> Appended string of current game iteration. </returns>
        public string PrintGame(GameList gameList)
        {
            #region character symbols used
            var boarderTop = "\u2584";
            var boarderLeft = " \u2588";
            var dot = "\u25CF";
            var boarderRight = "\u2588";
            var boarderBottom = "\u2580";
            #endregion

            var sb = new StringBuilder(string.Empty);

            var newGame = gameList.Progress.Count -1;

            #region stringBuilder

            sb.AppendLine();
            sb.AppendFormat("  Game ID: {0}    ", gameList.Progress[newGame].ID);
            sb.AppendLine();
            sb.AppendFormat("  Board size: {0}", gameList.Progress[newGame].BoardSize);
            sb.AppendLine();
            sb.AppendFormat("  Live cells: {0}    ", gameList.Progress[newGame].LiveCells);
            sb.AppendLine();
            sb.AppendFormat("  Iteration NR: {0}    ", gameList.Progress[newGame].Iteration);
            sb.AppendLine();
            sb.Append(" ");

            for (int width = 1; width < 50 * gameList.Progress[newGame].BoardSize; width++)
            {
                sb.Append(boarderTop);
            }

            sb.AppendLine();

            for (var rowIndex = 0; rowIndex < gameList.Progress[newGame].Rows; rowIndex++)
            {
                sb.Append(boarderLeft);
                for (var colIndex = 0; colIndex < gameList.Progress[newGame].Columns; colIndex++)
                {
                    if (gameList.Progress[newGame].Generation[rowIndex, colIndex])
                    {
                        sb.Append(dot);
                    }
                    else
                    {
                        sb.Append(" ");
                    }
                }

                sb.Append(boarderRight);
                sb.AppendLine();
            }

            sb.Append(" ");

            for (int width = 1; width < 50 * gameList.Progress[newGame].BoardSize; width++)
            {
                sb.Append(boarderBottom);
            }
            #endregion

            var result = sb.ToString();

            return result;
        }

        /// <summary>
        /// Reads user input
        /// </summary>
        /// <returns> User input </returns>
        public string UserAction()
        {
            string UserAction = Console.ReadLine().ToLower();
            return UserAction;
        }

        /// <summary>
        /// Shows menu where user can choose up to 8 games to load from file.
        /// </summary>
        /// <param name="gameList"> List with game ID's </param>
        public void ChooseGame(GameList gameList)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 3);

            Console.WriteLine("                            GAME LOADER \n\n" +
                              "                 Enter R to return to main menu \n\n" +
                              "               Currently there are {0} games saved\n" +
                              "               You can load and view up to 8 games\n" +
                            "\n\n         Enter the ID numer of game You want to load!", gameList.Progress.Count
                );
            Console.Write("              Enter L to load games You've chosen: ");
            foreach (int gameId in gamesLoaded)
            {
                Console.Write("{0}, ", gameId);
            }
            Console.SetCursorPosition(32, 14);
        }
        
        /// <summary>
        /// Message to user in case no file to load exists.
        /// </summary>
        public void NoGameExists()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("          No game has been saved so far!\n" +
                              "             Press any key to return!");
            Console.CursorVisible = false;
            Console.ReadKey(true);            
        }
    }
}
