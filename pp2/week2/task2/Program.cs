using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    class Program
    {
        public static bool Prime(int c)
        {
            if (c == 1)
                return false;
            for (int i = 2; i < c; i++)
            {
                if (c % i == 0) return false;
            }
            return true;
        }
        static string Solve(string str)
        {
            string[] x = str.Split(' ');    //пробел разделитель
            string Pnum = null;     //пустя строка

            for (int i = 0; i < x.Length; ++i)
            {
                if (Prime(int.Parse(x[i])))
                {
                    Pnum = Pnum + " " + x[i];       //ппростые числадобавляем в строку

                }
            }
            return Pnum;
        }
        static void Main(string[] args)
        {
            FileStream fs = new FileStream(@"C:\test\innerTest\hello.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            string line = sr.ReadLine();

            File.WriteAllText(@"C:\test\innerTest\output.txt", Solve(line));        //создаем новый файл внутрь пишем ответ


            sr.Close();
            fs.Close();
        }
    }
}