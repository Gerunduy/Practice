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
        ServiceReference1.myExpertiseForCard Expertise;

        List<ServiceReference1.Criterions> ltmpCriterions = new List<ServiceReference1.Criterions>();
        List<ServiceReference1.CatCrit> ltmpCatCrit = new List<ServiceReference1.CatCrit>();


        //=== ========================== ===
        ServiceReference1.Projects CurrentProj;
        //=== ========================== ===
        ServiceReference1.Experts CurrentExpert;



        public ExpertiseCard(int id_expertise)
        {
            InitializeComponent();

            client.GetMyExpertiseForCardByIDCompleted += Client_GetMyExpertiseForCardByIDCompleted;
            client.GetMyExpertiseForCardByIDAsync(id_expertise);
            Waiting(true);
        }
        //=======================================================================================
        private void Client_GetMyExpertiseForCardByIDCompleted(object sender, ServiceReference1.GetMyExpertiseForCardByIDCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Expertise = e.Result;
                FirstFillFileds();

                Waiting(false);
            }
            else
            {
                Waiting(false);

            }
        }
        //=======================================================================================
        private void FirstFillFileds()
        {
            tblNameExpertise.Text = Expertise.name_expertise;
            tblFosExpertise.Text = Expertise.fos_expertise;
            dgExpertiseProjectList.ItemsSource = Expertise.ListProjects;
            dgCatList.ItemsSource = Expertise.ListCategories;
            dgExpertiseCritList.ItemsSource = null;
        }
        private void GetCritCurrCat(int id_cat)
        {
            ltmpCriterions = new List<ServiceReference1.Criterions>();
            ltmpCatCrit = new List<ServiceReference1.CatCrit>();
            ltmpCatCrit = Expertise.ListCatCrit.Where(p => p.id_cat == id_cat).ToList();
            foreach (ServiceReference1.CatCrit pCC in ltmpCatCrit)
            {
                ltmpCriterions.Add(Expertise.ListCriterions.Where(o => o.id_crit == pCC.id_crit).FirstOrDefault());
            }

            dgExpertiseCritList.ItemsSource = null;
            dgExpertiseCritList.ItemsSource = ltmpCriterions;
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
        //=== Работа с проектами ====================================
        //private void dgProjectList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        //{
        //    try
        //    {
        //        CurrentProj = dgProjectList.SelectedCells[0].Item as ServiceReference1.Projects;
        //        tmpProj = new ServiceReference1.Projects();
        //        tmpProj = CurrentProj;

        //        CheckProjbtn();
        //    }
        //    catch { }
        //}
        //private void dgExpertiseProjectList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        //{
        //    try
        //    {
        //        CurrentExpertiseProj = dgExpertiseProjectList.CurrentCell.Item as ServiceReference1.Projects;
        //        CheckProjbtn();
        //    }
        //    catch { }
        //}
        //=== Работа с категориями и критериями ====================================
        private void dgCatList_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.Categories CurrentCat = dgCatList.CurrentCell.Item as ServiceReference1.Categories;
                GetCritCurrCat(CurrentCat.id_category);
            }
            catch { }
        }
        //=== Работа с экспертами ====================================
        //private void dgExpertiseExpertList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        //{
        //    try
        //    {
        //        CurrentExpertiseExpert = dgExpertiseExpertList.CurrentCell.Item as ServiceReference1.Experts;
        //        CheckExpertbtn();
        //    }
        //    catch { }
        //}
        //=== Редактирование экспертизы ====================================

        //=======================================================================================
    }
}
