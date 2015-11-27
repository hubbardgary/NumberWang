using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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



        [TestMethod()]
        public void Move_Left_When_Board_Is_Full_Test()
        {
            // ARRANGE
            int[,] preMove = new int[4, 4]
            {
                {3,8,3,3},
                {3,32,3,16},
                {16,64,128,3},
                {3,5,64,16}
            };

            int[,] expectedPostMove = new int[4, 4]
            {
                {3,8,3,3},
                {3,32,3,16},
                {16,64,128,3},
                {8,64,16,0}
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
                {3,8,0,0},
                {3,0,0,0},
                {16,64,128,3},
                {3,0,0,0}
            };

            int[,] expectedPostMove = new int[4, 4]
            {
                {3,8,0,0},
                {3,0,0,0},
                {16,64,128,3},
                {3,0,0,0}
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