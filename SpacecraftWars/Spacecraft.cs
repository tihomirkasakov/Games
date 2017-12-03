namespace SpacecraftWars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static SpacecraftWars;
    using static Bullet;

    public class Spacecraft
    {
        public static List<Position> elements = new List<Position>();

        public static void InitializeSpacecraft()
        {
            elements.Add(new Position(PLAYFIELD_HEIGHT/ 2 - 1, 0, @"  \\\  "));
            elements.Add(new Position(PLAYFIELD_HEIGHT / 2 , 0, @"=>==->>"));
            elements.Add(new Position(PLAYFIELD_HEIGHT / 2 +1, 0, @"  ///  "));
        }

        public static void DrawSpacecraft()
        {
            for (int i = 0; i < elements.Count; i++)
            {
                Console.SetCursorPosition(elements[i].Y, elements[i].X);
                Console.Write(elements[i].Elements);
            }
        }

        public static void MoveSpacecraft()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                while (Console.KeyAvailable) Console.ReadKey(true);
                if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    if (elements[0].X-2 >= 0)
                    {
                        for (int i = 0; i < elements.Count; i++)
                        {
                            elements[i] = new Position(elements[i].X-2, elements[i].Y, elements[i].Elements);
                        }
                    }
                }
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    if (elements[0].X+4  <= PLAYFIELD_HEIGHT)
                    {
                        for (int i = 0; i < elements.Count; i++)
                        {
                            elements[i] = new Position(elements[i].X+2, elements[i].Y, elements[i].Elements);
                        }
                    }
                }
                else if (pressedKey.Key == ConsoleKey.Spacebar)
                {
                    DrawBullet();
                }
            }

        }
    }
}
