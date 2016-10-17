using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    public class Player : AbstractPlayer
    {
        public Player()
            : base()
        {
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
            if (Field.Cells[row, column].CellValue == 0)
                return true;
            return false;
        }
    }
}
