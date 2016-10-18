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
        private bool _isShipMoving;
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
            _isShipMoving = false;
            this._player = player;
            _direction = ShipDirection.Horizontal;
        }

        private void FieldController_PreviewMouseLeftButtonDown(object sender)
        {
            Button but = sender as Button;
            var x = (int)( Canvas.GetLeft(but) / Cell.CellSize );
            var y = (int)( Canvas.GetTop(but) / Cell.CellSize );
            lab1.Content = x+1;
            shipDirection.Content = y+1;
            switch (_direction)
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
                            Button mostDownLeftChildButton = (Button)fieldController.canvas.Children[(x - 1) + ( y + 1 ) * 10];
                            mostDownLeftChildButton.Background = Brushes.Yellow;
                        }
                        if (x + _currentShip <= 9 && y + 1 < 9)
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
                            //mostUpLeftChildButton.Background = Brushes.Yellow;
                        }
                        if (x + _currentShip <= 9 && y - 1 >= 0)
                        {
                            Button mostUpRightChildButton = (Button)fieldController.canvas.Children[(x + _currentShip) + (y - 1) * 10];
                            //mostUpRightChildButton.Background = Brushes.Yellow;
                        }
                        if (x - 1 >= 0 && x - 1 <= 9)
                        {
                            Button mostMidleLeftChildButton = (Button)fieldController.canvas.Children[(x - 1) + y * 10];
                            //mostMidleLeftChildButton.Background = Brushes.Yellow;
                        }
                        if (x + _currentShip <= 9)
                        {
                            Button mostMidleRightChildButton = (Button)fieldController.canvas.Children[(x + _currentShip) + y * 10];
                            //mostMidleRightChildButton.Background = Brushes.Yellow;
                        }
                        if (y + 1 <= 9 && x - 1 >= 0)
                        {
                            Button mostDownLeftChildButton = (Button)fieldController.canvas.Children[(x - 1) + (y + 1) * 10];
                            //mostDownLeftChildButton.Background = Brushes.Yellow;
                        }
                        if (x + _currentShip <= 9 && y + 1 < 9)
                        {
                            Button mostDownRightChildButton = (Button)fieldController.canvas.Children[(x + _currentShip) + (y + 1) * 10];
                            //mostDownRightChildButton.Background = Brushes.Yellow;
                        }
                        for (int i = 0; i < _currentShip; ++i)
                        {
                            var numbOfShipCell = x + (y + i) * 10;
                            Button childButton = (Button)fieldController.canvas.Children[numbOfShipCell];
                            childButton.Background = Brushes.Red;
                            if (numbOfShipCell - 1 >= 0)
                            {
                                Button leftChildButton = (Button)fieldController.canvas.Children[numbOfShipCell - 1];
                                leftChildButton.Background = Brushes.Yellow;
                            }
                            if (numbOfShipCell + 1 <= 99)
                            {
                                Button belowerChildButton = (Button)fieldController.canvas.Children[numbOfShipCell + 1];
                                belowerChildButton.Background = Brushes.Yellow;
                            }
                        }
                    }
















                    break;
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
                _isShipMoving = true;
                Button shipButton = sender as Button;
                shipButton.SetValue(Canvas.LeftProperty, e.GetPosition(null).X - _mousePosition.X);
                shipButton.SetValue(Canvas.TopProperty, e.GetPosition(null).Y - _mousePosition.Y);
            }
        }

        private void ship_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            _isCanmove = false;
            _isShipMoving = false;
            FieldController_PreviewMouseLeftButtonDown(sender);
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