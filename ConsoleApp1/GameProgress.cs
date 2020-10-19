namespace GameofLife
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Stores all properties needed to perform serialization/deserialization
    /// </summary>
    [Serializable]
    public class GameProgress
    {
        // Stores Generation Array defined by GameLogic.
        [JsonProperty("Generation")]
        public bool[,] Generation { get; set; }

        // Current amount of Live cells in a generation.
        [JsonProperty("LiveCells")]
        public int LiveCells { get; set; }

        // Current board size as set by user at StartGame.
        [JsonProperty("BoardSize")]
        public int BoardSize { get; set; }

        // Current iteration number, defined by GawmeManager
        [JsonProperty("Iteration")]
        public int Iteration { get; set; }

        // Amount of columns in a 2D array
        [JsonProperty("Columns")]
        public int Columns { get; set; }

        // Amount of rows in a 2D array
        [JsonProperty("Rows")]
        public int Rows { get; set; }
    }
}
