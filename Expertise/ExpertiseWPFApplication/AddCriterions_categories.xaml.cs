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
    /// Логика взаимодействия для AddCriterions_categories.xaml
    /// </summary>
    public partial class AddCriterions_categories : Window
    {
        public int id_cat;
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public AddCriterions_categories()
        {
            InitializeComponent();
            client.AddCategoriesCompleted += Client_AddCategoriesCompleted;
            client.AddCriterionsCompleted += Client_AddCriterionsCompleted;
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
            string name_crit = textBox1.Text;
            Boolean qualit_crit=true;
            if (comboBox.SelectedIndex == 0)
            {
                qualit_crit = true;//Количесвенный
            }
            else if (comboBox.SelectedIndex == 1)
            {
                qualit_crit = false;//Качественный
            }
            
            string OT = textBox2.Text;
            string DO = textBox3.Text;
            string valid_values = OT + ";" + DO+";";
            string patronymic_expert = textBox4.Text;
            client.AddCriterionsAsync(name_crit, qualit_crit, valid_values, id_cat);
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

       
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(textBox5.Text);
            for (int i = 0; i < count; i++)
            {
                dataGrid.Items.Add(i);
            }

        }
    }
}
