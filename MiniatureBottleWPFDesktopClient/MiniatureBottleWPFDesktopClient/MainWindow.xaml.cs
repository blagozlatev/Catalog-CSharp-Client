using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiniatureBottleWPFDesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBrowse_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.FileName = "BottleImage";
            openFile.DefaultExt = ".jpg";
            openFile.Filter = "Image Files (*.bmp, *.jpg, *.jpeg)|*.bmp;*.jpg;*.jpeg";
            var fileFound = openFile.ShowDialog();
            if (fileFound == true)
            {
                string directory = openFile.FileName;
                BitmapImage img = new BitmapImage(new Uri(directory));
                imgBottle.Source = img;
                txtBrowse.Text = directory;
            }            
        }

        private void btnClear_OnClick(object sender, RoutedEventArgs e)
        {
            txtAge.Clear();
            txtAlcohol.Clear();
            txtAlcoholType.Clear();
            txtBrowse.Clear();
            txtCity.Clear();
            txtColor.Clear();
            txtContent.Clear();
            txtCountry.Clear();
            txtID.Clear();
            txtManufacturer.Clear();
            txtMaterial.Clear();
            txtName.Clear();
            txtNote.Clear();
            txtShape.Clear();
            txtShell.Clear();
            cmbContinent.SelectedIndex = -1;
            imgBottle.Source = null;
        }

        private void btnSave_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
