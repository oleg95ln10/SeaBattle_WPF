using SeaBattle.Model;
using SeaBattle.ViewModel;
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
        bool _isCanmove;
        private int _currentShip;
        Point _mousePosition;
        ShipDirection _direction;
        public PreGameWindow(ref Player player)
        {
            InitializeComponent();
            _currentShip = AbstractPlayer.ShipArray.Max();
            ship.Height = Cell.CellSize-2;
            ship.Width = (Cell.CellSize-2) * _currentShip;
            _isCanmove = false;
            this._player = player;
            _direction = ShipDirection.Horizontal;
        }

        private void FieldController_PreviewMouseLeftButtonDown(object sender)
        {
            Button but = sender as Button;
            var x = (int)(Canvas.GetLeft(but) / Cell.CellSize);
            var y = (int)(Canvas.GetTop(but) / Cell.CellSize);
            lab1.Content = Canvas.GetLeft(but) / Cell.CellSize;
            shipDirection.Content = Canvas.GetTop(but) / Cell.CellSize;

            switch (_direction)
            {
                case ShipDirection.Horizontal:
                    if ((x + _currentShip <= 10 && y <= 9) && (x + _currentShip >= 0 && y >= 0))
                        for (int i = 0; i < _currentShip; ++i)
                        {
                            var numbOfCell = x + i + y * 10;
                            Button childButton = (Button)fieldController.canvas.Children[numbOfCell];
                            childButton.Background = Brushes.Red;
                        }
                    break;
            }


            //if (numbOfCell<100 && _selectedShipLenght>0 && _player.IsShipCanPut(x, y))
            //{
            //    Button currentButton = (Button)fieldController.canvas.Children[numbOfCell];
            //    _player.PlaceShips(x,y,_selectedShipLenght);
            //    currentButton.Background = Brushes.Red;
            //    _selectedShipLenght--;
            //}
            //if (_selectedShipLenght == 0)
            //{
            //    _shipList.Remove((int)_selectedShipType);
            //    if (!IsShipConsist())
            //        _selectedShipLabel.Content = null;
            //}
        }
        private void ship_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button but = sender as Button;
            Mouse.Capture(but);
            _mousePosition = Mouse.GetPosition(but);
            _isCanmove = true;
        }
        private void ship_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_isCanmove)
            {
                Button but = sender as Button;
                but.SetValue(Canvas.LeftProperty, e.GetPosition(null).X - _mousePosition.X);
                but.SetValue(Canvas.TopProperty, e.GetPosition(null).Y - _mousePosition.Y);
                FieldController_PreviewMouseLeftButtonDown(sender);
            }
        }

        private void ship_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            _isCanmove = false;
        }

        private void HorizontalButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ship.LayoutTransform = new RotateTransform(0);
            _direction = ShipDirection.Horizontal;
        }
        private void VerticalButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ship.LayoutTransform = new RotateTransform(90);
            _direction = ShipDirection.Vertical;
        }
    }
}