using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kun_tun
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;
        Graphics graphics;
        int x = -10;
        int x2 = -10;
        SolidBrush brush = new SolidBrush(Color.Yellow);
        SolidBrush brush2 = new SolidBrush(Color.DarkBlue);
        SolidBrush brush3 = new SolidBrush(Color.Green);
        SolidBrush brush4 = new SolidBrush(Color.Blue);

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            pictureBox1.Image = bitmap;

            //DrawNight();
            //DrawMoon(x);
           
            timer1.Start(); 
                
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        void DrawMoon(int x)
        {
            Rectangle r = new Rectangle(x, 60, 80, 80);
            Rectangle r2 = new Rectangle(x + 30, 65, 55, 55);

            graphics.FillEllipse(brush, r);
            graphics.FillEllipse(brush2, r2);
        }

        void DrawNight()
        {
            graphics.FillRectangle(brush2, 0, 0, pictureBox1.Width, pictureBox1.Height / 2 + 50);
            graphics.FillRectangle(brush3, 0, pictureBox1.Height / 2 + 50, pictureBox1.Width, pictureBox1.Height / 2);
            Point[] stars = { new Point(15, 15), new Point(pictureBox1.Width / 2, pictureBox1.Height / 2),
                new Point(pictureBox1.Width - 89, 70), new Point(pictureBox1.Width / 2 + 20, pictureBox1.Height / 2 - 30) };

            foreach (Point point in stars)
            {
                Point[] points = { point, new Point(point.X + 14, point.Y), new Point(point.X + 7, point.Y - 9) };
                Point[] points2 = { new Point(point.X, point.Y - 5), new Point(point.X + 14, point.Y - 6),
                    new Point(point.X + 7, point.Y + 4) };


                graphics.FillPolygon(brush, points);
                graphics.FillPolygon(brush, points2);

            }
        }

        void DrawSun(int x)
        {
            Rectangle r = new Rectangle(x, 60, 80, 80);
            Rectangle r2 = new Rectangle(x + 30, 65, 55, 55);

            graphics.FillEllipse(brush, r);
            
        }

        void DrawDay()
        {
            graphics.FillRectangle(brush4, 0, 0, pictureBox1.Width, pictureBox1.Height / 2 + 50);
            graphics.FillRectangle(brush3, 0, pictureBox1.Height / 2 + 50, pictureBox1.Width, pictureBox1.Height / 2);
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
            
            DrawNight();
            x += 30;
            DrawMoon(x);
            pictureBox1.Refresh();

            if(x > pictureBox1.Width)
            {
                timer1.Stop();
                timer2.Start();
                x = -10;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);

            DrawDay();
            x2 += 30;
            DrawSun(x2);

            pictureBox1.Refresh();

            if (x2 > pictureBox1.Width)
            {
                timer2.Stop();
                timer1.Start();
                x2 = -10;
            }
        }

        int last = 0;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if(e.KeyCode == Keys.S)
            {
                if (timer1.Enabled)
                {
                    timer1.Stop();
                    last = 1;
                }
                else if (timer2.Enabled)
                {
                    timer2.Stop();
                    last = 2;
                }
                    
            
            }
            else if (e.KeyCode == Keys.R)
            {
                if (last == 1)
                    timer1.Start();

                else if (last == 2)
                    timer2.Start();
            }
        }
    }
}
