using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace ConsoleApp5
{
    class ListView
    {
        ConsoleGraphics graphics = new ConsoleGraphics();
        public string Current;
        int previousSelectedIndex;
        public int selectedIndex;
        private bool wasPainted;
        private int scroll;
        public object selectedItem { get; set; }
        public bool Focuse { get; set; }
        private int x, y, height;

        public ListView(int x, int y, int height)
        {
            this.x = x;
            this.y = y;
            this.height = height;
        }
        public List<int> columnWidth { get; set; }
        public List<ListNewItems> Items { get; set; }
        public BorderListView borders = new  BorderListView();
        public void Clean()
        {
            selectedIndex = previousSelectedIndex = 0;
            wasPainted = false;
            for (int i = 0; i < Math.Min(height, Items.Count); i++)
            {
                Console.CursorLeft = x;
                Console.CursorTop = i + y;
                Items[i].Clean(columnWidth, i, x, y);
            }
        }
        public ListNewItems SelectedItem => Items[selectedIndex];
        public void Render()
        {
            for (int i = 0; i < Math.Min(height, Items.Count); i++)
            {
                int elementIndex = i + scroll;
                if (wasPainted)
                {
                    if (elementIndex != selectedIndex && elementIndex != previousSelectedIndex)
                    {
                        continue;
                    }
                }
                var item = Items[elementIndex];
                var savedForeGround = Console.ForegroundColor;
                var savevBackground = Console.BackgroundColor;
                if (elementIndex == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                Console.CursorLeft = x;
                Console.CursorTop = i + y;
                item.Render(columnWidth, i, x, y);
                borders.Borders(columnWidth, i, x, y,graphics);
                Console.ForegroundColor = savedForeGround;
                Console.BackgroundColor = savevBackground;
            }
            wasPainted = true;
        }

        public void Update(ConsoleKeyInfo key)
        {
            previousSelectedIndex = selectedIndex;
            if (key.Key == ConsoleKey.UpArrow && selectedIndex > 0)
            {
                selectedIndex--;

            }
            if (key.Key == ConsoleKey.DownArrow && selectedIndex < Items.Count - 1)
            {
                selectedIndex++;
            }
            if (selectedIndex >= height + scroll)
            {
                scroll++;
                wasPainted = false;
            }
            else if (selectedIndex < scroll)
            {
                scroll--;
                wasPainted = false;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                Selected(this, EventArgs.Empty);

            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                Previous(this, EventArgs.Empty);
            }
          
        }


        public event EventHandler Selected;
        public event EventHandler Previous;
    }
}
