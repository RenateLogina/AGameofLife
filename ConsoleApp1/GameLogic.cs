
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
        private bool[,] newGeneration { get; set; }

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
            LiveCells = 0;

            // Populates the first generation with 2 characters, chosen randomly.
            bool[] randomFiller = new bool[] { true, false };
            for (int rowIndex = 0; rowIndex < Rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < Columns; colIndex++)
                {
                    int boolIndex = random.Next(randomFiller.Length);
                    Generation[rowIndex, colIndex] = randomFiller[boolIndex];
                    if (boolIndex == 0)
                    {
                        LiveCells++;
                    }
                }
            }
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

            for (var rowIndex = 0; rowIndex < Rows; rowIndex++)
            {
                sb.Append(boarderLeft);
                for (var colIndex = 0; colIndex < Columns; colIndex++)
                {
                    if(Generation[rowIndex, colIndex])
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
        public void NewGeneration()
        {
            newGeneration = new bool[Rows, Columns];
            LiveCells = 0;

            // Sifts through each cell, checking it's neighbours.
            for (int rowIndex = 0; rowIndex < Rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < Columns; colIndex++)
                {
                    // Checks for neighbours of the cell.
                    NeigbourCounter(rowIndex, colIndex); 
                    // Checks if cell is alive.
                    if(neighbours == 3 || (neighbours == 2 && Generation[rowIndex, colIndex]))
                    {
                        newGeneration[rowIndex, colIndex] = true;
                        LiveCells++;
                    }
                    else
                    {
                        newGeneration[rowIndex, colIndex] = false;
                    }
                }
            }

            Generation = newGeneration;
        }

        /// <summary>
        /// Algorythm to check the neighbours of a particular cell.
        /// </summary>
        /// <param name="rowIndex">Particular index of bool[,] element on x axis</param>
        /// <param name="colIndex">Particular index of bool[,] element on y axis</param>
        private void NeigbourCounter(int rowIndex, int colIndex)
        {
            neighbours = 0;

            // Loops through a 3x3 square with an array cell set in center.
            for (int Row = -1; Row < 2; Row++)
            {
                for (int Col = -1; Col < 2; Col++)
                {
                    // Checks if the neighbour is within array bounds.
                    if ((rowIndex + Row > -1) && (colIndex + Col > -1) && (Row + rowIndex < Rows) && (colIndex + Col < Columns))
                    {
                        if ((Row == 0) && (Col == 0))
                        {
                            // Do nothing.
                        }
                        else if (Generation[rowIndex + Row, colIndex + Col])
                        {
                            neighbours++;
                        }
                    }
                }
            }
        }           
    }
}
