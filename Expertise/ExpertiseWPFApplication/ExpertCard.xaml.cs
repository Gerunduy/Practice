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
    /// Логика взаимодействия для ExpertCard.xaml
    /// </summary>
    public partial class ExpertCard : Window
    {
      public  ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
       public List<ServiceReference1.FiledsOfScience> listFOSCurrentExpert = new List<ServiceReference1.FiledsOfScience>();
        public int j;
        public int id_expert=-1;
        public Boolean die = false;
        public ExpertCard()
        {
            InitializeComponent();
            client.GetListFOSCompleted += Client_GetListFOSCompleted;
            client.UpdateExpertCardCompleted += Client_UpdateExpertCardCompleted;
            client.Expertise_ExpertCompleted += Client_Expertise_ExpertCompleted;
            client.AddExpertCompleted += Client_AddExpertCompleted;
            client.DeleteExpertCompleted += Client_DeleteExpertCompleted;
            //client.Expertise_ExpertAsync(id_expert);
            client.GetListFOSAsync();
        }

        private void Client_DeleteExpertCompleted(object sender, ServiceReference1.DeleteExpertCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                MessageBox.Show("запись удалена");
                //button.Content = "Редактировать";
                this.DialogResult = true;
            }
            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_AddExpertCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                MessageBox.Show("запись добавлена");
                //button.Content = "Редактировать";
                this.DialogResult = true;
            }
            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_Expertise_ExpertCompleted(object sender, ServiceReference1.Expertise_ExpertCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                dataGrid.ItemsSource = e.Result.ToList();

            }
            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_UpdateExpertCardCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                MessageBox.Show("запись обновлена");
                button.Content = "Редактировать";
                //this.DialogResult = true;
            }
            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_GetListFOSCompleted(object sender, ServiceReference1.GetListFOSCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                comboBox.ItemsSource = e.Result.ToList();
                
            }


            else
                MessageBox.Show(e.Error.Message);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(button.Content.ToString()== "Редактировать")
            {
                textBox.IsReadOnly = false;
                textBox1.IsReadOnly = false;
                textBox2.IsReadOnly = false;
                textBox3.IsReadOnly = false;
                textBox4.IsReadOnly = false;
                textBox5.IsReadOnly = false;
                textBox6.IsReadOnly = false;
                textBox7.IsReadOnly = false;
                textBox8.IsReadOnly = false;
                textBox9.IsReadOnly = false;
                comboBox.Visibility = Visibility.Visible;
                button1.Visibility = Visibility.Visible;
                client.GetListFOSAsync();
                
                button.Content = "Сохранить";
            }
            else
            {
                string surname_expert = textBox.Text;
                string name_expert = textBox1.Text;
                string patronymic_expert = textBox2.Text;
                string job_expert = textBox3.Text;//место работы
                string post_expert = textBox7.Text;//должность
                string degree_expert = textBox4.Text;//степень
                string rank_expert = textBox5.Text;//звание
                Boolean delete_expert = die;//удален\активен
                string contacts_expert = textBox6.Text;
                string login_expert = textBox8.Text;
                string password_expert = textBox9.Text;
                int[] idFOSList = new int[listFOSCurrentExpert.Count];
                for (int i = 0; i < listFOSCurrentExpert.Count; i++)
                {
                    idFOSList[i] = listFOSCurrentExpert[i].id_fos;
                }
                if (id_expert != -1)
                {
                    client.UpdateExpertCardAsync(id_expert, surname_expert, name_expert, patronymic_expert, job_expert,
                   post_expert, degree_expert, rank_expert, delete_expert, contacts_expert, idFOSList, login_expert, password_expert);
                }
                else
                {
                   client.AddExpertAsync(surname_expert, name_expert, patronymic_expert, job_expert,
                   post_expert, degree_expert, rank_expert, contacts_expert, idFOSList, login_expert, password_expert);
                }
               
                //List<FiledsOfScience> ListFOS { get; set; }
                textBox.IsReadOnly = true;
                textBox1.IsReadOnly = true;
                textBox2.IsReadOnly = true;
                textBox3.IsReadOnly = true;
                textBox4.IsReadOnly = true;
                textBox5.IsReadOnly = true;
                textBox6.IsReadOnly = true;
                textBox7.IsReadOnly = true;
                textBox8.IsReadOnly = true;
                textBox9.IsReadOnly = true;
                comboBox.Visibility = Visibility.Hidden;
                button1.Visibility = Visibility.Hidden;
               
                
            }
        }
       
       
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "";
            listFOSCurrentExpert.Clear();
            j = 0;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            ServiceReference1.FiledsOfScience temp = comboBox.SelectedItem as ServiceReference1.FiledsOfScience;

            Boolean triger = true;
            if (comboBox.SelectedIndex != -1)
            {
                for (int i = 0; i < listFOSCurrentExpert.Count; i++)
                {
                    if (listFOSCurrentExpert[i].id_fos == temp.id_fos)
                    {
                        MessageBox.Show("Вы уже выбрали данный раздел");
                        triger = false;
                    }
                }
               
                if (triger == true)
                {
                   j= j + 1;
                    textBlock.Text += j + " " + temp.name_fos + "\r\n";
                    listFOSCurrentExpert.Add(temp);
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            
            if (MessageBox.Show("Вы точно хотите удалить эту запись?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                die = true;
                client.DeleteExpertAsync(id_expert);
                //DialogResult = true;

            }
            else
            {


            }
        }
    }
}
