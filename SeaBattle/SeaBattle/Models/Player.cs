using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Models
{
    class Player
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
        int [,] Field
        {
            get { return Field; }
            set { Field = value; }
        }
        List<Ship> Ships { get; set; }
        public Player()
        {
            Field = new int[10, 10];
            Ships = new List<Ship>();
            Ships.Capacity = 10;
        }
    }
}
