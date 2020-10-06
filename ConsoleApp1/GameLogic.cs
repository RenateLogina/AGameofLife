using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GameofLife
{
    class GameLogic
    {
        public int CellCount;
        Random random = new Random();
        //pointerposition
        public int xPos;
        public int yPos;
        public bool[,] Seed;
        public void SetSeed()
        {
            CellCount = 10;
            //set a 2D array size for the seed
            Seed = new bool[6,6];
            //fills seed with "true" until there are 15 seeds in 6x6 array
            for(int ColIndex = 0; ColIndex < 6; ColIndex ++)
            {
                for (int RowIndex = 0; RowIndex < 6; RowIndex++)
                {
                    int SeedCount = 0;
                    while(SeedCount < 15)
                    {
                        int randomColIndex = random.Next(ColIndex);
                        int randomRowIndex = random.Next(RowIndex);
                        // fills in the empty positions
                        if (Seed[randomColIndex, randomRowIndex] == false)
                        {
                            Seed[randomColIndex, randomRowIndex] = true;
                            SeedCount++;
                        }
                    }
                }
            }
            //How do I return the seed generated?
            //Turn this into a value... huh?
            foreach (bool value in Seed)
            {
                if (value == true)
                {
                    Console.Write(value);
                }
                else
                {
                    Console.Write("X");
                }
            }
            return;
            
            Console.ReadKey();

        }
    }
}
