using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Example1
{
    enum FSIMode
    {
        DirectoryInfo = 1, // Справочник
        File = 2
    }
    class Layer
    {
        public DirectoryInfo[] DirContent
        {
            get;
            set;
        }
        public FileInfo[] FileContent
        {
            get;
            set;
        }
        public int selectedIndex
        {
            get;
            set;
        }
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                if (value >= DirContent.Length + FileContent.Length)
                {
                    selectedIndex = 0;
                }
                else if (value < 0)
                {
                    selectedIndex = DirContent.Length + FileContent.Length - 1;
                }
                else
                {
                    selectedIndex = value;
                }
            }
        }
        void SelectedColor(int i)
        {
            if (i == SelectedIndex)
                Console.BackgroundColor = ConsoleColor.Red;
            else
                Console.BackgroundColor = ConsoleColor.Black;
        }
        public void Draw()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            for (int i = 0; i < DirContent.Length; ++i)
            {
                SelectedColor(i);
                Console.WriteLine((i + 1) + ". " + DirContent[i].Name);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < FileContent.Length; ++i)
            {
                SelectedColor(i + DirContent.Length);
                Console.WriteLine((i + DirContent.Length + 1) + ". " + FileContent[i].Name);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Test");
            if (!dir.Exists)
            {
                Console.WriteLine("Directory not exist");
                return;
            }
            Layer l = new Layer
            {
                DirContent = dir.GetDirectories(),
                FileContent = dir.GetFiles(),
                SelectedIndex = 0
            };
            Stack<Layer> history = new Stack<Layer>();
            history.Push(l);
            bool esc = false;
            FSIMode curMode = FSIMode.DirectoryInfo;
            while (!esc)
            {
                if (curMode == FSIMode.DirectoryInfo)
                {
                    history.Peek().Draw();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("\n" + "\t" + "rename: F6 || delete: del || back: backspace || open/confirm: enter || exit: esc");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                ConsoleKeyInfo consolekeyInfo = Console.ReadKey();
                switch (consolekeyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        history.Peek().SelectedIndex--;
                        break;
                    case ConsoleKey.DownArrow:
                        history.Peek().SelectedIndex++;
                        break;
                    case ConsoleKey.Enter:
                        if (history.Peek().DirContent.Length + history.Peek().FileContent.Length == 0)
                            break;
                        int index = history.Peek().SelectedIndex;
                        if (index < history.Peek().DirContent.Length)
                        {
                            DirectoryInfo d = history.Peek().DirContent[index];
                            history.Push(new Layer
                            {
                                DirContent = d.GetDirectories(),
                                FileContent = d.GetFiles(),
                                SelectedIndex = 0
                            });
                        }
                        else
                        {
                            curMode = FSIMode.File;
                            using (FileStream fs = new FileStream(history.Peek().FileContent[index - history.Peek().DirContent.Length].FullName, FileMode.Open, FileAccess.Read))
                            {
                                using (StreamReader sr = new StreamReader(fs))
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Clear();
                                    Console.WriteLine(sr.ReadToEnd());

                                }
                            }

                        }
                        break;
                    case ConsoleKey.Backspace:
                        if (curMode == FSIMode.DirectoryInfo)
                        {
                            if (history.Count > 1)
                                history.Pop();
                        }
                        else
                        {
                            curMode = FSIMode.DirectoryInfo;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;
                    case ConsoleKey.Escape:
                        esc = true;
                        break;
                    case ConsoleKey.Delete:
                        if (curMode != FSIMode.DirectoryInfo || (history.Peek().DirContent.Length + history.Peek().FileContent.Length) == 0)
                            break;
                        index = history.Peek().SelectedIndex;
                        int ind = index;
                        if (index < history.Peek().DirContent.Length)
                            history.Peek().DirContent[index].Delete(true);
                        else
                            history.Peek().FileContent[index - history.Peek().DirContent.Length].Delete();
                        int numofcontent = history.Peek().DirContent.Length + history.Peek().FileContent.Length - 2;
                        history.Pop();
                        if (history.Count == 0)
                        {
                            Layer nl = new Layer
                            {
                                DirContent = dir.GetDirectories(),
                                FileContent = dir.GetFiles(),
                                SelectedIndex = Math.Min(Math.Max(numofcontent, 0), ind)
                            };
                            history.Push(nl);
                        }
                        else
                        {
                            index = history.Peek().SelectedIndex;
                            DirectoryInfo di = history.Peek().DirContent[index];
                            Layer nl = new Layer
                            {
                                DirContent = di.GetDirectories(),
                                FileContent = di.GetFiles(),
                                SelectedIndex = Math.Min(Math.Max(numofcontent, 0), ind)
                            };
                            history.Push(nl);
                        }
                        break;
                    case ConsoleKey.F6:
                        if (history.Peek().DirContent.Length + history.Peek().FileContent.Length == 0)
                            break;
                        index = history.Peek().SelectedIndex;
                        string name, fullname;
                        int selectedMode;
                        if (index < history.Peek().DirContent.Length)
                        {
                            name = history.Peek().DirContent[index].Name;
                            fullname = history.Peek().DirContent[index].FullName;
                            selectedMode = 1;
                        }
                        else
                        {
                            name = history.Peek().FileContent[index - history.Peek().DirContent.Length].Name;
                            fullname = history.Peek().FileContent[index - history.Peek().DirContent.Length].FullName;
                            selectedMode = 2;
                        }
                        fullname = fullname.Remove(fullname.Length - name.Length);
                        Console.WriteLine("New name for {0}:", name);
                        Console.WriteLine(fullname);
                        string newname = Console.ReadLine();
                        
                        Console.WriteLine(selectedMode);
                        if (selectedMode == 1)
                        {
                            new DirectoryInfo(history.Peek().DirContent[index].FullName).MoveTo(fullname + newname);
                        }
                        else
                            new FileInfo(history.Peek().FileContent[index - history.Peek().DirContent.Length].FullName).MoveTo(fullname + newname);
                        index = history.Peek().SelectedIndex;
                        ind = index;
                        numofcontent = history.Peek().DirContent.Length + history.Peek().FileContent.Length - 1;
                        history.Pop();
                        if (history.Count == 0)
                        {
                            Layer nl = new Layer
                            {
                                DirContent = dir.GetDirectories(),
                                FileContent = dir.GetFiles(),
                                SelectedIndex = Math.Min(Math.Max(numofcontent, 0), ind)
                            };
                            history.Push(nl);
                        }
                        else
                        {
                            index = history.Peek().SelectedIndex;
                            DirectoryInfo di = history.Peek().DirContent[index];
                            Layer nl = new Layer
                            {
                                DirContent = di.GetDirectories(),
                                FileContent = di.GetFiles(),
                                SelectedIndex = Math.Min(Math.Max(numofcontent, 0), ind)
                            };
                            history.Push(nl);
                        }
                        break;
                    default:
                        break;
                }

            }
        }
    }
}