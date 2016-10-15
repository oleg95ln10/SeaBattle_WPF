using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    class Cell
    {
        public static readonly int CellSize = 20;
        /// <summary>
        /// 0 - Пусто
        /// 1 - Корабль
        /// 2 - Подбитый корабль
        /// </summary>
        private int _cellValue;
        public Cell()
        {
            _cellValue = 0;
        }
        public int CellValue
        {
            get {return _cellValue;}
            set {_cellValue = value; }
        }
    }
}
