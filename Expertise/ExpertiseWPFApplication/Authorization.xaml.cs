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

namespace ExpertiseWPFApplication
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        // ===================================================================
        private void btnClearLogin_Click(object sender, RoutedEventArgs e)
        {
            tbxLogin.Text = "";
        }

        private void btnClearPassword_Click(object sender, RoutedEventArgs e)
        {
            pbxPassword.Password = "";
        }
        // ===================================================================
        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        // ===================================================================
    }
}
