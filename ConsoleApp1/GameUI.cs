using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameofLife
{
    class GameUI
    {
        /// <summary>
        /// Prints main game menu.
        /// </summary>
        public string GameMenu()
        {
            Console.Clear();
            Console.SetWindowSize(155, 32);
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
        public string GameisSaved()
        {
            //call save game method
            Console.Clear();
            Console.WriteLine("The game is saved");
            Console.WriteLine("Press R to return to main menu");
            string input = Console.ReadKey(true).Key.ToString().ToLower();
            
            return input;
        }

    }
}
