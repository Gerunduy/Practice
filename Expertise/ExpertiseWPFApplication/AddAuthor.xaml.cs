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
    /// Логика взаимодействия для AddAuthor.xaml
    /// </summary>
    public partial class AddAuthor : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public AddAuthor()
        {
            InitializeComponent();
            client.AddAuthorsCompleted += Client_AddAuthorsCompleted;
        }

        private void Client_AddAuthorsCompleted(object sender, ServiceReference1.AddAuthorsCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                MessageBox.Show("добавлено");
                this.DialogResult = true;

            }


            else
                MessageBox.Show(e.Error.Message);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string surname_author = textBox.Text;
            string name_author = textBox1.Text;
            string patronymic_author = textBox2.Text;
            client.AddAuthorsAsync(surname_author, name_author, patronymic_author);
        }
    }
}
