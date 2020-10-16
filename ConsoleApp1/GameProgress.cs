namespace GameofLife
{
    using System;

    /// <summary>
    /// Stores all properties needed to perform serialization/deserialization
    /// </summary>
    [Serializable]
    public class GameProgress
    {
        // Stores Generation Array defined by GameLogic.
        public bool[,] GenerationArray { get; set; }

        // Current amount of Live cells in a generation.
        public int LiveCells { get; set; }

        // Current board size as set by user at StartGame.
        public int BoardSize { get; set; }

        // Current iteration number, defined by GawmeManager
        public int Iteration { get; set; }

        // Amount of columns in a 2D array
        public int Columns { get; set; }

        // Amount of rows in a 2D array
        public int Rows { get; set; }
    }
}
