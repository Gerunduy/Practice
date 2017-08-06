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
    /// Логика взаимодействия для FOSCard.xaml
    /// </summary>
    public partial class FOSCard : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public int id_fos;
        public FOSCard()
        {
            
            InitializeComponent();
            client.AddFiledsOfScienceCompleted += Client_AddFiledsOfScienceCompleted;
            client.EditFiledsOfScienceCompleted += Client_EditFiledsOfScienceCompleted;
        }

        private void Client_EditFiledsOfScienceCompleted(object sender, ServiceReference1.EditFiledsOfScienceCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                this.DialogResult = true;
                MessageBox.Show("Изменения сохранены.");
            }
            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_AddFiledsOfScienceCompleted(object sender, ServiceReference1.AddFiledsOfScienceCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                this.DialogResult = true;
                MessageBox.Show("Добавлено.");
            }
            else
                MessageBox.Show(e.Error.Message);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(button.Content.ToString() == "Сохранить")
            {
                string name_fos = textBox.Text;
                client.AddFiledsOfScienceAsync(name_fos);

            }
            else if(button.Content.ToString() == "Изменить")
            {
                string name_fos = textBox.Text;
                client.EditFiledsOfScienceAsync(id_fos, name_fos);
            }
        }
    }
}
