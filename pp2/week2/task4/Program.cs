using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4
{
    class Program
    {
        static void Main(string[] args)
        {
            string path1 = @"C:\test\1\text.txt";
            string path2 = @"C:\test\1\1 2\text2.txt";
            File.Move(path1, path2);    //удаляет оригинал и создает новый файл
        }
    }
}
