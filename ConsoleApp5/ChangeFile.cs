using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class ChangeFile
    {
        public void Message(object state, string message)
        {
            Console.CursorTop = Console.WindowHeight - 5;
            Console.CursorLeft = 0;
            Console.WriteLine(state.ToString() + " is " + message.PadRight(Console.WindowWidth, ' '));
        }
        public void Menu()
        {
            Console.CursorTop = Console.WindowHeight - 2;
            Console.CursorLeft = 0;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.Yellow;
            const string menu = "F1 - copy  F2 - cut  F3 - paste  F4 - root  F5 - list of disks  F6 - properties  F7 - rename  F9 - new folder";
            Console.Write(menu.PadRight(Console.WindowWidth, ' '));
            Console.ResetColor();
        }
    }
}
