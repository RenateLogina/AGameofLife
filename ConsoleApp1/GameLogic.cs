
namespace GameofLife
{
    using System;
    using System.Reflection.PortableExecutable;
    using System.Threading;

    public class GameLogic
    {
        //public int MyProperty { get; set; }
        public int neighbours;
        //public int CellCount;
        Random random = new Random();
        //pointerposition when printing out the array
        public int xPos;
        public int yPos;
        //board size
        public int SizeX;
        public int SizeY;
        //amount of live cells at given iteration
        public int LiveCells;
        //stores and resets Generation array values
        static string[,] Generation;

        //Sets the very first iteration of the game, randomly
        public void SetSeed(int boardSize)
        {
            //set a 2D array size, based on board size
            SizeX = 50 * boardSize - 3;
            SizeY = 14 + boardSize * boardSize;
            Generation = new string[SizeY, SizeX];

            //populates the first generation with 2 characters, chosen randomly
            string[] RandomFiller = new string[] { "\u25CF", " " };
            for (int RowIndex = 0; RowIndex < SizeY; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < SizeX; ColIndex++)
                {
                    int CharacterIndex = random.Next(RandomFiller.Length);
                    Generation[RowIndex, ColIndex] = RandomFiller[CharacterIndex];
                }
            }
        }

        //prints the Generation array
        public void PrintArray()
        {
            //Sets starting point for printing, according to graphic location
            xPos = 2;
            yPos = 6;
            for (int RowIndex = 0; RowIndex < SizeY; RowIndex++)//prints row
            {
                Console.SetCursorPosition(xPos, yPos);
                for (int ColIndex = 0; ColIndex < SizeX; ColIndex++)//prints individual cells in row (width)
                {
                    //prints a single character
                    Console.Write(Generation[RowIndex, ColIndex]);
                }
                //sets the cursor position to next line after the row is printed
                yPos++;
            }
            //Counts the cells in the printed iteration
            CellCounter();
            Console.SetCursorPosition(13, 1);
            //Prints out the amount of cells
            Console.Write(LiveCells +"   ");
        }

        //Populates NewGen array with cells according to Generation cell positions and resets Generation array
        public void NewGeneration()
        {
            string[,] NewGen = new string[SizeY, SizeX];
            //Sifts through each cell, checking it's neighbours
            for (int RowIndex = 0; RowIndex < SizeY; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < SizeX; ColIndex++)
                {
                    //Checks for neighbours of the cell
                    NeigbourCounter(RowIndex, ColIndex);
                    Console.SetCursorPosition(1, 28);
                    
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
            //resets the Generation array with new generation
            Generation = NewGen;
        }

        //algorythm to check the neighbours of a particular cell
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

        //Counts cells in the Generation array
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
