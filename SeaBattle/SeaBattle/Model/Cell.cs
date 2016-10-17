using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SeaBattle.Model
{
    enum CellType { Unknown, Water, Undamaged, Damaged, Sunk }
    public class Cell : DependencyObject
    {
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(CellType), typeof(Cell), null);
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
