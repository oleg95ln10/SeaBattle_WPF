using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    public class Player : AbstractPlayer
    {
        private int _previousRow;
        private int _previousColumn;
        public Player()
            : base()
        {
            _previousRow = -1;
        }
        public void PlaceShips()
        {

        }

        public void PlaceShips(int x, int y, int shipLenght)
        {
            if (!IsShipCanPut(x, y))
                return;
            Field.Cells[x, y].CellValue = shipLenght;

        }
        public bool IsShipCanPut(int row, int column)
        {
            bool result = false;
            if (Field.Cells[row, column].CellValue == 0)
                result = true;
            _previousColumn = column;
            _previousRow = row;
            return result;
        }
        public bool IsPuttedShipNotDiagonal(int row, int column)
        {
            bool isChangedRow, IsChangedColumn;


            if ((row == _previousRow || column == _previousRow) || _previousRow < 0)
                return true;
            return false;
        }
    }
}
