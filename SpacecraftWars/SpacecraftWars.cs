namespace SpacecraftWars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using static Obstacles;
    using static UI;
    using static Spacecraft;

    public class SpacecraftWars
    {
        public static bool isGameOver = false;
        public const int PLAYFIELD_HEIGHT = 40;
        public const int PLAYFIELD_WIDTH = 80;
        public const int PLAYFIELD_UI = 20;

        public static Random size = new Random();
        public static int score = 0;
        public static int difficulty = 100;
        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }

        static void Main()
        {
            Console.BufferHeight = Console.WindowHeight = PLAYFIELD_HEIGHT;
            Console.BufferWidth = Console.WindowWidth = PLAYFIELD_WIDTH + PLAYFIELD_UI;
            Console.CursorVisible = false;

            int loopCounter = 1;
            InitializeSpacecraft();
            while (!isGameOver)
            {
                if (loopCounter%25==0)
                {
                    GenerateObstacles();
                }
                MoveObstacles();
                DrawObstacles();
                DrawSpacecraft();
                MoveSpacecraft();
                RockCollision();
                ClearObstacles();
                DrawUI();
                Thread.Sleep(difficulty);
                Console.Clear();
                loopCounter++;
                score++;
            }

            Console.WriteLine("GAME OVER!");
            Console.WriteLine($"Your Score:{score}");
        }

        public static void RockCollision()
        {
            Position firstPart = elements[1];
            int firstPartX = firstPart.X;
            int firstPartY = firstPart.Y + 7;
            if (obstacles[firstPartX-1,firstPartY-2]==1|| obstacles[firstPartX, firstPartY]==1|| obstacles[firstPartX+1, firstPartY-2] == 1)
            {
                isGameOver = true;
            }
        }
    }
}
