﻿using SeaBattle.Model;
using SeaBattle.View;
using SeaBattle.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
            AutofacConfig.ConfigureContainer();

            var v = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString().Replace("/", "//");
            AppDomain.CurrentDomain.SetData("DataDirectory", v);
        }
        public MainViewModel Model
        {
            get { return _model;  }
            set { _model = value; }
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _model = new MainViewModel();
                this.Visibility = Visibility.Hidden;
                PreGameWindow pg = new PreGameWindow(this);
                pg.ShowDialog();
            }
            catch (Exception ex)
            {
                QuietLogger.LogQ(ex);
            }
        }

        private void LeaderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Visibility = Visibility.Collapsed;
                LeaderTable table = new LeaderTable();
                table.Closed += (sender2, e2) =>
                {
                    this.Visibility = Visibility.Visible;
                };
                table.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
