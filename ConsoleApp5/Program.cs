using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            ListView[] view = new ListView[2];
            for (int i = 0; i < view.Length; i++)
            {
                view[i] = new ListView(3 + i * 40, 5, height: 20);
                view[i].columnWidth = new List<int> { 15, 10, 10 };
                view[i].Items = GetItems("C:\\");
                view[i].Selected += View_Selected;
                view[i].Previous += View_Previous;
            }

            while (true)
            {
                for (int i = 0; i < view.Length; i++)
                {
                    view[0].Render();
                    view[1].Render();
                    bool changeDirectory = false;
                    while (!changeDirectory)
                    {
                        view[i].Render();
                        var key = Console.ReadKey();
                        view[i].Update(key);
                        if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow)
                        {
                            changeDirectory = true;
                        }
                    }
                }
            }
        }

        private static void View_Selected(object sender, EventArgs e)
        {
            var view = (ListView)sender;
            var info = view.SelectedItem.state;
            if (info is FileInfo file)
            {
                Process.Start(file.FullName);
            }
            else if (info is DirectoryInfo dir)
            {
                view.Clean();
                view.Items = GetItems(dir.FullName);
                view.Current.Add(dir.FullName);

            }

        }

        private static void View_Previous(object sender, EventArgs e)
        {
            string lastElement = "";
            var view = (ListView)sender;
            view.Clean();
            for (int i = 0; i < view.Current.Count; i++)
            {
                view.Items = GetItems(Path.GetDirectoryName(view.Current[view.Current.Count - 1]));
                lastElement = view.Current[view.Current.Count - 1];
            }
            view.Current.Remove(lastElement);
        }


        private static List<ListNewItems> GetItems(string v)
        {
            return new DirectoryInfo(v).GetFileSystemInfos()
                .Select(f => new ListNewItems(
                    f,
                    f.Name,
                    f is DirectoryInfo dir ? "<dir>" : f.Extension,
                    f is FileInfo file ? file.Length.ToString() : "")).ToList();
        }
    }
}
