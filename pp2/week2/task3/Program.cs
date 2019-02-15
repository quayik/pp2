using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    class Program
    {
        static int cnt = 0;
        public FileSystemInfo[] Items
        {
            get;
            set;
        }

        public static void Out(FileSystemInfo[] fsi)
        {
            for (int i = 0; i < fsi.Length; ++i)
            {
                for (int j = 0; j < cnt; ++j)
                {
                    Console.Write("\t");        //для табуляции
                }

                if (fsi[i].GetType() == typeof(DirectoryInfo))
                {
                    Console.WriteLine(fsi[i].Name);
                    DirectoryInfo d = fsi[i] as DirectoryInfo;
                    cnt++;
                    Out(d.GetFileSystemInfos());        //если папка, открываем ее используя рекурсию
                }
                else
                {
                    Console.WriteLine(fsi[i].Name);     //если файл, просто пишем имя
                }
                if (i == fsi.Length - 1) cnt--;     //чтобы было правильное количество табов
            }
        }

        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\test");
            Out(dir.GetFileSystemInfos());
        }
    }
}
