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
    /// Логика взаимодействия для CurrentExpertises.xaml
    /// </summary>
    public partial class CurrentExpertises : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        ServiceReference1.myCurrentexpertises CurrentExpertise;


        ExpertiseCard _ExpertiseCard;

        public CurrentExpertises()
        {
            InitializeComponent();
            client.GetListCurrentExpertisesCompleted += Client_GetListCurrentExpertisesCompleted;
            client.GetListCurrentExpertisesAsync();
        }

        private void Client_GetListCurrentExpertisesCompleted(object sender, ServiceReference1.GetListCurrentExpertisesCompletedEventArgs e)
        {
            if (e.Error == null)
            {

               dataGrid.ItemsSource = e.Result.ToList();
            }
            else
                MessageBox.Show(e.Error.Message);
        }


        private void bt_update_Click(object sender, RoutedEventArgs e)
        {
            _ExpertiseCard = new ExpertiseCard(CurrentExpertise.id_expertise);
            _ExpertiseCard.Owner = App.Current.MainWindow;
            _ExpertiseCard.Show();
        }

        private void dataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                CurrentExpertise = dataGrid.CurrentCell.Item as ServiceReference1.myCurrentexpertises;
            }
            catch { }
        }
    }
}
