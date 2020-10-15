using System;
using System.Collections.Generic;
using System.Text;

namespace GameofLife
{
    class GameUI
    {
        GameLogic gL = new GameLogic();
        public string UserAction;

        /// <summary>
        /// Prints main game menu
        /// </summary>
        public string GameMenu()
        {
            
            Console.SetWindowSize(155, 30);
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
            UserAction = Console.ReadLine().ToLower();
            return UserAction;
        }
        public void GameHeader(int boardSize)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("  Press P to pause and resume game");
            Console.WriteLine("  Press S to save game");
            //Console.WriteLine("\n  Live cells:");
            Console.WriteLine("  BoardSize is {0}", boardSize);
        }
        /// <summary>
        /// supposed to save the game. Doesn't save properly for some reason. :/
        /// </summary>
        public void GameisSaved()
        {
            GameManager gameManager = new GameManager();
            Console.Clear();
            Console.SetCursorPosition(13, 5);
            Console.WriteLine("The game is saved");
            Console.WriteLine("         Press R to return to menu");
            gameManager.SaveGame();
            if (Console.ReadKey(true).Key == ConsoleKey.R)
            {
                Console.Clear();
                gameManager.StartGame();
            }
        }
    }
}
