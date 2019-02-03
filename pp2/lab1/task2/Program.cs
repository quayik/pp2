using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    class Student
    {
        public string name;
        public string id;
        public int year;

        public Student(string name, string id)
        {
            this.name = name;
            this.id = id;
        }
        public void PrintName()
        {
            Console.WriteLine(name);
        }
        public void PrintId()
        {
            Console.WriteLine(id);
        }
        public void PrintYear()
        {
            Console.WriteLine(year);
        }
        public void PlusY()
        {
            year++;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student("Damir", "18BD110693")
            {
                year = 1
            };
            
            student.PrintName();
            student.PrintId();
            student.PrintYear();
            student.PlusY();
            student.PrintYear();
        }
    }
}
