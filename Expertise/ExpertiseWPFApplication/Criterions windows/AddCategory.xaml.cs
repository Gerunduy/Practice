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
    /// Логика взаимодействия для AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public AddCategory()
        {
            InitializeComponent();
            client.AddCategoriesCompleted += Client_AddCategoriesCompleted;
            Waiting(false);
        }
        // ======================================================================================================================
        private void Waiting(bool Wait)
        {
            if (Wait)
            {
                gGrid.Visibility = Visibility.Hidden;
                tblWait.Visibility = Visibility.Visible;
            }
            else
            {
                gGrid.Visibility = Visibility.Visible;
                tblWait.Visibility = Visibility.Hidden;
            }
        }
        // ======================================================================================================================
        private void Client_AddCategoriesCompleted(object sender, ServiceReference1.AddCategoriesCompletedEventArgs e)
        {
            if (e.Error == null && e.Result)
            {
                this.DialogResult = true;
                MessageBox.Show("Категория добавлена");
            }
            else
            {
                Waiting(false);
                MessageBox.Show(e.Error.Message);
            }
        }
        // ======================================================================================================================
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbxName.Text.Trim(' ') != "")
            {
                Waiting(true);
                client.AddCategoriesAsync(tbxName.Text);
            }
            else MessageBox.Show("Некорректное имя!");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
