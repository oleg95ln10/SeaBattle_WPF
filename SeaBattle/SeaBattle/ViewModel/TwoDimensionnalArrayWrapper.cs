using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.ViewModel
{
    /// <summary>
    /// Helper for binding fieldUserControl and array of ints (player's field)
    /// </summary>
    class TwoDimensionnalArrayWrapper<T> : IEnumerable<IEnumerable<T>>
    {
        T[,] _array;

        public TwoDimensionnalArrayWrapper(T[,] array)
        {
            this._array = array;
        }

        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            var w = _array.GetLength(0);
            for (int i = 0; i < w; i++)
                yield return ColumnWrapper(i);
        }

        IEnumerable<T> ColumnWrapper(int i)
        {
            var h = _array.GetLength(1);
            for (int j = 0; j < h; j++)
                yield return _array[i, j];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}