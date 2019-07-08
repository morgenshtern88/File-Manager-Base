using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace ConsoleApp5
{
    class BorderListView
    {
        // private int x, y;



        internal void Borders(List<int> columnWidth, int ElementIndex, int x, int y, ConsoleGraphics graphics)
        {
            for (int i = 0; i < ElementIndex; i++)
            {
                graphics.FillRectangle(0xFF00FFFF, x - 1, y - 1, ElementIndex, 5);
            }
        }
    }
}
