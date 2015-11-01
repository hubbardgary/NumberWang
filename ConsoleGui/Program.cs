using System;

namespace NumberWang.Gui
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                IGameGui gameGui = new ConsoleGui(new TwentyFortyEight());
                gameGui.Play();
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
