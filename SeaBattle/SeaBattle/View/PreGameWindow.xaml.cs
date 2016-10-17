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
        private TypeOfShips _selectedShipType;
        private Label _selectedShipLabel;
        private List<int> _shipList;
        public PreGameWindow(ref Player player)
        {
            InitializeComponent();
            _shipList = new List<int>();
            for (int i = 0; i < 10; ++i)
            {
                TypeOfShips t = (TypeOfShips)AbstractPlayer.ShipArray[i];
                _shipList.Add((int)t);
            }
            this._player = player;
        }

        private void FieldController_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePosition = e.GetPosition(this);
            var x = (int)mousePosition.X / 25;
            var y = (int)mousePosition.Y / 25;
            var numbOfCell = x + y * 10;
            if (numbOfCell<100 && _selectedShipLenght>0 && _player.IsPuttedShipNotDiagonal(x,y) && _player.IsShipCanPut(x, y))
            {
                Button currentButton = (Button)fieldController.canvas.Children[numbOfCell];
                _player.PlaceShips(x,y,_selectedShipLenght);
                currentButton.Background = Brushes.Red;
                _selectedShipLenght--;
                var f = (int)_selectedShipType;
            }
            if (_selectedShipLenght == 0)
            {
                _shipList.Remove((int)_selectedShipType);
                if (!IsShipConsist())
                    _selectedShipLabel.Content = null;
            }

        }

        private void FourthClassShipLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GetShip(TypeOfShips.Fourth,fourthClassShipLabel);
        }
        private void ThirdClassShipLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GetShip(TypeOfShips.Third,thirdClassShipLabel);
        }
        private void SecondClassShipLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GetShip(TypeOfShips.Second,secondClassShipLabel);
        }
        private void FirstClassShipLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GetShip(TypeOfShips.First,firstClassShipLabel);
        }
        private void GetShip(TypeOfShips selectedShipTye, Label shipLabel)
        {
            _selectedShipType = selectedShipTye;

            if (IsShipConsist())
            {
                _selectedShipLenght = (int)selectedShipTye;
                _selectedShipLabel = shipLabel;
            }
        }
        private bool IsShipConsist()
        {
            return _shipList.Contains((int)_selectedShipType);
        }
    }
}