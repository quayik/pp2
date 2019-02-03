using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task1
{
    class Program
    {
        public static bool Prime(int c) //function for checking if a number is prime
        {
            if (c == 1)
                return false;
            for (int i = 2; i < c; i++)
            {
                if (c % i == 0) return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            int n = int.Parse(s);           //getting "size" of input
            int[] a = new int[n];           //new array
            int cnt = 0;
            for (int i = 0; i < n; i++)
            {
                string ss = Console.ReadLine();
                int b = int.Parse(ss);
                a[i] = b;                   //put all of the numbers into the array
            }
            for (int i = 0; i < n; i++)
            {
                if (Prime(a[i]))
                {
                    cnt++; // counting the number of prime number via Prime() function
                }
            }
            Console.WriteLine(cnt); // amount of prime numbers
            for (int i = 0; i < n; i++)
            {
                if (Prime(a[i]))
                {
                    Console.Write(a[i]); // ptime numbers themself
                }
            }
        }
    }
}
