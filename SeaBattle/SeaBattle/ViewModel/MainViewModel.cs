using SeaBattle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.ViewModel
{
    /// <summary>
    /// Класс модель
    /// </summary>
    public class MainViewModel
    {
        Player _firstPlayer;
        ComputerPlayer _computerPlayer;
        public MainViewModel()
        {
            _firstPlayer = new Player();
            _computerPlayer = new ComputerPlayer();
        }

        public Player FirstPlayer
        {
            get { return _firstPlayer; }
            set {_firstPlayer = value; }
        }
        public ComputerPlayer ComputerPlayer
        {
            get {return _computerPlayer; }
            set {_computerPlayer = value; }
        }
    }
}
