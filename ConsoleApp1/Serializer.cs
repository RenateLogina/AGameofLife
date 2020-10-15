namespace GameofLife
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    public class Serializer
    {
        GameLogic gameLogic = new GameLogic();

        public void Serialize(GameProgress gameProgress, string filePath)
        {
            string result = JsonConvert.SerializeObject(gameProgress);

            if (File.Exists(filePath))
            { 
                File.Delete(filePath); 
            } 

            File.WriteAllText(filePath, result);
        }

        public GameLogic Deserialize(string filePath)
        {
            GameProgress gameProgress = JsonConvert.DeserializeObject<GameProgress>(File.ReadAllText(filePath));
            if (File.Exists(filePath)) // I should move this to manager
            {
                gameLogic.Columns = gameProgress.Columns;
                gameLogic.Rows = gameProgress.Rows;
                gameLogic.BoardSize = gameProgress.BoardSize;
                gameLogic.Iteration = gameProgress.Iteration;
                gameLogic.LiveCells = gameProgress.LiveCells;
                gameLogic.Generation = gameProgress.GenerationArray;
            }

            return gameLogic; //this should only return gameProgress
        }
    }
}
