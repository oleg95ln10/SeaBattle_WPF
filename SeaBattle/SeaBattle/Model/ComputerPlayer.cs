using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    public class ComputerPlayer : AbstractPlayer, IAIPlayer
    {
        List<int> _shotMap;
        static Random _r;
        int _currentnumbOfCell;
        public ComputerPlayer()
            :base()
        {
            _shotMap = new List<int>();
            _r = new Random();
            _currentnumbOfCell = 0;
            GetShotMap();
        }
        public void GetShotMap()
        {
            for (int i = 0; i < 100; ++i)
                _shotMap.Add(i);
            Shuffle();
        }
        public void Shuffle()
        {

            int n = _shotMap.Count;
            while (n > 1)
            {
                n--;
                int k = _r.Next(n + 1);
                var value = _shotMap[k];
                _shotMap[k] = _shotMap[n];
                _shotMap[n] = value;
            }
        }
        public int GetNextCell()
        {
            if (_currentnumbOfCell < 100)
            {
                int cell = _shotMap[_currentnumbOfCell];
                _currentnumbOfCell++;
                return cell;
            }
            return 0;

        }
    }
}
