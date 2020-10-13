using System;
using System.Collections.Generic;
using System.Text;

namespace GameofLife
{
    class GameUI
    {
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
                gameManager.GameMenu();
            }
        }
    }
}
