namespace EscapeFloor
{
    using System;
    using static EscapeFloorGame;

    public class UI
    {
        public static void DrawUI()
        {
            for (int i = 0; i < PLAYGROUND_HEIGHT; i++)
            {
                Console.SetCursorPosition(PLAYGROUND_WIDTH, i);
                Console.Write('|');
            }
            Console.SetCursorPosition(PLAYGROUND_WIDTH + 1, 3);
            Console.Write($"Score: {score}");
            if (score<20)
            {
                Console.SetCursorPosition(PLAYGROUND_WIDTH + 1, 8);
                Console.Write($"Diff: {Difficulty.Easy}");
            }
            else if (score<100)
            {
                Console.SetCursorPosition(PLAYGROUND_WIDTH + 1, 8);
                Console.Write($"Diff: {Difficulty.Medium}");
                difficulty = 85;
                holeSize = 8;
            }
            else
            {
                Console.SetCursorPosition(PLAYGROUND_WIDTH + 1, 8);
                Console.Write($"Diff: {Difficulty.Hard}");
                difficulty = 65;
                holeSize = 6;
            }
        }
    }
}
