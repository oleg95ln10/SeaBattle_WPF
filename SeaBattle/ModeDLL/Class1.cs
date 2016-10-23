using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeDLL
{
    public class ShotMapCreator
    {
        protected List<int> _shotMap;
        protected string _fileToSave;
        static Random _r;

        public ShotMapCreator(string fileToSave)
        {
            _r = new Random();
            _fileToSave = fileToSave;
            _shotMap = new List<int>();
        }
        public virtual void GetShotMap()
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
        public virtual void Shuffle()
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
        public void WriteFieldToFile()
        {
            if (_shotMap.Count == 100)
            {
                using (FileStream stream = new FileStream(_fileToSave, FileMode.Create))
                {
                    foreach (var v in _shotMap)
                        stream.WriteByte((byte)v);
                }
            }
        }
    }
}
