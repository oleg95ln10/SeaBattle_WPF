using SeaBattle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SeaBattle.View
{
    /// <summary>
    /// Interaction logic for PreGameWindow.xaml
    /// </summary>
    public partial class PreGameWindow : Window
    {
        private AbstractPlayer _player;
        public PreGameWindow(ref AbstractPlayer player)
        {
            InitializeComponent();
            this._player = player;
        }

        private void Gsd_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = e.GetPosition(this);
            var x = (int)mousePosition.X / 25;
            var y = (int)mousePosition.Y / 25;
            var numbOfCell = x + y * 10;
            if (numbOfCell<100)
            {
                Button currentButton = (Button)fieldController.canvas.Children[numbOfCell];
                currentButton.Background = Brushes.Red;
            }
        }
    }
}