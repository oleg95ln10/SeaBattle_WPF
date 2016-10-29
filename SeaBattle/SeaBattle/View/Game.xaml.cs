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
    /// Gape process window
    /// </summary>
    public partial class Game : Window
    {
        private MainWindow _mainWindow;// Main window
        private bool _isGameOver;// Game over flag
        private bool _isComputerCanShot;// Flag of computer shot opportunity
        private bool _isPlayerCanShot;// Flag for player shot opportunity
        private bool _isAddToDB;
        // For comfort
        private Player _player;
        private ComputerPlayer _computerPlayer;
        public Game(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _isGameOver = false;
            _isComputerCanShot = false;
            _isPlayerCanShot = true;
            _player = _mainWindow.Model.FirstPlayer;
            _computerPlayer = _mainWindow.Model.ComputerPlayer;
            CellColorConverter.SetColor(playerFieldController.canvas.Children, _mainWindow.Model.FirstPlayer.Field.Cells);
            computerFieldController.canvas.PreviewMouseLeftButtonDown += Canvas_PreviewMouseLeftButtonDown;
            _isAddToDB = !false;
            //addToPalyerToDatabase.Visibility = Visibility.Hidden;
            this.Closed += Game_Closed;
        }

        private void Game_Closed(object sender, EventArgs e)
        {
            this.Close();
            if (!_isAddToDB)
            _mainWindow.ShowDialog();
        }

        private void Canvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
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
                    _player.AttackPlayer(_computerPlayer, Field.DecartToLine(x, y), CellStatus.ShipOn, CellStatus.PlayerShot);
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
            catch (Exception ex)
            {
                QuietLogger.LogQ(ex);
            }

        }

        /// <summary>
        /// Method to determine the winner
        /// </summary>
        private void TryFindWinner()
        {
            if (_player.ShipCount == 0)
            {
                MessageBox.Show("ComputerPlayer player is WINS!");
                _isGameOver = true;

            }

            if (_computerPlayer.ShipCount == 0)
            {
                MessageBox.Show("You WON");
                _isGameOver = true;
            }

            if (_isGameOver)
                addToPalyerToDatabase.Visibility = Visibility.Visible;
        }


        private void AddToPalyerToDatabase_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddPlayerToDBWindow addPlayer = new AddPlayerToDBWindow(_mainWindow,_player);

            _isAddToDB = true;

            this.Close();

            addPlayer.ShowDialog();
        }
    }
}
