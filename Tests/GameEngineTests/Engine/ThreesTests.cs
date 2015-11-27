using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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



        [TestMethod()]
        public void Move_Left_When_Board_Is_Full_Test()
        {
            // ARRANGE
            int[,] preMove = new int[4, 4]
            {
                {1,12,1,1},
                {1,24,1,12},
                {12,48,96,1},
                {1,2,48,12}
            };

            int[,] expectedPostMove = new int[4, 4]
            {
                {1,12,1,1},
                {1,24,1,12},
                {12,48,96,1},
                {3,48,12,0}
            };

            game.Board = preMove;

            // ACT
            game.Move(Direction.Left);

            //ASSERT
            for (int i = 0; i <= expectedPostMove.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= expectedPostMove.GetUpperBound(1); j++)
                {
                    if (i == game.Board.GetUpperBound(0) && j == game.Board.GetUpperBound(1))
                    {
                        // New tile should be spawned in bottom right corner
                        Assert.IsTrue(game.SpawnNumbers.Contains(game.Board[i, j]));
                    }
                    else
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
                {1,3,0,0},
                {1,0,0,0},
                {12,48,96,1},
                {1,0,0,0}
            };

            int[,] expectedPostMove = new int[4, 4]
            {
                {1,3,0,0},
                {1,0,0,0},
                {12,48,96,1},
                {1,0,0,0}
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