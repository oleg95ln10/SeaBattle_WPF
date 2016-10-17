﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

namespace SeaBattle.View
{
    /// <summary>
    /// Interaction logic for FieldControl.xaml
    /// </summary>
    public partial class FieldControl : UserControl
    {
        Button[,] _cells;
        public FieldControl()
        {
            InitializeComponent();
            _cells = new Button[10, 10];
            DrawField();
        }
        private void DrawField()
        {
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    _cells[i, j] = new Button() { Width = 25, Height = 25 };
                    _cells[i, j].Name = "button_" + i.ToString() + j.ToString();
                    Canvas.SetLeft(_cells[i, j], 0 + j * 25);
                    Canvas.SetTop(_cells[i, j], 0 + i * 25);
                    canvas.Children.Add(_cells[i, j]);
                }
            }
        }
    }
}
