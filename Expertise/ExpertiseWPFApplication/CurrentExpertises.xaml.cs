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
    /// Логика взаимодействия для CurrentExpertises.xaml
    /// </summary>
    public partial class CurrentExpertises : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public CurrentExpertises()
        {
            InitializeComponent();
            client.GetListCurrentExpertisesCompleted += Client_GetListCurrentExpertisesCompleted;
            client.GetListCurrentExpertisesAsync();
        }

        private void Client_GetListCurrentExpertisesCompleted(object sender, ServiceReference1.GetListCurrentExpertisesCompletedEventArgs e)
        {
            if (e.Error == null)
            {

               dataGrid.ItemsSource = e.Result.ToList();
            }
            else
                MessageBox.Show(e.Error.Message);
        }


        private void bt_update_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("test");
           
        }
    }
}
