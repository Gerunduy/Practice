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
    /// Логика взаимодействия для ProjectCard.xaml
    /// </summary>
    public partial class ProjectCard : Window
    {
        string grnti_project;
        AddAuthor _AddAuthor;
        int fos;
        public List<ServiceReference1.myAuthors> listauthor = new List<ServiceReference1.myAuthors>();
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public ProjectCard()
        {
            InitializeComponent();
            client.GetListAuthorsCompleted += Client_GetListAuthorsCompleted;
            
        }

        private void Client_GetListAuthorsCompleted(object sender, ServiceReference1.GetListAuthorsCompletedEventArgs e)
        {
            if (e.Error == null)
            {

                comboBox.ItemsSource = e.Result.ToList();
                //e.Result.ToList().Count;
            }
            else
                MessageBox.Show(e.Error.Message);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            string name_project = textBox.Text;
            string lead_project = textBox1.Text;
           
            DateTime begin_project = DateTime.Parse(datePicker1.SelectedDate.Value.ToShortDateString());
            DateTime end_project = DateTime.Parse( datePicker2.SelectedDate.Value.ToShortDateString());
            string money_project = textBox6.Text;
            string email_project = textBox7.Text;
            int[] idAuthorList = new int[listauthor.Count];
            for (int i = 0; i < listauthor.Count; i++)
            {
                idAuthorList[i] = listauthor[i].id_author;
            }
            client.AddProjectsAsync(name_project, lead_project, grnti_project, begin_project, end_project, money_project, email_project, idAuthorList, fos);
            MessageBox.Show(begin_project.ToString());
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceReference1.GRNTI temp = comboBox2.SelectedItem as ServiceReference1.GRNTI;
            grnti_project = (temp.code_grnti);
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "";
            listauthor.Clear();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceReference1.myAuthors temp = comboBox.SelectedItem as ServiceReference1.myAuthors;

            Boolean triger = true;
            if (comboBox.SelectedIndex != -1)
            {
                for (int i = 0; i < listauthor.Count; i++)
                {
                    if (listauthor[i].id_author == temp.id_author)
                    {
                        MessageBox.Show("Вы уже выбрали данного автора");
                        triger = false;
                    }
                }

                if (triger == true)
                {
                    
                    textBlock.Text += temp.surname_author +" "+ temp.name_author +" "+  temp.patronymic_author+ "\r\n";
                    listauthor.Add(temp);
                }
            }
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceReference1.FiledsOfScience temp = comboBox1.SelectedItem as ServiceReference1.FiledsOfScience;
            fos = (temp.id_fos);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _AddAuthor = new AddAuthor();
            _AddAuthor.Owner = this;
            
            if (_AddAuthor.ShowDialog() == true)
            {
                client.GetListAuthorsAsync();
            }
            else
            {
                client.GetListAuthorsAsync();
            }
        }
    }
}
