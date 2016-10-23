using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SeaBattle.Model
{
    /// <summary>
    /// Класс для описания ячейки поля
    /// </summary>
    public class Cell
    {
        public static readonly int CellSize = 25;
        private CellStatus _cellValue;
        public Cell()
        {
            _cellValue = CellStatus.Empty;
        }
        public CellStatus CellValue
        {
            get {return _cellValue;}
            set {_cellValue = value; }
        }
    }
}
