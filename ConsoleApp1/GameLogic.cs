
namespace GameofLife
{
    using System;
    using System.Text;

    /// <summary>
    /// Game mechanics - setting the first seeds, generating new iteration, 
    /// counting iterations, counting neighbours, counting cells alive.
    /// Maybe all the public parameters should be stored elsewhere, like in GameProgress object.
    /// </summary>
    public class GameLogic
    {
        #region variables
        public int BoardSize { get; set; }
        public int LiveCells { get; set; }
        public int Iteration { get; set; }
        // Bool dimensions.
        public int Columns { get; set; }
        public int Rows { get; set; }

        public bool[,] Generation { get; set; }
        private bool[,] NewGeneration { get; set; }

        private int neighbours;
        #endregion

        /// <summary>
        /// Sets the very first iteration of the game, randomly.
        /// </summary>
        /// <param name="boardSize"> Sets the bool size according to chosen board size. Columns and Rows are derived from this. </param>
        public void SetSeed(int boardSize)
        {
            Random random = new Random();
            Columns = 50 * boardSize - 3;
            Rows = 14 + boardSize * boardSize;
            Generation = new bool[Rows, Columns];
            Iteration = 1;

            // Populates the first generation with 2 characters, chosen randomly.
            bool[] RandomFiller = new bool[] { true, false };
            for (int RowIndex = 0; RowIndex < Rows; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < Columns; ColIndex++)
                {
                    int boolIndex = random.Next(RandomFiller.Length);
                    Generation[RowIndex, ColIndex] = RandomFiller[boolIndex];
                }

            }

            CellCounter();
        }

        /// <summary>
        /// Prints the Generation array and boarders using StringBuilder.
        /// </summary>
        /// <returns> Appended string of current game iteration. </returns>
        public string PrintArray()
        {
            #region character symbols used
            var boarderTop = "\u2584";
            var boarderLeft = " \u2588";
            var dot = "\u25CF";
            var boarderRight = "\u2588";
            var boarderBottom = "\u2580";
            #endregion
            var sb = new StringBuilder(string.Empty);

            sb.AppendFormat("  Board size: {0}", BoardSize);
            sb.AppendLine();
            sb.AppendFormat("  Live cells: {0}    ", LiveCells);
            sb.AppendLine();
            sb.AppendFormat("  Iteration NR: {0}    ", Iteration);
            sb.AppendLine();
            sb.Append(" ");

            for (int width = 1; width < 50 * BoardSize; width++)
            {
                sb.Append(boarderTop);
            }

            sb.AppendLine();

            for (var RowIndex = 0; RowIndex < Rows; RowIndex++)
            {
                sb.Append(boarderLeft);
                for (var ColIndex = 0; ColIndex < Columns; ColIndex++)
                {
                    if(Generation[RowIndex, ColIndex])
                    {
                        sb.Append(dot);
                    }

                    else
                    {
                        sb.Append(" ");
                    }

                }

                sb.Append(boarderRight);
                sb.AppendLine();
            }

            sb.Append(" ");

            for (int width = 1; width < 50 * BoardSize; width++)
            {
                sb.Append(boarderBottom);
            }

            var result = sb.ToString();

            return result;
        }

        /// <summary>
        /// Populates NewGeneration array with cells according to Generation cell positions 
        /// and resets Generation array.
        /// </summary>
        public void NewGenerationeration()
        {
            NewGeneration = new bool[Rows, Columns];

            // Sifts through each cell, checking it's neighbours.
            for (int RowIndex = 0; RowIndex < Rows; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < Columns; ColIndex++)
                {
                    // Checks for neighbours of the cell.
                    NeigbourCounter(RowIndex, ColIndex); 
                    // Checks if cell is alive.
                    if (Generation[RowIndex, ColIndex])
                    {
                        // If it has less than 2 or more than 3 live neighbours, it dies.
                        if (neighbours < 2 || neighbours > 3)
                        {
                            NewGeneration[RowIndex, ColIndex] = false;
                        }

                        // Else it lives on.
                        else
                        {
                            NewGeneration[RowIndex, ColIndex] = true;
                        }

                    }

                    // Checks if the cell is dead.
                    else
                    {
                        if (neighbours == 3)
                        {
                            NewGeneration[RowIndex, ColIndex] = true;
                        }

                        // Else it remains dead.
                        else
                        {
                            NewGeneration[RowIndex, ColIndex] = false;
                        }

                    }

                }

            }

            Generation = NewGeneration;
            CellCounter();
        }

        /// <summary>
        /// Algorythm to check the neighbours of a particular cell.
        /// </summary>
        /// <param name="RowIndex">Particular index of bool[,] element on x axis</param>
        /// <param name="ColIndex">Particular index of bool[,] element on y axis</param>
        private void NeigbourCounter(int RowIndex, int ColIndex)
        {
            neighbours = 0;

            // Loops through a 3x3 square with an array cell set in center.
            for (int Row = -1; Row < 2; Row++)
            {
                for (int Col = -1; Col < 2; Col++)
                {
                    // Checks if the neighbour is within array bounds.
                    if ((RowIndex + Row > -1) && (ColIndex + Col > -1) && (Row + RowIndex < Rows) && (ColIndex + Col < Columns))
                    {
                        if ((Row == 0) && (Col == 0))
                        {
                            // Do nothing.
                        }

                        else if (Generation[RowIndex + Row, ColIndex + Col])
                        {
                            neighbours++;
                        }

                    }

                }

            }
        }        

        /// <summary>
        /// Counts live cells in the Generation array.
        /// </summary>
        /// <returns> Returns amount (int) of cells currently alive. </returns>
        public int CellCounter()
        {
            LiveCells = 0;

            for (int RowIndex = 0; RowIndex < Rows; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < Columns; ColIndex++)
                {
                    if(Generation[RowIndex,ColIndex])
                    {
                        LiveCells++;
                    }

                }

            }

            return LiveCells;
        }        
    }
}
