
namespace GameofLife
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Game mechanics - setting the first seeds, generating new iteration, 
    /// counting iterations, counting neighbours, counting cells alive
    /// </summary>
    public class GameLogic
    {
        public  int _liveCells;
        public int Iteration;
        private int _neighbours;
        //board size
        public int _sizeX;
        public int _sizeY;
        
        //stores and resets Generation array values
        public bool[,] Generation;
        public bool[,] NewGen;

        //Sets the very first iteration of the game, randomly
        public void SetSeed(int boardSize)
        {
            //set a 2D array size, based on board size
            Iteration = 1;
            _sizeX = 50 * boardSize - 3;
            _sizeY = 14 + boardSize * boardSize;
            Random random = new Random();
            Generation = new bool[_sizeY, _sizeX];

            //populates the first generation with 2 characters, chosen randomly
            bool[] RandomFiller = new bool[] { true, false };
            for (int RowIndex = 0; RowIndex < _sizeY; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < _sizeX; ColIndex++)
                {
                    int boolIndex = random.Next(RandomFiller.Length);
                    Generation[RowIndex, ColIndex] = RandomFiller[boolIndex];
                }
            }
            NewGen = Generation;
        }

        //prints the Generation array
        public void PrintArray()
        {
            //Sets starting point for printing, according to graphic location
            int xPos = 2;
            int yPos = 6;

            //resets the Generation array with new generation
            Generation = NewGen;

            for (int RowIndex = 0; RowIndex < _sizeY; RowIndex++)//prints row
            {
                Console.SetCursorPosition(xPos, yPos);
                for (int ColIndex = 0; ColIndex < _sizeX; ColIndex++)//prints individual cells in row (width)
                {
                    //prints a cell character
                    if (Generation[RowIndex, ColIndex])
                    {
                        Console.Write("\u25CF");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                //sets the cursor position to next line after the row is printed
                yPos++;
            }
            //Counts the cells in the printed iteration
            CellCounter();

            //var sb = new StringBuilder(string.Empty);
            //for (var y = 0; y < _sizeY; y++)
            //{
            //    sb.Append(",{");
            //    for (var x = 0; x < _sizeX; x++)
            //    {
            //        sb.AppendFormat("{0},", Generation[y, x]);
            //    }
            //    sb.Append("}");
            //    sb.AppendLine();
            //}
            //sb.Replace(",}", "}").Remove(0, 1);
            //var result = sb.ToString();
            //var result = $@"{{{JsonConvert.SerializeObject(Generation).Trim('[', ']').Replace("[", "{").Replace("]", "}")}}}";
            //return result;
        }

        //Populates NewGen array with cells according to Generation cell positions and resets Generation array
        public void NewGeneration()
        {
            NewGen = new bool[_sizeY, _sizeX];
            //Sifts through each cell, checking it's _neighbours
            for (int RowIndex = 0; RowIndex < _sizeY; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < _sizeX; ColIndex++)
                {
                    //Checks for _neighbours of the cell
                    NeigbourCounter(RowIndex, ColIndex);
                    Console.SetCursorPosition(1, 28);
                    
                    //checks if cell is alive
                    if (Generation[RowIndex, ColIndex])
                    {
                        //if has less than 2 or more than 3 live _neighbours, it dies
                        if (_neighbours < 2 || _neighbours > 3)
                        {
                            NewGen[RowIndex, ColIndex] = false;
                        }
                        //else it lives on
                        else
                        {
                            NewGen[RowIndex, ColIndex] = true;
                        }
                    }
                    //if the cell is dead
                    else
                    {
                        if (_neighbours == 3)
                        {
                            NewGen[RowIndex, ColIndex] = true;
                        }
                        //else it remains dead
                        else
                        {
                            NewGen[RowIndex, ColIndex] = false;
                        }
                    }
                }
            }
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
                    if (RowIndex + Row > -1 && ColIndex + Col > -1 && Row + RowIndex < _sizeY && ColIndex + Col < _sizeX && Generation[RowIndex + Row, ColIndex + Col])
                    {
                        if (Row != 0 & Col != 0)//unfortunately faulty when using != WHYYY
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
        }

        //Counts cells in the Generation array
        public int CellCounter()
        {
            _liveCells = 0;
            for (int RowIndex = 0; RowIndex < _sizeY; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < _sizeX; ColIndex++)
                {
                    if(Generation[RowIndex,ColIndex])
                    {
                        _liveCells++;
                    }
                }
            }
            return _liveCells;

        }
        /// <summary>
        /// Converts data to txt, saves file
        /// </summary>
        
    }
}
