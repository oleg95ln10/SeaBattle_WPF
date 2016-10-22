using SeaBattle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.ViewModel
{
    public class MainViewModel
    {
        Player _firstPlayer;
        AbstractPlayer _computerPlayer;

        public MainViewModel()
        {
            _firstPlayer = new Player();
            _computerPlayer = new Player();
        }

        public Player FirstPlayer
        {
            get { return _firstPlayer; }
            set {_firstPlayer = value; }
        }
        public AbstractPlayer ComputerPlayer
        {
            get {return _computerPlayer; }
            set {_computerPlayer = value; }
        }
    }
}
