namespace GameofLife.Tests
{
    using Xunit;
    using GameofLife;
    using Moq;
    using System.IO.Abstractions.TestingHelpers;
    using System;

    public class ManagerShould
    {
        [Fact]
        public void SetFirstIteration()
        {
            var mockLogic = new Mock<GameLogic>();
            var serializerMock = new Mock<ISerializer>();
            var mockUI = new Mock<IGameUI>();
            GameManager sut = new GameManager(serializerMock.Object, mockUI.Object );

            mockLogic.Setup(x => x.gameList.Progress.Count).Returns(999);
            mockLogic.Setup(x => x.gameProgress.BoardSize).Returns(1);

            sut.SetFirstIteration();


            mockLogic.Verify(x => x.SetSeed(999), Times.Once);
        }

        [Fact]
        public void SaveGameList()
        {
            // Arrange.
            var serializerMock = new Mock<ISerializer>();
            var mockUI = new Mock<IGameUI>();
            var mockLogic = new Mock<GameLogic>();
            var mockFileSystem = new MockFileSystem();
            
            
            // Creates a fake list
            var list = new GameList();

            GameManager sut = new GameManager(serializerMock.Object, mockUI.Object);
            var timer = new System.Timers.Timer(1000) { Enabled = false};
    
            sut.MyTimer = timer;
            sut.gameLogic.gameList = list;

            serializerMock.Setup(p => p.Serialize(list));
            mockUI.Setup(m => m.GameIsSaved());

            // Act.
            sut.SaveGame();

            // Assert.
            serializerMock.Verify(p => p.Serialize(It.IsAny<GameList>()), Times.Once);
            mockUI.Verify(p => p.GameIsSaved(), Times.Once);

        }
        [Fact]
        public void ReadFile()
        {
            var serializerMock = new Mock<ISerializer>();
            var gameLogic = new GameLogic();
            var mockUI = new Mock<IGameUI>();
            var mockFileSystem = new MockFileSystem();
            GameManager sut = new GameManager(serializerMock.Object, mockUI.Object);
            serializerMock.Setup(p => p.Deserialize());
            mockUI.Setup(p => p.NoGameExists());

            sut.ReadGame();

            Assert.NotNull(gameLogic.gameList);
            serializerMock.Verify(p => p.Deserialize(), Times.Once);
        }
    }
}
