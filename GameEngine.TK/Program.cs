using GameEngine.TK.Core;
using System;

namespace GameEngine.TK
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game game = new TestGame("Test", 800, 600);
            game.Run();
        }
    }
}