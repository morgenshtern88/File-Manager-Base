using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class ListNewItems
    {
        public readonly string[] columns;
        public object state { get; set; }
        public ListNewItems(object state, params string[] columns)
        {
            this.state = state;
            this.columns = columns;
        }
        internal void Render(List<int> columnsWidth, int elementIndex, int ListViewX, int ListviewY)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                Console.CursorTop = elementIndex + ListviewY; ;
                Console.CursorLeft = ListViewX + columnsWidth.Take(i).Sum();
                Console.Write(GetStringWith(columns[i],columnsWidth[i]));
            }
        }
        internal void Clean(List<int> columnsWidht,int i, int x,int y)
        {
            Console.CursorTop = i + y;
            Console.CursorLeft = x;
            Console.Write(new string(' ', columnsWidht.Sum()));
        }

        private string GetStringWith(string v1, int maxLenght)
        {
            if (v1.Length < maxLenght)
            {
                return v1.PadRight(maxLenght, ' ');
                
            }
            else
            {
                return v1.Substring(0, maxLenght - 5) + "[  ]";
            }
        }

        
    }
}
