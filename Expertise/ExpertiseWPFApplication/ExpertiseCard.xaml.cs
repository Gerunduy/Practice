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

        //=== ========================== ===
        ServiceReference1.Projects tmpProj;
        ServiceReference1.Projects CurrentProj;
        ServiceReference1.Projects CurrentExpertiseProj;
        //=== ========================== ===
        ServiceReference1.Criterions tmpCrit;
        ServiceReference1.Criterions CurrentCrit;
        ServiceReference1.Criterions CurrentExpertiseCrit;
        //=== ========================== ===
        ServiceReference1.Experts tmpExpert;
        ServiceReference1.Experts CurrentExpert;
        ServiceReference1.Experts CurrentExpertiseExpert;
        //=== ========================== ===


        List<ServiceReference1.Projects> lExpertiseProjects = new List<ServiceReference1.Projects>();
        List<ServiceReference1.Criterions> lExpertiseCrit = new List<ServiceReference1.Criterions>();
        List<ServiceReference1.Experts> lExpertiseExperts = new List<ServiceReference1.Experts>();

        List<ServiceReference1.FiledsOfScience> lFOS = new List<ServiceReference1.FiledsOfScience>();
        List<ServiceReference1.Projects> lProjects = new List<ServiceReference1.Projects>();
        List<ServiceReference1.ProjectFos> lProjectFos = new List<ServiceReference1.ProjectFos>();
        List<ServiceReference1.Categories> lCatigories = new List<ServiceReference1.Categories>();
        List<ServiceReference1.CatCrit> lCatCrit = new List<ServiceReference1.CatCrit>();
        List<ServiceReference1.Criterions> lCriterions = new List<ServiceReference1.Criterions>();
        List<ServiceReference1.CritValues> lCritValues = new List<ServiceReference1.CritValues>();
        List<ServiceReference1.Experts> lExperts = new List<ServiceReference1.Experts>();
        List<ServiceReference1.ExpertFos> lExpertFos = new List<ServiceReference1.ExpertFos>();

        List<ServiceReference1.Projects> ltmpProjects = new List<ServiceReference1.Projects>();
        List<ServiceReference1.Criterions> ltmpCriterions = new List<ServiceReference1.Criterions>();
        List<ServiceReference1.CatCrit> ltmpCatCrit = new List<ServiceReference1.CatCrit>();
        List<ServiceReference1.Experts> ltmpExperts = new List<ServiceReference1.Experts>();


        public ExpertiseCard()
        {
            InitializeComponent();
            client.GetTablesForExpertiseCompleted += Client_GetTablesForExpertiseCompleted;
            client.GetTablesForExpertiseAsync();
            Waiting(true);
            CheckProjbtn();
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

                    ltmpProjects = lProjects;
                    ltmpExperts = lExperts;
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
            dgCritList.ItemsSource = null;
            dgExpertList.ItemsSource = lExperts;
        }
        private void FilterFileds()
        {
            ServiceReference1.FiledsOfScience curFos = (ServiceReference1.FiledsOfScience)cmbFOS.SelectedItem;
            ltmpProjects = new List<ServiceReference1.Projects>();
            foreach (ServiceReference1.Projects pP in lProjects)
            {
                int Pid = pP.id_project;
                if (lProjectFos.Where(p => p.id_project == Pid).FirstOrDefault().id_fos == curFos.id_fos) // если направление науки проекта совпадает с выбранным направлением
                {
                    ltmpProjects.Add(pP);
                }
            }
            dgProjectList.ItemsSource = null;
            dgProjectList.ItemsSource = ltmpProjects;

            ltmpExperts = new List<ServiceReference1.Experts>();
            foreach (ServiceReference1.Experts pE in lExperts)
            {
                int Eid = pE.id_expert;
                foreach (ServiceReference1.ExpertFos pEFOS in lExpertFos)
                {
                    if (pEFOS.id_expert == Eid)
                    {
                        if (pEFOS.id_fos == curFos.id_fos) ltmpExperts.Add(pE); // если направление науки эксперта совпадает с выбранным направлением
                    }
                }
            }
            dgExpertList.ItemsSource = null;
            dgExpertList.ItemsSource = ltmpExperts;
        }
        private void GetCritCurrCat(int id_cat)
        {
            ltmpCriterions = new List<ServiceReference1.Criterions>();
            ltmpCatCrit = new List<ServiceReference1.CatCrit>();
            ltmpCatCrit = lCatCrit.Where(p => p.id_cat == id_cat).ToList();
            foreach (ServiceReference1.CatCrit pCC in ltmpCatCrit)
            {
                ltmpCriterions.Add(lCriterions.Where(o => o.id_crit == pCC.id_crit).FirstOrDefault());
            }

            dgCritList.ItemsSource = null;
            dgCritList.ItemsSource = ltmpCriterions;
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
        private void cmbFOS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterFileds();
        }

        //=== Работа с проектами ====================================
        private void CheckProjbtn()
        {
            if (CurrentProj != null && tmpProj != null) btnAddProject.IsEnabled = true;
            else btnAddProject.IsEnabled = false;
            if (CurrentExpertiseProj != null) btnDeleteProject.IsEnabled = true;
            else btnDeleteProject.IsEnabled = false;
        }
        private void dgProjectList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                CurrentProj = dgProjectList.SelectedCells[0].Item as ServiceReference1.Projects;
                tmpProj = new ServiceReference1.Projects();
                tmpProj = CurrentProj;

                CheckProjbtn();
            }
            catch { }
        }
        private void dgExpertiseProjectList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                CurrentExpertiseProj = dgExpertiseProjectList.CurrentCell.Item as ServiceReference1.Projects;
                CheckProjbtn();
            }
            catch { }
        }
        private void btnAddProject_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference1.Projects P = new ServiceReference1.Projects();
            P = tmpProj;
            if (lExpertiseProjects.Where(p => p.id_project == P.id_project).FirstOrDefault() == null) // если этот проект ещё не добавляли
            {
                lExpertiseProjects.Add(P);
                dgExpertiseProjectList.ItemsSource = null;
                dgExpertiseProjectList.ItemsSource = lExpertiseProjects;

                CheckProjbtn();
            }
            else
            {
                MessageBox.Show(string.Format("Проект с id = {0} уже был добавлен ранее!", P.id_project));
            }
        }
        private void btnDeleteProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < lExpertiseProjects.Count; i++)
                {
                    if (lExpertiseProjects[i].id_project == CurrentExpertiseProj.id_project)
                    {
                        lExpertiseProjects.RemoveAt(i);
                    }
                }
                dgExpertiseProjectList.ItemsSource = null;
                dgExpertiseProjectList.ItemsSource = lExpertiseProjects;

                CurrentExpertiseProj = null;
            }
            catch { }
        }
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
        private void CheckCritbtn()
        {
            if (CurrentCrit != null && tmpCrit != null) btnAddCrit.IsEnabled = true;
            else btnAddCrit.IsEnabled = false;
            if (CurrentExpertiseCrit != null) btnDeleteCrit.IsEnabled = true;
            else btnDeleteCrit.IsEnabled = false;
        }
        private void dgCritList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                CurrentCrit = dgCritList.SelectedCells[0].Item as ServiceReference1.Criterions;
                tmpCrit = new ServiceReference1.Criterions();
                tmpCrit = CurrentCrit;

                CheckCritbtn();
            }
            catch { }
        }
        private void dgExpertiseCritList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                CurrentExpertiseCrit = dgExpertiseCritList.CurrentCell.Item as ServiceReference1.Criterions;
                CheckCritbtn();
            }
            catch { }
        }
        private void btnAddCrit_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference1.Criterions C = new ServiceReference1.Criterions();
            C = tmpCrit;
            if (lExpertiseCrit.Where(p => p.id_crit == C.id_crit).FirstOrDefault() == null) // если этот критерий ещё не добавляли
            {
                lExpertiseCrit.Add(C);
                dgExpertiseCritList.ItemsSource = null;
                dgExpertiseCritList.ItemsSource = lExpertiseCrit;

                CheckCritbtn();
            }
            else
            {
                MessageBox.Show(string.Format("Критерий с id = {0} уже был добавлен ранее!", C.id_crit));
            }
        }
        private void btnDeleteCrit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < lExpertiseCrit.Count; i++)
                {
                    if (lExpertiseCrit[i].id_crit == CurrentExpertiseCrit.id_crit)
                    {
                        lExpertiseCrit.RemoveAt(i);
                    }
                }
                dgExpertiseCritList.ItemsSource = null;
                dgExpertiseCritList.ItemsSource = lExpertiseCrit;

                CurrentExpertiseCrit = null;
            }
            catch { }
        }
        //=== Работа с экспертами ====================================
        private void CheckExpertbtn()
        {
            if (CurrentExpert != null && tmpExpert != null) btnAddExpert.IsEnabled = true;
            else btnAddExpert.IsEnabled = false;
            if (CurrentExpertiseExpert != null) btnDeleteExpert.IsEnabled = true;
            else btnDeleteExpert.IsEnabled = false;
        }
        private void dgExpertList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                CurrentExpert = dgExpertList.SelectedCells[0].Item as ServiceReference1.Experts;
                tmpExpert = new ServiceReference1.Experts();
                tmpExpert = CurrentExpert;

                CheckExpertbtn();
            }
            catch { }
        }
        private void dgExpertiseExpertList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                CurrentExpertiseExpert = dgExpertiseExpertList.CurrentCell.Item as ServiceReference1.Experts;
                CheckExpertbtn();
            }
            catch { }
        }
        private void btnAddExpert_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference1.Experts E = new ServiceReference1.Experts();
            E = tmpExpert;
            if (lExpertiseExperts.Where(p => p.id_expert == E.id_expert).FirstOrDefault() == null) // если этого эксперта ещё не добавляли
            {
                lExpertiseExperts.Add(E);
                dgExpertiseExpertList.ItemsSource = null;
                dgExpertiseExpertList.ItemsSource = lExpertiseExperts;

                CheckProjbtn();
            }
            else
            {
                MessageBox.Show(string.Format("Эксперт с id = {0} уже был добавлен ранее!", E.id_expert));
            }
        }
        private void btnDeleteExpert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < lExpertiseExperts.Count; i++)
                {
                    if (lExpertiseExperts[i].id_expert == CurrentExpertiseExpert.id_expert)
                    {
                        lExpertiseExperts.RemoveAt(i);
                    }
                }
                dgExpertiseExpertList.ItemsSource = null;
                dgExpertiseExpertList.ItemsSource = lExpertiseExperts;

                CurrentExpertiseProj = null;
            }
            catch { }
        }
        //=======================================================================================

    }
}
