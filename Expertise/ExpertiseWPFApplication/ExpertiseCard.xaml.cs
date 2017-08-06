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
        int CountCriterions;
        int CountProject;

        List<myExpertiseComissionResultRow> ListComissionResult;

        // ==========
        List<double[,]> ListFuzzyMarkMatrixForCrit; // FuzzyMarkMatrixForCrit i - проект; j - критерий 
        double[,] AvgMarkMatrixForProject; // AvgMarkMatrixForCrit j - критерий 

        List<double[,]> ListSaatiMatrix; // Матрицы Саати для каждого эксперта этой экспертизы
        double[,] AVGSaatiMatrix; // Среднегеометрическое всех матриц Саати по этой экспертизе
        double[] OwnVectorComponents; // Компоненты собственного вектора
        double[] OwnVector; // Собственный вектор

        double[,] ModiferMarks; // Модифицированное множество нечётких оценок
        double[] MultiplyMarkMtrix; // Перемноженое OwnVector и AvgMarkMatrixForProject
        // ==========

        EditExpertiseCard _EditExpertiseCard;
        EspertiseResult _ExpertiseResult;
        CardExpertiseProject _CardExpertiseProject;
        ExpertCard _ExpertCard;

        public ExpertiseCard(int id_expertise, ServiceReference1.Experts expert)
        {
            InitializeComponent();
            this.id_expertise = id_expertise;
            this.expert = expert;

            client.GetMyExpertiseForCardByIDCompleted += Client_GetMyExpertiseForCardByIDCompleted;
            client.GetListAuthorsForProjectCompleted += Client_GetListAuthorsForProjectCompleted;
            client.GetMyExpertiseForCardByIDAsync(id_expertise);
            Waiting(true);
            ShowResultExpertiseTable(false);
        }
        public ExpertiseCard(int id_expertise)
        {
            InitializeComponent();
            this.id_expertise = id_expertise;

            client.GetMyExpertiseForCardByIDCompleted += Client_GetMyExpertiseForCardByIDCompleted;
            client.GetListAuthorsForProjectCompleted += Client_GetListAuthorsForProjectCompleted;
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
                CountCriterions = Expertise.ListCriterions.Count();
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

            if (Expertise.end_expertise)
            {
                MultiplyMatrix();
                for (int i = 0; i < CountProject; i++)
                {
                    Expertise.ListProjects[i].Rating = MultiplyMarkMtrix[i].ToString();
                }
            }
            else
            {
                dgExpertiseProjectList.Columns[4].Visibility = Visibility.Hidden;
                dgExpertiseProjectList.Columns[5].Visibility = Visibility.Hidden;
            }
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
            SetListSaatiMatrix();

            DataGridTextColumn ExpertColumn = new DataGridTextColumn();
            ExpertColumn.Header = "Эксперт";
            ExpertColumn.Binding = new Binding("name");
            dgExpertiseExpertCommissionResult.Columns.Add(ExpertColumn);
            for (int i = 0; i < CountCriterions; i++)
            {
                DataGridTextColumn Column = new DataGridTextColumn();
                Column.Header = Expertise.ListCriterions[i].name_crit;
                Column.Binding = new Binding(string.Format("content.[{0}]", i));
                dgExpertiseExpertCommissionResult.Columns.Add(Column);
            }

            ListComissionResult = new List<myExpertiseComissionResultRow>();
            // === Формируем строку ===
            for (int i = 0; i < CountExpert; i++)
            {
                double[] arr = new double[CountCriterions];
                arr = GetOwnVector(ListSaatiMatrix[i]);
                for (int d = 0; d < arr.Count(); d++)
                {
                    arr[d] = Math.Round(arr[d], 3);
                }

                myExpertiseComissionResultRow Row = new myExpertiseComissionResultRow(Expertise.ListExperts[i].id_expert, string.Format("{0} {1}, {2}", Expertise.ListExperts[i].surname_expert, Expertise.ListExperts[i].name_expert, Expertise.ListExperts[i].patronymic_expert), arr);
                ListComissionResult.Add(Row);
            }
            // === === ===
        }
        void ShowResultExpertiseTable(bool Show)
        {
            if (Show)
            {
                SetResultExpertiseTable();
                tblResultExpertiseWait.Visibility = Visibility.Hidden;
                dgExpertiseExpertCommissionResult.ItemsSource = ListComissionResult;
                dgExpertiseExpertCommissionResult.Visibility = Visibility.Visible;
                btnShowResult.IsEnabled = true;
            }
            else
            {
                dgExpertiseExpertCommissionResult.ItemsSource = null;
                dgExpertiseExpertCommissionResult.Visibility = Visibility.Hidden;
                tblResultExpertiseWait.Visibility = Visibility.Visible;
                btnShowResult.IsEnabled = false;
            } 
        }
        //=======================================================================================
        #region Расчёты
        double GetFuzzyValue(int Value) // Возврощает нечёткое значение
        {
            switch (Value)
            {
                case 0:
                    return 0.0;
                    break;
                case 1:
                    return 0.75;
                    break;
                case 2:
                    return 0.5;
                    break;
                case 3:
                    return 0.25;
                    break;
                case 4:
                    return 0.0;
                    break;
                default:
                    return -1.0;
                    break;
            }
        }
        void GetFuzzyMarkMatrix() // Строит матрицу нечётких значений
        {
            ListFuzzyMarkMatrixForCrit = new List<double[,]>();
            for (int c = 0; c< CountCriterions; c++)
            {
                double[,] AvgMarkMatrixForCrit = new double[CountProject, CountExpert];
                for (int p = 0; p < CountProject; p++)
                {
                    for (int e = 0; e < CountExpert; e++)
                    {
                        AvgMarkMatrixForCrit[p,e] = GetFuzzyValue(Expertise.ListMark.Where(m => m.id_crit == Expertise.ListCriterions[c].id_crit).Where(m => m.id_project == Expertise.ListProjects[p].id_project).Where(m => m.id_expert == Expertise.ListExperts[e].id_expert).FirstOrDefault().rating);
                    }
                }
                ListFuzzyMarkMatrixForCrit.Add(AvgMarkMatrixForCrit);
            }
        }
        void GetAvgMarkMatrix()
        {
            GetFuzzyMarkMatrix();

            AvgMarkMatrixForProject = new double[CountProject, CountCriterions];
            for (int c = 0; c < CountCriterions; c++)
            {
                for (int p = 0; p < CountProject; p++)
                {
                    double AVGProjectForCrit = 0;
                    for (int e = 0; e < CountExpert; e++)
                    {
                        AVGProjectForCrit = AVGProjectForCrit + ListFuzzyMarkMatrixForCrit[c][p, e];
                    }
                    AVGProjectForCrit = AVGProjectForCrit / CountExpert;
                    AvgMarkMatrixForProject[p, c] = AVGProjectForCrit;
                }
            }
        }


        void SetListSaatiMatrix()
        {
            ListSaatiMatrix = new List<double[,]>();
            for (int e = 0; e < CountExpert; e++)
            {
                int CurContextCompare = 0;
                double[,] SaatiMatrix = new double[CountCriterions, CountCriterions];
                for (int i = 0; i < CountCriterions; i++)
                {
                    for (int j = 0; j < CountCriterions; j++)
                    {
                        if (j == i) { SaatiMatrix[i, j] = 1; }
                        else
                        {
                            if (j > i)
                            {
                                SaatiMatrix[i, j] = Expertise.ListCritCompare.Where(p => p.id_expert == Expertise.ListExperts[e].id_expert).ToList()[CurContextCompare].mark_compare;
                                CurContextCompare = CurContextCompare + 1;
                            }
                            else
                            {
                                if (SaatiMatrix[j, i] < 1)
                                {
                                    SaatiMatrix[i, j] = Math.Round(1 / SaatiMatrix[j, i], 0);
                                }
                                else
                                {
                                    SaatiMatrix[i, j] = Math.Round(1 / SaatiMatrix[j, i], 3);
                                }
                            }
                        }
                    }
                }
                ListSaatiMatrix.Add(SaatiMatrix);
            }
        }
        void GetAVGSaatiMatrix()
        {
            SetListSaatiMatrix();

            AVGSaatiMatrix = new double[CountCriterions, CountCriterions];
            for (int i = 0; i < CountCriterions; i++)
            {
                for (int j = 0; j < CountCriterions; j++)
                {
                    double cell = 1.0;
                    for (int e = 0; e < CountExpert; e++)
                    {
                        cell = cell * ListSaatiMatrix[e][i, j];
                    }
                    cell = Math.Pow(cell, (1.0 / CountExpert));
                    AVGSaatiMatrix[i, j] = cell;
                }
            }
        }
        void GetOwnVectorComponent()
        {
            GetAVGSaatiMatrix();

            OwnVectorComponents = new double[CountCriterions];
            for (int i = 0; i < CountCriterions; i++)
            {
                double component = 1.0;
                for (int j = 0; j < CountCriterions; j++)
                {
                    component = component * AVGSaatiMatrix[i, j];
                }
                component = Math.Pow(component, (1.0 / CountCriterions));
                OwnVectorComponents[i] = component;
            }
        }
        void GetOwnVector()
        {
            GetOwnVectorComponent();

            double summ = 0.0;
            foreach (double ovc in OwnVectorComponents)
            {
                summ = summ + ovc;
            }

            OwnVector = new double[CountCriterions];
            for (int i = 0; i < CountCriterions; i++)
            {
                OwnVector[i] = OwnVectorComponents[i] / summ;
            }
        }
        double[] GetOwnVector(double[,] matrix)
        {
            int countcrit = 0;
            foreach (double r in matrix)
            {
                countcrit = countcrit + 1;
            }
            countcrit = int.Parse(Math.Sqrt(countcrit).ToString());

            double[] OVC = new double[countcrit];
            for (int i = 0; i < countcrit; i++)
            {
                double c = 1.0;
                for (int j = 0; j < countcrit; j++)
                {
                    c = c * matrix[i, j];
                }
                c = Math.Pow(c, (1.0 / countcrit));
                OVC[i] = c;
            }

            double s = 0.0;
            foreach (double ovc in OVC)
            {
                s = s + ovc;
            }

            double[] OV = new double[countcrit];
            for (int i = 0; i < countcrit; i++)
            {
                OV[i] = OVC[i] / s;
            }

            return OV;
        }


        void GetModiferMarks()
        {
            GetAvgMarkMatrix();
            GetOwnVector();

            ModiferMarks = new double[CountProject, CountCriterions];
            for (int i = 0; i < CountProject; i++)
            {
                for (int j = 0; j < CountCriterions; j++)
                {
                    ModiferMarks[i, j] = Math.Pow(AvgMarkMatrixForProject[i, j], (OwnVector[j] * 4));
                }
            }
        }
        void MultiplyMatrix()
        {
            GetModiferMarks();

            MultiplyMarkMtrix = new double[CountProject];
            for (int i = 0; i < CountProject; i++)
            {
                double summ = 0.0;
                for (int j = 0; j < CountCriterions; j++)
                {
                    summ = summ + AvgMarkMatrixForProject[i, j] * OwnVector[j];
                }
                MultiplyMarkMtrix[i] = summ;
            }
        }
        #endregion
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

        private void btnShowResult_Click(object sender, RoutedEventArgs e)
        {
            _ExpertiseResult = new EspertiseResult(Expertise,expert);
            _ExpertiseResult.Owner = this;
            _ExpertiseResult.Show();
        }
        //=======================================================================================
        private void Client_GetListAuthorsForProjectCompleted(object sender, ServiceReference1.GetListAuthorsForProjectCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                _CardExpertiseProject.listauthor = e.Result.ToList();
                for (int i = 0; i < e.Result.ToList().Count; i++)
                {
                    _CardExpertiseProject.textBlock.Text += e.Result.ToList()[i].FIO + "\r\n";

                }
                if (_CardExpertiseProject.ShowDialog() == true)
                {
                    Waiting(true);
                    client.GetMyExpertiseForCardByIDAsync(id_expertise);
                }
                else
                {
                    Waiting(true);
                }
            }
            else
                MessageBox.Show(e.Error.Message);
        }
        private void btnGoToProjectCard_Click(object sender, RoutedEventArgs e)
        {
            ServiceReference1.myProjectForExpertiseCard temp = dgExpertiseProjectList.SelectedItem as ServiceReference1.myProjectForExpertiseCard;

            _CardExpertiseProject = new CardExpertiseProject(temp.id_project);
            _CardExpertiseProject.Owner = this;
            _CardExpertiseProject.ShowDialog();
        }

        private void btnGoToExpertCard_Click(object sender, RoutedEventArgs e)
        {
            myExpertiseComissionResultRow temp = dgExpertiseExpertCommissionResult.SelectedItem as myExpertiseComissionResultRow;

            _ExpertCard = new ExpertCard(temp.id_expert);
            _ExpertCard.Owner = this;
            _ExpertCard.ShowDialog();
        }
        //=======================================================================================
    }
}
