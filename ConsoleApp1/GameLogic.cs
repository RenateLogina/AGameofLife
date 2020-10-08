
namespace GameofLife
{
    using System;
    using System.Reflection.PortableExecutable;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;

    public class GameLogic
    {
        //public int MyProperty { get; set; }
        private int _neighbours;
        //board size
        private int _sizeX;
        private int _sizeY;
        //amount of live cells at given iteration
        public int LiveCells;
        //stores and resets Generation array values
        private string[,] Generation;

        //Sets the very first iteration of the game, randomly
        public void SetSeed(int boardSize)
        {
            //set a 2D array size, based on board size
            _sizeX = 50 * boardSize - 3;
            _sizeY = 14 + boardSize * boardSize;
            Random random = new Random();
            Generation = new string[_sizeY, _sizeX];

            //populates the first generation with 2 characters, chosen randomly
            string[] RandomFiller = new string[] { "\u25CF", " " };
            for (int RowIndex = 0; RowIndex < _sizeY; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < _sizeX; ColIndex++)
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
            int xPos = 2;
            int yPos = 6;
            for (int RowIndex = 0; RowIndex < _sizeY; RowIndex++)//prints row
            {
                Console.SetCursorPosition(xPos, yPos);
                for (int ColIndex = 0; ColIndex < _sizeX; ColIndex++)//prints individual cells in row (width)
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
            string[,] NewGen = new string[_sizeY, _sizeX];
            //Sifts through each cell, checking it's _neighbours
            for (int RowIndex = 0; RowIndex < _sizeY; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < _sizeX; ColIndex++)
                {
                    //Checks for _neighbours of the cell
                    NeigbourCounter(RowIndex, ColIndex);
                    Console.SetCursorPosition(1, 28);
                    
                    //checks if cell is alive
                    if (Generation[RowIndex, ColIndex] == "\u25CF")
                    {
                        //if has less than 2 or more than 3 live _neighbours, it dies
                        if (_neighbours < 2 || _neighbours > 3)
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
                        if (_neighbours == 3)
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

        //algorythm to check the _neighbours of a particular cell
        private void NeigbourCounter(int RowIndex, int ColIndex)
        {
            _neighbours = 0;

            //loops through a 3x3 square
            for (int Row = -1; Row < 2; Row++)
            {
                for (int Col = - 1; Col < 2; Col++)
                {
                    if (RowIndex + Row > -1 && ColIndex + Col > -1 && Row + RowIndex < _sizeY && ColIndex + Col < _sizeX && Generation[RowIndex + Row, ColIndex + Col] == "\u25CF")
                    {
                        if (Row == 0 && Col == 0)//unfortunately faulty when using != WHYYY
                        {
                           //do nothing
                        }
                        else
                        {
                            _neighbours++;
                        }
                    }

                }
            }
            //Check _neighbours for each cell during testing
            //Console.SetCursorPosition(0, 0);
            //Console.WriteLine("Cell nr {0},{1} has {2} _neighbours.", RowIndex, ColIndex, _neighbours);

            //if (RowIndex > 0 && ColIndex > 0 && Generation[RowIndex - 1, ColIndex - 1] == "\u25CF")
            //{
            //    _neighbours++;
            //}            
            //if (RowIndex > 0 && Generation[RowIndex - 1, ColIndex] == "\u25CF")
            //{
            //    _neighbours++;
            //}
            //if (RowIndex > 0 && ColIndex < _sizeX - 1 && Generation[RowIndex - 1, ColIndex + 1] == "\u25CF")
            //{
            //    _neighbours++;
            //}
            //if (ColIndex > 0 && Generation[RowIndex, ColIndex - 1] == "\u25CF")
            //{
            //    _neighbours++;
            //}
            //if (ColIndex < _sizeX - 1 && Generation[RowIndex, ColIndex + 1] == "\u25CF")
            //{
            //    _neighbours++;
            //}
            //if (RowIndex < _sizeY - 1 && ColIndex > 0 && Generation[RowIndex + 1, ColIndex - 1] == "\u25CF")
            //{
            //    _neighbours++;
            //}
            //if (RowIndex < _sizeY - 1 && Generation[RowIndex + 1, ColIndex] == "\u25CF")
            //{
            //    _neighbours++;
            //}
            //if (RowIndex < _sizeY - 1 && ColIndex < _sizeX - 1 && Generation[RowIndex + 1, ColIndex + 1] == "\u25CF")
            //{
            //    _neighbours++;
            //}
        }

        //Counts cells in the Generation array
        public void CellCounter()
        {
            LiveCells = 0;
            for (int RowIndex = 0; RowIndex < _sizeY; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < _sizeX; ColIndex++)
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
