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
            get  { return _placementHist;  }
            set  { _placementHist = value; }
        }

        public void PlaceShips(int x, int y, int shipLenght, ShipDirection shipDirection)
        {
            if (Field.DecartToLine(x,y) < Field.Cells.Count && IsCanBePlaced(x, y))
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
                    var coord = ((x - 1) + (y - 1) * 10);
                    AddValuesToDictAndField(coord, -1);
                }
                //if (x + shipLenght <= 9 && y - 1 >= 0)
                //{
                //    Button mostUpRightChildButton = (Button)fieldController.canvas.Children[(x + shipLenght) + (y - 1) * 10];
                //    mostUpRightChildButton.Background = Brushes.Yellow;
                //}
                //if (x - 1 >= 0 && x - 1 <= 9)
                //{
                //    Button mostMidleLeftChildButton = (Button)fieldController.canvas.Children[(x - 1) + y * 10];
                //    mostMidleLeftChildButton.Background = Brushes.Yellow;
                //}
                //if (x + shipLenght <= 9)
                //{
                //    Button mostMidleRightChildButton = (Button)fieldController.canvas.Children[(x + shipLenght) + y * 10];
                //    mostMidleRightChildButton.Background = Brushes.Yellow;
                //}
                //if (y + 1 <= 9 && x - 1 >= 0)
                //{
                //    Button mostDownLeftChildButton = (Button)fieldController.canvas.Children[(x - 1) + (y + 1) * 10];
                //    mostDownLeftChildButton.Background = Brushes.Yellow;
                //}
                //if (x + shipLenght <= 9 && y + 1 <= 9)
                //{
                //    Button mostDownRightChildButton = (Button)fieldController.canvas.Children[(x + shipLenght) + (y + 1) * 10];
                //    mostDownRightChildButton.Background = Brushes.Yellow;
                //}
                //for (int i = 0; i < shipLenght; ++i)
                //{
                //    var numbOfShipCell = x + i + y * 10;
                //    Button childButton = (Button)fieldController.canvas.Children[numbOfShipCell];
                //    childButton.Background = Brushes.Red;
                //    if (numbOfShipCell - 10 >= 0)
                //    {
                //        Button higherChildButton = (Button)fieldController.canvas.Children[numbOfShipCell - 10];
                //        higherChildButton.Background = Brushes.Yellow;
                //    }
                //    if (numbOfShipCell + 10 <= 99)
                //    {
                //        Button belowerChildButton = (Button)fieldController.canvas.Children[numbOfShipCell + 10];
                //        belowerChildButton.Background = Brushes.Yellow;
                //    }
                //}
            }
        }
        private void PlaceVerticalShip(int x, int y, int shipLenght)
        {

        }
        private void AddValuesToDictAndField(int key,int value)
        {
            Field.Cells[key].CellValue = value;
            _placementHist.History.Add(key, value);
        }
    }
}
