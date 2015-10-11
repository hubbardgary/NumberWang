using System;

namespace NumberWang.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Rotate a 2D array clockwise the specified amount of degrees.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceArray"></param>
        /// <param name="degrees">The rotation amount. Must be 90, 180, 270 or 360.</param>
        /// <returns></returns>
        public static T[,] RotateClockwise<T>(this T[,] sourceArray, int degrees)
        {
            if (degrees > 360 || degrees % 90 != 0)
            {
                throw new NotSupportedException(String.Format("Rotation is only possible through 90, 180, 270 degrees, or 360. Invoked with 'degrees = {0}'", degrees.ToString()));
            }

            if (degrees == 0 || degrees == 360)
            {
                return sourceArray;
            }

            int len = sourceArray.GetLength(0);
            T[,] rotated = new T[len, len];

            for (int x = 0; x < len; x++)
            {
                for (int y = 0; y < len; y++)
                {
                    if (degrees == 90)
                    {
                        rotated[y, 3 - x] = sourceArray[x, y];
                    }
                    else if (degrees == 180)
                    {
                        rotated[3 - x, 3 - y] = sourceArray[x, y];
                    }
                    else if (degrees == 270)
                    {
                        rotated[3 - y, x] = sourceArray[x, y];
                    }
                }
            }
            return rotated;
        }

        /// <summary>
        /// Helper method to perform a given action on each cell in a 2D array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="action"></param>
        public static void ForEachCell<T>(this T[,] array, Action<int, int> action)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    action(i, j);
                }
            }
        }

        /// <summary>
        /// Helper method to perform a given action on each adjacent cell within a 2D array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="action"></param>
        public static void ForEachAdjacentCell<T>(this T[,] array, int i, int j, Action<int, int> action)
        {
            for (int i1 = i - 1; i1 <= i + 1; i1++)
            {
                for (int j1 = j - 1; j1 <= j + 1; j1++)
                {
                    if (array.InBounds(i1, j1) && !(i1 == i && j1 == j) && !(i1 != i && j1 != j))
                    {
                        action(i1, j1);
                    }
                }
            }
        }

        /// <summary>
        /// Check whether the given cell is within the bounds of the array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static bool InBounds<T>(this T[,] array, int i, int j)
        {
            return i >= 0 && i < array.GetLength(0) && j >= 0 && j < array.GetLength(1);
        }
    }
}
