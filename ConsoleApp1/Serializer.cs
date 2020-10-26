namespace GameofLife
{
    using Newtonsoft.Json;
    using System.IO;
    using System.IO.Abstractions;

    /// <summary>
    /// Saves and reads game file that contains GameProgress object and is initiated from GameManager
    /// </summary>
    public class Serializer : ISerializer
    {
        public string FilePath = @"C:\Users\r.logina\Documents\gameprogress.save";
        private readonly IFileSystem _fileSystem;
        public Serializer() : this(new FileSystem()) { }
        public Serializer(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }        

        /// <summary>
        /// Serializes the current game progress to a file.
        /// </summary>
        public void Serialize(GameList gameList)
        {
            string result = JsonConvert.SerializeObject(gameList);
            File.WriteAllText(FilePath, result);
        }

        /// <summary>
        /// Deserializes a file containing an object.
        /// </summary>
        /// <param name="filePath"> Location of the file as set in GameManager </param>
        /// <returns> Returns a list of games </returns>
        public GameList Deserialize()
        {
            GameList gameList = null;

            if (File.Exists(FilePath))
            {
                gameList = JsonConvert.DeserializeObject<GameList>(File.ReadAllText(FilePath));
            }

            return gameList;
        }
    }
}
