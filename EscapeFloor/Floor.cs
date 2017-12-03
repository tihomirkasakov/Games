namespace EscapeFloor
{
    using System;
    using static EscapeFloor.EscapeFloorGame;

    public class Floor
    {
        public static Random rng = new Random();
        public static int[,] floor = new int[PLAYGROUND_HEIGHT, PLAYGROUND_WIDTH];
        public static int skipFloor = 4;

        public static void GenerateFloor()
        {
            if (skipFloor>=3)
            {
                int startingPosition = rng.Next(0, PLAYGROUND_WIDTH - holeSize);
                int startingRow = PLAYGROUND_HEIGHT - 1;
                for (int i = 0; i < PLAYGROUND_WIDTH; i++)
                {
                    floor[startingRow, i] = 1;
                }

                for (int i = startingPosition; i <= startingPosition + holeSize; i++)
                {
                    floor[startingRow, i] = 0;
                }
            }
            skipFloor++;
        }

        public static void DrawFloor()
        {
            for (int i = 0; i < PLAYGROUND_HEIGHT; i++)
            {
                for (int j = 0; j < PLAYGROUND_WIDTH; j++)
                {
                    if (floor[i, j] == 1)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write("*");
                    }
                }
            }
        }

        public static void MoveFloorUp()
        {
            for (int i = 0; i < PLAYGROUND_HEIGHT; i++)
            {
                for (int j = 0; j < PLAYGROUND_WIDTH; j++)
                {
                    if (floor[i, j] == 1)
                    {
                        floor[i, j] = 0;
                        floor[i - 1, j] = 1;
                    }
                }
            }
        }

        public static void ClearFloors()
        {
            for (int i = 0; i < PLAYGROUND_WIDTH; i++)
            {
                floor[0, i] = 0;
            }
        }
    }
}
