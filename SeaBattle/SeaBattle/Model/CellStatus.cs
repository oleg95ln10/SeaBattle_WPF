using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    /// <summary>
    /// Перечисление для отображения статуса ячейки
    /// </summary>
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
