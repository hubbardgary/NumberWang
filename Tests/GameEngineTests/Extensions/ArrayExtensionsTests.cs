﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NumberWang.Extensions.Tests
{
    [TestClass()]
    public class ArrayExtensionsTests
    {
        string notSupportedMessage = "Rotation is only possible through 90, 180, 270 degrees, or 360. Invoked with 'degrees = {0}'";

        int[,] startMatrix = new int[4, 4]
            {
                {10,11,12,13},
                {20,21,22,23},
                {30,31,32,33},
                {40,41,42,43}
            };

        [TestMethod()]
        [ExpectedException(typeof(NotSupportedException))]
        public void RotateClockwise_Non_90_Degree_Angle_ThrowsException()
        {
            // ARRANGE
            int degrees = 89;

            try
            {
                // ACT
                int[,] result = startMatrix.RotateClockwise(degrees);
            }
            catch (Exception ex)
            {
                //ASSERT
                Assert.AreEqual(String.Format(notSupportedMessage, degrees.ToString()), ex.Message);
                throw;
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(NotSupportedException))]
        public void RotateClockwise_Greater_Than_360_Degree_Angle_ThrowsException()
        {
            // ARRANGE
            int degrees = 450;

            try
            {
                // ACT
                int[,] result = startMatrix.RotateClockwise(degrees);
            }
            catch (Exception ex)
            {
                //ASSERT
                Assert.AreEqual(String.Format(notSupportedMessage, degrees.ToString()), ex.Message);
                throw;
            }
        }

        [TestMethod()]
        public void RotateClockwise_90_Degrees_Test()
        {
            // ARRANGE
            int[,] rotated90Expected = new int[4, 4]
            {
                {40,30,20,10},
                {41,31,21,11},
                {42,32,22,12},
                {43,33,23,13}
            };

            // ACT
            int[,] endMatrix = startMatrix.RotateClockwise(90);

            // ASSERT
            int len = startMatrix.GetLength(0);
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    Assert.AreEqual(rotated90Expected[i, j], endMatrix[i, j]);
                }
            }
        }

        [TestMethod()]
        public void RotateClockwise_180_Degrees_Test()
        {
            // ARRANGE
            int[,] rotated180Expected = new int[4, 4]
            {
                {43,42,41,40},
                {33,32,31,30},
                {23,22,21,20},
                {13,12,11,10}
            };

            // ACT
            int[,] endMatrix = startMatrix.RotateClockwise(180);

            // ASSERT
            int len = startMatrix.GetLength(0);
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    Assert.AreEqual(rotated180Expected[i, j], endMatrix[i, j]);
                }
            }
        }

        [TestMethod()]
        public void RotateClockwise_270_Degrees_Test()
        {
            // ARRANGE
            int[,] rotated270Expected = new int[4, 4]
            {
                {13,23,33,43},
                {12,22,32,42},
                {11,21,31,41},
                {10,20,30,40}
            };

            // ACT
            int[,] endMatrix = startMatrix.RotateClockwise(270);

            // ASSERT
            int len = startMatrix.GetLength(0);
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    Assert.AreEqual(rotated270Expected[i, j], endMatrix[i, j]);
                }
            }
        }

        [TestMethod()]
        public void RotateClockwise_360_Degrees_Test()
        {
            // ARRANGE
            int[,] rotated360Expected = startMatrix;

            // ACT
            int[,] endMatrix = startMatrix.RotateClockwise(360);

            // ASSERT
            int len = startMatrix.GetLength(0);
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    Assert.AreEqual(rotated360Expected[i, j], endMatrix[i, j]);
                }
            }
        }

        [TestMethod()]
        public void ForEachCell_Test()
        {
            // ARRANGE
            int[,] grid = new int[4, 4]
            {
                {1,2,3,4},
                {5,6,7,8},
                {9,10,11,12},
                {13,14,15,16}
            };

            int counter = 1;

            // ACT
            grid.ForEachCell((i, j) =>
            {
                // ASSERT
                Assert.AreEqual(counter, grid[i, j]);
                counter++;
            });
        }

        [TestMethod()]
        public void ForEachAdjacentCell_Test()
        {
            // ARRANGE
            int[,] grid = new int[4, 4]
            {
                {1,2,3,4},
                {5,6,7,8},
                {9,10,11,12},
                {13,14,15,16}
            };

            // ACT
            int i = 1;
            int j = 1;
            grid.ForEachAdjacentCell(i, j, (i1, j1) =>
            {
                // ASSERT - only horizontal and vertical neighbours should be considered
                Assert.IsFalse(i1 == i && j1 == j);
            });
        }

        [TestMethod()]
        public void InBounds_Test()
        {
            // ARRANGE
            int[,] grid = new int[4, 4]
            {
                {1,2,3,4},
                {5,6,7,8},
                {9,10,11,12},
                {13,14,15,16}
            };

            int min = 0;
            int max = grid.GetUpperBound(0);

            // ASSERT
            Assert.IsTrue(grid.InBounds(min, min));
            Assert.IsTrue(grid.InBounds(min, max));
            Assert.IsTrue(grid.InBounds(max, min));
            Assert.IsTrue(grid.InBounds(max, max));

            Assert.IsFalse(grid.InBounds(min - 1, min));
            Assert.IsFalse(grid.InBounds(min, min - 1));
            Assert.IsFalse(grid.InBounds(min - 1, min - 1));
            Assert.IsFalse(grid.InBounds(min, max + 1));
            Assert.IsFalse(grid.InBounds(max + 1, min));
            Assert.IsFalse(grid.InBounds(max + 1, max + 1));
        }
    }
}