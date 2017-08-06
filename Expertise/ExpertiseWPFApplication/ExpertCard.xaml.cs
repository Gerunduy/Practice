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
        ExpertiseCard _ExpertiseCard;

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
        public ExpertCard(int id_expert)
        {
            InitializeComponent();
            client.GetListFOSCompleted += Client_GetListFOSCompleted;
            client.UpdateExpertCardCompleted += Client_UpdateExpertCardCompleted;
            client.Expertise_ExpertCompleted += Client_Expertise_ExpertCompleted;
            client.AddExpertCompleted += Client_AddExpertCompleted;
            client.DeleteExpertCompleted += Client_DeleteExpertCompleted;
            client.GetExpertsWithCountExpertiseCompleted += Client_GetExpertsWithCountExpertiseCompleted;
            //client.Expertise_ExpertAsync(id_expert);
            client.GetExpertsWithCountExpertiseAsync(id_expert);
            button.IsEnabled = false;
            button2.IsEnabled = false;
            
        }

        private void Client_GetExpertsWithCountExpertiseCompleted(object sender, ServiceReference1.GetExpertsWithCountExpertiseCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                textBox.Text = e.Result.surname_expert;
                textBox1.Text = e.Result.name_expert;
                textBox2.Text = e.Result.patronymic_expert;
                textBox3.Text = e.Result.job_expert;
                textBox7.Text = e.Result.post_expert;
                textBox4.Text = e.Result.degree_expert;
                textBox5.Text = e.Result.rank_expert;
                textBox6.Text = e.Result.contacts_expert;
                textBox8.Text = e.Result.login_expert;
                textBox9.Text = e.Result.password_expert;
                if (e.Result.commision_chairman) chkbxChairman.IsChecked = true; else chkbxChairman.IsChecked = false;
                chkbxChairman.IsEnabled = false;
                id_expert = e.Result.id_expert;
                client.Expertise_ExpertAsync(e.Result.id_expert);
                textBlock.Text = "";
                dataGrid.ItemsSource = null;
                tabControl.Visibility = Visibility.Visible;
                listFOSCurrentExpert.Clear();

                for (int i = 0; i < e.Result.ListFOS.ToList().Count; i++)
                {
                    int j = i + 1;
                    listFOSCurrentExpert.Add(e.Result.ListFOS[i]);
                    textBlock.Text += j + " " + e.Result.ListFOS[i].name_fos + "\r\n";
                    j = j;
                }
                client.GetListFOSAsync();
                button.IsEnabled = true;
                button2.IsEnabled = true;
            }
            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_DeleteExpertCompleted(object sender, ServiceReference1.DeleteExpertCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                MessageBox.Show("Эксперт удалён");
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
                MessageBox.Show("Эксперт добавлен");
                //button.Content = "Редактировать";
                this.DialogResult = true;
                button.IsEnabled = true;
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
                MessageBox.Show("Запись обновлена");
                button.Content = "Редактировать";
                button.IsEnabled = true;
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
            if(button.Content.ToString() == "Редактировать")
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
                chkbxChairman.IsEnabled = true;
                comboBox.Visibility = Visibility.Visible;
                button1.Visibility = Visibility.Visible;
                client.GetListFOSAsync();
                
                button.Content = "Сохранить";
            }
            else
            {
                if (textBox.Text.Trim(' ') != "" && textBox1.Text.Trim(' ') != "" && textBox2.Text.Trim(' ') != "" && textBox3.Text.Trim(' ') != "" && textBox4.Text.Trim(' ') != "" &&
                    textBox5.Text.Trim(' ') != "" && textBox6.Text.Trim(' ') != "" && textBox7.Text.Trim(' ') != "" && textBox8.Text.Trim(' ') != "" && textBox9.Text.Trim(' ') != "" &&
                    listFOSCurrentExpert.Count() != 0)
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
                    bool commisiom_chairman;
                    if (chkbxChairman.IsChecked == true) commisiom_chairman = true;
                    else commisiom_chairman = false;

                    int[] idFOSList = new int[listFOSCurrentExpert.Count];
                    for (int i = 0; i < listFOSCurrentExpert.Count; i++)
                    {
                        idFOSList[i] = listFOSCurrentExpert[i].id_fos;
                    }
                    if (id_expert != -1)
                    {
                        client.UpdateExpertCardAsync(id_expert, surname_expert, name_expert, patronymic_expert, job_expert,
                       post_expert, degree_expert, rank_expert, delete_expert, contacts_expert, idFOSList, login_expert, password_expert, commisiom_chairman);
                        button.IsEnabled = false;
                    }
                    else
                    {
                        client.AddExpertAsync(surname_expert, name_expert, patronymic_expert, job_expert,
                        post_expert, degree_expert, rank_expert, contacts_expert, idFOSList, login_expert, password_expert, commisiom_chairman);
                        button.IsEnabled = false;
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
                else
                {
                    MessageBox.Show("Некорректное заполнение полей!");
                }                
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
            
            if (MessageBox.Show("Вы точно хотите удалить этого эксперта?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                die = true;
                client.DeleteExpertAsync(id_expert);
                //DialogResult = true;

            }
            else
            {


            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ServiceReference1.Expertise_Expert temp = dataGrid.SelectedItem as ServiceReference1.Expertise_Expert;

            _ExpertiseCard = new ExpertiseCard(temp.id_expertise);
            _ExpertiseCard.Owner = this;
            _ExpertiseCard.ShowDialog();
        }
    }
}
