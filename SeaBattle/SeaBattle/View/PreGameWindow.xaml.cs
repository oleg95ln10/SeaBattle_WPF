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
        private Player _player;
        private int _selectedShipLenght;
        private int[] _shipArray = { 1, 1, 1, 1, 2, 2, 2, 3, 3, 4 };
        public PreGameWindow(ref Player player)
        {
            InitializeComponent();
            this._player = player;
        }

        private void FieldController_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = e.GetPosition(this);
            var x = (int)mousePosition.X / 25;
            var y = (int)mousePosition.Y / 25;
            var numbOfCell = x + y * 10;
            if (numbOfCell<100 && _selectedShipLenght>0 && _player.IsShipCanPut(x,y))
            {
                Button currentButton = (Button)fieldController.canvas.Children[numbOfCell];
                _player.PlaceShips(x,y,_selectedShipLenght);
                currentButton.Background = Brushes.Red;
                _selectedShipLenght--;
            }
        }

        private void FourthClassShipLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _selectedShipLenght = 4;
        }

        private void ThirdClassShipLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _selectedShipLenght = 3;
        }

        private void SecondClassShipLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _selectedShipLenght = 2;
        }

        private void FirstClassShipLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _selectedShipLenght = 1;
        }
    }
}