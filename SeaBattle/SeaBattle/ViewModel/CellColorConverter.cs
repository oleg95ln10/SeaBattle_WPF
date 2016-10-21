using SeaBattle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SeaBattle.ViewModel
{
    public static class CellColorConverter
    {
        public static void SetColorOfCell(ref UIElementCollection collection,ref Dictionary<int, CellStatus> history)
        {
            foreach (var k in history)
            {
                Button b = (Button)collection[k.Key];
                switch (k.Value)
                {
                    case CellStatus.ShipOn:
                        b.Background = Brushes.Red;
                        break;
                    case CellStatus.Busy:
                        b.Background = Brushes.Yellow;
                        break;
                }
            }
        }
    }
}