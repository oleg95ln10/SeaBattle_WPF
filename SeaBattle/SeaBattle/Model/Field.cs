using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    public class Field
    {
        private List<Cell> _cells;
        public Field()
        {
            _cells = new List<Cell>() { Capacity = 100};

            for (int i = 0 ; i < _cells.Capacity; ++i)
            {
                _cells.Add(new Cell());
            }
        }
        public List<Cell> Cells
        {
            get { return _cells; }
            set { _cells = value;}
        }
        public static int DecartToLine(int x, int y)
        {
            return x + (y * 10);
        }
    }
}
