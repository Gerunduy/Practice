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
        ExpertCard _ExpertCard;
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
            ServiceReference1.ExpertsWithCountExpertise temp = dataGrid1.SelectedItem as ServiceReference1.ExpertsWithCountExpertise;
            
            _ExpertCard = new ExpertCard();
            _ExpertCard.Owner = this;
            _ExpertCard.textBox.Text = temp.surname_expert;
            _ExpertCard.textBox1.Text = temp.name_expert;
            _ExpertCard.textBox2.Text = temp.patronymic_expert;
            _ExpertCard.textBox3.Text = temp.job_expert;
            _ExpertCard.textBox7.Text = temp.post_expert;
            _ExpertCard.textBox4.Text = temp.degree_expert;
            _ExpertCard.textBox5.Text = temp.rank_expert;
            _ExpertCard.textBox6.Text = temp.contacts_expert;
            _ExpertCard.id_expert = temp.id_expert;
            _ExpertCard.textBlock.Text = "";
            _ExpertCard.dataGrid.ItemsSource = null;
            _ExpertCard.tabControl.Visibility = Visibility.Visible;
            _ExpertCard.listFOSCurrentExpert.Clear();
            
            for(int i = 0; i < temp.ListFOS.ToList().Count; i++)
            {
                int j = i+1;
                _ExpertCard.listFOSCurrentExpert.Add(temp.ListFOS[i]);
                _ExpertCard.textBlock.Text +=j +" "+ temp.ListFOS[i].name_fos + "\r\n";
                _ExpertCard.j = j;
            }
            if (_ExpertCard.ShowDialog() == true)
            {
                client.GetListExpertsWithCountExpertiseAsync();
            }
            else
            {
                client.GetListExpertsWithCountExpertiseAsync();
            }
        }
        
    }
}
