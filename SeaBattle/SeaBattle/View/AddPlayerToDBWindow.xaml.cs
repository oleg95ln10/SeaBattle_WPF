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
    /// Interaction logic for AddPlayerToDBWindow.xaml
    /// </summary>
    public partial class AddPlayerToDBWindow : Window
    {
        private MainWindow _mainWindow;
        private AbstractPlayer _player;

        public AddPlayerToDBWindow(MainWindow mainWindow, AbstractPlayer player)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _player = player;
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (playerNameTextbox.Text != null)
                {
                    DbPlayer pl = new DbPlayer();
                    pl.Name = playerNameTextbox.Text;
                    pl.Score = _player.Score;
                    PlayerRepository pr = new PlayerRepository();
                    pr.AddPlayer(pl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _mainWindow.Visibility = Visibility.Visible;
                this.Close();
            }
        }
    }
}
