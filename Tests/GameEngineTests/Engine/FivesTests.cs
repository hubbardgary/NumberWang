using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NumberWang.Tests
{
    [TestClass]
    public class FivesTests
    {
        Fives game = new Fives();

        [TestMethod()]
        public void Score_Test()
        {
            // ARRANGE
            int[,] board = new int[4, 4]
            {
                {2,3,5,10},
                {20,40,80,160},
                {320,640,1280,2560},
                {5120,10240,20480,40960}
            };

            game.Board = board;

            // ACT
            // ASSERT
            Assert.AreEqual(2549885, game.Score());
        }



        [TestMethod()]
        public void Move_Left_When_Board_Is_Full_Test()
        {
            // ARRANGE
            int[,] preMove = new int[4, 4]
            {
                {2,10,2,2},
                {2,20,2,10},
                {10,20,40,2},
                {2,3,80,10}
            };

            int[,] expectedPostMove = new int[4, 4]
            {
                {2,10,2,2},
                {2,20,2,10},
                {10,20,40,2},
                {5,80,10,0}
            };

            game.Board = preMove;

            // ACT
            game.Move(Direction.Left);

            //ASSERT
            for (int i = 0; i <= expectedPostMove.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= expectedPostMove.GetUpperBound(1); j++)
                {
                    // Don't check bottom right corner as random tile should have spawned there
                    if (i != 3 && j != 3)
                    {
                        Assert.AreEqual(expectedPostMove[i, j], game.Board[i, j]);
                    }
                }
            }
        }

        [TestMethod()]
        public void Move_Left_When_Cant_Move_Left_Test()
        {
            // ARRANGE
            int[,] preMove = new int[4, 4]
            {
                {2,5,0,0},
                {2,0,0,0},
                {10,20,40,2},
                {2,0,0,0}
            };

            int[,] expectedPostMove = new int[4, 4]
            {
                {2,5,0,0},
                {2,0,0,0},
                {10,20,40,2},
                {2,0,0,0}
            };

            game.Board = preMove;

            // ACT
            game.Move(Direction.Left);

            //ASSERT
            for (int i = 0; i <= expectedPostMove.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= expectedPostMove.GetUpperBound(1); j++)
                {
                    Assert.AreEqual(expectedPostMove[i, j], game.Board[i, j]);
                }
            }
        }
    }
}
