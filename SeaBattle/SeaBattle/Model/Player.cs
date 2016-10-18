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

        public void PlaceShips(int x, int y, int shipLenght)
        {
            //if (!IsShipCanPut(x, y))
            //    return;
            //Field.Cells[x, y].CellValue = shipLenght;

        }
    }
}
