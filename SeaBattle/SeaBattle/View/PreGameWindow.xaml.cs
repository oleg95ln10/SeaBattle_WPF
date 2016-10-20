using SeaBattle.Model;
using SeaBattle.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private bool _isCanmove;
        private int[] _shipArray;
        private bool _isShipCanPlace;
        private int _currentShip;
        Point _mousePosition;
        Point _zeroShipPosition;
        ShipDirection _shipDirection;
        public PreGameWindow(ref Player player)
        {
            InitializeComponent();
            _shipArray = new int[AbstractPlayer.ShipArray.Length];
            _shipArray = AbstractPlayer.ShipArray;
            _currentShip = _shipArray.Max();
            _shipArray.SetValue(-100, Array.IndexOf(_shipArray, _shipArray.Max()));
            ship.Height = Cell.CellSize-2;
            ship.Width = (Cell.CellSize-2) * _currentShip;
            _zeroShipPosition = new Point(Canvas.GetLeft(ship), Canvas.GetTop(ship));
            _isShipCanPlace = true;
            _isCanmove = false;
            this._player = player;
            _shipDirection = ShipDirection.Horizontal;
        }

        private void FieldController_PreviewMouseLeftButtonDown(object sender)
        {
            Button but = sender as Button;
            var x = (int)(Canvas.GetLeft(but) / Cell.CellSize);
            var y = (int)(Canvas.GetTop(but) / Cell.CellSize);

            if (_player.IsCanBePlaced(x, y))
            {
                _isShipCanPlace = true;
            }
            else
               _isShipCanPlace = false;
            if (Mouse.GetPosition(this).X < 250 && Mouse.GetPosition(this).Y < 243 && _isShipCanPlace)
            {
                lab1.Content = Mouse.GetPosition(this);
                shipDirection.Content = y + 1;
                //нужно вызывать это метод у пользователя и у него менять значения полей, потом выводить на кнопки
                //PlaceShip(x, y);

                //меняем числа ячеек
                _player.PlaceShips(x,y,_currentShip, _shipDirection);
                //на основе измененных ячеек раскрашиваем поле

                UIElementCollection childs = fieldController.canvas.Children;
                Dictionary<int, int> hist = _player.PlacementHist.History;
                CellColorConverter.SetColorOfCell(ref childs,ref hist);
                _player.PlacementHist.History.Clear();
                Canvas.SetLeft(ship, _zeroShipPosition.X);
                Canvas.SetTop(ship, _zeroShipPosition.Y);

                if (_shipArray.Max() > 0)
                {
                    ship.Width = Cell.CellSize * _shipArray.Max();
                    _currentShip = _shipArray.Max();
                    _shipArray.SetValue(-100, Array.IndexOf(_shipArray, _shipArray.Max()));
                }
                else
                {
                    ship.Visibility = Visibility.Hidden;
                }
            }
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
                Button shipButton = sender as Button;
                shipButton.SetValue(Canvas.LeftProperty, e.GetPosition(null).X - _mousePosition.X);
                shipButton.SetValue(Canvas.TopProperty, e.GetPosition(null).Y - _mousePosition.Y);
            }
        }
        private void ship_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            _isCanmove = false;
            FieldController_PreviewMouseLeftButtonDown(sender);
        }
        private void HorizontalButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ship.LayoutTransform = new RotateTransform(0);
            _shipDirection = ShipDirection.Horizontal;
        }
        private void VerticalButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ship.LayoutTransform = new RotateTransform(90);
            _shipDirection = ShipDirection.Vertical;
        }
        private void PlaceShip(int x, int y)
        {
            switch (_shipDirection)
            {
                case ShipDirection.Horizontal:
                    if ((x + _currentShip <= 10 && y <= 9) && (x + _currentShip >= 0 && y >= 0))
                    {
                        if (y - 1 >= 0 && x - 1 >= 0)
                        {
                            Button mostUpLeftChildButton = (Button)fieldController.canvas.Children[(x - 1) + (y - 1) * 10];
                            mostUpLeftChildButton.Background = Brushes.Yellow;
                        }
                        if (x + _currentShip <= 9 && y - 1 >= 0)
                        {
                            Button mostUpRightChildButton = (Button)fieldController.canvas.Children[(x + _currentShip) + (y - 1) * 10];
                            mostUpRightChildButton.Background = Brushes.Yellow;
                        }
                        if (x - 1 >= 0 && x - 1 <= 9)
                        {
                            Button mostMidleLeftChildButton = (Button)fieldController.canvas.Children[(x - 1) + y * 10];
                            mostMidleLeftChildButton.Background = Brushes.Yellow;
                        }
                        if (x + _currentShip <= 9)
                        {
                            Button mostMidleRightChildButton = (Button)fieldController.canvas.Children[(x + _currentShip) + y * 10];
                            mostMidleRightChildButton.Background = Brushes.Yellow;
                        }
                        if (y + 1 <= 9 && x - 1 >= 0)
                        {
                            Button mostDownLeftChildButton = (Button)fieldController.canvas.Children[(x - 1) + (y + 1) * 10];
                            mostDownLeftChildButton.Background = Brushes.Yellow;
                        }
                        if (x + _currentShip <= 9 && y + 1 <= 9)
                        {
                            Button mostDownRightChildButton = (Button)fieldController.canvas.Children[(x + _currentShip) + (y + 1) * 10];
                            mostDownRightChildButton.Background = Brushes.Yellow;
                        }
                        for (int i = 0; i < _currentShip; ++i)
                        {
                            var numbOfShipCell = x + i + y * 10;
                            Button childButton = (Button)fieldController.canvas.Children[numbOfShipCell];
                            childButton.Background = Brushes.Red;
                            if (numbOfShipCell - 10 >= 0)
                            {
                                Button higherChildButton = (Button)fieldController.canvas.Children[numbOfShipCell - 10];
                                higherChildButton.Background = Brushes.Yellow;
                            }
                            if (numbOfShipCell + 10 <= 99)
                            {
                                Button belowerChildButton = (Button)fieldController.canvas.Children[numbOfShipCell + 10];
                                belowerChildButton.Background = Brushes.Yellow;
                            }
                        }
                    }
                    break;

                case ShipDirection.Vertical:

                    if ((x <= 10 && y + _currentShip <= 10) && (y + _currentShip >= 0 && x >= 0))
                    {
                        if (y - 1 >= 0 && x - 1 >= 0)
                        {
                            Button mostUpLeftChildButton = (Button)fieldController.canvas.Children[(x - 1) + (y - 1) * 10];
                            mostUpLeftChildButton.Background = Brushes.Yellow;
                        }
                        if (x + 1 <= 9 && y - 1 >= 0)
                        {
                            Button mostUpRightChildButton = (Button)fieldController.canvas.Children[(x + 1) + (y - 1) * 10];
                            mostUpRightChildButton.Background = Brushes.Yellow;
                        }
                        if (y - 1 >= 0)
                        {
                            Button mostMidleUpperChildButton = (Button)fieldController.canvas.Children[x + (y - 1) * 10];
                            mostMidleUpperChildButton.Background = Brushes.Yellow;
                        }
                        if (y + _currentShip <= 9 && x - 1 >= 0)
                        {
                            Button mostDownLefChildButton = (Button)fieldController.canvas.Children[(x - 1) + (y + _currentShip) * 10];
                            mostDownLefChildButton.Background = Brushes.Yellow;
                        }
                        if (y + _currentShip <= 9)
                        {
                            Button mostDownMiddleChildButton = (Button)fieldController.canvas.Children[x + (y + _currentShip) * 10];
                            mostDownMiddleChildButton.Background = Brushes.Yellow;
                        }
                        if (y + _currentShip <= 9 && x + 1 < 9)
                        {
                            Button mostDownRightChildButton = (Button)fieldController.canvas.Children[(x + 1) + (y + _currentShip) * 10];
                            mostDownRightChildButton.Background = Brushes.Yellow;
                        }
                        for (int i = 0; i < _currentShip; ++i)
                        {
                            var numbOfShipCell = x + (y + i) * 10;
                            Button childButton = (Button)fieldController.canvas.Children[numbOfShipCell];
                            childButton.Background = Brushes.Red;
                            if (x - 1 >= 0)
                            {
                                Button leftChildButton = (Button)fieldController.canvas.Children[numbOfShipCell - 1];
                                leftChildButton.Background = Brushes.Yellow;
                            }
                            if (x + 1 <= 9)
                            {
                                Button rightChildButton = (Button)fieldController.canvas.Children[numbOfShipCell + 1];
                                rightChildButton.Background = Brushes.Yellow;
                            }
                        }
                    }
                    break;
            }
        }
    }
}