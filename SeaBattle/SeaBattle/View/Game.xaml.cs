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
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private MainWindow _mainWindow;
        private bool _isGameOver;
        private bool _isComputerCanShot;
        private bool _isPlayerCanShot;
        // Для уменьшения записи
        private Player _player;
        private ComputerPlayer _computerPlayer;
        public Game(MainWindow mainWindow)
        {
            InitializeComponent();
            this._mainWindow = mainWindow;
            _isGameOver = false;
            _isComputerCanShot = false;
            _isPlayerCanShot = true;
            _player = _mainWindow.Model.FirstPlayer;
            _computerPlayer = _mainWindow.Model.ComputerPlayer;
            CellColorConverter.SetColor(playerFieldController.canvas.Children, _mainWindow.Model.FirstPlayer.Field.Cells);
            computerFieldController.canvas.PreviewMouseLeftButtonDown += Canvas_PreviewMouseLeftButtonDown;
        }

        private void Canvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePositionOnElement = e.GetPosition(computerFieldController.canvas);
            var x = (int)(mousePositionOnElement.X / Cell.CellSize);
            var y = (int)(mousePositionOnElement.Y / Cell.CellSize);
            if (!_isGameOver 
                && 
                _isPlayerCanShot
                &&
                _player.IsCanOpponentBeAttacked(_computerPlayer, 
                                                Field.DecartToLine(x, y), 
                                                CellStatus.PlayerShot, 
                                                CellStatus.ComputerShip))
            {
                _player.AttackPlayer(_computerPlayer, Field.DecartToLine(x,y), CellStatus.ShipOn, CellStatus.PlayerShot);
                CellColorConverter.SetColorOfCell(computerFieldController.canvas.Children, _player.PlacementHist.History);

                playerScoreValue.Content = _player.Score;

                if (_player.IsShotOnShip)
                    _isPlayerCanShot = true;
                else
                    _isComputerCanShot = true;
            }
            if (!_isGameOver && _isComputerCanShot)
            {
                _computerPlayer.AttackPlayer(_player, _computerPlayer.GetNextCell(), CellStatus.ComputerShot, CellStatus.ComputerShip);
                CellColorConverter.SetColorOfCell(playerFieldController.canvas.Children, _computerPlayer.PlacementHist.History);

                computerScoreValue.Content = _computerPlayer.Score;

                if (_computerPlayer.IsShotOnShip)
                {
                    _isPlayerCanShot = false;
                    Canvas_PreviewMouseLeftButtonDown(sender, e);
                }
                else
                {
                    _isComputerCanShot = false;
                    _isPlayerCanShot = true;
                }
            }
            TryFindWinner();
        }
        private void TryFindWinner()
        {
            if (_player.ShipCount == 0)
                MessageBox.Show("ComputerPlayer player is WINS!");
            if (_computerPlayer.ShipCount == 0)
                MessageBox.Show("You WON");
        }
    }
}
