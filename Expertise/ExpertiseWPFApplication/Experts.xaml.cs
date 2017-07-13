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
        ExpertСard _ExpertСard;
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
            _ExpertСard = new ExpertСard();
            ServiceReference1.ExpertsWithCountExpertise temp = dataGrid1.SelectedItem as ServiceReference1.ExpertsWithCountExpertise;
            _ExpertСard.textBox.Text = temp.surname_expert;
            _ExpertСard.textBox1.Text = temp.name_expert;
            _ExpertСard.textBox2.Text = temp.patronymic_expert;
            _ExpertСard.textBox3.Text = temp.job_expert;
            _ExpertСard.textBox4.Text = temp.post_expert;
            _ExpertСard.textBox5.Text = temp.degree_expert;
            _ExpertСard.textBox6.Text = temp.rank_expert;
            _ExpertСard.textBox7.Text = temp.contacts_expert;
            _ExpertСard.Owner = this;
            _ExpertСard.ShowDialog();
        }
        
    }
}
