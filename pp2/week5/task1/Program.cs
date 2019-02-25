using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Example2
{
    public class ComplexNumber
    {
        public double a;
        public double b;
        public ComplexNumber()
        {
            
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
            FileStream fs = new FileStream("cn.txt", FileMode.Open, FileAccess.Read);
            XmlSerializer xs = new XmlSerializer(typeof(ComplexNumber));

            ComplexNumber t = xs.Deserialize(fs) as ComplexNumber;

            fs.Close();
        }

        private static void F1()
        {
            ComplexNumber cn = new ComplexNumber
            {
                a = 3,
                b = 2.4
            };
           

            FileStream fs = new FileStream("cn.txt", FileMode.Create, FileAccess.Write);
            XmlSerializer xs = new XmlSerializer(typeof(ComplexNumber));

            xs.Serialize(fs, cn);

            fs.Close();
        }
    }
}