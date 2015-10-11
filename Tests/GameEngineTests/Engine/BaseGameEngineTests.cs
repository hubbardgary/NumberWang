using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NumberWang.Tests
{
    [TestClass()]
    public class BaseGameEngineTests
    {
        // All tests in this class assume the rules of Eights.
        Eights game = new Eights();

        [TestMethod()]
        public void Move_Right_Test()
        {
            // ARRANGE
            int[,] preMove = new int[4, 4]
            {
                {3,0,0,0},
                {0,0,8,8},
                {0,0,5,0},
                {0,8,8,0}
            };

            game.Board = preMove;

            // ACT
            game.Move(Direction.Right);

            //ASSERT
            Assert.AreEqual(3, game.Board[0, 1]);
            Assert.AreEqual(16, game.Board[1, 3]);
            Assert.AreEqual(5, game.Board[2, 3]);
            Assert.AreEqual(8, game.Board[3, 2]);
            Assert.AreEqual(8, game.Board[3, 3]);
        }

        [TestMethod()]
        public void Move_Left_Test()
        {
            // ARRANGE
            int[,] preMove = new int[4, 4]
            {
                {0,0,0,3},
                {8,8,0,0},
                {0,5,0,0},
                {0,8,8,0}
            };

            game.Board = preMove;

            // ACT
            game.Move(Direction.Left);

            //ASSERT
            Assert.AreEqual(3, game.Board[0, 2]);
            Assert.AreEqual(16, game.Board[1, 0]);
            Assert.AreEqual(5, game.Board[2, 0]);
            Assert.AreEqual(8, game.Board[3, 0]);
            Assert.AreEqual(8, game.Board[3, 1]);
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
                for (int j = 0; j <= expectedPostMove.GetUpperBound(1); j++)
                {
                    if (i != 3 && j != 3)
                    {
                        Assert.AreEqual(expectedPostMove[i, j], game.Board[i, j]);
                    }
                }
        }

        [TestMethod()]
        public void Move_Up_Test()
        {
            // ARRANGE
            int[,] preMove = new int[4, 4]
            {
                {3,8,0,0},
                {0,8,0,8},
                {0,0,5,8},
                {0,0,0,0}
            };

            game.Board = preMove;

            // ACT
            game.Move(Direction.Up);

            //ASSERT
            Assert.AreEqual(3, game.Board[0, 0]);
            Assert.AreEqual(16, game.Board[0, 1]);
            Assert.AreEqual(5, game.Board[1, 2]);
            Assert.AreEqual(8, game.Board[0, 3]);
            Assert.AreEqual(8, game.Board[1, 3]);
        }

        [TestMethod()]
        public void Move_Down_Test()
        {
            // ARRANGE
            int[,] preMove = new int[4, 4]
            {
                {0,0,0,0},
                {0,0,5,8},
                {0,8,0,8},
                {3,8,0,0}
            };

            game.Board = preMove;

            // ACT
            game.Move(Direction.Down);

            //ASSERT
            Assert.AreEqual(3, game.Board[3, 0]);
            Assert.AreEqual(16, game.Board[3, 1]);
            Assert.AreEqual(5, game.Board[2, 2]);
            Assert.AreEqual(8, game.Board[2, 3]);
            Assert.AreEqual(8, game.Board[3, 3]);
        }

        [TestMethod()]
        public void Game_Over_Test()
        {
            // ARRANGE
            game.Board = new int[4, 4]
            {
                {3,3,3,3},
                {3,3,3,3},
                {3,3,3,3},
                {3,3,3,3}
            };

            // ACT
            // ASSERT
            Assert.AreEqual(true, game.GameOver());
        }

        [TestMethod()]
        public void Game_Not_Over_Test()
        {
            // ARRANGE
            game.Board = new int[4, 4]
            {
                {3,3,5,3},
                {3,3,3,3},
                {3,3,3,3},
                {3,3,3,3}
            };

            // ACT
            // ASSERT
            Assert.AreEqual(false, game.GameOver());
        }

        [TestMethod()]
        public void Get_Max_Number_Test()
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
            Assert.AreEqual(256, game.GetMaxNumber());
        }

        [TestMethod()]
        public void Get_Occurances_Of_Test()
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
            Assert.AreEqual(8, game.GetOccurancesOf(3));
        }
    }
}