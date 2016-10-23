using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    /// <summary>
    /// Класс для реализации истории размещения кораблей,
    /// чтобы в случае изменения (постановки корабля)
    /// изменить цвет конкретных элементов,
    /// не перерисовывая все поле
    /// </summary>
    public class PlacementHistory
    {
        /// <summary>
        /// Выбран словарь, т.к. одна ячейка(ключ) не может иметь более 1 значения (есть корабль, нет корабля)
        /// </summary>
        private Dictionary<int, CellStatus> _history;
        public PlacementHistory()
        {
            _history = new Dictionary<int, CellStatus>();
        }

        public Dictionary<int, CellStatus> History
        {
            get  { return _history; }
            set  { _history = value;}
        }
    }
}
