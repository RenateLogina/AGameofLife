namespace GameofLife
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    public class Serializer
    {
        /// <summary>
        /// Serializes the current game progress to a file.
        /// </summary>
        /// <param name="gameProgress"> GameProgress object filled with values via GameManager. </param>
        /// <param name="filePath"> Path where the file is saved as set in GameManager. </param>
        public void Serialize(GameProgress gameProgress, string filePath)
        {
            string result = JsonConvert.SerializeObject(gameProgress);

            if (File.Exists(filePath))
            { 
                File.Delete(filePath); 
            } 

            File.WriteAllText(filePath, result);
        }

        /// <summary>
        /// Deserializes a file containing an object.
        /// </summary>
        /// <param name="filePath"> Location of the file as set in GameManager </param>
        /// <returns></returns>
        public GameProgress Deserialize(string filePath)
        {
            GameProgress gameProgress = JsonConvert.DeserializeObject<GameProgress>(File.ReadAllText(filePath));

            return gameProgress; //this should only return gameProgress
        }
    }
}
