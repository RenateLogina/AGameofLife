using System;
using Xunit;
using GameofLife;

namespace GameofLife.Tests
{
    public class LogicTests
    {
        /// <summary>
        /// Checks if the neigbhours of a particular cell are counted correctly.
        /// Should be true if indexes are 0,0 and false if 0,1
        /// </summary>
        [Fact]
        public void NeighboursTest()
        {
            // Arrange
            int expected = 1;
            GameProgress testgame = new GameProgress() { Rows = 2, Columns = 2, Generation = new bool [,]{ { true, false }, { true, false }, }, };

            GameLogic gameLogic = new GameLogic();
            
            // Act
            gameLogic.NeigbourCounter(0, 1, testgame);

            int actual = gameLogic.neighbours;

            // Assert
            Assert.Equal(expected, actual);
            
        }
    }
}
