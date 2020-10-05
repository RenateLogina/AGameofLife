using System;
using System.Collections.Generic;
using System.Text;

namespace GameofLife
{
    class Game
    {
        public string UserAction = "";
        GameLogic logic = new GameLogic();
        public int Diff;

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
            string UserAction = Console.ReadLine().ToLower();
            switch (UserAction)
            {
                case "1":
                case "2":
                case "3":
                    Console.Clear();
                    Diff = Convert.ToInt32(UserAction);
                    GameBoard();
                    logic.GameStart(Diff);
                    break;
                default:
                    Console.Clear();
                    break;
                case "q":
                    return;
            }
        }
        public void GameBoard()
        {
            Console.CursorVisible = false;
            
            Console.WriteLine("\n  A new game has started");
            Console.WriteLine("  Difficulty is {0}", Diff);
            //change console size?
            int dimx = 50 * Diff;
            int dimy = 20 + Diff*Diff;

            Console.SetWindowSize(dimx,dimy) ;
            //draw game field
            for (int i = 5; i < 20 + Diff * Diff - 2; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("\u2588");
                Console.SetCursorPosition(50 * Diff-2, i);
                Console.Write("\u2588");
            }
            for (int i = 1; i < 50 * Diff-1; i++)
            {                
                Console.SetCursorPosition(i, 5);
                Console.Write("\u2584");
                Console.SetCursorPosition(i, 20 + Diff * Diff-2);
                Console.Write("\u2580");
            }
                                   
        }
        
    }
}
