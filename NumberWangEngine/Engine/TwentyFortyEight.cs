using NumberWang.Extensions;
using System.Collections.Generic;

namespace NumberWang
{
    public class TwentyFortyEight : BaseGameEngine
    {
        private const int BoardSize = 4;
        private const int InitialTileCount = 3;
        private static int[] SpawnNumbers = new int[] { 2, 4 };
        private int[,] MergeMatrix = new int[BoardSize, BoardSize];
        private int score = 0;

        public TwentyFortyEight() : base(BoardSize, InitialTileCount, SpawnNumbers)
        {
            NextNumberVisible = false;
        }
        
        public override int Score()
        {
            return score;
        }

        private void UpdateScore(int points)
        {
            score += points;
        }

        protected override int MergeTiles(int a, int b)
        {
            // b is sliding onto a
            if (a == 0)
            {
                return b;
            }
            else if (a == b)
            {
                return a + b;
            }
            return -1;
        }

        protected override bool ShiftTilesLeft()
        {
            bool moved = false;
            ResetMergeMatrix();

            Board.ForEachRow((row) =>
            {
                for (int col = 1; col < Board.GetLength(1); col++)
                {
                    if (AttemptToMoveTileLeft(row, col))
                    {
                        moved = true;
                    }
                }
            });
            // Calculate score by adding the value of all tiles merged in this move
            MergeMatrix.ForEachCell((i, j) =>
            {
                if (MergeMatrix[i, j] == 1)
                    UpdateScore(Board[i, j]);
            });
            return moved;
        }

        private bool AttemptToMoveTileLeft(int row, int col)
        {
            if (Board[row, col] != 0)
            {
                // Moving a non-zero tile so continue
                int destination = FindDestination(row, col);
                if (destination != col)
                {
                    // The tile can be moved, so calculate new tile value
                    if (Board[row, destination] != 0)
                    {
                        MergeMatrix[row, destination] = 1;
                    }
                    // Set new destination tile value
                    Board[row, destination] = MergeTiles(Board[row, destination], Board[row, col]);
                    // Reset tile's previous location
                    Board[row, col] = 0;
                    // Update MoveMatrix with distance moved to assist with animation calculation
                    MoveMatrix[row, col] = col - destination;
                    return true;
                }
            }
            return false;
        }

        private void ResetMergeMatrix()
        {
            MergeMatrix.ForEachCell((i, j) =>
            {
                MergeMatrix[i, j] = 0;
            });
        }

        private int FindDestination(int row, int col)
        {
            int destination = col;

            // Shifting left, so we want to consider each cell in the row to the left of the source cell
            for (int j = col - 1; j >= 0; j--)
            {
                if (MergeMatrix[row, j] == 1)
                {
                    // A tile has already merged here in this turn so its out of bounds
                    break;
                }
                else if (Board[row, j] == 0)
                {
                    // This is good, but there might be better
                    destination = j;
                    continue;
                }
                else if (Board[row, j] != Board[row, col])
                {
                    // Occupied by a non-matching tile, so can't land here
                    break;
                }
                else if (Board[row, j] == Board[row, col])
                {
                    // Occupied by identical tile. We will merge with this one so stop looking
                    destination = j;
                    break;
                }
            }
            return destination;
        }

        public override Coordinate PickACoordinate()
        {
            // Consider all vacant cells
            var vacantCells = new List<Coordinate>();
            Board.ForEachCell((i, j) =>
            {
                if (Board[i, j] == 0)
                    vacantCells.Add(new Coordinate(i, j));
            });
            return vacantCells[MyRandom.Next(vacantCells.Count - 1)];
        }
    }
}
