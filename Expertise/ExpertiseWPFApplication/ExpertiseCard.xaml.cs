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
    /// Логика взаимодействия для ExpertiseCard.xaml
    /// </summary>
    public partial class ExpertiseCard : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        List<ServiceReference1.FiledsOfScience> lFOS = new List<ServiceReference1.FiledsOfScience>();
        ServiceReference1.FiledsOfScience tFOS = new ServiceReference1.FiledsOfScience();
        List<ServiceReference1.Projects> lProjects = new List<ServiceReference1.Projects>();
        List<ServiceReference1.ProjectFos> lProjectFos = new List<ServiceReference1.ProjectFos>();
        List<ServiceReference1.Categories> lCatigories = new List<ServiceReference1.Categories>();
        List<ServiceReference1.CatCrit> lCatCrit = new List<ServiceReference1.CatCrit>();
        List<ServiceReference1.Criterions> lCriterions = new List<ServiceReference1.Criterions>();
        List<ServiceReference1.CritValues> lCritValues = new List<ServiceReference1.CritValues>();
        List<ServiceReference1.Experts> lExperts = new List<ServiceReference1.Experts>();
        List<ServiceReference1.ExpertFos> lExpertFos = new List<ServiceReference1.ExpertFos>();

        List<ServiceReference1.Projects> ltmpProjects = new List<ServiceReference1.Projects>();
        List<ServiceReference1.Experts> ltmpExperts = new List<ServiceReference1.Experts>();


        public ExpertiseCard()
        {
            InitializeComponent();
            client.GetTablesForExpertiseCompleted += Client_GetTablesForExpertiseCompleted;
            client.GetTablesForExpertiseAsync();
            Waiting(true);
        }
        //=======================================================================================
        private void Client_GetTablesForExpertiseCompleted(object sender, ServiceReference1.GetTablesForExpertiseCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (!e.Result.Err)
                {
                    lFOS = e.Result.lFOS.ToList();
                    lProjects = e.Result.lProjects.ToList();
                    lProjectFos = e.Result.lProjectFos.ToList();
                    lCatigories = e.Result.lCatigories.ToList();
                    lCatCrit = e.Result.lCatCrit.ToList();
                    lCriterions = e.Result.lCriterions.ToList();
                    lCritValues = e.Result.lCritValues.ToList();
                    lExperts = e.Result.lExperts.ToList();
                    lExpertFos = e.Result.lExpertFos.ToList();

                    ltmpExperts = lExperts;
                    ltmpProjects = lProjects;
                    FirstFillFileds();

                    Waiting(false);
                }
                else
                {
                    Waiting(false);
                    
                }
            }
            else
            {
                Waiting(false);
                
            }
        }
        //=======================================================================================
        private void FirstFillFileds()
        {
            cmbFOS.ItemsSource = lFOS;
            dgProjectList.ItemsSource = ltmpProjects;
            dgCatList.ItemsSource = lCatigories;
            dgCritList.ItemsSource = lCriterions;
            dgExpertList.ItemsSource = lExperts;
        }
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
        //=======================================================================================
        //=======================================================================================

    }
}
