using NumberWang.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace NumberWang
{
    public class Fives : BaseGameEngine
    {

        private const int BoardSize = 4;
        private const int InitialTileCount = 8;
        private static int[] InitSpawnNumbers = new int[] { 2, 3, 2, 3, 2, 3, 2, 3, 2, 3 };

        public Fives() : base(BoardSize, InitialTileCount, InitSpawnNumbers)
        {
            ScoreVisible = true;
        }

        public override int Score()
        {
            int points = 0;
            Board.ForEachCell((i, j) =>
            {
                if (Board[i, j] > 3)
                {
                    points += getTileScore(Board[i, j]);
                }
            });
            return points;
        }

        // Fives employs a seemingly fairly arbitrary score to each tile.
        private int getTileScore(int tile)
        {
            switch(tile)
            {
                case 5 :
                    return 10;
                case 10 :
                    return 25;
                case 20 :
                    return 65;
                case 40 :
                    return 170;
                case 80 :
                    return 395;
                case 160 :
                    return 975;
                case 320 :
                    return 2455;
                case 640 :
                    return 6540;
                case 1280 :
                    return 16750;
                case 2560 :
                    return 39500;
                case 5120 :
                    return 95500;
                case 10240 :
                    return 238750;
                case 20480 :
                    return 716250;
            }
            return 1432500;
        }

        protected override int MergeTiles(int a, int b)
        {
            // Tile b is sliding onto Tile a
            // Tiles are merged if the current tile in the target cell is 0, or they sum to 5, or they're of the same value of 5 or greater
            if (!(a == 0 && b == 0) &&
                ((a == 0 && b != 0) ||
                    (b != 0 && a + b == 5) ||
                    (a == b && a >= 5 && b >= 5)))
            {
                return a + b;
            }
            return -1;
        }

        public override bool Move(Direction dir)
        {
            bool moveResult = base.Move(dir);
            if (!SpawnNumbers.Contains(GetMaxNumber()))
            {
                AddToSpawnNumbers(GetMaxNumber());
                // Although a wider range of numbers can be spawned, we still want to usually spawn 2 or 3.
                AddToSpawnNumbers(2);
                AddToSpawnNumbers(3);
            }
            return moveResult;
        }

        private void AddToSpawnNumbers(int newNumber)
        {
            List<int> spawnNumbers = SpawnNumbers.ToList();
            spawnNumbers.Add(newNumber);
            SpawnNumbers = spawnNumbers.ToArray();
        }
    }
}
