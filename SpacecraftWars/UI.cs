namespace SpacecraftWars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static SpacecraftWars;

    public class UI
    {
        public static void DrawUI()
        {
            for (int i = 0; i < PLAYFIELD_HEIGHT; i++)
            {
                Console.SetCursorPosition(PLAYFIELD_WIDTH,i);
                Console.Write('|');
            }
            Console.SetCursorPosition(PLAYFIELD_WIDTH + 3, 3);
            Console.Write($"Score: {score}");
            Console.SetCursorPosition(10, 1);
            Console.Write($"Bullets: {score}");

            if (score < 20)
            {
                Console.SetCursorPosition(PLAYFIELD_WIDTH + 3, 8);
                Console.Write($"Diff: {Difficulty.Easy}");
            }
            else if (score < 100)
            {
                Console.SetCursorPosition(PLAYFIELD_WIDTH + 3, 8);
                Console.Write($"Diff: {Difficulty.Medium}");
                difficulty = 85;
            }
            else
            {
                Console.SetCursorPosition(PLAYFIELD_WIDTH + 3, 8);
                Console.Write($"Diff: {Difficulty.Hard}");
                difficulty = 65;
            }

        }
    }
}
