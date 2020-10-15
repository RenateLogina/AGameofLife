using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameofLife
{
    class GameUI
    {
        GameLogic gameLogic = new GameLogic();
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

        /// <summary>
        /// Describes main menu actions available during game
        /// </summary>
        public void GameHeader()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("  Press P to pause and resume game");
            Console.WriteLine("  Press S to save game");
        }

        public void Cycle(string array)
        {
            Console.SetCursorPosition(0, 3);
            Console.Write(array);
        }

        public string Toggle(System.Timers.Timer timer)
        {
            string result = "pause";
            ConsoleKey input= Console.ReadKey(true).Key;
            if (input == ConsoleKey.S)
                {
                timer.Enabled = false;
                GameisSaved();

                result = "save";

                return result;
            }
            else { 
                if(input == ConsoleKey.P)
                {
                    timer.Enabled = false;
                    ConsoleKey input2 = Console.ReadKey(true).Key;
                    if (input2 == ConsoleKey.S)
                    {
                        GameisSaved();
                        result = "save";
                        return result;
                    }
                    else
                    {
                        if (input2 == ConsoleKey.P)
                        {
                            timer.Enabled = true;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// supposed to save the game. Doesn't save properly for some reason. :/
        /// </summary>
        public void  GameisSaved()
        {
            //call save game method
            Console.Clear();
            Console.WriteLine("The game is saved");

            //ConsoleKey input3 = Console.ReadKey(true).Key;

            //if (input3 == ConsoleKey.R)
            //{
            //    Console.Clear();
            //    //return to start
            //}

        }
    }
}
