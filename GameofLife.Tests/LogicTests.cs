namespace GameofLife.Tests
{
    using Xunit;
    using GameofLife;

    public class LogicTests
    {
        /// <summary>
        /// Checks if the neigbhours of a particular cell in a simple shape are counted correctly. Should work.
        /// </summary>
        [Theory]
        [InlineData(0, 1, 2)]
        [InlineData(0,0,1)]
        public void NeighboursTestShouldWork(int x, int y, int expected)
        {
            // Arrange.
            GameProgress testgame = new GameProgress() { Rows = 2, Columns = 2, Generation = new bool [,]{ { true, false }, { true, false }, }, };
            GameLogic gameLogic = new GameLogic();         
            
            // Act.
            gameLogic.NeigbourCounter(x,y, testgame);
            int actual = gameLogic.Neighbours;

            // Assert.
            Assert.Equal(expected, actual);            
        }

        /// <summary>
        /// Checks if the neigbhours of a particular cell in a simple shape are counted correctly. Should not work.
        /// </summary>
        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(0, 0, 2)]
        public void NeighboursTestShouldNotBeEqual(int x, int y, int expected)
        {
            // Arrange.
            GameProgress testgame = new GameProgress() { Rows = 2, Columns = 2, Generation = new bool[,] { { true, false }, { true, false }, }, };
            GameLogic gameLogic = new GameLogic();

            // Act.
            gameLogic.NeigbourCounter(x, y, testgame);
            int actual = gameLogic.Neighbours;

            // Assert.
            Assert.NotEqual(expected, actual);
        }
    }
}
