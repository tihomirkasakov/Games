namespace SpacecraftWars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static SpacecraftWars;

    public class Obstacles
    {
        public static Random rng = new Random();
        public static int[,] obstacles = new int[PLAYFIELD_HEIGHT,PLAYFIELD_WIDTH];

        public static void GenerateObstacles()
        {
            int holeSize = size.Next(4,11);
            int startingPosition= rng.Next(0, PLAYFIELD_HEIGHT - holeSize);
            int startingCol = PLAYFIELD_WIDTH - 1;

            if (startingPosition % 3 == 0)
            {
                for (int i = 0; i < PLAYFIELD_HEIGHT; i++)
                {
                    obstacles[i,startingCol] = 1;
                }
                for (int i = startingPosition; i <= startingPosition + holeSize; i++)
                {
                    obstacles[i,startingCol] = 0;
                }
        }
            else
            {
                for (int i = 0; i<PLAYFIELD_HEIGHT; i++)
                {
                    obstacles[i, startingCol] = 0;
                }
                for (int i = startingPosition; i<startingPosition + holeSize; i++)
                {
                    obstacles[i, startingCol] = 1;
                }
            }
        }

        public static void DrawObstacles()
        {
            for (int i = 0; i < PLAYFIELD_HEIGHT; i++)
            {
                for (int j = 0; j < PLAYFIELD_WIDTH; j++)
                {
                    if (obstacles[i,j]==1)
                    {
                        Console.SetCursorPosition(j,i);
                        Console.Write('|');
                    }
                }
            }
        }

        public static void MoveObstacles()
        {
            for (int i = 0; i < PLAYFIELD_HEIGHT; i++)
            {
                for (int j = 0; j < PLAYFIELD_WIDTH; j++)
                {
                    if (obstacles[i, j] == 1)
                    {
                        obstacles[i, j] = 0;
                        obstacles[i, j - 1] = 1;
                    }
                }
            }
        }

        public static void ClearObstacles()
        {
            for (int i = 0; i < PLAYFIELD_HEIGHT; i++)
            {
                obstacles[i, 0] = 0;
            }
        }
    }
}
