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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
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


            var b = new Bottle();
            b.Age = int.Parse(txtAge.Text);
            b.Alcohol = txtAlcohol.Text;
            b.AlcoholType = txtAlcoholType.Text;
            b.City = txtCity.Text;
            b.Color = txtColor.Text;
            b.Content = txtContent.Text;
            b.Continent = "North America";
            b.Country = txtCountry.Text;
            b.ID = int.Parse(txtID.Text);
            b.Manufacturer = txtManufacturer.Text;
            b.Material = txtMaterial.Text;
            b.Name = txtName.Text;
            b.Note = txtNote.Text;
            b.Shape = txtShape.Text;
            b.Shell = txtShell.Text;

            string json = JsonConvert.SerializeObject(b);

            WebRequestinJson(new Uri("http://localhost:47506/JSON/Post"), json);
        }        

        public string WebRequestinJson(Uri url, string postData)
        {
            string ret = string.Empty;

            StreamWriter requestWriter;

            var webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";
                //POST the data.
                using (requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(postData);
                }
            }

            try
            {
                HttpWebResponse resp = (HttpWebResponse) webRequest.GetResponse();
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
