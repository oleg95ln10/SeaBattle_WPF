﻿using SeaBattle.Model;
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
    /// Interaction logic for LeaderTable.xaml
    /// </summary>
    public partial class LeaderTable : Window
    {
        public LeaderTable()
        {
            InitializeComponent();

            var _pr = AutofacConfig.Repository;

            playersGrid.ItemsSource = _pr.GetPlayers();
        }
    }
}
