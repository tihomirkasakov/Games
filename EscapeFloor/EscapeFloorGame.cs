namespace EscapeFloor
{
    using System;
    using System.Linq;
    using System.Threading;
    using static Floor;
    using static Player;
    using static PowerUps;
    using static UI;

    public class EscapeFloorGame
    {
        public static bool isGameOver = false;
        public const int PLAYGROUND_HEIGHT = 30;
        public const int PLAYGROUND_WIDTH = 40;
        public const int UI_FIELD = 15;

        public static bool moveVertically = true;
        public static int holeSize = 10;
        public static int difficulty = 100;
        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }
        public static int score = 0;


        static void Main()
        {
            Console.BufferHeight = Console.WindowHeight = PLAYGROUND_HEIGHT;
            Console.BufferWidth = Console.WindowWidth = PLAYGROUND_WIDTH + UI_FIELD;
            Console.CursorVisible = false;
            int loopCounter = 1;

            InitializePlayer();
            while (!isGameOver)
            {
                if (loopCounter % 12 == 0)
                {
                    GenerateFloor();
                    GeneratePowerUps();
                }
                DrawFloor();
                DrawPowerUps();
                ClearFloors();
                DrawPlayer();
                MoveFloorUp();
                MovePowerUps();
                FloorCollision();
                MovePlayerHorizontal();
                MovePlayerVertically();
                PowerUpCollision();
                DrawUI();
                ResetDifficulty();
                Thread.Sleep(difficulty);
                Console.Clear();
                loopCounter++;
                score++;
                SetPoints();
                GameOver();
            }
            Console.WriteLine("GAME OVER!");
            Console.WriteLine($"Your Score:{score}");
        }

        static void FloorCollision()
        {
            Positions rightLeg = playerPosition.Last();
            int legX = rightLeg.X;
            int legY = rightLeg.Y;

            if (floor[legX, legY] == 1 || floor[legX, legY - 2] == 1)
            {
                moveVertically = false;
            }
            else
            {
                moveVertically = true;
            }
        }

        public static void PowerUpCollision()
        {
            Positions rightLeg = playerPosition.Last();
            int legX = rightLeg.X;
            int legY = rightLeg.Y;
            foreach (var bodyPart in playerPosition)
            {
                for (int i = 0; i < powerups.Count; i++)
                {
                    if (legX == powerups[i].X && legY - 1 == powerups[i].Y || (bodyPart.X == powerups[i].X && bodyPart.Y == powerups[i].Y))
                    {
                        if (powerups[i].Symbol == '@' && faster == false)
                        {
                            startTime = Environment.TickCount;
                            faster = true;
                            slower = false;
                            difficulty -= 15;

                        }
                        else if (powerups[i].Symbol == '&' && slower == false)
                        {
                            startTime = Environment.TickCount;
                            slower = true;
                            faster = false;
                            difficulty += 15;
                        }
                        else if (powerups[i].Symbol == '$')
                        {
                            skipFloor = 0;
                        }
                        powerups[i] = new Positions(0, 0, ' ');
                    }
                }
            }
        }

        private static void ResetDifficulty()
        {
            if (Environment.TickCount - startTime > 5000 && faster)
            {
                startTime = Environment.TickCount;
                faster = false;
                difficulty += 15;
            }
            else if (Environment.TickCount - startTime > 7000 && slower)
            {
                startTime = Environment.TickCount;
                slower = false;
                difficulty -= 15;
            }
        }

        private static void SetPoints()
        {
            var row = playerPosition.Last().X;
            for (int j = 0; j < PLAYGROUND_WIDTH; j++)
            {
                if (floor[row, j]==1)
                {
                    score += 20;
                    break;
                }
            }
        }

        static void GameOver()
        {
            if (playerPosition[0].X == 0)
            {
                isGameOver = true;
            }
        }
    }
}
