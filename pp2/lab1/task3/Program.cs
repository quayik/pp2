using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = int.Parse(Console.ReadLine());  //getting the "size" of input
            int[] a = new int[x];                   // array to store inputs
            for (int i = 0; i < x; ++i)
            {
                a[i] = int.Parse(Console.ReadLine());   // getting the array itself
            }

                for (int i = 0; i < a.Length; ++i)
            {
                for (int j = 0; j < 2; ++j)             //loop to twice every output
                {
                    Console.Write(a[i] + " ");
                }
            }
        }
    }
}
