using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    public class Player : AbstractPlayer
    {
        private PlacementHistory _placementHist;
        public Player()
            : base()
        {
            _placementHist = new PlacementHistory();
        }

        public PlacementHistory PlacementHist
        {
            get { return _placementHist; }
            set { _placementHist = value; }
        }

        public void PlaceShips(int x, int y, int shipLenght, ShipDirection shipDirection)
        {
            if (Field.DecartToLine(x, y) < Field.Cells.Count && IsCanBePlaced(x, y))
            {
                var key = Field.DecartToLine(x, y);
                //Field.Cells[key].CellValue = 1;
                //_placementHist.History.Add(key, 1);
                switch (shipDirection)
                {
                    case ShipDirection.Horizontal:
                        PlaceHorizontalShip(x, y, shipLenght);
                        break;
                    case ShipDirection.Vertical:
                        PlaceVerticalShip(x, y, shipLenght);
                        break;
                }

            }
        }
        public bool IsCanBePlaced(int x, int y)
        {
            if (Field.Cells[Field.DecartToLine(x, y)].CellValue == 0)
                return true;
            return false;
        }
        private void PlaceHorizontalShip(int x, int y, int shipLenght)
        {
            if ((x + shipLenght <= 10 && y <= 9) && (x + shipLenght >= 0 && y >= 0))
            {
                // Левая верхняя ячейка
                if (y - 1 >= 0 && x - 1 >= 0)
                {
                    AddValuesToDictAndField(((x - 1) + (y - 1) * 10));
                }
                // Правая верхняя ячейка
                if (x + shipLenght <= 9 && y - 1 >= 0)
                {
                    AddValuesToDictAndField(((x + shipLenght) + (y - 1) * 10));
                }
                // Средняя левая ячейка
                if (x - 1 >= 0 && x - 1 <= 9)
                {
                    AddValuesToDictAndField(((x - 1) + y * 10));
                }
                // Средняя правая ячейка
                if (x + shipLenght <= 9)
                {
                    AddValuesToDictAndField((x + shipLenght) + y * 10);
                }
                // Левая нижняя ячейка
                if (y + 1 <= 9 && x - 1 >= 0)
                {
                    AddValuesToDictAndField((x - 1) + (y + 1) * 10);
                }
                // Правая нижняя ячейка
                if (x + shipLenght <= 9 && y + 1 <= 9)
                {
                    AddValuesToDictAndField((x + shipLenght) + (y + 1) * 10);
                }
                for (int i = 0; i < shipLenght; ++i)
                {
                    var numbOfShipCell = x + i + y * 10;
                    AddValuesToDictAndField(numbOfShipCell, CellStatus.ShipOn);
                    if (numbOfShipCell - 10 >= 0)
                    {
                        AddValuesToDictAndField(numbOfShipCell - 10);
                    }
                    if (numbOfShipCell + 10 <= 99)
                    {
                        AddValuesToDictAndField(numbOfShipCell + 10);
                    }
                }
            }
        }
        private void PlaceVerticalShip(int x, int y, int shipLenght)
        {
            if ((x <= 10 && y + shipLenght <= 10) && (y + shipLenght >= 0 && x >= 0))
            {
                // Левая верхняя ячейка
                if (y - 1 >= 0 && x - 1 >= 0)
                {
                    AddValuesToDictAndField((x - 1) + (y - 1) * 10);
                }
                // Правая верхняя ячейка
                if (x + 1 <= 9 && y - 1 >= 0)
                {
                    AddValuesToDictAndField((x + 1) + (y - 1) * 10);
                }
                // Средняя левая ячейка
                if (y - 1 >= 0)
                {
                    AddValuesToDictAndField(x + (y - 1) * 10);
                }
                // Средняя правая ячейка
                if (y + shipLenght <= 9 && x - 1 >= 0)
                {
                    AddValuesToDictAndField((x - 1) + (y + shipLenght) * 10);
                }
                // Левая нижняя ячейка
                if (y + shipLenght <= 9)
                {
                    AddValuesToDictAndField(x + (y + shipLenght) * 10);
                }
                // Правая нижняя ячейка
                if (y + shipLenght <= 9 && x + 1 <= 9)
                {
                    AddValuesToDictAndField((x + 1) + (y + shipLenght) * 10);
                }
                for (int i = 0; i < shipLenght; ++i)
                {
                    var numbOfShipCell = x + (y + i) * 10;
                    AddValuesToDictAndField(numbOfShipCell, CellStatus.ShipOn);
                    if (x - 1 >= 0)
                    {
                        AddValuesToDictAndField(numbOfShipCell - 1);
                    }
                    if (x + 1 <= 9)
                    {
                        AddValuesToDictAndField(numbOfShipCell + 1);
                    }
                }
            }
        }
        private void AddValuesToDictAndField(int key, CellStatus status = CellStatus.Busy)
        {
            Field.Cells[key].CellValue = (int)status;
            _placementHist.History.Add(key, status);
        }
    }
}
