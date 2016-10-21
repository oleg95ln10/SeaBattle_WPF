using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    public class Field : IEnumerable, IEnumerator
    {
        private List<Cell> _cells;
        private int _index = -1;
        public Field()
        {
            _index = -1;
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
        public object Current
        {
            get  { return _cells[_index]; }
        }
        public IEnumerator GetEnumerator()
        {
            return this;
        }
        public bool MoveNext()
        {
            if (_index == _cells.Count - 1)
            {
                Reset();
                return false;
            }

            _index++;
            return true;
        }
        public void Reset()
        {
            _index = -1;
        }
    }
}
