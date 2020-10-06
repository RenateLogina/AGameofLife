using System;
using System.Collections.Generic;
using System.Text;

namespace GameofLife
{
    class Game
    {
        //UserAction should store the string entered when navigating menu
        public string UserAction = "";
        GameLogic logic = new GameLogic();
        //BoardSize stores the level of BoardSizeiculty(board BoardSize) entered buy user
        public int BoardSize;

        public void GameMenu()
        {
            Console.SetWindowSize(150, 30);
            Console.WriteLine("\n\n\n\n\n                                                    Welcome to\n" +
                              "                                                        the\n" +
                              "                                                   GAME OF LIFE\n"+
                              "\n\n                               Choose the prefferred board BoardSize by entering a number!\n" +
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
                    BoardSize = Convert.ToInt32(UserAction);
                    GameBoard();
                    break;
                default:
                    Console.Clear();
                    break;
                case "q":
                    return;
            }
        }
        //changes the size of window, prints gameboard borders
        public void GameBoard()
        {
            Console.CursorVisible = false;
            
            Console.WriteLine("\n  A new game has started");
            Console.WriteLine("  BoardSize is {0}", BoardSize);
            //this sets the window size according to the user input
            //int dimx = 50 * BoardSize;
            //int dimy = 20 + BoardSize*BoardSize;
            //actually changes the window size
            //Console.SetWindowSize(dimx,dimy) ;
            //draws game board --- vertical lines
            for (int height = 5; height < 20 + BoardSize * BoardSize - 2; height++)
            {
                Console.SetCursorPosition(1, height);
                Console.Write("\u2588");
                Console.SetCursorPosition(50 * BoardSize-2, height);
                Console.Write("\u2588");
            }
            //draws game board --- horizontal lines
            for (int width = 1; width < 50 * BoardSize-1; width++)
            {                
                Console.SetCursorPosition(width, 5);
                Console.Write("\u2584");
                Console.SetCursorPosition(width, 20 + BoardSize * BoardSize-2);
                Console.Write("\u2580");
            }
            //I should enter code to print the first seed here. I want it to be able to grow from the center.
            Console.SetCursorPosition((50 * BoardSize) / 2 - 15, 10);
            Console.WriteLine("The first seed to appear here");
            //read and print logic.
            Console.ReadKey();

        }
        
    }
}
