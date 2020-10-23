namespace GameofLife
{
    public interface ISerializer
    {
        void Serialize(GameList gameList);
        GameList Deserialize();
    }
}
