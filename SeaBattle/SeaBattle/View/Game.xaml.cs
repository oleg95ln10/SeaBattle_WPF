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
        // Для уменьшения записи
        private Player _player;
        private ComputerPlayer _computerPlayer;
        public Game(MainWindow mainWindow)
        {
            InitializeComponent();
            this._mainWindow = mainWindow;
            _isGameOver = false;
            _player = _mainWindow.Model.FirstPlayer;
            _computerPlayer = _mainWindow.Model.ComputerPlayer;
            ((ComputerPlayer)_computerPlayer).GetShotMap();
            CellColorConverter.SetColor(playerFieldController.canvas.Children, _mainWindow.Model.FirstPlayer.Field.Cells);
            //CellColorConverter.SetColor(computerFieldController.canvas.Children, _mainWindow.Model.ComputerPlayer.Field.Cells);
            computerFieldController.canvas.PreviewMouseLeftButtonDown += Canvas_PreviewMouseLeftButtonDown;
        }

        private void Canvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePositionOnElement = e.GetPosition(computerFieldController.canvas);
            var x = (int)(mousePositionOnElement.X / Cell.CellSize);
            var y = (int)(mousePositionOnElement.Y / Cell.CellSize);
            if (!_isGameOver 
                && 
                _player.IsCanOpponentBeAttacked(_computerPlayer, Field.DecartToLine(x, y)))
            {

                PlayerShot(Field.DecartToLine(x, y));
            }
            if (!_isGameOver)
            {
                _computerPlayer.AttackPlayer(_player, _computerPlayer.GetNextCell(), CellStatus.ComputerShot);
                CellColorConverter.SetColorOfCell(playerFieldController.canvas.Children, _player.PlacementHist.History);
            }

        }

        private void PlayerShot(int coordinate)
        {
            _player.AttackPlayer(_computerPlayer, coordinate);
            CellColorConverter.SetColorOfCell(computerFieldController.canvas.Children, _computerPlayer.PlacementHist.History);              
        }
    }
}
