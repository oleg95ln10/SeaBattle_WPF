using SeaBattle.Model;
using SeaBattle.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeaBattle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _model;
        public MainWindow()
        {
            InitializeComponent();

            _model = new MainViewModel();
        }
        public MainViewModel Model
        {
            get { return _model;  }
            set { _model = value; }
        }
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            PreGameWindow pg = new PreGameWindow(this);
            this.Close();
            pg.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            LeaderTable lt = new LeaderTable();
            lt.Closed += (sender2, e2) =>
            {
                this.Visibility = Visibility.Visible;
            };
            lt.ShowDialog();
        }
    }
}
