namespace GameofLife
{
    using Newtonsoft.Json;
    using System.IO;
    public class Serializer
    {
        GameLogic gL = new GameLogic();
        public void SaveGame(bool[,]generation, int sizeX, int sizeY, int iteration, int liveCells, int boardSize)
        {
            
            GameProgress gameProgress = new GameProgress
            { GenerationArray = generation, 
                LiveCellCount = liveCells, 
                BoardSize = boardSize, 
                Iteration = iteration, 
                SizeX = sizeX, 
                SizeY = sizeY };

            string filePath = @"C:\Users\r.logina\Documents\gameprogress.save";
            string result = JsonConvert.SerializeObject(gameProgress);
            if (File.Exists(filePath)) File.Delete(filePath);
            File.WriteAllText(filePath, result);
        }
        public void LoadGame(bool[,] generation, int sizeX, int sizeY, int iteration, int liveCells, int boardSize)
        {
            string filePath = @"C:\Users\r.logina\Documents\gameprogress.save";
            GameProgress g = JsonConvert.DeserializeObject<GameProgress>(File.ReadAllText(filePath));
            if (File.Exists(filePath))
            {
                gL.SizeX = g.SizeX;
                gL.SizeY = g.SizeY;
                gL.BoardSize = g.BoardSize;
                gL.Iteration = g.Iteration;
                gL.LiveCells = g.LiveCellCount;
                gL.Generation = g.GenerationArray;
            }
        }
    }
}
