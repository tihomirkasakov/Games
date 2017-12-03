namespace EscapeFloor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static EscapeFloorGame;

    public class Player
    {
        public static List<Positions> playerPosition = new List<Positions>();
        public static bool isMoveDown = true;

        public static void InitializePlayer()
        {
            playerPosition.Add(new Positions(PLAYGROUND_HEIGHT / 2, PLAYGROUND_WIDTH / 2, 'O'));
            playerPosition.Add(new Positions(PLAYGROUND_HEIGHT / 2, PLAYGROUND_WIDTH / 2 - 1, '_'));
            playerPosition.Add(new Positions(PLAYGROUND_HEIGHT / 2, PLAYGROUND_WIDTH / 2 + 1, '_'));
            playerPosition.Add(new Positions(PLAYGROUND_HEIGHT / 2 + 1, PLAYGROUND_WIDTH / 2, '|'));
            playerPosition.Add(new Positions(PLAYGROUND_HEIGHT / 2 + 2, PLAYGROUND_WIDTH / 2 - 1, '/'));
            playerPosition.Add(new Positions(PLAYGROUND_HEIGHT / 2 + 2, PLAYGROUND_WIDTH / 2 + 1, '\\'));
        }

        public static void MovePlayerHorizontal()
        {
            if (Console.KeyAvailable)
            {
                //hack
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                while (Console.KeyAvailable) Console.ReadKey(true);
                if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    if (playerPosition[0].Y - 3 >= 0)
                    {
                        for (int i = 0; i < playerPosition.Count; i++)
                        {
                            playerPosition[i] = new Positions(playerPosition[i].X, playerPosition[i].Y - 3, playerPosition[i].Symbol);
                        }
                    }
                }
                else if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    if (playerPosition[0].Y + 3 <= PLAYGROUND_WIDTH)
                    {
                        for (int i = 0; i < playerPosition.Count; i++)
                        {
                            playerPosition[i] = new Positions(playerPosition[i].X, playerPosition[i].Y + 3, playerPosition[i].Symbol);
                        }
                    }
                }
            }
        }

        public static void MovePlayerVertically()
        {
            if (moveVertically)
            {
                if (playerPosition.Last().X+2 < PLAYGROUND_HEIGHT)
                {
                    for (int i = 0; i < playerPosition.Count; i++)
                    {
                        playerPosition[i] = new Positions(playerPosition[i].X + 1, playerPosition[i].Y, playerPosition[i].Symbol);
                    }
                }
            }
            else
            {
                for (int i = 0; i < playerPosition.Count; i++)
                {
                    playerPosition[i] = new Positions(playerPosition[i].X - 1, playerPosition[i].Y, playerPosition[i].Symbol);
                }
            }
        }
        public static void DrawPlayer()
        {
            for (int i = 0; i < playerPosition.Count; i++)
            {
                Console.SetCursorPosition(playerPosition[i].Y, playerPosition[i].X);
                Console.Write(playerPosition[i].Symbol);
            }
        }
    }
}
