
namespace GameofLife
{
    using System;
    using System.Threading;

    public class GameLogic
    {
        public int MyProperty { get; set; }
        public int neighbours;
        //public int CellCount;
        Random random = new Random();
        //pointerposition
        public int xPos;
        public int yPos;
        public int SizeX;
        public int SizeY;
        public int LiveCells;
        //stores array values
        static string[,] Generation;
        //Sets the very first iteration of the game, randomly
        public void SetSeed(int boardSize)
        {
            //set a 2D array size, based on board size
            SizeX = 50 * boardSize - 3;
            SizeY = 14 + boardSize * boardSize;
            Generation = new string[SizeY, SizeX];

            //populates the first generation, randomly
            string[] Filler = new string[] { "\u25CF", " " };
            for (int RowIndex = 0; RowIndex < SizeY; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < SizeX; ColIndex++)
                {
                    int CharacterIndex = random.Next(Filler.Length);
                    Generation[RowIndex, ColIndex] = Filler[CharacterIndex];
                }
            }
        }
        public void PrintArray()
        {
            //prints the array
            xPos = 2;
            yPos = 6;
            for (int RowIndex = 0; RowIndex < SizeY; RowIndex++)//how tall
            {
                Console.SetCursorPosition(xPos, yPos);
                for (int ColIndex = 0; ColIndex < SizeX; ColIndex++)//how wide
                {
                    //prints a single character
                    Console.Write(Generation[RowIndex, ColIndex]);
                }
                yPos++;
            }
            CellCounter();
            Console.SetCursorPosition(13, 1);
            Console.Write(LiveCells);
        }
        public void NewGeneration()//neadekvati dzemdee ssuunas
        {
            string[,] NewGen = new string[SizeY, SizeX];
            for (int RowIndex = 0; RowIndex < SizeY; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < SizeX; ColIndex++)
                {
                    NeigbourCounter(RowIndex, ColIndex);
                    Console.SetCursorPosition(1, 28);
                    //test if cellcount is correct and voila, it is
                    //Console.WriteLine("cell nr {1},{2} has {0} neighbours", neighbours, RowIndex, ColIndex);
                    //Thread.Sleep(1000);

                    //checks if cell is alive
                    if (Generation[RowIndex, ColIndex] == "\u25CF")
                    {
                        //if has less than 2 or more than 3 live neighbours, it dies
                        if (neighbours < 2 || neighbours > 3)
                        {
                            NewGen[RowIndex, ColIndex] = " ";
                        }
                        //else it lives on
                        else
                        {
                            NewGen[RowIndex, ColIndex] = "\u25CF";
                        }
                    }
                    //if the cell is dead
                    else
                    {
                        if (neighbours == 3)
                        {
                            NewGen[RowIndex, ColIndex] = "\u25CF";
                        }
                        //else it remains dead
                        else
                        {
                            NewGen[RowIndex, ColIndex] = " ";
                        }
                    }
                }
            }
            Generation = NewGen;
        }
        public void NeigbourCounter(int RowIndex, int ColIndex)
        {
            neighbours = 0;
            if (RowIndex > 0 && ColIndex > 0 && Generation[RowIndex - 1, ColIndex - 1] == "\u25CF")
            {
                neighbours++;
            }
            
            if (RowIndex > 0 && Generation[RowIndex - 1, ColIndex] == "\u25CF")
            {
                neighbours++;
            }
            if (RowIndex > 0 && ColIndex < SizeX - 1 && Generation[RowIndex - 1, ColIndex + 1] == "\u25CF")
            {
                neighbours++;
            }
            if (ColIndex > 0 && Generation[RowIndex, ColIndex - 1] == "\u25CF")
            {
                neighbours++;
            }
            if (ColIndex < SizeX - 1 && Generation[RowIndex, ColIndex + 1] == "\u25CF")
            {
                neighbours++;
            }
            if (RowIndex < SizeY - 1 && ColIndex > 0 && Generation[RowIndex + 1, ColIndex - 1] == "\u25CF")
            {
                neighbours++;
            }
            if (RowIndex < SizeY - 1 && Generation[RowIndex + 1, ColIndex] == "\u25CF")
            {
                neighbours++;
            }
            if (RowIndex < SizeY - 1 && ColIndex < SizeX - 1 && Generation[RowIndex + 1, ColIndex + 1] == "\u25CF")
            {
                neighbours++;
            }
        }
        public void CellCounter()
        {
            LiveCells = 0;
            for (int RowIndex = 0; RowIndex < SizeY; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < SizeX; ColIndex++)
                {
                    if(Generation[RowIndex,ColIndex] == "\u25CF")
                    {
                        LiveCells++;
                    }
                }
            }

        }
    }
}
