﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Snake
{
    class Wall : GameObject
    {
        public int level = 1;

        public Wall(char sign) : base(sign)
        {
            LoadLevel(1);
            
        }

        public void LoadLevel(int level)
        {
            this.level = level;
            string name = string.Format("Levels/Level{0}.txt", level);
            StreamReader sr = new StreamReader(name);

            int r = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                for (int c = 0; c < line.Length; ++c)
                {
                    if (line[c] == '#')
                    {
                        body.Add(new Point { X = c, Y = r });
                    }
                }
                r++;
            }

            sr.Close();
        }
    }
}