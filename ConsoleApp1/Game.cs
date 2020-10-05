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
            Console.SetWindowSize(120, 40);
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
        public void GameBoard(string userAction)
        {
            Console.CursorVisible = false;
            diff = Convert.ToInt32(userAction);
            Console.WriteLine("\n  A new game has started");
            Console.WriteLine("  Difficulty is {0}", diff);
            //change console size?
            int dimx = 50 * diff;
            int dimy = 20 + diff*diff;

            Console.SetWindowSize(dimx,dimy) ;
            //draw game field
            for (int i = 5; i < 20 + diff * diff - 2; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("\u2588");
                Console.SetCursorPosition(50 * diff-2, i);
                Console.Write("\u2588");
            }
            for (int i = 1; i < 50 * diff-1; i++)
            {                
                Console.SetCursorPosition(i, 5);
                Console.Write("\u2584");
                Console.SetCursorPosition(i, 20 + diff * diff-2);
                Console.Write("\u2580");
            }
                                   
        }
        public void GameLogic()
        {
            Console.SetCursorPosition((50 * diff)/2 - 15, 10);
            Console.WriteLine("The first seed to appear here");
            Console.ReadKey();

        }
    }
}
