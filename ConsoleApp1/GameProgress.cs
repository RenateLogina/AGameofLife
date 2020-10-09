namespace GameofLife
{
    using System;

    [Serializable]
    public class GameProgress
    {
        public string GenerationArray { get; set; }
        public int LiveCellCount { get; set; }
        public int BoardSize { get; set; }
        public int Iteration { get; set; }
    }
}
