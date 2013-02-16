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
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

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
                ImageView ImageView = new ImageView(openFile.FileName);
                ImageView.Show();                
                BitmapImage img = new BitmapImage(new Uri(directory));                
                imgBottle.Source = img;
                rotateImage.Angle = 0;
                scaleImage.CenterX = imgBottle.ActualWidth / 2;
                scaleImage.CenterY = imgBottle.ActualHeight / 2;
                scaleImage.ScaleX = 1;
                scaleImage.ScaleY = 1;
                scrlVwrForImage.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                scrlVwrForImage.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                imgBottle.Width = scrlVwrForImage.ActualWidth - 10;
                imgBottle.Height = scrlVwrForImage.ActualHeight - 10;                
                txtBrowse.Text = directory;                
            }            
        }

        private void btnClear_OnClick(object sender, RoutedEventArgs e)
        {
            //txtAge.Text = string.Empty;
            //txtAlcohol.Text = string.Empty;
            //txtAlcoholType.Text = string.Empty;
            //txtBrowse.Text = string.Empty;
            //txtCity.Text = string.Empty;
            //txtColor.Text = string.Empty;
            //txtContent.Text = string.Empty;
            //txtCountry.Text = string.Empty;
            //txtID.Text = string.Empty;
            //txtManufacturer.Text = string.Empty;
            //txtMaterial.Text = string.Empty;
            //txtName.Text = string.Empty;
            //txtNote.Text = string.Empty;
            //txtShape.Text = string.Empty;
            //txtShell.Text = string.Empty;
            //cmbContinent.SelectedIndex = -1;
            //imgBottle.Source = null;        
            scrlVwrForImage.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrlVwrForImage.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            rotateImage.Angle += 90;
            if (rotateImage.Angle > 360)
            {
                rotateImage.Angle = 90;
            }            
            scaleImage.ScaleX += 0.2;
            scaleImage.ScaleY += 0.2;
        }

        private void btnSave_OnClick(object sender, RoutedEventArgs e)
        {
            Dictionary<int, string> continents = new Dictionary<int, string>() {
                {0, "Africa"},
                {1, "Asia"},
                {2, "Australia"},
                {3, "Europe"},
                {4, "North America"},
                {5, "South America"}
            };
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


            Bottle b = new Bottle();
            try
            {                
                int testValue;
                if (int.TryParse(txtAge.Text, out testValue))
                {
                    b.Age = int.Parse(txtAge.Text);
                }
                else
                {
                    throw new Exception("Invalid value for Age!");
                }
                b.Alcohol = txtAlcohol.Text;
                b.AlcoholType = txtAlcoholType.Text;
                b.City = txtCity.Text;
                b.Color = txtColor.Text;
                b.Content = txtContent.Text;
                b.Continent = continents[cmbContinent.SelectedIndex];
                b.Country = txtCountry.Text;
                if (int.TryParse(txtID.Text, out testValue))
                {
                    b.ID = int.Parse(txtID.Text);
                }
                else
                {
                    throw new Exception("Invalid value for ID!");
                }
                b.Manufacturer = txtManufacturer.Text;
                b.Material = txtMaterial.Text;
                b.Name = txtName.Text;
                b.Note = txtNote.Text;
                b.Shape = txtShape.Text;
                b.Shell = txtShell.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            txtBrowse.Text = WebRequestPostBottle(new Uri("http://miniaturebottlemvcwebapplication.apphb.com/Serialized/Post"), b.Serialize()); 
        }

        public string WebRequestPostBottle(Uri url, string postData)
        {
            string ret = string.Empty;

            StreamWriter requestWriter;

            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = "POST";
                webRequest.ContentType = "text/plain";                
                using (requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(postData);
                }
            }

            try
            {
                HttpWebResponse resp = webRequest.GetResponse() as HttpWebResponse;
                Stream resStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                ret = reader.ReadToEnd();
                return ret;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }            
        }
    }
}
