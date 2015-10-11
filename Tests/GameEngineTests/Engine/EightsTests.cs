using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NumberWang.Tests
{
    [TestClass()]
    public class EightsTests
    {
        Eights game = new Eights();

        [TestMethod()]
        public void Score_Test()
        {
            // ARRANGE
            game.Board = new int[4, 4]
            {
                {3,16,3,8},
                {3,3,64,3},
                {3,3,16,256},
                {16,3,32,16}
            };

            // ACT
            // ASSERT
            Assert.AreEqual(704, game.Score());
        }
    }
}