using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class FileSystem
    {
        private static string sourcePath = null;
        public void Render()
        {
            Console.CursorVisible = false;
            ListView[] view = new ListView[2];
            for (int i = 0; i < view.Length; i++)
            {
                view[i] = new ListView(3 + i * 60, 5, height: 20);
                view[i].columnWidth = new List<int> { 30, 10, 15 };
                view[i].Items = GetItems("C:\\");
                view[i].Selected += View_Selected;
                view[i].Previous += View_Previous;
                view[i].Copy += View_Copy;
                view[i].Cut += View_Cut;
                view[i].Paste += View_Paste;
            }

            while (true)
            {
                for (int i = 0; i < view.Length; i++)
                {
                    if (i == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
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

        private static void View_Copy(object sender, EventArgs e)
        {
            var view = (ListView)sender;
            var info = view.SelectedItem.state;
           // sourcePath = view.selectedItem.ToString();

        }
        private static void View_Cut(object sender, EventArgs e)
        {

        }
        private static void View_Paste(object sender, EventArgs e)
        {
            var view = (ListView)sender;
            //string destPath = view.selectedItem.ToString();
           // System.IO.File.Copy(sourcePath, destPath, true);
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


