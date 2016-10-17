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
        private List<Ship> _ships;
        private Field _field;
        public AbstractPlayer()
        {
            _ships = new List<Ship>();
            _ships.Capacity = 10;
            AddShips();
            _field = new Field();
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
        public List<Ship> Ships
        {
            get { return _ships; }
            set { _ships = value; }
        }
        public Field Field
        {
            get { return _field; }
            set { _field = value;}
        }
        #endregion
        private void AddShips()
        {
            //локальный массив с количеством разнопалубных кораблей
            int [] arr = { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 };

            for (int i = 0; i < 10; ++i)
            {
                TypeOfShips t = (TypeOfShips)arr[i];
                Ships.Add(new Ship { Type = t, Status = ShipStatus.Live });
            }
        }
    }
}
