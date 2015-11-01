using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberWang.Gui
{
    public class ConsoleGui : IGameGui
    {
        IGameEngine game;
        Dictionary<ConsoleKey, Direction> ControlMap = new Dictionary<ConsoleKey, Direction>()
        {
            { ConsoleKey.UpArrow, Direction.Up },
            { ConsoleKey.DownArrow, Direction.Down },
            { ConsoleKey.LeftArrow, Direction.Left },
            { ConsoleKey.RightArrow, Direction.Right }
        };

        public ConsoleGui(IGameEngine game)
        {
            this.game = game;
        }

        public void Play()
        {
            DrawBoard(game);

            while (!game.GameOver())
            {
                ConsoleKey key = Console.ReadKey().Key;
                bool moved = false;
                if (ControlMap.Keys.Contains(key))
                    moved = game.Move(ControlMap[key]);

                if (moved)
                    DrawBoard(game);

                FlushKeyBuffer();
            }
            GameOver();
        }

        public void DrawBoard(IGameEngine game)
        {
            Console.Clear();
            ShowBoard(game);
        }

        private void ShowBoard(IGameEngine game)
        {
            int max = game.GetMaxNumber();
            int cellSize = max.ToString().Length + 1;   // +1 for space between cells

            string s = "";

            for (int x = 0; x < game.Board.GetLength(0); x++)
            {
                for (int y = 0; y < game.Board.GetLength(1); y++)
                {
                    string cell = game.Board[x, y].ToString();
                    while (cell.Length < cellSize)
                        cell += " ";
                    s += cell;
                }
                s += Environment.NewLine;
            }
            s += Environment.NewLine;
            s += "Next number: " + game.NextNumber.ToString();
            s += Environment.NewLine;
            Console.WriteLine(s);
        }

        private void GameOver()
        {
            Console.WriteLine("GAME OVER" + Environment.NewLine +
               "Score: " + game.Score().ToString() + Environment.NewLine + Environment.NewLine +
               "Press Esc to exit." + Environment.NewLine +
               "Press any other key to play again.");
        }

        private void FlushKeyBuffer()
        {
            // Used for preventing processing of any buffered input.
            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }
    }
}
