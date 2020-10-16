namespace GameofLife
{
    using System;
    using System.Text;

    /// <summary>
    /// Prints console commands and collects user input.
    /// </summary>
    public class GameUI
    {
        GameLogic gameLogic = new GameLogic();
        
        /// <summary>
        /// Prints main game menu.
        /// </summary>
        public string GameMenu()
        {
            Console.Clear();
            Console.SetWindowSize(155, 34);
            Console.WriteLine("\n\n\n\n\n                                                    Welcome to\n" +
                              "                                                        the\n" +
                              "                                                   GAME OF LIFE\n" +
                              "\n                                            Enter L to load saved game\n" +
                              "\n\n                               Choose the prefferred board BoardSize by entering a number!\n" +
                              "\n                                                      1. Small" +
                              "\n                                                      2. Medium" +
                              "\n                                                      3. Large" +
                              "\n\n\n                                                  ENTER Q TO QUIT");

            Console.SetCursorPosition(59, 22);
            string UserAction = Console.ReadLine().ToLower();
            return UserAction;
        }

        /// <summary>
        /// Describes main menu actions available during game.
        /// </summary>
        public void GameHeader()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("  Press P to pause and resume game");
            Console.WriteLine("  Press S to save game");
            Console.WriteLine("  Press R to return to main menu");
        }

        /// <summary>
        /// Prints Generation Array.
        /// </summary>
        /// <param name="generationArray"> This represents the generation array from GameManager. </param>
        public void Cycle(string generationArray)
        {
            Console.SetCursorPosition(0, 3);
            Console.Write(generationArray);
        }

        /// <summary>
        /// Listens for user input during the game.
        /// </summary>
        /// <returns> Returns user input value. </returns>
        public string ToggleInput()
        {
            string input = Console.ReadKey(true).Key.ToString().ToLower();

            return input;
        }

        /// <summary>
        /// Informs user that the game is saved. Informs of a way to return to main menu.
        /// </summary>
        /// <returns> Reads further user input. </returns>
        public string GameIsSaved()
        {
            //call save game method
            Console.Clear();
            Console.WriteLine("The game is saved");
            Console.WriteLine("Press R to return to main menu");
            string input = Console.ReadKey(true).Key.ToString().ToLower();
            
            return input;
        }

        /// <summary>
        /// Prints the Generation array and boarders using StringBuilder.
        /// </summary>
        /// <returns> Appended string of current game iteration. </returns>
        public string PrintArray(GameProgress gameProgress)
        {
            #region character symbols used
            var boarderTop = "\u2584";
            var boarderLeft = " \u2588";
            var dot = "\u25CF";
            var boarderRight = "\u2588";
            var boarderBottom = "\u2580";
            #endregion
            var sb = new StringBuilder(string.Empty);
            sb.AppendLine();
            sb.AppendFormat("  Board size: {0}", gameProgress.BoardSize);
            sb.AppendLine();
            sb.AppendFormat("  Live cells: {0}    ", gameProgress.LiveCells);
            sb.AppendLine();
            sb.AppendFormat("  Iteration NR: {0}    ", gameProgress.Iteration);
            sb.AppendLine();
            sb.Append(" ");

            for (int width = 1; width < 50 * gameProgress.BoardSize; width++)
            {
                sb.Append(boarderTop);
            }

            sb.AppendLine();

            for (var rowIndex = 0; rowIndex < gameProgress.Rows; rowIndex++)
            {
                sb.Append(boarderLeft);
                for (var colIndex = 0; colIndex < gameProgress.Columns; colIndex++)
                {
                    if (gameProgress.Generation[rowIndex, colIndex])
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

            for (int width = 1; width < 50 * gameProgress.BoardSize; width++)
            {
                sb.Append(boarderBottom);
            }

            var result = sb.ToString();

            return result;
        }

    }
}
