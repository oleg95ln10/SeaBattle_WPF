using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    class Field
    {
        private Cell [,] _cells;
        public Field()
        {
            _cells = new Cell[10, 10];

            for (int i = 0 ; i < 10; ++i)
            {
                for (int j = 0 ; j < 10; ++j)
                {
                    _cells[i, j] = new Cell();
                }
            }
        }
        public Cell[,] Cells
        {
            get { return _cells; }
            set { _cells = value;}
        }
    }
}
