﻿namespace GameofLife
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Game mechanics - setting the first seeds, generating new iteration, 
    /// counting iterations, counting neighbours, counting cells alive.
    /// Maybe all the public parameters should be stored elsewhere, like in GameProgress object.
    /// </summary>
    public class GameLogic
    {
        #region variables
        // Defines an object game progress that contains all the info of a particular game.
        public GameProgress gameProgress = new GameProgress();
        // Defines a List of objects - games.
        public GameList gameList = new GameList();
        public List<int> gamesLoaded = new List<int>();
        // Amount of neighbours of a particular element in array. Made public for testing
        public int Neighbours;
        #endregion

        /// <summary>
        /// Sets the very first iteration of the game, randomly.
        /// Adds a new game to list.
        /// </summary>
        /// <param name="boardSize"> Sets the bool size according to chosen board size. Columns and Rows are derived from this. </param>
        public void SetSeed(int boardSize)
        {
            Random random = new Random();

            gameProgress.ID = 0;
            gameProgress.Columns = 20 * boardSize - 3;
            gameProgress.Rows = 7 + boardSize * boardSize;
            gameProgress.Iteration = 1;
            gameProgress.LiveCells = 0;            
            gameProgress.Generation = new bool[gameProgress.Rows, gameProgress.Columns];

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
                IsGameAlive = true,
            });
        }

        /// <summary>
        /// Resets a GameList Progress generation according to neighbours
        /// </summary>
        /// <param name="game"> Particular game in the list </param>
        public void NewGeneration(GameProgress game)
        {
            bool[,] newGeneration = new bool[game.Rows, game.Columns];
            game.LiveCells = 0;
            
            // Sifts through each cell, checking it's neighbours.
            for (int rowIndex = 0; rowIndex < game.Rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < game.Columns; colIndex++)
                {
                    // Checks for neighbours of the cell.
                    NeigbourCounter(rowIndex, colIndex, game);
                    // Checks if cell is alive.
                    if (Neighbours == 3 || (Neighbours == 2 && game.Generation[rowIndex, colIndex]))
                    {
                        newGeneration[rowIndex, colIndex] = true;
                        game.LiveCells++;
                        gameList.CellsAlive++;
                    }
                    else
                    {
                        newGeneration[rowIndex, colIndex] = false;
                    }
                }
            }

            var x = JsonConvert.SerializeObject(newGeneration);
            var y = JsonConvert.SerializeObject(game.Generation);

            if (x == y)
            {
                game.IsGameAlive = false;
            }

            game.Iteration++;            
           
            game.Generation = newGeneration;
        }

        /// <summary>
        /// Algorythm to check the neighbours of a particular cell.
        /// </summary>
        /// <param name="game"> Which particular game in the List GameList Progress to check. </param>
        public void NeigbourCounter(int rowIndex, int colIndex, GameProgress game)
        {
            Neighbours = 0;

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
                            Neighbours++;
                        }
                    }
                }
            }
        }
    }
}
