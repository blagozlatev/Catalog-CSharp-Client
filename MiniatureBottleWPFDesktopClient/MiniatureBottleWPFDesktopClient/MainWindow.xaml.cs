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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AddBottle addbtl;
        ShowBottle shbtl;
        ShowAllBottles shallbtl;

        public MainWindow()
        {
            InitializeComponent();            
        }

        private void btnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            addbtl = new AddBottle();
            addbtl.mainWindow = this;
            this.Hide();
            addbtl.Show();
        }

        private void btnShow_OnClick(object sender, RoutedEventArgs e)
        {
            shbtl = new ShowBottle();
            shbtl.Show();
        }

        private void btnShowAll_OnClick(object sender, RoutedEventArgs e)
        {
            shallbtl = new ShowAllBottles();
            shallbtl.Show();
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            if (addbtl != null)
            {
                addbtl.Close();
            }

            if (shbtl != null)
            {
                shbtl.Close();
            }

            if (shallbtl != null)
            {
                shallbtl.Close();
            }
        }
    }
}
