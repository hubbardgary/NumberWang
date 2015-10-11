using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NumberWang.Tests
{
    [TestClass()]
    public class ThreesTests
    {
        Threes game = new Threes();

        [TestMethod()]
        public void Score_Test()
        {
            // ARRANGE
            int[,] board = new int[4, 4]
            {
                {3,6,96,6},
                {2,2,48,3},
                {2,12,1,1},
                {3,2,6,1}
            };

            game.Board = board;

            // ACT
            // ASSERT
            Assert.AreEqual(1035, game.Score());
        }
    }
}