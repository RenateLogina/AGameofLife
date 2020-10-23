namespace GameofLife.Tests
{
    using Xunit;
    using GameofLife;
    using Moq;

    public class ManagerTests
    {

        [Fact]
        public void ReadGameFile()
        {
            // Arrange.
            var serializerMock = new Mock<Serializer>();

            GameList list = new GameList();
            list.Progress.Add(new GameProgress()
            {
                Rows = 2,
                Columns = 2,
                Generation = new bool[,] { { true, false }, { true, false }, },
            });

            
            serializerMock.Setup(p => p.Serialize(list)).Returns();
        }
    }
}
