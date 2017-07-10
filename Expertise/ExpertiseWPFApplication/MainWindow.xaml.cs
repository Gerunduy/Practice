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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExpertiseWPFApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public MainWindow()
        {
            InitializeComponent();
            client.GetListGRNTICompleted += Client_GetListGRNTICompleted;
            client.GethelloCompleted += Client_GethelloCompleted;
        }

        private void Client_GethelloCompleted(object sender, ServiceReference1.GethelloCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                
                MessageBox.Show(e.Result);
            }


            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_GetListGRNTICompleted(object sender, ServiceReference1.GetListGRNTICompletedEventArgs e)
        {
            if (e.Error == null)
            {
                dataGrid1.ItemsSource = e.Result.ToList();

            }


            else
                MessageBox.Show(e.Error.Message);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("test");
            //client.GethelloAsync();
            client.GetListGRNTIAsync();
        }
    }
}
