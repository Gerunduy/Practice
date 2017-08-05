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
    /// Логика взаимодействия для EspertiseResult.xaml
    /// </summary>
    public partial class EspertiseResult : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        ServiceReference1.myExpertiseForCard Expertise;
        ServiceReference1.Experts expert;

        List<int> lWinnerIndex;

        int CountExpert;
        int CountCriterions;
        int CountProject;
        
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
        public EspertiseResult(ServiceReference1.myExpertiseForCard Expertise, ServiceReference1.Experts expert)
        {
            InitializeComponent();
            client.SupportProjectCompleted += Client_SupportProjectCompleted;
            this.Expertise = Expertise;
            this.expert = expert;
            CountExpert = Expertise.ListExperts.Count();
            CountCriterions = Expertise.ListCriterions.Count();
            CountProject = Expertise.ListProjects.Count();
            MultiplyMatrix();
            FirstFillFileds();
            Waiting(false);
        }
        public EspertiseResult(ServiceReference1.myExpertiseForCard Expertise)
        {
            InitializeComponent();
            client.SupportProjectCompleted += Client_SupportProjectCompleted;
            this.Expertise = Expertise;
            CountExpert = Expertise.ListExperts.Count();
            CountCriterions = Expertise.ListCriterions.Count();
            CountProject = Expertise.ListProjects.Count();
            MultiplyMatrix();
            FirstFillFileds();
            Waiting(false);
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
        private void FirstFillFileds()
        {
            ShowResultExpertiseTable();
            try
            {
                if (Expertise.end_expertise) gSupport.Visibility = Visibility.Hidden;
                else
                {
                    if (expert.comission_chairman) gSupport.Visibility = Visibility.Visible;
                    else gSupport.Visibility = Visibility.Hidden;
                }                
            }
            catch
            {
                gSupport.Visibility = Visibility.Hidden;
            }
            ShowStage1Tables();
            ShowStage2Tables();
            ShowHierarchy();            
        }

        void ShowResultExpertiseTable()
        {
            DataGridTextColumn ProjColumn = new DataGridTextColumn();
            ProjColumn.Binding = new Binding("name");
            dgExpertiseResult.Columns.Add(ProjColumn);
            
            DataGridTextColumn Rating = new DataGridTextColumn();
            Rating.Header = "Рейтинг проекта";
            Rating.Binding = new Binding("rating");
            dgExpertiseResult.Columns.Add(Rating);

            DataGridTextColumn Col = new DataGridTextColumn();
            Col.Header = " ";
            Col.Binding = new Binding("status");
            dgExpertiseResult.Columns.Add(Col);

            lWinnerIndex = new List<int>();
            List<double> lm = MultiplyMarkMtrix.ToList();
            for (int i = 0; i < Expertise.count_project_expertise; i++)
            {
                for (int j = 0; j < lm.Count(); j++)
                {
                    if (lm[j] == lm.Max())
                    {
                        lWinnerIndex.Add(j);
                        lm.RemoveAt(j);
                        break;
                    }
                    else continue;
                }
            }

            List<myExpertiseProjectResult> lRow = new List<myExpertiseProjectResult>();
            // === Формируем строку ===
            for (int i = 0; i < CountProject; i++)
            {
                myExpertiseProjectResult Row = new myExpertiseProjectResult(Expertise.ListProjects[i].name_project, MultiplyMarkMtrix[i]);
                lRow.Add(Row);
            }
            foreach (int index in lWinnerIndex)
            {
                lRow[index].status = "Победитель";
            }
            // === === ===
            dgExpertiseResult.ItemsSource = lRow;  
        }
        void ShowStage1Tables()
        {
            gInsideStage1.Children.Clear();
            gInsideStage1.RowDefinitions.Clear();
            gInsideStage1.ColumnDefinitions.Clear();

            RowDefinition rd0 = new RowDefinition();
            rd0.Height = new GridLength(15);
            gInsideStage1.RowDefinitions.Add(rd0);
            for (int i = 0; i < ListSaatiMatrix.Count() + ListSaatiMatrix.Count() - 1; i++)
            {
                int x;
                Math.DivRem(i, 2, out x);

                RowDefinition rd = new RowDefinition();
                if (x != 0) rd.Height = new GridLength(25);
                gInsideStage1.RowDefinitions.Add(rd);
            }

            int curGridLine = 1;
            for (int m = 0; m< ListSaatiMatrix.Count(); m++)
            {
                DataGrid dgSaatiMatrix = new DataGrid();
                dgSaatiMatrix.AutoGenerateColumns = false;

                DataGridTextColumn CritColumn = new DataGridTextColumn();
                CritColumn.Binding = new Binding("name");
                dgSaatiMatrix.Columns.Add(CritColumn);
                for (int i = 0; i < CountCriterions; i++)
                {
                    DataGridTextColumn Column = new DataGridTextColumn();
                    Column.Header = Expertise.ListCriterions[i].name_crit;
                    Column.Binding = new Binding(string.Format("content.[{0}]", i));
                    dgSaatiMatrix.Columns.Add(Column);
                }

                List<myCompareRow> lRow = new List<myCompareRow>();
                // === Формируем строку ===
                for (int i = 0; i < CountCriterions; i++)
                {
                    double[] arrMark = new double[CountCriterions];
                    for (int j = 0; j < CountCriterions; j++)
                    {
                        arrMark[j] = ListSaatiMatrix[m][i, j];
                    }
                    myCompareRow Row = new myCompareRow(Expertise.ListCriterions[i].name_crit, arrMark);
                    lRow.Add(Row);
                }
                // === === ===
                dgSaatiMatrix.ItemsSource = lRow;
                Grid.SetRow(dgSaatiMatrix, curGridLine);

                TextBlock tbl = new TextBlock();
                tbl.Text = string.Format("   {0}:", Expertise.ListExperts[m].name_expert);
                tbl.TextAlignment = TextAlignment.Left;
                tbl.VerticalAlignment = VerticalAlignment.Bottom;
                Grid.SetRow(tbl, curGridLine-1);
                gInsideStage1.Children.Add(tbl);

                gInsideStage1.Children.Add(dgSaatiMatrix);
                curGridLine = curGridLine + 2;
            } 
        }
        void ShowStage2Tables()
        {
            gInsideStage2.Children.Clear();
            gInsideStage2.RowDefinitions.Clear();
            gInsideStage2.ColumnDefinitions.Clear();

            RowDefinition rd0 = new RowDefinition();
            rd0.Height = new GridLength(15);
            gInsideStage2.RowDefinitions.Add(rd0);
            for (int i = 0; i < ListFuzzyMarkMatrixForCrit.Count() + ListFuzzyMarkMatrixForCrit.Count() - 1; i++)
            {
                int x;
                Math.DivRem(i, 2, out x);

                RowDefinition rd = new RowDefinition();
                if (x != 0) rd.Height = new GridLength(25);
                gInsideStage2.RowDefinitions.Add(rd);
            }

            int curGridLine = 1;
            for (int c = 0; c < ListFuzzyMarkMatrixForCrit.Count(); c++)
            {
                DataGrid MarkMatrixForCrit = new DataGrid();
                MarkMatrixForCrit.AutoGenerateColumns = false;

                DataGridTextColumn ProjColumn = new DataGridTextColumn();
                ProjColumn.Binding = new Binding("name");
                MarkMatrixForCrit.Columns.Add(ProjColumn);
                for (int e = 0; e < CountExpert; e++)
                {
                    DataGridTextColumn Column = new DataGridTextColumn();
                    Column.Header = Expertise.ListExperts[e].name_expert;
                    Column.Binding = new Binding(string.Format("content.[{0}]", e));
                    MarkMatrixForCrit.Columns.Add(Column);
                }

                List<myMarkRow> lRow = new List<myMarkRow>();
                // === Формируем строку ===
                for (int p = 0; p < CountProject; p++)
                {
                    int[] arrMark = new int[CountExpert];
                    for (int e = 0; e < CountExpert; e++)
                    {
                        arrMark[e] = Expertise.ListMark.Where(m => m.id_crit == Expertise.ListCriterions[c].id_crit).Where(m => m.id_project == Expertise.ListProjects[p].id_project).Where(m => m.id_expert == Expertise.ListExperts[e].id_expert).FirstOrDefault().rating + 1;
                    }
                    myMarkRow Row = new myMarkRow(Expertise.ListProjects[p].name_project, arrMark);
                    lRow.Add(Row);
                }
                // === === ===
                MarkMatrixForCrit.ItemsSource = lRow;
                Grid.SetRow(MarkMatrixForCrit, curGridLine);

                TextBlock tbl = new TextBlock();
                tbl.Text = string.Format("   {0}:", Expertise.ListCriterions[c].name_crit);
                tbl.TextAlignment = TextAlignment.Left;
                tbl.VerticalAlignment = VerticalAlignment.Bottom;
                Grid.SetRow(tbl, curGridLine - 1);
                gInsideStage2.Children.Add(tbl);

                gInsideStage2.Children.Add(MarkMatrixForCrit);
                curGridLine = curGridLine + 2;
            }
        }
        void ShowHierarchy()
        {
            int length1 = 20;
            foreach (ServiceReference1.Criterions pC in Expertise.ListCriterions)
            {
                length1 = length1 + (pC.name_crit.Count() * 8) + 20;
            }
            int length2 = 20;
            foreach (ServiceReference1.myProjectForExpertiseCard pP in Expertise.ListProjects)
            {
                length2 = length2 + (pP.name_project.Count() * 10) + 20;
            }
            cnvsHierarchy.Height = 280;
            if (length1 > length2) cnvsHierarchy.Width = length1; else cnvsHierarchy.Width = length2;

            List<int[]> lLineCoords = new List<int[]>();

            SolidColorBrush ExpertiseNameBckgrnd = new SolidColorBrush();
            ExpertiseNameBckgrnd.Color = Color.FromRgb(243, 109, 109);
            TextBlock tblExpertiseName = new TextBlock();
            tblExpertiseName.Background = ExpertiseNameBckgrnd;
            tblExpertiseName.Text = Expertise.name_expertise;
            tblExpertiseName.FontSize = 20;
            tblExpertiseName.TextAlignment = TextAlignment.Center;
            tblExpertiseName.Width = (tblExpertiseName.Text.Count() * 15);
            tblExpertiseName.SetValue(Canvas.LeftProperty, ((Math.Round(cnvsHierarchy.Width / 2, 0)) - Math.Round(tblExpertiseName.Width / 2, 0)));
            tblExpertiseName.SetValue(Canvas.TopProperty, (20.0));
            cnvsHierarchy.Children.Add(tblExpertiseName);

            int[] ExpertiseCoord = new int[2];
            ExpertiseCoord[0] = int.Parse(Math.Round(cnvsHierarchy.Width / 2, 0).ToString());
            ExpertiseCoord[1] = 45;

            List<int[]> lCritCoords = new List<int[]>();
            double Left = 20.0;
            foreach (ServiceReference1.Criterions pC in Expertise.ListCriterions)
            {
                SolidColorBrush CritBckgrnd = new SolidColorBrush();
                CritBckgrnd.Color = Color.FromRgb(234, 150, 30);
                TextBlock tblCrit = new TextBlock();
                tblCrit.Background = CritBckgrnd;
                tblCrit.Text = pC.name_crit;
                tblCrit.FontSize = 14;
                tblCrit.TextAlignment = TextAlignment.Center;
                tblCrit.Width = (tblCrit.Text.Count() * 8);
                tblCrit.SetValue(Canvas.LeftProperty, (Left));
                tblCrit.SetValue(Canvas.TopProperty, (120.0));
                Left = Left + tblCrit.Width + 20.0;
                cnvsHierarchy.Children.Add(tblCrit);

                int[] Coord = new int[2];
                Coord[0] = int.Parse((Left - 20 - Math.Round(tblCrit.Width / 2, 0)).ToString());
                Coord[1] = 140;
                lCritCoords.Add(Coord);

                Line MyLine = new Line();
                MyLine.X1 = ExpertiseCoord[0];
                MyLine.Y1 = ExpertiseCoord[1];
                MyLine.X2 = Left - 20 - Math.Round(tblCrit.Width / 2, 0);
                MyLine.Y2 = 120;
                MyLine.Stroke = System.Windows.Media.Brushes.Black;
                MyLine.StrokeThickness = 2;
                cnvsHierarchy.Children.Add(MyLine);
            }

            Left = 20.0;
            foreach (ServiceReference1.myProjectForExpertiseCard pP in Expertise.ListProjects)
            {
                SolidColorBrush ProjBckgrnd = new SolidColorBrush();
                ProjBckgrnd.Color = Color.FromRgb(109, 139, 243);
                TextBlock tblProj = new TextBlock();
                tblProj.Background = ProjBckgrnd;
                tblProj.Text = pP.name_project;
                tblProj.FontSize = 16;
                tblProj.TextAlignment = TextAlignment.Center;
                tblProj.Width = (tblProj.Text.Count() * 10);
                tblProj.SetValue(Canvas.LeftProperty, (Left));
                tblProj.SetValue(Canvas.TopProperty, (220.0));
                Left = Left + tblProj.Width + 20.0;
                cnvsHierarchy.Children.Add(tblProj);

                foreach (int[] pC in lCritCoords)
                {
                    Line MyLine = new Line();
                    MyLine.X1 = pC[0];
                    MyLine.Y1 = pC[1];
                    MyLine.X2 = Left - 20 - Math.Round(tblProj.Width / 2, 0);
                    MyLine.Y2 = 220;
                    MyLine.Stroke = System.Windows.Media.Brushes.Black;
                    MyLine.StrokeThickness = 2;
                    cnvsHierarchy.Children.Add(MyLine);
                }
            }
        }
        void ShowGraphic()
        {
            
        }
        //=======================================================================================
        #region Расчёты
        double GetFuzzyValue(int Value) // Возврощает нечёткое значение
        {
            switch (Value)
            {
                case 0:
                    return 0.0;
                case 1:
                    return 0.75;
                case 2:
                    return 0.5;
                case 3:
                    return 0.25;
                case 4:
                    return 0.0;
                default:
                    return -1.0;
            }
        }
        void GetFuzzyMarkMatrix() // Строит матрицу нечётких значений
        {
            ListFuzzyMarkMatrixForCrit = new List<double[,]>();
            for (int c = 0; c < CountCriterions; c++)
            {
                double[,] AvgMarkMatrixForCrit = new double[CountProject, CountExpert];
                for (int p = 0; p < CountProject; p++)
                {
                    for (int e = 0; e < CountExpert; e++)
                    {
                        AvgMarkMatrixForCrit[p, e] = GetFuzzyValue(Expertise.ListMark.Where(m => m.id_crit == Expertise.ListCriterions[c].id_crit).Where(m => m.id_project == Expertise.ListProjects[p].id_project).Where(m => m.id_expert == Expertise.ListExperts[e].id_expert).FirstOrDefault().rating);
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
        private void Client_SupportProjectCompleted(object sender, ServiceReference1.SupportProjectCompletedEventArgs e)
        {
            if (e.Error == null && e.Result)
            {
                gSupport.Visibility = Visibility.Hidden;
                Waiting(false);
                MessageBox.Show("Проект(ы) поддержаны. Экспертиза завершена.");
            }
            else
            {
                Waiting(false);
                MessageBox.Show("Ошибка!");
            }
        }
        private void btnSupport_Click(object sender, RoutedEventArgs e)
        {
            List<int> lIdProjects = new List<int>();
            foreach (int index in lWinnerIndex)
            {
                lIdProjects.Add(Expertise.ListProjects[index].id_project);
            }

            client.SupportProjectAsync(Expertise.id_expertise, lIdProjects.ToArray());
            Waiting(true);
        }
        //=======================================================================================
    }
}
