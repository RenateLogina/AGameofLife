namespace GameofLife
{
    using Newtonsoft.Json;
    using System.IO;

    /// <summary>
    /// Saves and reads game file that contains GameProgress object and is initiated from GameManager
    /// </summary>
    public class Serializer
    {
        public string filePath = @"C:\Users\r.logina\Documents\gameprogress.save";

        /// <summary>
        /// Serializes the current game progress to a file.
        /// </summary>
        /// <param name="gameProgress"> GameProgress object filled with values via GameManager. </param>
        /// <param name="filePath"> Path where the file is saved as set in GameManager. </param>
        public void Serialize(GameProgress gameProgress)
        {
            string result = JsonConvert.SerializeObject(gameProgress);
            File.WriteAllText(filePath, result);
        }

        /// <summary>
        /// Deserializes a file containing an object.
        /// </summary>
        /// <param name="filePath"> Location of the file as set in GameManager </param>
        /// <returns> Returns GameProgress values </returns>
        public GameProgress Deserialize()
        {
            GameProgress gameProgress = null;
            if (File.Exists(filePath))
            {
                gameProgress = JsonConvert.DeserializeObject<GameProgress>(File.ReadAllText(filePath));
            }

            return gameProgress; //this should only return gameProgress
        }
    }
}
