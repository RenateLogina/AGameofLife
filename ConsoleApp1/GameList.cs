using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameofLife
{
    [Serializable]
    public class GameList
    {
        [JsonProperty("Progress")]
        public List<GameProgress> Progress { get; set; }
        public int GamesAlive { get; set; }
        public int CellsAlive { get; set; }
        public GameList()
        {
            Progress = new List<GameProgress>();
        }
    }
}
