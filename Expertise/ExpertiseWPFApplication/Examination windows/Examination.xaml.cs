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
    /// Логика взаимодействия для Examination.xaml
    /// </summary>
    public partial class Examination : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        ServiceReference1.myExpertiseExaminationTables Tables;
        int id_expertise;
        int id_expert;

        // =====================================
        double[,] SaatiMatrix;
        DataGrid dgSaatiMatrix;
        int CountCriterions;
        // =====================================
        DataGrid dgMarkTabel;
        int CountProject;
        // =====================================

        PriorityWizard _PriorityWizard;
        ExaminationProjectWizard _ExaminationProjectWizard;

        public Examination(int id_expertise, int id_expert)
        {
            InitializeComponent();

            client.GetExpertiseExaminationTablesByIDCompleted += Client_GetExpertiseExaminationTablesByIDCompleted;

            this.id_expertise = id_expertise;
            this.id_expert = id_expert;
            client.GetExpertiseExaminationTablesByIDAsync(id_expertise, id_expert);
            Waiting(true);
        }
        //=======================================================================================
        private void Client_GetExpertiseExaminationTablesByIDCompleted(object sender, ServiceReference1.GetExpertiseExaminationTablesByIDCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Tables = new ServiceReference1.myExpertiseExaminationTables();
                Tables = e.Result;
                CountCriterions = Tables.ListCriterions.Count();
                CountProject = Tables.ListProjects.Count();
                FillFileds();

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
        private void FillFileds()
        {
            PrintHierarchy();
            ViewStage1();
            ViewStage2();
        }
        private void PrintHierarchy()
        {
            int length1 = 20;
            foreach (ServiceReference1.Criterions pC in Tables.ListCriterions)
            {
                length1 = length1 + (pC.name_crit.Count() * 8) + 20;
            }
            int length2 = 20;
            foreach (ServiceReference1.Projects pP in Tables.ListProjects)
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
            tblExpertiseName.Text = Tables.expertise.name_expertise;
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
            foreach (ServiceReference1.Criterions pC in Tables.ListCriterions)
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
            foreach (ServiceReference1.Projects pP in Tables.ListProjects)
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
        // === Первый этап ===
        void SetPairCompareMatrix()
        {
            int CurContextCompare = 0;
            SaatiMatrix = new double[CountCriterions, CountCriterions];
            for (int i = 0; i < CountCriterions; i++)
            {
                for (int j = 0; j < CountCriterions; j++)
                {
                    if (j == i) { SaatiMatrix[i, j] = 1; }
                    else
                    {
                        if (j > i)
                        {
                            SaatiMatrix[i, j] = Tables.ListCritCompare[CurContextCompare].mark_compare;
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
        }
        void ShowPairCompareMatrix()
        {
            dgSaatiMatrix = new DataGrid();
            dgSaatiMatrix.AutoGenerateColumns = false;

            DataGridTextColumn CritColumn = new DataGridTextColumn();
            CritColumn.Binding = new Binding("name");
            dgSaatiMatrix.Columns.Add(CritColumn);
            for (int i = 0; i < CountCriterions; i++)
            {
                DataGridTextColumn Column = new DataGridTextColumn();
                Column.Header = Tables.ListCriterions[i].name_crit;
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
                    arrMark[j] = SaatiMatrix[i, j];
                }
                myCompareRow Row = new myCompareRow(Tables.ListCriterions[i].name_crit, arrMark);
                lRow.Add(Row);
            }
            // === === ===
            dgSaatiMatrix.ItemsSource = lRow;

            gInsideStage1.Children.Clear();
            gInsideStage1.RowDefinitions.Clear();
            gInsideStage1.ColumnDefinitions.Clear();
            gInsideStage1.Children.Add(dgSaatiMatrix);
        }
        private void ViewStage1()
        {
            if (Tables.ListCritCompare.Count() == 0)
            {
                Button btnGoToCompareCrit = new Button();
                btnGoToCompareCrit.Content = "Перейти к определению";
                btnGoToCompareCrit.Height = 40;
                btnGoToCompareCrit.Width = 200;
                btnGoToCompareCrit.Click += BtnGoToCompareCrit_Click;

                gInsideStage1.Children.Add(btnGoToCompareCrit);
            }
            else
            {
                // отобразить таблицу с данными сравнения
                SetPairCompareMatrix();
                ShowPairCompareMatrix();
            }
        }
        // === Второй этап ===
        private void ViewStage2()
        {
            if (Tables.ListMark.Count() == 0)
            {
                Button btnGoToSetMark = new Button();
                btnGoToSetMark.Content = "Перейти к оцениванию";
                btnGoToSetMark.Height = 40;
                btnGoToSetMark.Width = 200;
                btnGoToSetMark.Click += BtnGoToSetMark_Click;

                gInsideStage2.Children.Add(btnGoToSetMark);
            }
            else
            {
                // отобразить таблицу с оценками критериев
                ShowMarkTabel();
            }
        }
        void ShowMarkTabel()
        {
            dgMarkTabel = new DataGrid();
            dgMarkTabel.AutoGenerateColumns = false;

            DataGridTextColumn ProjectColumn = new DataGridTextColumn();
            ProjectColumn.Binding = new Binding("name");
            dgMarkTabel.Columns.Add(ProjectColumn);
            for (int i = 0; i < CountCriterions; i++)
            {
                DataGridTextColumn Column = new DataGridTextColumn();
                Column.Header = Tables.ListCriterions[i].name_crit;
                Column.Binding = new Binding(string.Format("content.[{0}]", i));
                dgMarkTabel.Columns.Add(Column);
            }

            List<myMarkRow> lRow = new List<myMarkRow>();
            // === Формируем строку ===
            for (int i = 0; i < CountProject; i++)
            {
                int[] arrMark = new int[CountCriterions];
                for (int j = 0; j < CountCriterions; j++)
                {
                    arrMark[j] = Tables.ListMark.Where(b => b.id_crit == Tables.ListCriterions[j].id_crit).Where(n => n.id_project == Tables.ListProjects[i].id_project).FirstOrDefault().rating + 1;
                }
                myMarkRow Row = new myMarkRow(Tables.ListProjects[i].name_project, arrMark);
                lRow.Add(Row);
            }
            // === === ===
            dgMarkTabel.ItemsSource = lRow;

            gInsideStage2.Children.Clear();
            gInsideStage2.RowDefinitions.Clear();
            gInsideStage2.ColumnDefinitions.Clear();
            gInsideStage2.Children.Add(dgMarkTabel);
        }
        //=======================================================================================
        private void BtnGoToCompareCrit_Click(object sender, RoutedEventArgs e)
        {
            _PriorityWizard = new PriorityWizard(id_expertise, id_expert, Tables.ListCriterions);
            _PriorityWizard.Owner = this;
            _PriorityWizard.ShowDialog();
            if (_PriorityWizard.DialogResult == true)
            {
                client.GetExpertiseExaminationTablesByIDAsync(id_expertise, id_expert);
                Waiting(true);
            }
        }
        private void BtnGoToSetMark_Click(object sender, RoutedEventArgs e)
        {
            _ExaminationProjectWizard = new ExaminationProjectWizard(id_expertise, id_expert, Tables.ListCriterions, Tables.ListProjects);
            _ExaminationProjectWizard.Owner = this;
            _ExaminationProjectWizard.ShowDialog();
            if(_ExaminationProjectWizard.DialogResult == true)
            {
                client.GetExpertiseExaminationTablesByIDAsync(id_expertise, id_expert);
                Waiting(true);
            }
        }
    }
}
