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
        CardExpertiseProject _CardExpertiseProject;
        public Projects()
        {
            InitializeComponent();
            client.GetListProjectsCompleted += Client_GetListProjectsCompleted;
            client.GetListAuthorsForProjectCompleted += Client_GetListAuthorsForProjectCompleted;
            Waiting(true);
            client.GetListProjectsAsync();
        }

        private void Client_GetListAuthorsForProjectCompleted(object sender, ServiceReference1.GetListAuthorsForProjectCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                _CardExpertiseProject.listauthor = e.Result.ToList();
                for (int i = 0; i < e.Result.ToList().Count; i++)
                {
                    _CardExpertiseProject.textBlock.Text += e.Result.ToList()[i].FIO+ "\r\n";
                   
                }
                if (_CardExpertiseProject.ShowDialog() == true)
                {
                    Waiting(true);
                    dataGrid.ItemsSource = null;
                    client.GetListProjectsAsync();
                }
                else
                {
                    Waiting(true);
                    dataGrid.ItemsSource = null;
                    client.GetListProjectsAsync();
                }
            }
            else
                MessageBox.Show(e.Error.Message);
        }  
        private void Client_GetListProjectsCompleted(object sender, ServiceReference1.GetListProjectsCompletedEventArgs e)
        {
            if (e.Error == null)
            {

                dataGrid.ItemsSource = e.Result.ToList();
                //e.Result.ToList().Count;
                Waiting(false);
            }
            else
                MessageBox.Show(e.Error.Message);
        }
        private void Waiting(bool Wait)
        {
            if (Wait)
            {
                dataGrid.Visibility = Visibility.Hidden;
                tblWait.Visibility = Visibility.Visible;
            }
            else
            {
                dataGrid.Visibility = Visibility.Visible;
                tblWait.Visibility = Visibility.Hidden;
            }
        }
        private void bt_project_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference1.myProject temp = dataGrid.SelectedItem as ServiceReference1.myProject;
            
            _CardExpertiseProject = new CardExpertiseProject();
            _CardExpertiseProject.Owner = this;
            _CardExpertiseProject.id_project = temp.id_project;
            _CardExpertiseProject.textBox.Text = temp.name_project;
            _CardExpertiseProject.textBox1.Text = temp.lead_project;
            _CardExpertiseProject.textBox2.Text = temp.fos;
            _CardExpertiseProject.textBox3.Text = temp.grnti_project;
            _CardExpertiseProject.datePicker1.SelectedDate =(temp.begin_project);
            _CardExpertiseProject.datePicker2.SelectedDate = temp.end_project;
            _CardExpertiseProject.tbxOrganization.Text = temp.org_project;
            _CardExpertiseProject.textBox6.Text = temp.money_project.Trim(' ');
            _CardExpertiseProject.textBox7.Text = temp.email_project;
            _CardExpertiseProject.client.GetListExpertForProjectAsync(temp.id_project);
            _CardExpertiseProject.textBox4.Text = temp.name_expertise;
            if (temp.date_expertise.ToShortDateString() != "01.01.0001")
            {
                _CardExpertiseProject.textBox5.Text = temp.date_expertise.ToShortDateString();
            }
            
            client.GetListAuthorsForProjectAsync(temp.id_project);
            

            //MessageBox.Show("test");
        }
    }
}
