using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    public class Ship
    {
        private TypeOfShips type;
        private ShipStatus status;
        public TypeOfShips Type
        {
            get { return type; }
            set { type = value;}
        }
        public ShipStatus Status
        {
            get { return status; }
            set { status = value;}
        }
    }
}
