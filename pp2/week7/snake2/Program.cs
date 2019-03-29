using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Program : GameState
    {
        static void Main(string[] args)
        {
            GameState gameState = new GameState();
            if (!end)
            {
                gameState.Run();
            }

            while (!end)
            {
                gameState.Draw();
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.F2:
                        gameState.Save();
                        break;
                    case ConsoleKey.F3:
                        gameState.Reset();
                        gameState = gameState.Load();
                        gameState.Run();
                        break;
                    default:
                        gameState.PressedKey(consoleKeyInfo);
                        break;
                }
            }
        }
    }
}