using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    abstract class AbstractPlayer
    {
        #region Properties
        private String name;
        private int score;
        private int[,] ownField;
        private int[,] opponentField;
        private List<Ship> ships;
        #endregion
        public int[,] OwnField
        {
            get { return ownField; }
            set { ownField = value;}
        }
        public int[,] OpponentField
        {
            get { return opponentField; }
            set { opponentField = value;}
        }

        public int Score
        {
            get { return score; }
            set { score = value;}
        }
        public string Name
        {
            get { return name; }
            set { name = value;}
        }
        public List<Ship> Ships
        {
            get { return ships; }
            set { ships = value; }
        }
        public AbstractPlayer()
        {
            OwnField = new int[10, 10];
            OpponentField = new int[10, 10];
            Ships = new List<Ship>();
            Ships.Capacity = 10;
            AddShips();
        }
        private void AddShips()
        {
            //локальный массив с количеством разнопалубных кораблей
            int []arr = { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 };

            for (int i = 0; i < 10; ++i)
            {
                TypeOfShips t = (TypeOfShips)arr[i];
                Ships.Add(new Ship { Type = t, Status = ShipStatus.Live });
            }
        }
    }
}
