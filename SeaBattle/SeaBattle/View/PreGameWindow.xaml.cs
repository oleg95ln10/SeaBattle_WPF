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
    /// Interaction logic for PreGameWindow.xaml
    /// </summary>
    public partial class PreGameWindow : Window
    {
        Button[,] _cells;
        Field _f;
        public PreGameWindow()
        {
            InitializeComponent();
            _f = new Field();
            _cells = new Button[10, 10];
            DrawField();

        }
        public Button[,] Cells
        {
            get { return _cells; }
            set { _cells = value; }
        }

        void DrawField()
        {
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    _cells[i, j] = new Button() { Width = 25, Height = 25 };
                    _cells[i, j].Name = "button_"+i.ToString() + j.ToString();
                    Canvas.SetLeft(_cells[i, j], 20 + j * 25);
                    Canvas.SetTop(_cells[i, j], 20 + i * 25);
                    canvas.Children.Add(_cells[i, j]);
                }
            }
        }

        private void canvas_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
}



//Button bbb = new Button() { Width = 40, Height = 40 };
//grid.RowDefinitions.Add(new RowDefinition());
//                grid.ColumnDefinitions.Add(new ColumnDefinition());//добавить в сторону
//                bbb.Content ="i: "+ i.ToString();
//                //
//                Grid.SetColumn(bbb, i);
//                grid.Children.Add(bbb);


//for (int j = 0; j < 2; ++j)
//{
//    Button bb = new Button() { Width = 40, Height = 40 };
//    grid.ColumnDefinitions.Add(new ColumnDefinition());//добавить в сторону
//    bb.Content = "j: "+(j).ToString();
//    Grid.SetColumn(bb, j);// Добавить в сторону
//    grid.Children.Add(bb);
//    //_cells[i, j] = new Button();
//    //_cells[i, j].Width = _cells[i, j].Height = Cell.CellSize;
//    //grid.Children.Add(_cells[i, j]);
//}



//grid.ShowGridLines = true;
//            grid.HorizontalAlignment = HorizontalAlignment.Left;
//            for (int i = 0; i< 10; ++i)
//            {
//                grid.RowDefinitions.Add(new RowDefinition());
//                Button bb = new Button() { Width = 40, Height = 40 };
//bb.Content = "i: " + (i).ToString();
//Grid.SetRow(bb, i);
//                grid.Children.Add(bb);
//                grid.ColumnDefinitions.Add(new ColumnDefinition());
//                Button bbb = new Button() { Width = 40, Height = 40 };
//bbb.Content = "j: " + (j).ToString();
//Grid.SetColumn(bbb, i + i);
//                grid.Children.Add(bbb);
//            }

//            var f = 0;

//            for (int i = 0; i< 10; ++i)
//            {

//                for (int j = 0; j< 10; ++j)
//                {

//                }
//            }



        //    <Button Width = "20" Height="20"></Button>
        //<Button Width = "20" Height="20" Canvas.Left="20"></Button>
        //<Button Width = "20" Height="20" Canvas.Left="40"></Button>
        //<Button Width = "20" Height="20" Canvas.Left="60"></Button>
        //<Button Width = "20" Height="20" Canvas.Left="80"></Button>
        //<Button Width = "20" Height="20" Canvas.Left="100"></Button>
        //<Button Width = "20" Height="20" Canvas.Left="120"></Button>
        //<Button Width = "20" Height="20" Canvas.Left="140"></Button>
        //<Button Width = "20" Height="20" Canvas.Left="160"></Button>
        //<Button Width = "20" Height="20" Canvas.Left="180"></Button>
        //<Button Width = "20" Height="20" Canvas.Top="20"/>
        //<Button Width = "20" Height="20" Canvas.Top="20" Canvas.Left="20"/>
        //<Button Width = "20" Height="20" Canvas.Top="20" Canvas.Left="40"/>
        //<Button Width = "20" Height="20" Canvas.Top="40"/>