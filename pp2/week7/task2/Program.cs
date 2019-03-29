using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task2
{
    public class MyThread
    {
        
        public string name;

        public MyThread(string name)
        {
            this.name = name;
            //Thread.CurrentThread.Name = name;
        }

        public static void DoIt()
        {
            for (int i = 0; i < 4; ++i)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " выводит " + (i + 1));
            }

            Console.WriteLine(Thread.CurrentThread.Name + " завершился");
        }

        public void startThread()
        {
            Thread threadField = new Thread(new ThreadStart(DoIt));
            threadField.Name = name;
            threadField.Start();
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            MyThread t1 = new MyThread("Thread 1");
            MyThread t2 = new MyThread("Thread 2");
            MyThread t3 = new MyThread("Thread 3"); 

            t1.startThread();
            t2.startThread();
            t3.startThread();
        }
    }
}
