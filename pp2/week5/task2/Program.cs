using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace task2
{
    public class Mark
    {
        public int Points
        {
            get;
            set;
        }
        public Mark()
        {

        }
        public void GetLetter()
        {
            if (Points < 100 && Points >= 90)
                Console.WriteLine("A");
            else if (75 <= Points && Points < 90)
                Console.WriteLine("B");
            else if (60 <= Points && Points < 75)
                Console.WriteLine("C");
            else if (50 <= Points && Points < 60)
                Console.WriteLine("D");
            else Console.WriteLine("F");

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            F1();

        }
        private static void F2()
        {
            FileStream fs = new FileStream("mark.txt", FileMode.Open, FileAccess.Read);
            XmlSerializer xs = new XmlSerializer(typeof(Mark));

            Mark t = xs.Deserialize(fs) as Mark;

            fs.Close();
        }
        private static void F1()
        {
            Mark one = new Mark()
            {
                Points = 95
            };
            Mark two = new Mark()
            {
                Points = 44
            };
            Mark three = new Mark()
            {
                Points = 75
            };
            List<Mark> a = new List<Mark>
            {
                one,
                two,
                three
            };
            FileStream fs = new FileStream("mark.txt", FileMode.Create, FileAccess.Write);
            XmlSerializer xs = new XmlSerializer(typeof(Mark));
            for (int i = 0; i < a.Capacity - 1; ++i)
            {
                xs.Serialize(fs, a[i]);
            }
            

            fs.Close();
        }
    }
    
}