using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class ChangeFile
    {
        private int x;
        private int y;
        private string[] futures = { "F1-copy   ", "F2-cut   ", "F3-paste   ", "F4-root   ", "F5-list of disk   ", "F6-properties   ", "F7-rename   ", "F8- found   ", "F9- new folder  " };

        public ChangeFile(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Draw()
        {
           
            Console.CursorTop = Console.WindowHeight - 2;
            Console.CursorLeft = 0;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.Yellow;
            const string menu = "F1 - copy  F2 - cut  F3 - paste  F4 - root  " +
                "F5 - list of disks  F6 - properties  F7 - rename  F9 - new folder";
            Console.Write(menu.PadRight(Console.WindowWidth, ' '));
            Console.ResetColor();
        }
    }
}
