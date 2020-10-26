namespace GameofLife
{
    /// <summary>
    /// Wraps Serializer
    /// </summary>
    public interface ISerializer
    {
        void Serialize(GameList gameList);
        GameList Deserialize();
        
    }
}
