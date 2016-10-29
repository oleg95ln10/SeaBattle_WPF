using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    /// <summary>
    /// Class implements a computer player
    /// </summary>
    public class ComputerPlayer : AbstractPlayer
    {
        private static Random _r;
        private List<int> _shotMap;// Map of hit opponent ships
        int _currentnumbOfCell;// Current cell from shot map

        public ComputerPlayer()
            :base()
        {
            _shotMap = new List<int>();
            _r = new Random();
            _currentnumbOfCell = 0;
        }

        public void GenerateMap(bool isRandomMap, string mapFilename)
        {
            if (_shotMap.Count == 0)
            if (!isRandomMap && mapFilename != null)
            {
                using (FileStream stream = new FileStream(mapFilename, FileMode.Open))
                {
                    byte[] array = new byte[stream.Length];

                    stream.Read(array, 0, array.Length);

                    foreach (var b in array)
                    {
                        _shotMap.Add(Convert.ToInt32(b));
                    }
                }
            }
            else
                GetShotMap();
        }

        public void GetShotMap()
        {
            try
            {
                for (int i = 0; i < 100; ++i)
                    _shotMap.Add(i);
                Shuffle();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Mix the map
        /// </summary>
        public void Shuffle()
        {
            try
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Get next cell for shot
        /// </summary>
        /// <returns></returns>
        public int GetNextCell()
        {
            try
            {
                if (_currentnumbOfCell < 100)
                {
                    int cell = _shotMap[_currentnumbOfCell];
                    _currentnumbOfCell++;
                    return cell;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
