using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using MiniatureBottleWPFDesktopClient.Nomenclatures;
using System.Drawing;
namespace MiniatureBottleWPFDesktopClient
{
    /// <summary>
    /// Interaction logic for AddBottle.xaml
    /// </summary>
    public partial class AddBottle : Window
    {
        ImageView ImageView;
        public Window mainWindow { get; set; }
        public AddBottle()
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
                ImageView = new ImageView();
                ImageView.imgBottle.Source = img;
                ImageView.Show();
                txtBrowse.Text = directory;
            }
        }

        private void btnClear_OnClick(object sender, RoutedEventArgs e)
        {
            txtAge.Text = string.Empty;
            txtAlcohol.Text = string.Empty;
            txtAlcoholType.Text = string.Empty;
            txtBrowse.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtColor.Text = string.Empty;
            txtContent.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtID.Text = string.Empty;
            txtManufacturer.Text = string.Empty;
            txtMaterial.Text = string.Empty;
            txtName.Text = string.Empty;
            txtNote.Text = string.Empty;
            txtShape.Text = string.Empty;
            txtShell.Text = string.Empty;
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
            if (txtAge.Text == string.Empty)
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
                b.Continent = cmbContinent.SelectedValue.ToString();
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
                txtBrowse.Text = WebRequesting(new Uri("http://bottlewebapp.apphb.com/Serialized/Post"), Bottle.Serialize(b),
                Constants.Web.MethodPost, Constants.Web.ContentText);
                byte[] requestBytes = File.ReadAllBytes(txtBrowse.Text);
                txtNote.Text = WebRequesting(new Uri("http://bottlewebapp.apphb.com/Serialized/PostImage/" + txtID.Text),
                Convert.ToBase64String(requestBytes, Base64FormattingOptions.None)
                , Constants.Web.MethodPost, Constants.Web.ContentBinaryFormData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public string WebRequesting(Uri url, string data, string method, string contentType)
        {
            StreamWriter requestWriter;
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = method;
                webRequest.ContentType = contentType;
                using (requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(data);
                }
            }

            try
            {
                String result;
                HttpWebResponse resp = webRequest.GetResponse() as HttpWebResponse;
                Stream resStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                result = reader.ReadToEnd();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void AddBottle_OnClose(object o, EventArgs e)
        {
            if (ImageView != null)
            {
                ImageView.Close();
            }
            if (mainWindow != null)
            {
                mainWindow.Show();
                this.Close();
            }
        }
    }
}
