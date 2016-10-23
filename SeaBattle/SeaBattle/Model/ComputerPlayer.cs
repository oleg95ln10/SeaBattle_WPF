using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model
{
    /// <summary>
    /// Класс для реализации компьютерного игрока
    /// Реализует класс для ИИ
    /// </summary>
    public class ComputerPlayer : AbstractPlayer, IAIPlayer
    {
        List<int> _shotMap;// Карта обстрела корабля противника
        static Random _r;
        int _currentnumbOfCell;// Текущая ячейка из карты обстрела
        public ComputerPlayer()
            :base()
        {
            _shotMap = new List<int>();
            _r = new Random();
            _currentnumbOfCell = 0;
            GetShotMap();
        }

        /// <summary>
        /// Реализация интерфейса IAIPlayer
        /// Заполнить карту числами от 1 до 99
        /// и перемешать
        /// </summary>
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
        /// Перемешать карту
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
        /// Извлечь следущую ячейку для обстрела
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
