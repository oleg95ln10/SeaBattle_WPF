﻿using SeaBattle.Model;
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
    /// </summary>
    public partial class PreGameWindow : Window
    {
        private MainViewModel _model;
        private bool _isCanmove;
        private int[] _shipArray;
        private bool _isShipCanPlace;
        private int _currentShip;
        Point _mousePosition;
        Point _zeroShipPosition;
        ShipDirection _shipDirection;
        private MainWindow _mainWindow;

        public PreGameWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            this._model = mainWindow.Model;
            Init();
        }
        private void Init()
        {
            _shipArray = new int[ AbstractPlayer.ShipArray.Length ];
            _shipArray = (int[])AbstractPlayer.ShipArray.Clone();
            _currentShip = _shipArray.Max();
            _shipArray.SetValue( -100, Array.IndexOf(_shipArray, _shipArray.Max() ) );
            ship.Height = Cell.CellSize - 2;
            ship.Width = ( Cell.CellSize - 2 ) * _currentShip;
            _zeroShipPosition = new Point( Canvas.GetLeft( ship ), Canvas.GetTop( ship ) );
            _isShipCanPlace = true;
            _isCanmove = false;
            _model.FirstPlayer.ResetField();
            _shipDirection = ShipDirection.Horizontal;
        }
        private void FieldController_PreviewMouseLeftButtonDown(object sender)
        {
            Button selectedButton = sender as Button;
            var x = (int)( Canvas.GetLeft( selectedButton) / Cell.CellSize );
            var y = (int)( Canvas.GetTop( selectedButton) / Cell.CellSize );

            if ( _model.FirstPlayer.IsCanBePlaced( x, y, _currentShip, _shipDirection ) )
                _isShipCanPlace = true;
            else
               _isShipCanPlace = false;

            if ( Mouse.GetPosition( this ).X < 250 && Mouse.GetPosition( this ).Y < 243 && _isShipCanPlace )
            {
                PlaceShipOnMap( x, y );
                Canvas.SetLeft( ship, _zeroShipPosition.X );
                Canvas.SetTop( ship, _zeroShipPosition.Y );
                if ( _shipArray.Max() > 0 )
                    GetShip();
                else
                {
                    ship.Visibility = Visibility.Hidden;
                    StartGameButton_PreviewMouseLeftButtonDown(sender, null);
                }

            }
        }
        private void ship_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button but = sender as Button;
            Mouse.Capture( but );
            _mousePosition = Mouse.GetPosition( but );
            _isCanmove = true;
        }
        private void ship_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if ( _isCanmove )
            {
                Button shipButton = sender as Button;
                shipButton.SetValue( Canvas.LeftProperty, e.GetPosition( null ).X - _mousePosition.X );
                shipButton.SetValue( Canvas.TopProperty, e.GetPosition( null ).Y - _mousePosition.Y );
            }
        }
        private void ship_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture( null );
            _isCanmove = false;
            FieldController_PreviewMouseLeftButtonDown( sender );
        }
        private void HorizontalButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ship.LayoutTransform = new RotateTransform( 0 );
            _shipDirection = ShipDirection.Horizontal;
        }
        private void VerticalButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ship.LayoutTransform = new RotateTransform( 90 );
            _shipDirection = ShipDirection.Vertical;
        }
        private void GetShip()
        {
            ship.Width = Cell.CellSize * _shipArray.Max();
            _currentShip = _shipArray.Max();
            _shipArray.SetValue(-100, Array.IndexOf( _shipArray, _shipArray.Max() ) );
        }
        private void PlaceShipOnMap(int x, int y)
        {
            _model.FirstPlayer.PlaceShips(x, y, _currentShip, _shipDirection);
            ChangeShipCellsColor();
            _model.FirstPlayer.PlacementHist.History.Clear();
        }
        private void ChangeShipCellsColor()
        {
            UIElementCollection childs = fieldController.canvas.Children;
            CellColorConverter.SetColor(ref childs, _model.FirstPlayer.Field.Cells);
        }
        private void HideControls()
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
        private void AutomaticShipGeneration_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _model.FirstPlayer.AutomaticShipPlacing();
            ChangeShipCellsColor();
            HideControls();
        }

        private void StartGameButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HideControls();
            _mainWindow.Model.SecondPlayer.AutomaticShipPlacing();
            Game gameWindow = new Game(_mainWindow);
            gameWindow.ShowDialog();
            this.Close();
        }

    }
}