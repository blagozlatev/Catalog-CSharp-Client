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
    /// Interaction logic for ShowAllBottles.xaml
    /// </summary>
    public partial class ShowAllBottles : Window
    {
        public Window mainWindow { get; set; }
        private ShowBottle showBottle;
        List<Bottle> bottles;
        public ShowAllBottles()
        {
            InitializeComponent();
            lbBottleNames.MouseDoubleClick += new MouseButtonEventHandler(lbBottleNames_MouseDoubleClick);
            List<string> serializedBottles = WebRequestingList(new Uri("http://bottlewebapp.apphb.com/Serialized/"),
                Constants.Web.MethodGet, Constants.Web.ContentText);
            bottles = new List<Bottle>();
            foreach (string s in serializedBottles)
            {
                Bottle b = Bottle.Deserialize(s);
                bottles.Add(b);
            }
            foreach (Bottle b in bottles)
            {
                lbBottleNames.Items.Add(b.Name);
            }
        }

        private void lbBottleNames_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (lbBottleNames.SelectedItem != null)
            {
                if (bottles != null)
                {
                    int id = bottles.ElementAt(lbBottleNames.SelectedIndex).ID;
                    showBottle = new ShowBottle(id);
                    showBottle.mainWindow = this;
                    this.Hide();
                    showBottle.Show();
                }                
            }
        }

        private void ShowAllBottles_OnClosed(object sender, EventArgs e)
        {
            if (showBottle != null)
            {
                showBottle.Close();
            }
            mainWindow.Show();
            this.Close();
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

        public List<string> WebRequestingList(Uri url, string method, string contentType)
        {
            List<string> result = new List<string>();
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = method;
                webRequest.ContentType = contentType;
            }

            try
            {                
                HttpWebResponse resp = webRequest.GetResponse() as HttpWebResponse;
                Stream resStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(line);
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
