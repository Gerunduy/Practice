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
    /// Логика взаимодействия для Experts.xaml
    /// </summary>
    public partial class Experts : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public Experts()
        {
            InitializeComponent();
            client.GetListExpertsWithCountExpertiseCompleted += Client_GetListExpertsWithCountExpertiseCompleted;
            client.GetListExpertsWithCountExpertiseAsync();
        }

        private void Client_GetListExpertsWithCountExpertiseCompleted(object sender, ServiceReference1.GetListExpertsWithCountExpertiseCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                dataGrid1.ItemsSource = e.Result.ToList();

            }


            else
                MessageBox.Show(e.Error.Message);
        }

        private void bt_expert_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("тест");
        }
        
    }
}
