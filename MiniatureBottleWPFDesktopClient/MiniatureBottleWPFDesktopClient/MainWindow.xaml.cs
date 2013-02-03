using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Data;
using System.Data.SqlServerCe;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Configuration;

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
            string errorMessage = string.Empty;
            if (txtID.Text == string.Empty)
            {
                errorMessage += "You didn't enter an ID!\n";
            }

            if (txtName.Text == string.Empty)
            {
                errorMessage += "You didn't enter a name!\n";
            }
            if (!(errorMessage == string.Empty))
            {
                MessageBox.Show(errorMessage, "Error!");
            }

            //Test for SQL CE with a simple Insert Query for a remote LocalDB 
            
            SqlCeConnection sqlCn = new SqlCeConnection();
            sqlCn.ConnectionString = ConfigurationManager.ConnectionStrings["MiniatureBottles"].ConnectionString;
            sqlCn.Open();
            SqlCeCommand cmd = new SqlCeCommand(string.Format("INSERT INTO Bottle (ID, Name, Age) VALUES ('{0}', '{1}', '{2}')", txtID.Text, txtName.Text, txtAge.Text));
            cmd.Connection = sqlCn;            
            cmd.ExecuteNonQuery();
            sqlCn.Close();            

        }        
    }
}
