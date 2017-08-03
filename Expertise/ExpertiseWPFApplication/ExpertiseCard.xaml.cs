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
        int id_expertise;
        ServiceReference1.myExpertiseForCard Expertise;
        ServiceReference1.Experts expert;

        List<ServiceReference1.Criterions> ltmpCriterions = new List<ServiceReference1.Criterions>();
        List<ServiceReference1.CatCrit> ltmpCatCrit = new List<ServiceReference1.CatCrit>();

        int SelectedProjectID;
        int SelectedExpertID;

        int CountExpert;
        int CounCriterions;
        int CountProject;

        EditExpertiseCard _EditExpertiseCard;

        public ExpertiseCard(int id_expertise, ServiceReference1.Experts expert)
        {
            InitializeComponent();
            this.id_expertise = id_expertise;
            this.expert = expert;

            client.GetMyExpertiseForCardByIDCompleted += Client_GetMyExpertiseForCardByIDCompleted;
            client.GetMyExpertiseForCardByIDAsync(id_expertise);
            Waiting(true);
            ShowResultExpertiseTable(false);
        }
        public ExpertiseCard(int id_expertise)
        {
            InitializeComponent();
            this.id_expertise = id_expertise;

            client.GetMyExpertiseForCardByIDCompleted += Client_GetMyExpertiseForCardByIDCompleted;
            client.GetMyExpertiseForCardByIDAsync(id_expertise);
            btnEditExpertise.Visibility = Visibility.Hidden;
            Waiting(true);
            ShowResultExpertiseTable(false);
        }
        //=======================================================================================
        private void Client_GetMyExpertiseForCardByIDCompleted(object sender, ServiceReference1.GetMyExpertiseForCardByIDCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Expertise = e.Result;
                CountExpert = Expertise.ListExperts.Count();
                CounCriterions = Expertise.ListCriterions.Count();
                CountProject = Expertise.ListProjects.Count();
                FirstFillFileds();
                try
                {
                    if (expert.comission_chairman && !Expertise.begin_expertise) btnEditExpertise.IsEnabled = true; else btnEditExpertise.IsEnabled = false;
                }
                catch { }
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
            tblStatusExpertise.Text = Expertise.status;
            dgExpertiseProjectList.ItemsSource = Expertise.ListProjects;
            tblCountProject.Text = string.Format("Количество проектов: {0}", Expertise.ListProjects.Count());
            dgCatList.ItemsSource = Expertise.ListCategories;
            dgExpertiseCritList.ItemsSource = null;

            if (Expertise.MarkIsCompleted) ShowResultExpertiseTable(true);
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
        void SetResultExpertiseTable()
        {
            GetExpertiseTableContent();

            //dgExpertiseExpertCommissionResult = new DataGrid();

            //DataGridTextColumn CritProjectColumn = new DataGridTextColumn();
            //CritProjectColumn.Binding = new Binding("name");
            //dgExpertiseExpertCommissionResult.Columns.Add(CritProjectColumn);
            //for (int i = 0; i < CountExpert; i++)
            //{
            //    DataGridTextColumn Column = new DataGridTextColumn();
            //    Column.Header = Tables.ListCriterions[i].name_crit;
            //    Column.Binding = new Binding(string.Format("content.[{0}]", i));
            //    dgMarkTabel.Columns.Add(Column);
            //}

            //List<myMarkRow> lRow = new List<myMarkRow>();
            //// === Формируем строку ===
            //for (int i = 0; i < CountProject; i++)
            //{
            //    int[] arrMark = new int[CountCriterions];
            //    for (int j = 0; j < CountCriterions; j++)
            //    {
            //        arrMark[j] = Tables.ListMark.Where(b => b.id_crit == Tables.ListCriterions[j].id_crit).Where(n => n.id_project == Tables.ListProjects[i].id_project).Where(l => l.id_expert == id_expert).FirstOrDefault().rating + 1;
            //    }
            //    myMarkRow Row = new myMarkRow(Tables.ListProjects[i].name_project, arrMark);
            //    lRow.Add(Row);
            //}
            //// === === ===
            //dgMarkTabel.ItemsSource = lRow;

            //gInsideStage2.Children.Clear();
            //gInsideStage2.RowDefinitions.Clear();
            //gInsideStage2.ColumnDefinitions.Clear();
            //gInsideStage2.Children.Add(dgMarkTabel);
        }
        void GetExpertiseTableContent()
        {

        }
        void ShowResultExpertiseTable(bool Show)
        {
            if (Show)
            {
                SetResultExpertiseTable();
                tblResultExpertiseWait.Visibility = Visibility.Hidden;
                dgExpertiseExpertCommissionResult.Visibility = Visibility.Visible;
                btnShowGraphic.IsEnabled = true;
            }
            else
            {
                dgExpertiseExpertCommissionResult.Visibility = Visibility.Hidden;
                tblResultExpertiseWait.Visibility = Visibility.Visible;
                btnShowGraphic.IsEnabled = false;
            } 
        }
        //=======================================================================================
        //=== Работа с проектами ====================================
        private void dgExpertiseProjectList_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.myProjectForExpertiseCard CurrentProjectExpertise = dgExpertiseProjectList.CurrentCell.Item as ServiceReference1.myProjectForExpertiseCard;
                SelectedProjectID = CurrentProjectExpertise.id_project;
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
        //=== Работа с экспертами ====================================
        //=== Редактирование экспертизы ====================================
        private void btnEditExpertise_Click(object sender, RoutedEventArgs e)
        {
            _EditExpertiseCard = new EditExpertiseCard(id_expertise);
            _EditExpertiseCard.Owner = this;
            _EditExpertiseCard.ShowDialog();
            if (_EditExpertiseCard.DialogResult == true)
            {
                client.GetMyExpertiseForCardByIDAsync(id_expertise);
                Waiting(true);
            }
        }

        private void btnGetResultExpertise_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("test");
        }
        //=======================================================================================
    }
}
