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
using System.Security.Cryptography;

namespace ExpertiseWPFApplication
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        string Login;
        string Password;
        public ServiceReference1.Experts User;



        public Authorization()
        {
            InitializeComponent();
            client.AuthorizationCompleted += Client_AuthorizationCompleted;
            Waiting(false);
        }
        // ===================================================================
        private void Client_AuthorizationCompleted(object sender, ServiceReference1.AuthorizationCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null) // например проблемы с интернетом
                {
                    if (MessageBox.Show("Нет доступа к базе данных", "Повторить попытку?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        client.AuthorizationAsync(Login, Password);
                    }
                    else this.Close();
                }
                else
                {
                    User = e.Result;
                    switch (User.id_expert)
                    {
                        case -1:
                            Waiting(false);
                            MessageBox.Show("Логин и/или пароль введены неверно.");
                            break;
                        case -2:
                            Waiting(false);
                            if (MessageBox.Show("Ошибка!", "Повторить попытку?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                client.AuthorizationAsync(Login, Password);
                            }
                            else this.Close();

                            break;
                        default:
                            Waiting(false);
                            DialogResult = true;
                            break;
                    }
                }
            }
            catch
            {
                if (MessageBox.Show("Нет доступа к сервису", "Повторить попытку?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    client.AuthorizationAsync(Login, Password);
                }
                else this.Close();
            }
        }
        // ===================================================================
        private void SetValues()
        {
            Login = tbxLogin.Text;
            Password = pbxPassword.Password;

            //byte[] bytes = Encoding.Unicode.GetBytes(pbxPassword.Password);
            //MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
            //byte[] byteHash = CSP.ComputeHash(bytes);
            //string hash = string.Empty;
            //foreach (byte b in byteHash)
            //{
            //    hash += string.Format("{0:x2}", b);
            //}

            //Password = hash;
        }
        private void Waiting(bool Wait)
        {
            if (Wait)
            {
                tblLogin.Visibility = Visibility.Hidden;
                tblPassword.Visibility = Visibility.Hidden;
                tbxLogin.Visibility = Visibility.Hidden;
                pbxPassword.Visibility = Visibility.Hidden;
                btnClearLogin.Visibility = Visibility.Hidden;
                btnClearPassword.Visibility = Visibility.Hidden;
                btnLogIn.Visibility = Visibility.Hidden;
                btnCancel.Visibility = Visibility.Hidden;

                tblWaitInfo.Visibility = Visibility.Visible;
            }
            else
            {
                tblWaitInfo.Visibility = Visibility.Hidden;

                tblLogin.Visibility = Visibility.Visible;
                tblPassword.Visibility = Visibility.Visible;
                tbxLogin.Visibility = Visibility.Visible;
                pbxPassword.Visibility = Visibility.Visible;
                btnClearLogin.Visibility = Visibility.Visible;
                btnClearPassword.Visibility = Visibility.Visible;
                btnLogIn.Visibility = Visibility.Visible;
                btnCancel.Visibility = Visibility.Visible; 
            }
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
            SetValues();
            client.AuthorizationAsync(Login, Password);
            Waiting(true);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        // ===================================================================
    }
}
