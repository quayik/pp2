using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Serialization;

namespace Snake
{
    public class GameState
    {
        public static bool end = false;
        Worm w = new Worm('O');
        Food f = new Food('@');
        Wall b = new Wall('#');
        public int level = 1;
        Timer timer = new Timer();
        Timer timer2 = new Timer();
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameState));

        public GameState()
        {
            Console.SetWindowSize(60, 41);
            Console.SetBufferSize(60, 41);
            Console.CursorVisible = false;
        }
        public void Draw()
        {

            w.Draw();
            f.Draw();
            b.Draw();
        }

        public void Save()
        {
            using (FileStream fileStream = new FileStream("game.xml", FileMode.Create, FileAccess.Write))
            {
                xmlSerializer.Serialize(fileStream, this);
            }
        }

        public void Reset()
        {
            Console.Clear();
            timer.Elapsed -= Timer_Elapsed;
        }

        public GameState Load()
        {
            GameState res = null;
            using (FileStream fileStream = new FileStream("game.xml", FileMode.Open, FileAccess.Read))
            {
                res = xmlSerializer.Deserialize(fileStream) as GameState;
            }

            return res;
        }
        public void Run()
        {
            //timer2.Elapsed += ChangeTime;
            timer2.Interval = 1000;
            timer2.Start();

            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 120;
            timer.Start();

            f.Draw();
            b.Draw();
        }

        /* private void ChangeTime(object sender, ElapsedEventArgs e)
         {
             Console.SetCursorPosition(47, 27);
             Console.WriteLine(DateTime.Now.Second);
         }*/

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            w.Clear();
            w.Move();
            if (!end)
            {
                w.Draw();
            }
            CheckFood();
            if (Bally.Score >= 20)
            {
                b.body.Clear();
                Console.Clear();
                b.LoadLevel(2);
                Bally.PrevS += Bally.Score;
                Bally.Score = 0;
            }
            CheckWall();
            if (w.body.Count >= 4)
            {
                CheckBody();
            }
        }

        public bool eating = new bool();
        void CheckFood()
        {
            if (w.CheckCollision(f.body[0]))
            {
                eating = true;
                w.Eat(f.body[0]);
                Bally.Score += 10;
                f.Generate();

                foreach (Point p in b.body)
                {
                    if (f.body[0].X == p.X && f.body[0].Y == p.Y)
                    {
                        f.Generate();
                    }
                }

                foreach (Point p in w.body)
                {
                    if (p.X == f.body[0].X && p.Y == f.body[0].Y && !eating)
                    {
                        f.Generate();
                    }
                }
                eating = false;
            }
        }
        void GameOver()
        {
            end = true;
            Console.SetCursorPosition(15, 20);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("GAME OVER!\n" + "\t" + "YOUR SCORE IS " + (Bally.Score + Bally.PrevS));
        }
        void CheckWall()
        {
            foreach (Point el in b.body)
            {
                if (w.CheckCollision(el))
                {
                    GameOver();
                }
            }
        }
        void CheckBody()
        {
            for (int i = 4; i < w.body.Count; ++i)
            {
                if (w.CheckCollision(w.body[i - 1]))
                {
                    GameOver();
                }
            }
        }
        public string movement = null;
        public void PressedKey(ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (w.body.Count > 1)
                    {
                        if (movement != "down")
                        {
                            w.Dx = 0;
                            w.Dy = -1;
                            movement = "up";
                        }
                    }
                    else
                    {
                        w.Dx = 0;
                        w.Dy = -1;
                        movement = "up";
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (w.body.Count > 1)
                    {
                        if (movement != "up")
                        {
                            w.Dx = 0;
                            w.Dy = 1;
                            movement = "down";
                        }
                    }
                    else
                    {
                        w.Dx = 0;
                        w.Dy = 1;
                        movement = "down";
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (w.body.Count > 1)
                    {
                        if (movement != "right")
                        {
                            w.Dx = -1;
                            w.Dy = 0;
                            movement = "left";
                        }
                    }
                    else
                    {
                        w.Dx = -1;
                        w.Dy = 0;
                        movement = "left";
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (w.body.Count > 1)
                    {
                        if (movement != "left")
                        {
                            w.Dx = 1;
                            w.Dy = 0;
                            movement = "right";
                        }
                    }
                    else
                    {
                        w.Dx = 1;
                        w.Dy = 0;
                        movement = "right";
                    }
                    break;
                case ConsoleKey.Spacebar:
                    timer.Enabled = !timer.Enabled;
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}