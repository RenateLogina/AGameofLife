namespace GameofLife
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Saves and reads game file that contains GameProgress object and is initiated from GameManager
    /// </summary>
    public class Serializer
    {
        public string filePath = @"C:\Users\r.logina\Documents\gameprogress.save";
        /// <summary>
        /// Serializes the current game progress to a file.
        /// </summary>
        public void Serialize(GameList gameList)
        {
            string result = JsonConvert.SerializeObject(gameList);
            File.WriteAllText(filePath, result);
        }

        /// <summary>
        /// Deserializes a file containing an object.
        /// </summary>
        /// <param name="filePath"> Location of the file as set in GameManager </param>
        /// <returns> Returns GameProgress values </returns>
        public GameList Deserialize()
        {
            GameList gameList = null;

            if (File.Exists(filePath))
            {
                gameList = JsonConvert.DeserializeObject<GameList>(File.ReadAllText(filePath));
            }

            return gameList; //this should only return gameProgress
        }
    }
}
