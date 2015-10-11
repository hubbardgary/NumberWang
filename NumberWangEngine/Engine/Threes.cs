using NumberWang.Extensions;
using System;

namespace NumberWang
{
    public class Threes : BaseGameEngine
    {
        private const int BoardSize = 4;
        private const int InitialTileCount = 9;
        private static int[] SpawnNumbers = new int[] { 1, 2, 3, 3, 3 };

        public Threes() : base(BoardSize, InitialTileCount, SpawnNumbers) { }
        
        public override int Score()
        {
            int points = 0;
            Board.ForEachCell((i, j) =>
            {
                if(Board[i,j] % 3 == 0)
                {
                    /* 1 and 2 are worth nothing.
                     * Merged tiles start at 3 and double each time they're merged (3, 6, 12, 24, 48, etc).
                     * The point score of each tile is 3 to the power of the tile's position in the above sequence,
                     * so a 3 tile = 3^1 = 3 points, a 6 tile = 3^2 = 9 points, a 48 tile = 3^5 = 243 points.
                     * The formula for the above sequence is a(n) = 3*2^(n-1)
                     * Given a tile, its position in the sequence can be calculated by log(2x/3) / log(2).
                     */
                    double power = (Math.Log((2 * Board[i, j]) / 3)) / Math.Log(2);
                    points += (int)Math.Pow(3, power);
                }
            });
            return points;
        }

        protected override int MergeTiles(int a, int b)
        {
            // Tile b is sliding onto Tile a
            // Tiles are merged if the current tile in the target cell is 0, or they sum to 3, or they're of the same value of 3 or greater
            if (!(a == 0 && b == 0) &&
                (   (a == 0 && b != 0) ||
                    (a + b == 3) ||
                    (a == b && a >= 3 && b >= 3)))
            {
                return a + b;
            }
            return -1;
        }
    }
}
