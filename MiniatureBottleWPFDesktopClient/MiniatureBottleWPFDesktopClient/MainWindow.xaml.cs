using System;
using System.Collections.Generic;
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
            BitmapImage img = new BitmapImage(new Uri(@"C:\Users\Blagovest Zlatev\Downloads\lala\lala.jpg"));
            imgBottle.Source = img;
        }

        private void btnClear_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
