namespace GameofLife
{
    using System;

    [Serializable]
    public class GameProgress
    {
        public bool[,] GenerationArray { get; set; }
        public int LiveCells { get; set; }
        public int BoardSize { get; set; }
        public int Iteration { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
    }
}
