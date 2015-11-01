using NumberWang.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberWang
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        None
    }

    public abstract class BaseGameEngine : IGameEngine
    {
        private readonly int InitialTileCount;
        private readonly int BoardSize;

        public int[,] Board { get; set; }
        public int[,] MoveMatrix { get; set; }
        private int[] SpawnNumbers { get; set; }
        public Random MyRandom { get; set; }
        public int NextNumber { get; set; }

        public struct Coordinate
        {
            public int x, y;

            public Coordinate(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        // Determines how to rotate the board to perform 'ShiftTilesLeft()'
        Dictionary<Direction, int> RotationMap = new Dictionary<Direction, int>()
        {
            { Direction.Up, 270 },
            { Direction.Right, 180 },
            { Direction.Down, 90 },
            { Direction.Left, 0 }
        };

        public BaseGameEngine(int boardSize, int tileCount, int[] spawnNumbers)
        {
            BoardSize = boardSize;
            InitialTileCount = tileCount;
            SpawnNumbers = spawnNumbers;
            SetBoard();
            NextNumber = PickANumber();
        }

        private void SetBoard()
        {
            Board = new int[BoardSize, BoardSize];
            MoveMatrix = new int[BoardSize, BoardSize];
            MyRandom = new Random();
            int tilesLaid = 0;

            do
            {
                int x = MyRandom.Next(BoardSize);
                int y = MyRandom.Next(BoardSize);
                if (Board[x, y] == 0)
                {
                    Board[x, y] = PickANumber();
                    tilesLaid++;
                }
            } while (tilesLaid < InitialTileCount);
        }
        
        /// <summary>
        /// Calculates the score for the board in its current state.
        /// </summary>
        /// <returns></returns>
        public abstract int Score();
        
        protected abstract int MergeTiles(int a, int b);

        protected virtual bool ShiftTilesLeft()
        {
            bool moved = false;
            for (int x = 0; x < Board.GetLength(0); x++)
            {
                for (int y = 0; y < Board.GetLength(1) - 1; y++)
                {
                    int newValue = MergeTiles(Board[x, y], Board[x, y + 1]);
                    if (newValue != -1)
                    {
                        Board[x, y] = newValue;
                        Board[x, y + 1] = 0;
                        moved = true;
                        MoveMatrix[x, y + 1] = 1; // Moved
                    }
                }
            }
            return moved;
        }

        /// <summary>
        /// Returns the value of the highest valued tile currently on the board.
        /// </summary>
        /// <returns></returns>
        public int GetMaxNumber()
        {
            return (from int tile in Board
                    select tile).Max();
        }

        /// <summary>
        /// Returns the amount of tiles currently on the board with the specified value.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int GetOccurancesOf(int n)
        {
            int occurences = 0;
            Board.ForEachCell((i, j) =>
            {
                if (Board[i, j] == n)
                {
                    occurences++;
                }
            });
            return occurences;
        }

        /// <summary>
        /// Applies the specified move to the board.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public bool Move(Direction dir)
        {
            MoveMatrix.ForEachCell((i, j) =>
            {
                MoveMatrix[i, j] = 0;
            });
            Board = RotateBoardBeforeMove(dir);
            bool moved = ShiftTilesLeft();

            if (moved)
            {
                SpawnNewTile();
                NextNumber = PickANumber();
            }

            Board = RotateBoardAfterMove(dir);
            MoveMatrix = MoveMatrix.RotateClockwise(360 - RotationMap[dir]);
            return moved;
        }

        /// <summary>
        /// Returns whether the game is still in progress or not.
        /// </summary>
        /// <returns></returns>
        public bool GameOver()
        {
            if (CanMove())
            {
                return false;
            }
            return true;
        }

        private bool CanMove()
        {
            bool canMove = false;
            Board.ForEachCell((i, j) =>
            {
                Board.ForEachAdjacentCell(i, j, (i1, j1) =>
                {
                    if (MergeTiles(Board[i, j], Board[i1, j1]) != -1)
                    {
                        canMove = true;
                    }
                });
            });
            return canMove;
        }

        private int[,] RotateBoardBeforeMove(Direction dir)
        {
            return Board.RotateClockwise(RotationMap[dir]);
        }

        private int[,] RotateBoardAfterMove(Direction dir)
        {
            return Board.RotateClockwise(360 - RotationMap[dir]);
        }

        private void SpawnNewTile()
        {
            Coordinate newTile = PickACoordinate();
            Board[newTile.x, newTile.y] = NextNumber;
        }

        private int PickANumber()
        {
            return SpawnNumbers[MyRandom.Next(SpawnNumbers.Length)];
        }

        public virtual Coordinate PickACoordinate()
        {
            // Only consider vacant cells in the relevant row
            var vacantCells = new List<int>();
            for (int i = 0; i <= Board.GetUpperBound(0); i++)
            {
                if (Board[i, Board.GetUpperBound(0)] == 0)
                {
                    vacantCells.Add(i);
                }
            }
            return new Coordinate(
                vacantCells[MyRandom.Next(vacantCells.Count - 1)],
                Board.GetUpperBound(0));
        }
    }
}
