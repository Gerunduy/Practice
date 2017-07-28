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
    /// Логика взаимодействия для CompletedExpertises.xaml
    /// </summary>
    public partial class CompletedExpertises : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        List<ServiceReference1.myCompletedexpertises> lCompletedExpertises = new List<ServiceReference1.myCompletedexpertises>();
        List<ServiceReference1.myCompletedexpertisesProject> ltmpProjects = new List<ServiceReference1.myCompletedexpertisesProject>();
        List<string> ltmpExperts = new List<string>();


        public CompletedExpertises()
        {
            InitializeComponent();
            client.GetListComoletedExpertisesCompleted += Client_GetListComoletedExpertisesCompleted;

            Waiting(true);
            client.GetListComoletedExpertisesAsync();
        }
        //=======================================================================================
        private void Client_GetListComoletedExpertisesCompleted(object sender, ServiceReference1.GetListComoletedExpertisesCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                lCompletedExpertises = e.Result.ToList();

                dgExpertiseList.ItemsSource = null;
                dgExpertiseList.ItemsSource = lCompletedExpertises;

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
            ServiceReference1.myCompletedexpertises tmpExpertise = lCompletedExpertises.Where(p => p.id_expertise == id_expertise).FirstOrDefault();

            ltmpProjects = new List<ServiceReference1.myCompletedexpertisesProject>();
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
                ServiceReference1.myCompletedexpertises CurrentExpertise = dgExpertiseList.CurrentCell.Item as ServiceReference1.myCompletedexpertises;
                GetInfoCurrExpertise(CurrentExpertise.id_expertise);
            }
            catch { }
        }
        //=======================================================================================
    }
}
