
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

        public GameProgress gameProgress = new GameProgress();
        public GameList gameList = new GameList();
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
            gameProgress.Columns = 50 * boardSize - 3;
            gameProgress.Rows = 14 + boardSize * boardSize;
            gameProgress.Generation = new bool[gameProgress.Rows, gameProgress.Columns];
            gameProgress.Iteration = 1;
            gameProgress.LiveCells = 0;
            gameProgress.ID = 0;

            // Populates the first generation with 2 characters, chosen randomly.
            bool[] randomFiller = new bool[] { true, false };
            for (int rowIndex = 0; rowIndex < gameProgress.Rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < gameProgress.Columns; colIndex++)
                {
                    int boolIndex = random.Next(randomFiller.Length);
                    gameProgress.Generation[rowIndex, colIndex] = randomFiller[boolIndex];
                    if (boolIndex == 0)
                    {
                        gameProgress.LiveCells++;
                    }
                }
            }
            if (gameList == null)
            {
                gameList = new GameList();
            }

            gameList.Progress.Add(new GameProgress()
            {
                Generation = gameProgress.Generation,
                LiveCells = gameProgress.LiveCells,
                BoardSize = gameProgress.BoardSize,
                Iteration = gameProgress.Iteration,
                Columns = gameProgress.Columns,
                Rows = gameProgress.Rows,
                ID = gameList.Progress.Count + 1,
            });
        }

        /// <summary>
        /// Populates NewGeneration array with cells according to Generation cell positions 
        /// and resets Generation array.
        /// </summary>
        public void NewGeneration()
        {
            newGeneration = new bool[gameProgress.Rows, gameProgress.Columns];
            gameProgress.LiveCells = 0;

            // Sifts through each cell, checking it's neighbours.
            for (int rowIndex = 0; rowIndex < gameProgress.Rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < gameProgress.Columns; colIndex++)
                {
                    // Checks for neighbours of the cell.
                    NeigbourCounter(rowIndex, colIndex); 
                    // Checks if cell is alive.
                    if(neighbours == 3 || (neighbours == 2 && gameProgress.Generation[rowIndex, colIndex]))
                    {
                        newGeneration[rowIndex, colIndex] = true;
                        gameProgress.LiveCells++;
                    }
                    else
                    {
                        newGeneration[rowIndex, colIndex] = false;
                    }
                }
            }
            gameProgress.Iteration++;
            gameProgress.Generation = newGeneration;
        }

        public void NewGenerationList(GameProgress game)
        {
            newGeneration = new bool[game.Rows, game.Columns];
            game.LiveCells = 0;

            // Sifts through each cell, checking it's neighbours.
            for (int rowIndex = 0; rowIndex < game.Rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < game.Columns; colIndex++)
                {
                    // Checks for neighbours of the cell.
                    ListNeigbourCounter(rowIndex, colIndex, game);
                    // Checks if cell is alive.
                    if (neighbours == 3 || (neighbours == 2 && game.Generation[rowIndex, colIndex]))
                    {
                        newGeneration[rowIndex, colIndex] = true;
                        game.LiveCells++;
                    }
                    else
                    {
                        newGeneration[rowIndex, colIndex] = false;
                    }
                }
            }
            game.Iteration++;
            game.Generation = newGeneration;
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
                    if ((rowIndex + Row > -1) && (colIndex + Col > -1) && (Row + rowIndex < gameProgress.Rows) && (colIndex + Col < gameProgress.Columns))
                    {
                        if ((Row == 0) && (Col == 0))
                        {
                            // Do nothing.
                        }
                        else if (gameProgress.Generation[rowIndex + Row, colIndex + Col])
                        {
                            neighbours++;
                        }
                    }
                }
            }
        }
        private void ListNeigbourCounter(int rowIndex, int colIndex, GameProgress game)
        {
            neighbours = 0;

            // Loops through a 3x3 square with an array cell set in center.
            for (int Row = -1; Row < 2; Row++)
            {
                for (int Col = -1; Col < 2; Col++)
                {
                    // Checks if the neighbour is within array bounds.
                    if ((rowIndex + Row > -1) && (colIndex + Col > -1) && (Row + rowIndex < game.Rows) && (colIndex + Col < game.Columns))
                    {
                        if ((Row == 0) && (Col == 0))
                        {
                            // Do nothing.
                        }
                        else if (game.Generation[rowIndex + Row, colIndex + Col])
                        {
                            neighbours++;
                        }
                    }
                }
            }
        }
    }
}
