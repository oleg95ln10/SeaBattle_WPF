using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Models
{
    abstract class AbstractPlayer
    {
        String Name
        {
            get { return Name; }
            set { Name = value; }
        }
        int Score
        {
            get { return Score; }
            set { Score = value; }
        }
        int[,] OwnField
        {
            get { return OwnField; }
            set { OwnField = value; }
        }
        int[,] OpponentField
        {
            get { return OpponentField; }
            set { OpponentField = value; }
        }
        List<Ship> Ships { get; set; }
        public AbstractPlayer()
        {
            OwnField = new int[10, 10];
            OpponentField = new int[10, 10];
            Ships = new List<Ship>();
            Ships.Capacity = 10;
        }
    }
}
