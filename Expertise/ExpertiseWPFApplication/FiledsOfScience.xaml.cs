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
    /// Логика взаимодействия для FiledsOfScience.xaml
    /// </summary>
    public partial class FiledsOfScience : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        FOSCard _FOSCard;
        public FiledsOfScience()
        {
         
            InitializeComponent();
            client.GetListFOSCompleted += Client_GetListFOSCompleted;
            client.GetListFOSAsync();
        }

        private void Client_GetListFOSCompleted(object sender, ServiceReference1.GetListFOSCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                dataGrid.ItemsSource = e.Result.ToList();
                //MessageBox.Show(e.Result.ToList()[0].name_crit);
            }
            else
                MessageBox.Show(e.Error.Message);
        }
        //добавить новый направление
        private void button_Click(object sender, RoutedEventArgs e)
        {
            _FOSCard = new FOSCard();
            _FOSCard.Owner = this;
            _FOSCard.button.Content = "сохранить";
            if (_FOSCard.ShowDialog() == true)
            {
                client.GetListFOSAsync();
            }
            else
            {
                client.GetListFOSAsync();
            }
        }

        private void bt_updatefos_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference1.FiledsOfScience temp = dataGrid.SelectedItem as ServiceReference1.FiledsOfScience;
            _FOSCard = new FOSCard();
            _FOSCard.Owner = this;
            _FOSCard.id_fos = temp.id_fos;
            _FOSCard.textBox.Text = temp.name_fos;
            _FOSCard.button.Content = "изменить";
            if (_FOSCard.ShowDialog() == true)
            {
                client.GetListFOSAsync();
            }
            else
            {
                client.GetListFOSAsync();
            }
        }

        
    }
}
