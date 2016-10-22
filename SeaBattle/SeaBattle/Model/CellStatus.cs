using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    public enum CellStatus
    {
        ShipOn = -2,
        Busy,
        Empty,
        PlayerShot,
        PlayerShip,
        ComputerShot,
        ComputerShip
    }
}
