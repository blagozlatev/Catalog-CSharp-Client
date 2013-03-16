using MiniatureBottleWPFDesktopClient.Nomenclatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace MiniatureBottleWPFDesktopClient
{
    /// <summary>
    /// Interaction logic for ShowBottle.xaml
    /// </summary>
    public partial class ShowBottle : Window
    {        
        public Window mainWindow { get; set; }
        private int bid;
        public ShowBottle()
        {
            InitializeComponent();            
        }

        public ShowBottle(int id)
        {
            InitializeComponent();
            bid = id;
            txtBID.Visibility = System.Windows.Visibility.Collapsed;
            btnGetInfo.Visibility = System.Windows.Visibility.Collapsed;
            txtBID.Text = id.ToString();
            btnGetInfo_OnClick(this, null);            
        }

        private void ShowBottle_OnClosed(object sender, EventArgs e)
        {
            if (mainWindow != null)
            {
                mainWindow.Show();
                this.Close();
            }
        }

        private void btnGetInfo_OnClick(object sender, RoutedEventArgs e)
        {
            string bottleString = WebRequesting(new Uri("http://bottlewebapp.apphb.com/Serialized/GetBottle/" + txtBID.Text),
                Constants.Web.MethodGet, Constants.Web.ContentText);
            string bottleBase64String = WebRequesting(new Uri("http://bottlewebapp.apphb.com/Serialized/GetImageBase/" + txtBID.Text), 
                 Constants.Web.MethodGet, Constants.Web.ContentBinaryFormData);

            if (bottleBase64String != "0")
            {
                byte[] imageBytes = Convert.FromBase64String(bottleBase64String);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.StreamSource = ms;
                    bi.EndInit();
                    imgBottle.Source = bi;
                }
            }
            if (bottleString != "0")
            {
                Bottle b = Bottle.Deserialize(bottleString);
                lblAge.Content = b.Age;
                lblAlcohol.Content = b.Alcohol;
                lblAlcoholType.Content = b.AlcoholType;
                lblCity.Content = b.City;
                lblColor.Content = b.Color;
                lblContent.Content = b.Content;
                lblContinent.Content = b.Continent;
                lblCountry.Content = b.Country;
                lblID.Content = b.ID;
                lblManufacturer.Content = b.Manufacturer;
                lblMaterial.Content = b.Material;
                lblName.Content = b.Name;
                lblNote.Content = b.Note;
                lblShape.Content = b.Shape;
                lblShell.Content = b.Shell;
            }
        }

        public string WebRequesting(Uri url, string method, string contentType)
        {            
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = method;
                webRequest.ContentType = contentType;                
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
    }
}
