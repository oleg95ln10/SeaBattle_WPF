using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SeaBattle.Model
{
    public class Cell : DependencyObject
    {
        public static readonly int CellSize = 25;
        /// <summary>
        /// -1 - Недостижимое поле
        /// 0 - Пусто, Можно ставить корабль
        /// 1 - Стоит корабль
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
