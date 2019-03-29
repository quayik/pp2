using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Thread> myArray = new List<Thread>();
            ThreadStart threadStart1 = new ThreadStart(DoIt);
            ThreadStart threadStart2 = new ThreadStart(DoIt);
            ThreadStart threadStart3 = new ThreadStart(DoIt);
            Thread thread1 = new Thread(threadStart1);
            Thread thread2 = new Thread(threadStart2);
            Thread thread3 = new Thread(threadStart3);

            thread1.Name = "first";
            thread2.Name = "second";
            thread3.Name = "third";

            myArray.Add(thread1);
            myArray.Add(thread2);
            myArray.Add(thread3);

            /*
            thread1.Start();
            thread2.Start();
            thread3.Start(); */

            myArray[0].Start();
            myArray[1].Start();
        }

        public static void DoIt()
        {
            for (int i = 0; i < 3; ++i)
            {
                Console.WriteLine(Thread.CurrentThread.Name);
            }
        }
    }
}
