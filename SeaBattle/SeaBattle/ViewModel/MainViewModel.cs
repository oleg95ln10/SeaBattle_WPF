using SeaBattle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.ViewModel
{
    class MainViewModel
    {
        Player p1;
        Player p2;

        public MainViewModel()
        {
            p1 = new Player();
            p2 = new Player();
        }
    }
}
