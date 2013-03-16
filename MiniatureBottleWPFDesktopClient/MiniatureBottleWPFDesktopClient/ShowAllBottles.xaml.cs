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
            List<string> serializedBottles = WebRequests.GetList
                (new Uri(Constants.Links.GetAllBottles),
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
    }
}
