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

        List<ServiceReference1.myCurrentexpertises> lCurrentExpertises = new List<ServiceReference1.myCurrentexpertises>();
        List<ServiceReference1.myCurrentexpertisesProject> ltmpProjects = new List<ServiceReference1.myCurrentexpertisesProject>();
        List<string> ltmpExperts = new List<string>();
        ServiceReference1.myCurrentexpertises CurrentExpertise;

        ExpertiseCard _ExpertiseCard;

        public CurrentExpertises()
        {
            InitializeComponent();
            client.GetListCurrentExpertisesCompleted += Client_GetListCurrentExpertisesCompleted;

            Waiting(true);
            client.GetListCurrentExpertisesAsync();
        }
        //=======================================================================================
        private void Client_GetListCurrentExpertisesCompleted(object sender, ServiceReference1.GetListCurrentExpertisesCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                lCurrentExpertises = e.Result.ToList();

                dgExpertiseList.ItemsSource = null;
                dgExpertiseList.ItemsSource = lCurrentExpertises;

                Waiting(false);
            }
            else
            {
                Waiting(false);
            }
        }
        //=======================================================================================
        private void Waiting(bool Wait)
        {
            if (Wait)
            {
                Grid.Visibility = Visibility.Hidden;
                tblWait.Visibility = Visibility.Visible;
            }
            else
            {
                Grid.Visibility = Visibility.Visible;
                tblWait.Visibility = Visibility.Hidden;
            }
        }
        private void GetInfoCurrExpertise(int id_expertise)
        {
            ServiceReference1.myCurrentexpertises tmpExpertise = lCurrentExpertises.Where(p => p.id_expertise == id_expertise).FirstOrDefault();

            ltmpProjects = new List<ServiceReference1.myCurrentexpertisesProject>();
            ltmpProjects = tmpExpertise.ListProject.ToList();
            dgProjectList.ItemsSource = null;
            dgProjectList.ItemsSource = ltmpProjects;
            tblProjectCount.Text = string.Format("Количество: {0}", ltmpProjects.Count());

            ltmpExperts = new List<string>();
            ltmpExperts = tmpExpertise.ListExperts.ToList();
            dgExpertList.ItemsSource = null;
            dgExpertList.ItemsSource = ltmpExperts;
        }
        //=======================================================================================
        private void dgExpertiseList_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                CurrentExpertise = dgExpertiseList.CurrentCell.Item as ServiceReference1.myCurrentexpertises;
                GetInfoCurrExpertise(CurrentExpertise.id_expertise);
            }
            catch { }
        }

        private void btnGoToExpertiseCard_Click(object sender, RoutedEventArgs e)
        {
            _ExpertiseCard = new ExpertiseCard(CurrentExpertise.id_expertise);
            _ExpertiseCard.Owner = App.Current.MainWindow;
            _ExpertiseCard.Show();
        }
        //=======================================================================================
    }
}
