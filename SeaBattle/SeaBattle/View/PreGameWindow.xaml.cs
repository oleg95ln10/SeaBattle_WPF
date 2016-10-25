using SeaBattle.Model;
using SeaBattle.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SeaBattle.View
{
    /// <summary>
    /// Interaction logic for PreGameWindow.xaml
    /// Окно для расстановки пользовательских кораблей
    /// </summary>
    public partial class PreGameWindow : Window
    {
        private MainViewModel _model;// Модель из шлавного окна
        private bool _isCanmove;// Может ли игрок ходить 
        private int[] _shipArray;// Массив кораблей
        private bool _isShipCanPlace;// Можно ли поставить корабль
        private int _currentShip;// Длина текущего корабля
        private Point _mousePosition;// Позиция мыши
        private Point _zeroShipPosition;// Начальное положение кнопки "корабля"
        private ShipDirection _shipDirection;// Направление корабля
        private MainWindow _mainWindow;// Главное окно для возможного возврата
        private bool _isUsePlayerMods;// Использовать ли пользовательские моды
        private string _fileModeName;// Имя файла с пользовательским модом
        private bool _isStartGame;
        public PreGameWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _model = mainWindow.Model;
            this.Closed += PreGameWindow_Closed;
            Init();
        }

        private void Init()
        {
            try
            {
                _shipArray = new int[AbstractPlayer.SHIP_ARRAY.Length];
                _shipArray = (int[])AbstractPlayer.SHIP_ARRAY.Clone();

                GetShip();

                ship.Height = Cell.CellSize - 2;
                ship.Width = (Cell.CellSize - 2) * _currentShip;
                _zeroShipPosition = new Point(Canvas.GetLeft(ship), Canvas.GetTop(ship));
                _isShipCanPlace = true;
                _isCanmove = false;
                _model.FirstPlayer.ResetField();
                _shipDirection = ShipDirection.Horizontal;
                _isUsePlayerMods = false;
                _isStartGame = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void PreGameWindow_Closed(object sender, EventArgs e)
        {
            if (!_isStartGame)
            _mainWindow.ShowDialog();
            this.Close();
        }

        private void FieldController_PreviewMouseLeftButtonDown(object sender)
        {
            try
            {
                Button selectedButton = sender as Button;
                var x = (int)(Canvas.GetLeft(selectedButton) / Cell.CellSize);
                var y = (int)(Canvas.GetTop(selectedButton) / Cell.CellSize);

                if (_model.FirstPlayer.IsCanBePlaced(x, y, _currentShip, _shipDirection))
                    _isShipCanPlace = true;
                else
                    _isShipCanPlace = false;

                if (Mouse.GetPosition(this).X < 250 && Mouse.GetPosition(this).Y < 243 && _isShipCanPlace)
                {
                    PlaceShipOnMap(x, y);
                    Canvas.SetLeft(ship, _zeroShipPosition.X);
                    Canvas.SetTop(ship, _zeroShipPosition.Y);
                    if (_shipArray.Max() > 0)
                        GetShip();
                    else
                    {
                        ship.Visibility = Visibility.Hidden;
                        StartGameButton_PreviewMouseLeftButtonDown(sender, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Ship_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Button but = sender as Button;
                Mouse.Capture(but);
                _mousePosition = Mouse.GetPosition(but);
                _isCanmove = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Ship_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (_isCanmove)
                {
                    Button shipButton = sender as Button;
                    shipButton.SetValue(Canvas.LeftProperty, e.GetPosition(null).X - _mousePosition.X);
                    shipButton.SetValue(Canvas.TopProperty, e.GetPosition(null).Y - _mousePosition.Y);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Ship_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Mouse.Capture(null);
                _isCanmove = false;
                FieldController_PreviewMouseLeftButtonDown(sender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HorizontalButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ship.LayoutTransform = new RotateTransform(0);
                _shipDirection = ShipDirection.Horizontal;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void VerticalButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ship.LayoutTransform = new RotateTransform(90);
                _shipDirection = ShipDirection.Vertical;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetShip()
        {
            try
            {
                ship.Width = Cell.CellSize * _shipArray.Max();
                _currentShip = _shipArray.Max();
                _shipArray.SetValue(-100, Array.IndexOf(_shipArray, _shipArray.Max()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PlaceShipOnMap(int x, int y)
        {
            try
            {
                _model.FirstPlayer.PlaceShips(x, y, _currentShip, _shipDirection);
                ChangeShipCellsColor();
                _model.FirstPlayer.PlacementHist.History.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeShipCellsColor()
        {
            CellColorConverter.SetColor(fieldController.canvas.Children, _model.FirstPlayer.Field.Cells);
        }

        private void HideControls()
        {
            try
            {
                startGameButton.Visibility = Visibility.Visible;
                ship.Visibility = Visibility.Hidden;
                automaticShipGeneration.Visibility = Visibility.Hidden;
                startGameButton.Visibility = Visibility.Visible;
                horizontalShipPlacingButton.Visibility = Visibility.Hidden;
                verticalShipPlacingButton.Visibility = Visibility.Hidden;
                shipDirectionLabel.Content = "";
                shipPutLabel.Content = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AutomaticShipGeneration_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _model.FirstPlayer.AutomaticShipPlacing();
                ChangeShipCellsColor();
                HideControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StartGameButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                HideControls();
                _isStartGame = true;
                _mainWindow.Model.ComputerPlayer.GenerateMap(_isUsePlayerMods, _fileModeName);
                _mainWindow.Model.ComputerPlayer.AutomaticShipPlacing();
                Game gameWindow = new Game(_mainWindow);
                this.Close();
                gameWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddingPlayerModifications_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.Visibility = Visibility.Collapsed;
                PlayerModsWindow pm = new PlayerModsWindow();
                pm.Closed += (sender2, e2) =>
                {
                    this.Visibility = Visibility.Visible;
                };
                pm.ShowDialog();

                _isUsePlayerMods = pm.IsUsePlayerMods;
                _fileModeName = pm.FullPath + pm.FileModeName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}