using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    /// <summary>
    /// Класс, описывающий игрока
    /// </summary>
    public abstract class AbstractPlayer
    {
        private String _name;// Имя игрока
        private int _score;// Счет игрока
        private Field _field;// Поле игрока
        private PlacementHistory _placementHist;// Используется для удобства закрашивания ячеек
        private int _shipCount;// Количество клеток, занимаемых кораблями (сумма чисел в ShipArray)
        private static Random _r;// Для автоматической расстановки кораблей
        private bool _isShotOnShip;// Попали ли по кораблю
        private int _move; // Количество ходов (чем меньше, тем больше счет)
        private int[] _currentShipArray;
        public static readonly int[] SHIP_ARRAY = { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 }; // Количество кораблей

        public AbstractPlayer()
        {
            _field = new Field();
            _placementHist = new PlacementHistory();
            _r = new Random();
            _shipCount = 20;
            _move = 0;
            _currentShipArray = new int[SHIP_ARRAY.Length];
            _currentShipArray = (int[])SHIP_ARRAY.Clone();
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
        public int ShipCount
        {
            get { return _shipCount;  }
            set { _shipCount = value; }
        }

        public bool IsShotOnShip
        {
            get { return _isShotOnShip; }
            set { _isShotOnShip = value; }
        }

        public int[] CurrentShipArray
        {
            get  { return _currentShipArray;   }
            set  { _currentShipArray = value;  }
        }
        #endregion

        /// <summary>
        /// Обнулить поле
        /// </summary>
        public void ResetField()
        {
            foreach (Cell c in _field)
            {
                c.CellValue = CellStatus.Empty;
            }
        }

        /// <summary>
        /// Авторасстановка кораблей
        /// Проходим по массиву и в соответствии с правилами на поле расставляем корабли
        /// </summary>
        public void AutomaticShipPlacing()
        {
            try
            {

                Array.Reverse(_currentShipArray);
                ShipDirection shipDirection;
                int startX = 0;
                int startY = 0;

                foreach (var v in _currentShipArray)
                {
                    if (v > 0)
                    {
                        GetRandomShipDirection(out shipDirection);
                        GetRandomCoordinates(ref startX, ref startY);
                        while (!IsCanBePlaced(startX, startY, v, shipDirection))
                        {
                            GetRandomCoordinates(ref startX, ref startY);
                            GetRandomShipDirection(out shipDirection);
                        }

                        PlaceShips(startX, startY, v, shipDirection);

                        PlacementHist.History.Clear();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Метод для расстановки кораблей
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="shipLenght"></param>
        /// <param name="shipDirection"></param>
        public void PlaceShips(int x, int y, int shipLenght, ShipDirection shipDirection)
        {
            try
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
                    _currentShipArray.SetValue(-100, Array.IndexOf(_currentShipArray, _currentShipArray.Max()));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Метод для проверки возмодности постановки корабля
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="shipLenght"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool IsCanBePlaced(int x, int y, int shipLenght, ShipDirection direction)
        {
            try
            {
                if ( Field.Cells[ Field.DecartToLine( x, y ) ].CellValue == CellStatus.Empty )
                {
                    switch ( direction )
                    {
                        case ShipDirection.Horizontal:
                            if ( IsPossibleCoordinate( x + shipLenght - 1 ) )
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Можем ли атаковать оппонента
        /// </summary>
        /// <param name="attackingPlayer"></param>
        /// <param name="indexOfFieldCell"></param>
        /// <param name="playerShotType"></param>
        /// <param name="opponentFindedShip"></param>
        /// <returns></returns>
        public bool IsCanOpponentBeAttacked(AbstractPlayer attackingPlayer, int indexOfFieldCell, CellStatus playerShotType, CellStatus opponentFindedShip)
        {
            try
            {
                if (indexOfFieldCell >= 0 && indexOfFieldCell < Field.Cells.Count)
                    // Если мы раньше не стреляли по этому полю
                    if ((attackingPlayer.Field.Cells[indexOfFieldCell].CellValue != playerShotType 
                        || attackingPlayer.Field.Cells[indexOfFieldCell].CellValue != opponentFindedShip)
                        && !PlacementHist.History.ContainsKey(indexOfFieldCell))
                        return true;
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// Атаковать игрока
        /// Если попали на корабль противника прибавляем счет
        /// </summary>
        /// <param name="attackingPlayer"></param>
        /// <param name="indexOfFieldCell"></param>
        /// <param name="opponentFindedShip"></param>
        /// <param name="shotType"></param>
        public void AttackPlayer(AbstractPlayer attackingPlayer, int indexOfFieldCell, CellStatus opponentFindedShip, CellStatus shotType)
        {
            try
            {
                if (IsCanOpponentBeAttacked(attackingPlayer, indexOfFieldCell, shotType, opponentFindedShip))
                {
                    _move++;
                    if (attackingPlayer.Field.Cells[indexOfFieldCell].CellValue == CellStatus.ShipOn)
                    {
                        _isShotOnShip = true;
                        attackingPlayer.Field.Cells[indexOfFieldCell].CellValue = opponentFindedShip;
                        PlacementHist.History.Add(indexOfFieldCell, opponentFindedShip);
                        ChangeScore(attackingPlayer);
                        attackingPlayer.ShipCount--;
                    }
                    else
                    {
                        _isShotOnShip = false;
                        attackingPlayer.Field.Cells[indexOfFieldCell].CellValue = shotType;
                        PlacementHist.History.Add(indexOfFieldCell, shotType);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// Изменяем счет в зависимости от количества ходов
        /// </summary>
        /// <param name="attackingPlayer"></param>
        private void ChangeScore(AbstractPlayer attackingPlayer)
        {
            try
            {
                if (_move <= 25)
                    _score += 20;
                else if (_move <= 50)
                    _score += 15;
                else if (_move <= 75)
                    _score += 10;
                else
                    _score += 5;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// Размещаем на поле корабль по горизонтали и записываем клетки для закрашивания
        /// на поле
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="shipLenght"></param>
        private void PlaceHorizontalShip(int x, int y, int shipLenght)
        {
            try
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
       
        /// <summary>
        /// Размещаем на поле корабль по вертикали и записываем клетки для закрашивания
        /// на поле
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="shipLenght"></param>
        private void PlaceVerticalShip(int x, int y, int shipLenght)
        {
            try
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Получить случайное направление для постановки корабля
        /// </summary>
        /// <param name="direction"></param>
        private void GetRandomShipDirection(out ShipDirection direction)
        {
            try
            {
                var rand = _r.Next(0, 100000);

                if (rand % 2 == 0)
                    direction = ShipDirection.Horizontal;
                else
                    direction = ShipDirection.Vertical;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Получить случайные координаты для постановки корабля
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void GetRandomCoordinates(ref int x, ref int y)
        {
            try
            {
                x = y = _r.Next(0, 9);

                while (Field.Cells[Field.DecartToLine(x, y)].CellValue != CellStatus.Empty)
                {
                    x = _r.Next(0, 9);
                    y = _r.Next(0, 9);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Проверка на корректность координат
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        private bool IsPossibleCoordinate(int coordinate)
        {
            try
            {
                if (coordinate >= 0 && coordinate <= 9)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Метод для связывания значения координат на поле с их значениями
        /// </summary>
        /// <param name="key"></param>
        /// <param name="status"></param>
        protected void AddValuesToDictAndField(int key, CellStatus status = CellStatus.Busy)
        {
            try
            {
                Field.Cells[key].CellValue = status;
                PlacementHist.History.Add(key, status);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
