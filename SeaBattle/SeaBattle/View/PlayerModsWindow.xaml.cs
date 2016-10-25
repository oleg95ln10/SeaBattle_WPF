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
using System.Windows.Shapes;

namespace SeaBattle.View
{
    /// <summary>
    /// Interaction logic for PlayerModsWindow.xaml
    /// </summary>
    public partial class PlayerModsWindow : Window
    {
        private string _fileModeName;
        private string _fullPath;
        private bool _isUsePlayerMods;
        public PlayerModsWindow()
        {
            InitializeComponent();

            FillListbox();
        }

        #region Properties
        public string FileModeName
        {
            get { return _fileModeName;  }
            set { _fileModeName = value; }
        }

        public string FullPath
        {
            get { return _fullPath;  }
            set { _fullPath = value; }
        }

        public bool IsUsePlayerMods
        {
            get {  return _isUsePlayerMods;  }
            set { _isUsePlayerMods = value;  }
        }
        #endregion

        private void FillListbox()
        {
            _fullPath = Directory.GetCurrentDirectory() + "\\PlayerPatches\\";
            var dir = new System.IO.DirectoryInfo(_fullPath);
            FileInfo[] files = dir.GetFiles("*.*");
            listBox.Items.Clear();
            listBox.ItemsSource = files;
            listBox.DisplayMemberPath = "Name";
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _fileModeName = listBox.SelectedItem.ToString();

            if (_fileModeName != null)
                _isUsePlayerMods = false;
        }
    }
}
