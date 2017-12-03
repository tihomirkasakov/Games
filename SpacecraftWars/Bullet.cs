using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpacecraftWars.Spacecraft;

namespace SpacecraftWars
{
    public class Bullet
    {
        List<Position> bullets = new List<Position>();

        public static void DrawBullet()
        {
            Console.SetCursorPosition(elements[1].Y + 7, elements[1].X);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("o");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void MoveBullet()
        {

        }
    }
}
