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
        static bool isFile = true;
        static ChangeFile file = new ChangeFile();
        private static string sourcePath = " ";
        private static string destPath = " ";
        private static string fileName = " ";
        public void Render()
        {

            Console.CursorVisible = false;
            ListView[] view = new ListView[2];
            for (int i = 0; i < view.Length; i++)
            {
                view[i] = new ListView(3 + i * 60, 2, height: 20);
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
            string denied = "Access denied";
            var view = (ListView)sender;
            var info = view.SelectedItem.state;

            try
            {
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
            catch (System.UnauthorizedAccessException)
            {
                file.Message(info, denied);
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
            string copy = "Copy";
            var view = (ListView)sender;
            var info = view.SelectedItem.state;
            fileName = view.SelectedItem.state.ToString();
            file.Message(info, copy);
            if (info is FileInfo files)
            {
                sourcePath = System.IO.Path.Combine(files.DirectoryName, fileName);
                isFile = true;
            }
            else if (info is DirectoryInfo dir)
            {
                sourcePath = dir.FullName.ToString();
                isFile = false;
            }

        }
        private static void View_Cut(object sender, EventArgs e)
        {
            var view = (ListView)sender;
        }
        private static void View_Paste(object sender, EventArgs e)
        {
            string targetPath = " ";
            var view = (ListView)sender;
            for (int i = 0; i < view.Current.Count; i++)
            {
                destPath = System.IO.Path.Combine(view.Current[view.Current.Count - 1], fileName);
                targetPath = view.Current[view.Current.Count - 1];
            }
            string paste = $"Past in + {destPath} ";
            if (isFile == true)
            {
                System.IO.File.Copy(sourcePath, destPath, true);
                file.Message(fileName, paste);
                view.Items = GetItems(targetPath);
            }
            else
            {
                if (System.IO.Directory.Exists(sourcePath))
                {
                    string[] files = System.IO.Directory.GetFiles(sourcePath);
                    string dirName = fileName + " Pasted ";
                    foreach (string s in files)
                    {
                        fileName = System.IO.Path.GetFileName(s);
                        destPath = System.IO.Path.Combine(targetPath, fileName);
                        System.IO.File.Copy(s, destPath, true);
                    }
                    file.Message(dirName, targetPath);
                }
            }
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


