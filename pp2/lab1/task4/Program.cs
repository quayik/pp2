using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            a = int.Parse(Console.ReadLine());  //getting the size of triangle
            for( int i = 1; i <= a; ++i)        //for lines
            {
                for (int j = 1; j <= i; ++j)    //for columns
                {
                    Console.Write("[*]");
                    
                }
                Console.WriteLine();            //ending line after every line
            }
        }
    }
}
