using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NumberWang.Tests
{
    [TestClass]
    public class TwentyFortyEightTests
    {
        TwentyFortyEight game = new TwentyFortyEight();

        [TestMethod()]
        public void Score_Test()
        {
            // ARRANGE
            int[,] preMove = new int[4, 4]
            {
                {2,2,4,4},
                {8,8,16,16},
                {32,32,64,64},
                {128,128,256,256}
            };

            game.Board = preMove;

            // ACT
            game.Move(Direction.Left);

            // ASSERT
            Assert.AreEqual(1020, game.Score());
        }
        
        [TestMethod()]
        public void Move_Left_When_Board_Is_Full_Test()
        {
            // ARRANGE
            int[,] preMove = new int[4, 4]
            {
                {2,4,8,4},
                {2,4,8,4},
                {2,4,8,4},
                {2,2,4,8}
            };

            int[,] expectedPostMove = new int[4, 4]
            {
                {2,4,8,4},
                {2,4,8,4},
                {2,4,8,4},
                {4,4,8,0}
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
                {2,4,0,0},
                {4,0,0,0},
                {8,16,4,2},
                {2,0,0,0}
            };

            int[,] expectedPostMove = new int[4, 4]
            {
                {2,4,0,0},
                {4,0,0,0},
                {8,16,4,2},
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
