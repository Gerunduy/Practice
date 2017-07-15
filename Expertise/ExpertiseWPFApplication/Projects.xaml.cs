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
    /// Логика взаимодействия для Projects.xaml
    /// </summary>
    public partial class Projects : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public Projects()
        {
            InitializeComponent();
            client.GetListProjectsCompleted += Client_GetListProjectsCompleted;
            client.GetListProjectsAsync();
        }

        private void Client_GetListProjectsCompleted(object sender, ServiceReference1.GetListProjectsCompletedEventArgs e)
        {
            if (e.Error == null)
            {

                dataGrid.ItemsSource = e.Result.ToList();
                //e.Result.ToList().Count;
            }
            else
                MessageBox.Show(e.Error.Message);
        }
        private void bt_project_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("test");
        }
    }
}
