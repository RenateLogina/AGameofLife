using System;
using System.Collections.Generic;
using System.Text;

namespace GameofLife
{
    class Game
    {
        //public string UserAction = "";
        public int diff;
        public void GameMenu()
        {
            Console.WriteLine("\n\n\n\n\n                                                    Welcome to\n" +
                              "                                                        the\n" +
                              "                                                   GAME OF LIFE\n"+
                              "\n\n                               Choose the prefferred board size by entering a number!\n" +
                              "\n                                                      1. Small" +
                              "\n                                                      2. Medium" +
                              "\n                                                      3. Large"+
                              "\n\n\n                                                  ENTER Q TO QUIT");

            Console.SetCursorPosition(59, 22);
        }
        public void NewGame(string userAction)
        {
            Console.CursorVisible = false;
            int diff = Convert.ToInt32(userAction);
            Console.WriteLine("\n\n\n\n\n\n\n\n                                                A new game has started");
            Console.WriteLine("                                                Difficulty is {0}", diff);
            Console.ReadKey();
        }
    }
}
