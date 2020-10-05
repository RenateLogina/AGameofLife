using System;
using System.Collections.Generic;
using System.Text;

namespace GameofLife
{
    class GameLogic
    {
        public int CellCount;
        Random random = new Random();
        public void GameStart(int diff)
        {
            CellCount = 10;
            Console.SetCursorPosition(15, 15);
            string[] Seed = new string[25];
            int SeedXdim = random.Next(16, 20);
            int SeedYdim = random.Next(16, 20);
            Console.WriteLine("o");

            Console.SetCursorPosition((50 * diff) / 2 - 15, 10);
            Console.WriteLine("The first seed to appear here");
            Console.ReadKey();

        }
    }
}
