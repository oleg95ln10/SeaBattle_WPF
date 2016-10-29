using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    /// <summary>
    /// Enum for display the status of the cell
    /// </summary>
    public enum CellStatus
    {
        ShipOn = -2,
        Busy,
        Empty,
        PlayerShot,
        ComputerShot,
        ComputerShip
    }
}
