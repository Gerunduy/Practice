using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для CardExpertiseProject.xaml
    /// </summary>
    public partial class CardExpertiseProject : Window
    {
        string grnti_project;
        AddAuthor _AddAuthor;
        int fos;
       public int id_project;
        public List<ServiceReference1.myAuthors> listauthor = new List<ServiceReference1.myAuthors>();
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public CardExpertiseProject()
        {
            InitializeComponent();
            client.GetListAuthorsCompleted += Client_GetListAuthorsCompleted;
            client.GetListFOSCompleted += Client_GetListFOSCompleted;
            client.GetListGRNTICompleted += Client_GetListGRNTICompleted;
            client.EditProjectCompleted += Client_EditProjectCompleted;
            client.DeleteProjectCompleted += Client_DeleteProjectCompleted;
            client.GetListExpertForProjectCompleted += Client_GetListExpertForProjectCompleted;
            client.GetListRaitingForExpertiseCompleted += Client_GetListRaitingForExpertiseCompleted;
            //client.testCompleted += Client_testCompleted;
            //client.testAsync();
            client.GetListAuthorsAsync();
            client.GetListFOSAsync();
            client.GetListGRNTIAsync();
        }

        //private void Client_testCompleted(object sender, ServiceReference1.testCompletedEventArgs e)
        //{
        //    if (e.Error == null)
        //    {
              
        //        MessageBox.Show(e.Result.ToList()[0].id_expertise.ToString());
        //        //DialogResult = true;
        //    }


        //    else
        //    {
        //        MessageBox.Show(e.Error.Message);
        //        //DialogResult = false;
        //    }
        //}

        private void Client_GetListRaitingForExpertiseCompleted(object sender, ServiceReference1.GetListRaitingForExpertiseCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                for (int i = 0; i < e.Result.ToList().Count; i++)
                {
                    DataTable dt = new DataTable();
                    string s = e.Result.ToList()[i].raiting_crit;
                    String[] words = s.Split(new char[] { ';' });
                    string t="";
                    for (int j = 0; i < words.Length - 1; j++)
                    {
                       
                        t += "\"" + words[i] + "\"";
                        //temp.name_term = words[j];
                        //temp.value_term = i + 1;
                        //_AddCriterions_categories.ListTerm.Add(temp);
                    }
                    //string f = ("\"1\", \"2\"");
                    dt.Rows.Add(t);
                    dataGrid.ItemsSource = dt.DefaultView;
                }
            }
            else
            {
                MessageBox.Show(e.Error.Message);
                
            }
        }

        private void Client_GetListExpertForProjectCompleted(object sender, ServiceReference1.GetListExpertForProjectCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result.ToList().Count != 0)
                {
                    for (int i = 0; i < e.Result.ToList().Count; i++)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add(e.Result.ToList()[i].surname_expert + e.Result.ToList()[i].name_expert + e.Result.ToList()[i].patronymic_expert);
                        dataGrid.ItemsSource = dt.DefaultView;
                    }
                    
                }
                client.GetListRaitingForExpertiseAsync(id_project);


                //DataTable dt = new DataTable();
                //dt.Columns.Add("Заказчик");
                //dt.Columns.Add("Количество");
                //dt.Rows.Add("1", "2");
                //my_DataGrid.Items.Add(dt);

            }


            else
            {
                MessageBox.Show(e.Error.Message);
               
            }
        }

        private void Client_DeleteProjectCompleted(object sender, ServiceReference1.DeleteProjectCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result == true)
                {
                    MessageBox.Show("Проект удален");
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Проект не может бть удален так как он участвует в экспертизе");
                    DialogResult = true;
                }
            }
            else
            {
                MessageBox.Show(e.Error.Message);
                DialogResult = false;
            }
        }

        private void Client_EditProjectCompleted(object sender, ServiceReference1.EditProjectCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                MessageBox.Show("Обновлено");
                DialogResult = true;
            }


            else
            {
                MessageBox.Show(e.Error.Message);
                DialogResult = false;
            }
                
        }

        private void Client_GetListGRNTICompleted(object sender, ServiceReference1.GetListGRNTICompletedEventArgs e)
        {
            if (e.Error == null)
            {
               comboBox2.ItemsSource = e.Result.ToList();
            }


            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_GetListFOSCompleted(object sender, ServiceReference1.GetListFOSCompletedEventArgs e)
        {
            if (e.Error == null)
            {

                comboBox1.ItemsSource = e.Result.ToList();
            }
            else
                MessageBox.Show(e.Error.Message);
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
            if(button.Content.ToString()== "Редактировать")
            {
                textBox.IsReadOnly = false;
                textBox1.IsReadOnly = false;
                textBox2.Visibility = Visibility.Hidden;
                textBox3.Visibility = Visibility.Hidden;
                datePicker1.IsEnabled = true;
                datePicker2.IsEnabled = true;
                textBox6.IsReadOnly = false;
                textBox7.IsReadOnly = false;
                comboBox.Visibility = Visibility.Visible;
                comboBox1.Visibility = Visibility.Visible;
                comboBox2.Visibility = Visibility.Visible;
                button.Content = "Сохранить";
                button1.Visibility = Visibility.Visible;
                button2.Visibility = Visibility.Visible;
            }
            else if(button.Content.ToString() == "Сохранить")
            {
                string name_project = textBox.Text;
                string lead_project = textBox1.Text;

                DateTime begin_project = DateTime.Parse(datePicker1.SelectedDate.Value.ToShortDateString());
                DateTime end_project = DateTime.Parse(datePicker2.SelectedDate.Value.ToShortDateString());
                string money_project = textBox6.Text;
                string email_project = textBox7.Text;
                int[] idAuthorList = new int[listauthor.Count];
                for (int i = 0; i < listauthor.Count; i++)
                {
                    idAuthorList[i] = listauthor[i].id_author;
                }
                client.EditProjectAsync(id_project, name_project, lead_project, grnti_project, begin_project, end_project, money_project, email_project, idAuthorList, fos);
            }
           
            
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

                    textBlock.Text += temp.surname_author + " " + temp.name_author + " " + temp.patronymic_author + "\r\n";
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить эту запись?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                client.DeleteProjectAsync(id_project);

            }
            else
            {


            }
            
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            client.GetListExpertForProjectAsync(id_project);
        }
    }
}
