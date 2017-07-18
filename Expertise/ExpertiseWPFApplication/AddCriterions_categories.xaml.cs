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
    /// Логика взаимодействия для AddCriterions_categories.xaml
    /// </summary>
    public partial class AddCriterions_categories : Window
    {
        public int id_cat;
        public int id_crit;
        public int id_value;
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public List<term> ListTerm=new List<term>();
        public AddCriterions_categories()
        {
            InitializeComponent();
            client.AddCategoriesCompleted += Client_AddCategoriesCompleted;
            client.AddCriterionsCompleted += Client_AddCriterionsCompleted;
            client.EditCriterionsCompleted += Client_EditCriterionsCompleted;
            client.EditCritValuesCompleted += Client_EditCritValuesCompleted;
        }

        private void Client_EditCritValuesCompleted(object sender, ServiceReference1.EditCritValuesCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                MessageBox.Show("Обновлено");
                //button.Content = "Редактировать";
                //this.DialogResult = true;
            }
            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_EditCriterionsCompleted(object sender, ServiceReference1.EditCriterionsCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                MessageBox.Show("Обновлено");
                //button.Content = "Редактировать";
                //this.DialogResult = true;
            }
            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_AddCriterionsCompleted(object sender, ServiceReference1.AddCriterionsCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                MessageBox.Show("Добавлена");
                //button.Content = "Редактировать";
                this.DialogResult = true;
            }
            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_AddCategoriesCompleted(object sender, ServiceReference1.AddCategoriesCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                MessageBox.Show("Добавлена");
                //button.Content = "Редактировать";
                this.DialogResult = true;
            }
            else
                MessageBox.Show(e.Error.Message);
        }

        //сохранить категорию
        private void button_Click(object sender, RoutedEventArgs e)
        {
            client.AddCategoriesAsync(textBox.Text);
        }
        //сохранить критерий
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if(button1.Content.ToString() == "Сохранить")
            {
                Boolean qualit_crit = true;
                if (comboBox.SelectedIndex == 0)
                {
                    string name_crit = textBox1.Text;
                    qualit_crit = true;//Количесвенный
                    string OT = textBox2.Text;
                    string DO = textBox3.Text;
                    string measurement = textBox4.Text;
                    string valid_values = OT + ";" + DO + ";" + measurement + ";";
                    client.AddCriterionsAsync(name_crit, qualit_crit, valid_values, id_cat);
                }
                else if (comboBox.SelectedIndex == 1)
                {
                    string name_crit = textBox1.Text;
                    qualit_crit = false;//Качественный
                    bool triger = true;
                    for (int i = 0; i < dataGrid.Items.Count; i++)
                    {
                        term temp = dataGrid.Items[i] as term;
                        if (temp.name_term == "" || temp.name_term == null)
                        {
                            triger = false;
                            break;
                        }
                    }
                    if (triger == false)
                    {
                        MessageBox.Show("не все строки заполнены");
                    }
                    else if (triger == true)
                    {
                        string valid_values = null;
                        for (int i = 0; i < ListTerm.Count; i++)
                        {
                            valid_values += ListTerm[i].name_term + ";";
                        }
                        client.AddCriterionsAsync(name_crit, qualit_crit, valid_values, id_cat);
                    }
                }
            }
            else if(button1.Content.ToString() == "Изменить")
            {
                if (comboBox.SelectedIndex == 0)//Количественный
                {
                    Boolean qualit_crit = true;
                    string name_crit = textBox1.Text;
                    qualit_crit = true;//Количесвенный
                    string OT = textBox2.Text;
                    string DO = textBox3.Text;
                    string measurement = textBox4.Text;
                    string valid_values = OT + ";" + DO + ";" + measurement + ";";
                    client.EditCriterionsAsync(id_crit, name_crit, qualit_crit);
                    client.EditCritValuesAsync(id_value, id_crit, valid_values);
                }
                else if (comboBox.SelectedIndex == 1)//Качественный
                {
                    Boolean qualit_crit = true;
                    string name_crit = textBox1.Text;
                    qualit_crit = false;//Качественный
                    bool triger = true;
                    for (int i = 0; i < dataGrid.Items.Count; i++)
                    {
                        term temp = dataGrid.Items[i] as term;
                        if (temp.name_term == "" || temp.name_term == null)
                        {
                            triger = false;
                            break;
                        }
                    }
                    if (triger == false)
                    {
                        MessageBox.Show("не все строки заполнены");
                    }
                    else if (triger == true)
                    {
                        string valid_values = null;
                        for (int i = 0; i < ListTerm.Count; i++)
                        {
                            valid_values += ListTerm[i].name_term + ";";
                        }
                        client.EditCriterionsAsync(id_crit, name_crit, qualit_crit);
                        client.EditCritValuesAsync(id_value, id_crit, valid_values);
                    }
                }
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedIndex == 0)
            {
                tabControl1.Visibility = Visibility.Visible;
                tabControl1.SelectedIndex = 0;
            }
            else if (comboBox.SelectedIndex == 1)
            {
                tabControl1.Visibility = Visibility.Visible;
                tabControl1.SelectedIndex = 1;
            }
        }

       public class term
        {
           public string name_term { get; set; }
           public int value_term { get; set; }
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {

            int count = int.Parse(textBox5.Text);
            ListTerm.Clear();
            for (int i = 1; i < count+1; i++)
            {
                term temp = new term();
                temp.value_term = i;
                ListTerm.Add(temp);
                
            }
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = ListTerm;
        }

    }
}
