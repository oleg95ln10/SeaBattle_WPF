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
        AbstractPlayer _firstPlayer;
        AbstractPlayer _secondPlayer;

        public MainViewModel()
        {
            _firstPlayer = new Player();
            _secondPlayer = new Player();
        }

        public AbstractPlayer FirstPlayer
        {
            get { return _firstPlayer; }
            set {_firstPlayer = value; }
        }
        public AbstractPlayer SecondPlayer
        {
            get {return _secondPlayer; }
            set {_secondPlayer = value; }
        }
    }
}
