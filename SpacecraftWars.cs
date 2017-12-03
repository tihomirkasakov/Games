using System;
using System.Collections.Generic;

class SpacecraftWars
{
    public static Random rng = new Random();
    public static bool gameOver = false;
    public static int bulletCount = 3;
    public static int playField = Console.BufferWidth - 20;
    public static int startTimer = 0;
    public static int threadSleep = 200;
    public static Difficulty currentDifficulty = Difficulty.EASY;
    public static int score = 0;

    public enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD
    }

    public class Vector2
    {
        public int X;
        public int Y;

        public Vector2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void Add(Vector2 value)
        {
            this.X += value.X;
            this.Y += value.Y;
        }

        public bool IsColliding(Vector2 value, int length, int rows)
        {
            if (this.X >= value.X && this.X <= value.X + length && this.Y >= value.Y && this.Y <= value.Y + rows)
            {
                return true;
            }
            return false;
        }
    }

    public class Spacecraft
    {
        public Vector2 Position;
        public List<string> Elements;
        public List<Bullet> Clip;

        public Spacecraft()
        {
            this.Position = new Vector2(0, Console.BufferHeight / 2);
            this.Elements = new List<string>
            {
                " \\\\\\",
                "=>==_>>",
                " ///",
            };

            this.Clip = new List<Bullet>();

        }

        public void Draw()
        {

            for (int i = 0; i < this.Elements.Count; i++)
            {
                Console.SetCursorPosition(this.Position.X, this.Position.Y + i);
                Console.Write(this.Elements[i]);
            }
        }

        public void Delete()
        {
            for (int i = 0; i < this.Elements.Count; i++)
            {
                Console.SetCursorPosition(this.Position.X, this.Position.Y + i);
                Console.Write(new string(' ', this.Elements[i].Length));
            }
        }

        public void Move(int y)
        {
            if (this.Position.Y + y < 0 || this.Position.Y + y > Console.WindowHeight - Elements.Count)
            {
                return;
            }
            this.Position.Add(new Vector2(0, y));
        }
    }

    public class Meteor
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public char Element;

        public Meteor(Vector2 position)
        {
            this.Position = position;
            this.Velocity = new Vector2(-1, 0);
            this.Element = '|';
        }

        public void Draw()
        {

            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.Write(this.Element);
        }

        public void Delete()
        {

            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.Write(' ');

        }

        public void MoveMeteor()
        {
            this.Position.Add(this.Velocity);
        }
    }

    public class MeteorWall
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public List<Meteor> Elements;
        public int CurrentHoleLength;

        public MeteorWall(Vector2 position, int currentHoleLength)
        {
            this.Position = position;
            this.Velocity = new Vector2(-1, 0);
            this.CurrentHoleLength = currentHoleLength;
            this.CreateElements();
        }

        public void CreateElements()
        {
            this.Elements = new List<Meteor>();
            int holePosition = rng.Next(0, Console.WindowHeight - this.CurrentHoleLength);
            int wallType = rng.Next(0, 2);
            if (wallType == 0)
            {
                for (int i = 0; i < Console.WindowHeight; i++)
                {
                    if (i < holePosition || i > holePosition + CurrentHoleLength)
                    {
                        Vector2 position = new Vector2(this.Position.X, this.Position.Y + i);
                        this.Elements.Add(new Meteor(position));
                    }
                }
            }
            else
            {
                for (int i = 0; i < Console.WindowHeight; i++)
                {
                    if (i >= holePosition && i <= CurrentHoleLength + holePosition)
                    {
                        Vector2 position = new Vector2(this.Position.X, this.Position.Y + i);
                        this.Elements.Add(new Meteor(position));
                    }
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < this.Elements.Count; i++)
            {
                this.Elements[i].Draw();
            }
        }

        public void Delete()
        {
            for (int i = 0; i < this.Elements.Count; i++)
            {
                this.Elements[i].Delete();
            }
        }

        public void MoveMeteorWall()
        {
            this.Position.Add(this.Velocity);
            for (int i = 0; i < this.Elements.Count; i++)
            {
                if (this.Elements[i].Position.X <= 0)
                {
                    this.Elements[i].Delete();
                    this.Elements.Remove(this.Elements[i]);
                    i--;
                    continue;
                }
                this.Elements[i].MoveMeteor();
            }
        }

    }

    public class Bullet
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public char Element;

        public Bullet(Vector2 position)
        {
            this.Position = position;
            this.Velocity = new Vector2(1, 0);
            this.Element = 'o';
        }

        public void Draw()
        {
            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(this.Element);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Delete()
        {
            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.Write(' ');
        }

        public void MoveBullet()
        {
            this.Position.Add(this.Velocity);
        }
    }

    public class UI
    {
        public int Score;
        public int BulletCount;
        public Difficulty Difficulty;

        public UI(Difficulty difficulty)
        {
            this.Score = score;
            this.BulletCount = bulletCount;
            this.Difficulty = difficulty;
        }

        public void Draw()
        {
            Console.SetCursorPosition(playField, 0);
            Console.WriteLine("Score: {0}", this.Score.ToString());
            Console.SetCursorPosition(playField, 1);
            Console.WriteLine("Bullets: {0}", this.BulletCount.ToString());
            Console.SetCursorPosition(playField, 2);
            Console.WriteLine("Difficulty: {0}   ", this.Difficulty);
        }

        public void Delete()
        {
            Console.SetCursorPosition(playField, 0);
            Console.WriteLine(" ");
            Console.SetCursorPosition(playField, 1);
            Console.WriteLine(" ");
            Console.SetCursorPosition(playField, 2);
            Console.Write(" ");
        }

        public void UpdateUI(int score)
        {
            this.Score = score;
            this.BulletCount = bulletCount;
            this.Difficulty = currentDifficulty;
        }
    }

    public class Ammo
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public char Element;

        public Ammo(Vector2 position)
        {
            this.Position = position;
            this.Velocity = new Vector2(-1, 0);
            this.Element = '8';
        }

        public void Draw()
        {
            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(this.Element);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Delete()
        {
            Console.SetCursorPosition(this.Position.X, this.Position.Y);
            Console.Write(' ');
        }

        public void Move()
        {
            this.Position.Add(this.Velocity);
        }
    }

    static void Main()
    {
        //Game Plan:
        //Spacecraft
        //Walls
        //Movement
        //Wall Generator
        //UI
        //Bullets
        //PowerUps
        //Collision

        Console.BufferWidth = Console.WindowWidth = 120;
        Console.BufferHeight = Console.BufferHeight = 30;
        Console.CursorVisible = false;

        Spacecraft spacecraft = new Spacecraft();
        Meteor meteor1 = new Meteor(new Vector2(5, 0));
        List<MeteorWall> meteorWalls = new List<MeteorWall>();
        List<Ammo> powerUp = new List<Ammo>();
        UI ui = new UI(Difficulty.EASY);

        while (!gameOver)
        {
            startTimer++;
            InputHandler(spacecraft);
            WallGenerator(meteorWalls);
            MoveWall(meteorWalls);
            PowerUpSpawn(spacecraft, meteorWalls, powerUp);
            MoveBullets(spacecraft);
            foreach (var powerup in powerUp)
            {
                powerup.Move();
            }
            ChangeDifficulty();
            ui.UpdateUI(score);
            ui.Draw();
            spacecraft.Draw();
            foreach (var powerup in powerUp)
            {
                powerup.Draw();
            }
            foreach (var wall in meteorWalls)
            {
                wall.Draw();
            }
            foreach (var bullet in spacecraft.Clip)
            {
                bullet.Draw();
            }
            TakePowerUp(powerUp, spacecraft);
            SpacecraftMeteorWallCollision(spacecraft, meteorWalls);
            BulletMeteorWallCollision(spacecraft.Clip, meteorWalls);
            System.Threading.Thread.Sleep(threadSleep);
            foreach (var wall in meteorWalls)
            {
                wall.Delete();

            }
            foreach (var bullet in spacecraft.Clip)
            {
                bullet.Delete();
            }
            foreach (var powerup in powerUp)
            {
                powerup.Delete();
            }
            spacecraft.Delete();
            ui.Delete();
        }
    }

    public static void InputHandler(Spacecraft spacecraft)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo userInput = Console.ReadKey();

            while (Console.KeyAvailable)
            {
                Console.ReadKey();
            }

            if (userInput.Key == ConsoleKey.DownArrow)
            {
                spacecraft.Move(1);
            }
            else if (userInput.Key == ConsoleKey.UpArrow)
            {
                spacecraft.Move(-1);
            }
            else if (userInput.Key == ConsoleKey.Spacebar)
            {
                if (bulletCount > 0)
                {
                    Bullet bullet = new Bullet(new Vector2(spacecraft.Position.X + 7, spacecraft.Position.Y + 1));
                    spacecraft.Clip.Add(bullet);
                    bulletCount--;
                }
            }

        }
    }

    public static void MoveWall(List<MeteorWall> walls)
    {
        for (int i = 0; i < walls.Count; i++)
        {
            if (walls[i].Position.X <= 0)
            {
                walls[i].Delete();
                walls.Remove(walls[i]);
                i--;
                score = score + 20;
                continue;

            }
            walls[i].MoveMeteorWall();
        }
    }

    public static void WallGenerator(List<MeteorWall> walls)
    {
        int respawnTime = 20;
        if (startTimer % respawnTime == 0)
        {
            walls.Add(new MeteorWall(new Vector2(playField, 0), 5));
        }
    }

    public static void ChangeDifficulty()
    {
        int changeDifficultyTimer = 200;
        if (startTimer % changeDifficultyTimer == 0 && currentDifficulty == Difficulty.EASY)
        {
            currentDifficulty = Difficulty.MEDIUM;
            threadSleep = 100;

        }
        else if (startTimer % changeDifficultyTimer == 0 && currentDifficulty == Difficulty.MEDIUM)
        {
            currentDifficulty = Difficulty.HARD;
            threadSleep = 50;
        }
    }

    public static void SpacecraftMeteorWallCollision(Spacecraft spacecraft, List<MeteorWall> meteorWalls)
    {
        foreach (var wall in meteorWalls)
        {
            foreach (var element in wall.Elements)
            {
                if (element.Position.IsColliding(spacecraft.Position, spacecraft.Elements[1].Length, spacecraft.Elements.Count))
                {
                    gameOver = true;
                    Console.Clear();
                    Console.WriteLine("You Lost");
                    Console.WriteLine("Score: {0}", score);
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }

    public static void BulletMeteorWallCollision(List<Bullet> bullets, List<MeteorWall> meteorWalls)
    {
        for (int k = 0; k < bullets.Count; k++) 
        {
            for (int i = 0; i < meteorWalls.Count; i++)
            {
                for (int j = 0; j < meteorWalls[i].Elements.Count; j++)
                {
                    
                    if (meteorWalls[i].Elements[j].Position.IsColliding(bullets[k].Position, 1, 1))
                    {
                        meteorWalls[i].Delete();
                        meteorWalls.Remove(meteorWalls[i]);
                      
                        bullets[k].Delete();
                        bullets.Remove(bullets[k]);
                        
                        return;
                    }
                    
                }
            }
        }
    }

    public static void PowerUpSpawn(Spacecraft spacecraft, List<MeteorWall> meteorWalls, List<Ammo> ammo)
    {
        int respawnTime = 30;
        if (startTimer % respawnTime == 0)
        {
            int X = playField;
            int Y = rng.Next(0, Console.BufferHeight);
            foreach (var wall in meteorWalls)
            {
                foreach (var element in wall.Elements)
                {
                    if (X != element.Position.X && Y != element.Position.Y)
                    {
                        Ammo extraAmmo = new Ammo(new Vector2(X, Y));
                        ammo.Add(extraAmmo);
                        return;
                    }
                }
            }
        }
    }

    public static void TakePowerUp(List<Ammo> powerUp, Spacecraft spacecraft)
    {
        for (int i = 0; i < powerUp.Count; i++)
        {
            if (powerUp[i].Position.IsColliding(spacecraft.Position, spacecraft.Elements[1].Length, spacecraft.Elements.Count))
            {
                powerUp[i].Delete();
                powerUp.Remove(powerUp[i]);
                bulletCount = bulletCount + 1;
                i--;
            }
            else if (powerUp[i].Position.X == 0)
            {
                powerUp[i].Delete();
                powerUp.Remove(powerUp[i]);
                i--;
            }
        }
    }

    public static void MoveBullets(Spacecraft spacecraft)
    {
        for (int i = 0; i < spacecraft.Clip.Count; i++)
        {
            if (spacecraft.Clip[i].Position.X > playField)
            {
                spacecraft.Clip[i].Delete();
                spacecraft.Clip.Remove(spacecraft.Clip[i]);
                i--;
            }
            spacecraft.Clip[i].MoveBullet();
        }
    }
}