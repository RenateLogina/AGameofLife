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

        public GameProgress Deserialize(string filePath)
        {
            GameProgress gameProgress = JsonConvert.DeserializeObject<GameProgress>(File.ReadAllText(filePath));

            return gameProgress; //this should only return gameProgress
        }
    }
}
