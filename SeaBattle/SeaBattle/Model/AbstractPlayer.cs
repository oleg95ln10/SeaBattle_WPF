using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    public abstract class AbstractPlayer
    {
        private String _name;
        private int _score;
        private Field _field;
        private PlacementHistory _placementHist;
        private Random _r;
        public static readonly int[] ShipArray = { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 };
        public AbstractPlayer()
        {
            _field = new Field();
            _placementHist = new PlacementHistory();
            _r = new Random();
        }
        #region Properties
        public int Score
        {
            get { return _score; }
            set { _score = value;}
        }
        public string Name
        {
            get { return _name; }
            set { _name = value;}
        }
        public Field Field
        {
            get { return _field; }
            set { _field = value;}
        }
        public PlacementHistory PlacementHist
        {
            get { return _placementHist;  }
            set { _placementHist = value; }
        }
        #endregion
        public void ResetField()
        {
            foreach (Cell l in _field)
            {
                l.CellValue = CellStatus.Empty;
            }
        }
        public void AutomaticShipPlacing()
        {
            //Если верить математике, у нас будет всегда как минимум 1 расстановка кораблей, 
            // удовлетворяющая правлам, если начать расставлять с наибольшего корабля
            var tempShipArray = (int [])ShipArray.Clone();
            Array.Reverse(tempShipArray);
            ShipDirection shipDirection;
            int startX = 0;
            int startY = 0;
            int tempX, tempY;
            foreach (var v in tempShipArray)
            {
                //startY = startX = 7;
                GetRandomShipDirection(out shipDirection);
                GetRandomCoordinates(out startX, out startY);

                if (IsCanBePlaced(startX, startY, v, shipDirection) && Field.Cells[Field.DecartToLine(startX, startY)].CellValue == CellStatus.Empty)
                    PlaceShips(startX, startY, v, shipDirection);
                else
                {
                    while (!IsCanBePlaced(startX, startY, v, shipDirection) && Field.Cells[Field.DecartToLine(startX,startY)].CellValue != CellStatus.Empty)
                    {
                        GetRandomCoordinates(out tempX, out tempY);
                        startX = tempX;
                        startY = tempY;
                        GetRandomShipDirection(out shipDirection);
                    }
                    PlaceShips(startX, startY, v, shipDirection);
                }
                PlacementHist.History.Clear();
            }




            //Field.Cells[0].CellValue = CellStatus.ShipOn;
            //_placementHist.History.Add(0, CellStatus.ShipOn);
        }
        public void PlaceShips(int x, int y, int shipLenght, ShipDirection shipDirection)
        {
            if (Field.DecartToLine(x, y) < Field.Cells.Count && IsCanBePlaced(x, y, shipLenght, shipDirection))
            {
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
        public bool IsCanBePlaced(int x, int y, int shipLenght, ShipDirection direction)
        {
            if ( Field.Cells[ Field.DecartToLine( x, y ) ].CellValue == CellStatus.Empty )
            {
                switch ( direction )
                {
                    case ShipDirection.Horizontal:
                        if( IsPossibleCoordinate( x + shipLenght - 1 ) )
                            if ( Field.Cells[ Field.DecartToLine( x + shipLenght - 1, y ) ].CellValue == CellStatus.Empty )
                                return true;
                        break;
                    case ShipDirection.Vertical:
                        if ( IsPossibleCoordinate( y + shipLenght - 1) )
                            if ( Field.Cells[ Field.DecartToLine( x , y + shipLenght - 1 ) ].CellValue == CellStatus.Empty )
                                return true;
                        break;
                }
            }
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
        private void GetRandomShipDirection(out ShipDirection direction)
        {
            var rand = _r.Next(0, 100000);
            if (rand % 2 == 0)
                direction = ShipDirection.Horizontal;
            else
                direction = ShipDirection.Vertical;
        }
        private void GetRandomCoordinates(out int x, out int y)
        {
            x = y = _r.Next(0,9);
            while (Field.Cells[Field.DecartToLine(x,y)].CellValue != CellStatus.Empty)
            {
                x = _r.Next(0, 9);
                y = _r.Next(0, 9);
            }
        }
        private bool IsPossibleCoordinate(int coordinate)
        {
            if ( coordinate >= 0 && coordinate <= 9 )
                return true;
            return false;
        }
        protected void AddValuesToDictAndField(int key, CellStatus status = CellStatus.Busy)
        {
            Field.Cells[ key ].CellValue = status;
            PlacementHist.History.Add( key, status );
        }
    }
}
