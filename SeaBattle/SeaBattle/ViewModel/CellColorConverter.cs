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
        public static void SetColorOfCell(ref UIElementCollection collection,ref Dictionary<int, int> history)
        {
            foreach (var k in history)
            {
                if (k.Value == 1)
                {
                    Button b = (Button)collection[k.Key];
                    b.Background = Brushes.Red;
                }
                if (k.Value == -1)
                {
                    Button b = (Button)collection[k.Key];
                    b.Background = Brushes.Yellow;
                }
            }
        }
    }
}
