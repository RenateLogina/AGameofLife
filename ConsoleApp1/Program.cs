using System;
using System.Linq;

namespace GameofLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Game game = new Game();
            game.GameMenu();
        }
    }
}
