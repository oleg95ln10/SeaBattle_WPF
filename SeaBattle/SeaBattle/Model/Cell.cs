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
        private CellStatus _cellValue;
        public Cell()
        {
            _cellValue = (int)CellStatus.Empty;
        }
        public CellStatus CellValue
        {
            get {return _cellValue;}
            set {_cellValue = value; }
        }
    }
}
