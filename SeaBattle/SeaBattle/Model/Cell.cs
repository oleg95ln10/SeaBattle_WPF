using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SeaBattle.Model
{
    /// <summary>
    /// Class implements the cell of field
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
