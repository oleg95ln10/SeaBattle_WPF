using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    interface IAIPlayer
    {
        void GetShotMap();
        void Shuffle();
    }
}
