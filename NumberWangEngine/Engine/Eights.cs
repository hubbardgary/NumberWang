using NumberWang.Extensions;

namespace NumberWang
{
    public class Eights : BaseGameEngine
    {
        private const int BoardSize = 4;
        private const int InitialTileCount = 4;
        private static int[] InitSpawnNumbers = new int[] { 3, 5, 8, 8, 8 };

        public Eights() : base(BoardSize, InitialTileCount, InitSpawnNumbers)
        {
            ScoreVisible = false;
        }

        public override int Score()
        {
            int points = 0;
            Board.ForEachCell((i, j) =>
            {
                points += Board[i, j];
            });

            // Add the largest number to the final score
            points += GetMaxNumber();
            return points;
        }

        protected override int MergeTiles(int a, int b)
        {
            // Tile b is sliding onto Tile a
            // Tiles will merge if the current tile in the target cell is 0, or they sum to 8, or they're of the same value of 8 or greater
            if (!(a == 0 && b == 0) &&
                ( (a == 0 && b != 0) ||
                  (b != 0 && a + b == 8) ||
                  (a == b && a >= 8 && b >= 8)))
            {
                return a + b;
            }
            return -1;
        }
    }
}
