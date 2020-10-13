namespace GameofLife
{
    using System;

    [Serializable]
    public class GameProgress
    {
        public bool[,] GenerationArray { get; set; }
        public int LiveCellCount { get; set; }
        public int BoardSize { get; set; }
        public int Iteration { get; set; }
    }
}
