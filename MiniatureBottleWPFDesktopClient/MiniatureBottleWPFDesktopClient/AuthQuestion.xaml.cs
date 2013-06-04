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
using System.Windows.Shapes;

namespace MiniatureBottleWPFDesktopClient
{
    /// <summary>
    /// Interaction logic for AuthQuestion.xaml
    /// </summary>
    public partial class AuthQuestion : Window
    {
        public Boolean isAuth = false;
        public AuthQuestion()
        {
            InitializeComponent();
        }

        private void okButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text.Equals("administrator") && txtPass.Password.Equals("blagozlatev"))
            {
                isAuth = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("The username or the password is wrong!", "Error!");
            }
        }
    }
}
