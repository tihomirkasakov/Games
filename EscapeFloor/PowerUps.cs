namespace EscapeFloor
{
    using System;
    using System.Collections.Generic;
    using static EscapeFloorGame;

    public class PowerUps
    {
        public static List<Positions> powerups = new List<Positions>();
        public static Random rng = new Random();
        public static int startTime;
        public static bool faster = false;
        public static bool slower = false;

        public static void GeneratePowerUps()
        {
            int powerupChance = rng.Next(0, 101);
            if (powerupChance<=30)
            {
                powerups.Add(new Positions(PLAYGROUND_HEIGHT - 2, rng.Next(1, PLAYGROUND_WIDTH), '$'));
            }
            else if (powerupChance <= 50)
            {
                powerups.Add(new Positions(PLAYGROUND_HEIGHT - 2, rng.Next(1, PLAYGROUND_WIDTH), '&'));
            }
            else if (powerupChance <= 100)
            {
                powerups.Add(new Positions(PLAYGROUND_HEIGHT - 2, rng.Next(1, PLAYGROUND_WIDTH), '@'));
            }
        }

        public static void DrawPowerUps()
        {
            for (int i = 0; i < powerups.Count; i++)
            {
                Console.SetCursorPosition(powerups[i].Y, powerups[i].X);
                if (powerups[i].Symbol == '@')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (powerups[i].Symbol == '&')
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (powerups[i].Symbol == '$')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write(powerups[i].Symbol);
                Console.ForegroundColor = ConsoleColor.White;

            }
        }

        public static void MovePowerUps()
        {
            for (int i = 0; i < powerups.Count; i++)
            {
                if (powerups[i].X!=0)
                {
                    powerups[i] = new Positions(powerups[i].X - 1, powerups[i].Y, powerups[i].Symbol);
                }
                else
                {
                    powerups[i] = new Positions(0, 0, ' ');
                }
            }
        }

    }
}
