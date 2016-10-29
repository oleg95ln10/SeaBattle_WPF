using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    /// <summary>
    /// The class for the implementation
    /// of the history of the ships placin',
    /// if we place ship we change the color
    /// of specific elements without redrawing the entire field
    /// </summary>
    public class PlacementHistory
    {
        /// <summary>
        /// Dictionary selected because one cell (the key) can't have more than one value (a ship, there is a ship)
        /// </summary>
        private Dictionary<int, CellStatus> _history;

        public PlacementHistory()
        {
            _history = new Dictionary<int, CellStatus>();
        }

        public Dictionary<int, CellStatus> History
        {
            get  { return _history; }
            set  { _history = value;}
        }
    }
}
