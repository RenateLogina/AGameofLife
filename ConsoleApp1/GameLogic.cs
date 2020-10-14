﻿
namespace GameofLife
{
    using System;
    using System.Text;

    /// <summary>
    /// Game mechanics - setting the first seeds, generating new iteration, 
    /// counting iterations, counting neighbours, counting cells alive
    /// </summary>
    public class GameLogic
    {
        #region variables
        public int BoardSize;
        public int LiveCells;
        public int Iteration;
        private int neighbours;
        //bool dimensions
        public int SizeX;
        public int SizeY; 

        public bool[,] Generation;
        public bool[,] NewGen;
        #endregion

        /// <summary>
        /// Sets the very first iteration of the game, randomly
        /// </summary>
        /// <param name="boardSize"></param>
        public void SetSeed(int boardSize)
        {
            //set a 2D array size, based on board size
            Iteration = 1;
            SizeX = 50 * boardSize - 3;
            SizeY = 14 + boardSize * boardSize;
            Random random = new Random();
            Generation = new bool[SizeY, SizeX];

            //populates the first generation with 2 characters, chosen randomly
            bool[] RandomFiller = new bool[] { true, false };
            for (int RowIndex = 0; RowIndex < SizeY; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < SizeX; ColIndex++)
                {
                    int boolIndex = random.Next(RandomFiller.Length);
                    Generation[RowIndex, ColIndex] = RandomFiller[boolIndex];
                }
            }
            CellCounter();
        }

        /// <summary>
        /// prints the Generation array and boarders using StringBuilder
        /// </summary>
        public string PrintArray()
        {
            var sb = new StringBuilder(string.Empty);
            sb.Append(" ");
            for (int width = 1; width < 50 * BoardSize; width++)
            {
                sb.Append("\u2584");
            }
            sb.AppendLine();
            for (var y = 0; y < SizeY; y++)
            {
                sb.Append(" \u2588");
                for (var x = 0; x < SizeX; x++)
                {
                    if(Generation[y, x])
                    {
                        sb.Append("\u25CF");
                    }
                    else
                    {
                        sb.Append(" ");
                    }
                }
                sb.Append("\u2588");
                sb.AppendLine();
            }
            sb.Append(" ");
            for (int width = 1; width < 50 * BoardSize; width++)
            {
                sb.Append("\u2580");
            }
            var result = sb.ToString();
            return result;
        }

        /// <summary>
        /// Populates NewGen array with cells according to Generation cell positions 
        /// and resets Generation array
        /// </summary>
        public void NewGeneration()
        {
            NewGen = new bool[SizeY, SizeX];
            //Sifts through each cell, checking it's neighbours
            for (int RowIndex = 0; RowIndex < SizeY; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < SizeX; ColIndex++)
                {
                    //Checks for neighbours of the cell
                    NeigbourCounter(RowIndex, ColIndex);
                    Console.SetCursorPosition(1, 28);
                    
                    //checks if cell is alive
                    if (Generation[RowIndex, ColIndex])
                    {
                        //if has less than 2 or more than 3 live neighbours, it dies
                        if (neighbours < 2 || neighbours > 3)
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
                        if (neighbours == 3)
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
            Generation = NewGen;
            CellCounter();
        }

        /// <summary>
        /// algorythm to check the neighbours of a particular cell
        /// </summary>
        /// <param name="RowIndex"></param>
        /// <param name="ColIndex"></param>
        private void NeigbourCounter(int RowIndex, int ColIndex)
        {
            neighbours = 0;

            //loops through a 3x3 square, ignores diagonal dots!!!! :O
            for (int Row = -1; Row < 2; Row++)
            {
                for (int Col = -1; Col < 2; Col++)
                {
                    if (RowIndex + Row > -1 && ColIndex + Col > -1 && Row + RowIndex < SizeY && ColIndex + Col < SizeX)
                    {
                        if (Row == 0 && Col == 0)
                        {
                            //do nothing
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
        /// Counts cells in the Generation array
        /// </summary>
        /// <returns></returns>
        public int CellCounter()
        {
            LiveCells = 0;
            for (int RowIndex = 0; RowIndex < SizeY; RowIndex++)
            {
                for (int ColIndex = 0; ColIndex < SizeX; ColIndex++)
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
