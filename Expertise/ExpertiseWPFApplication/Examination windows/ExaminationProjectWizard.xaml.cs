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
    /// Логика взаимодействия для ExaminationProjectWizard.xaml
    /// </summary>
    public partial class ExaminationProjectWizard : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        ServiceReference1.Criterions[] arrCriterions;
        ServiceReference1.Projects[] arrProjects;

        int id_expertise;
        int id_expert;

        int CountCriterions;
        int CountProjects;

        List<ServiceReference1.Marks> lMarks = new List<ServiceReference1.Marks>();

        int CurrentCriterionsIndex;

        public ExaminationProjectWizard(int id_expertise, int id_expert, ServiceReference1.Criterions[] arrCriterions, ServiceReference1.Projects[] arrProjects)
        {
            InitializeComponent();
            client.AddNewMarkCompleted += Client_AddNewMarkCompleted;
            btnOK.IsEnabled = false;

            this.id_expertise = id_expertise;
            this.id_expert = id_expert;
            this.arrCriterions = arrCriterions;
            this.arrProjects = arrProjects;
            CountCriterions = arrCriterions.Count();
            CountProjects = arrProjects.Count();
            CurrentCriterionsIndex = 0;

            CreateExaminationGrid(CurrentCriterionsIndex);
            Waiting(false);
        }
        //==============================================================================================
        private void Waiting(bool Wait)
        {
            if (Wait)
            {
                gGrid.Visibility = Visibility.Hidden;
                tblWait.Visibility = Visibility.Visible;
            }
            else
            {
                gGrid.Visibility = Visibility.Visible;
                tblWait.Visibility = Visibility.Hidden;
            }
        }
        //==============================================================================================
        void CreateExaminationGrid(int CurrCritIndex)
        {
            btnNext.IsEnabled = false;
            ServiceReference1.Criterions CurrCrit = arrCriterions[CurrCritIndex];

            tblCritName.Text = string.Format("Критерий: {0}", CurrCrit.name_crit);
            gContent.Children.Clear();
            gContent.RowDefinitions.Clear();

            if (!CurrCrit.qualit_crit) // если критерий количественный
            {
                List<string> ltmpValidValues = CurrCrit.CritValues[0].valid_values.Split(new char[] { ';' }).ToList();
                int C = ltmpValidValues.Count();
                ltmpValidValues.RemoveAt(C - 1);
                C = ltmpValidValues.Count();

                List<string> lValidValues = new List<string>();
                string UOM = "";
                for (int i = 0; i < C; i++)
                {
                    if (i != C - 1) lValidValues.Add(ltmpValidValues[i]);
                    else UOM = ltmpValidValues[i];
                }

                List<MarkForExaminationProject> lM = new List<MarkForExaminationProject>();
                for (int i = 0; i < lValidValues.Count(); i++)
                {
                    MarkForExaminationProject M = new MarkForExaminationProject(lValidValues[i].ToString(), i + 1);
                    lM.Add(M);
                }

                for (int i = 0; i < CountProjects + CountProjects-1; i++)
                {
                    int x;
                    Math.DivRem(i, 2, out x);

                    RowDefinition rd = new RowDefinition();
                    if (x == 0) rd.Height = new GridLength(55); else rd.Height = new GridLength(5);
                    gContent.RowDefinitions.Add(rd);      
                }

                int curGridLine = 0;
                for (int i = 0; i < CountProjects; i++)
                {
                    TextBlock tbl = new TextBlock();
                    tbl.Text = arrProjects[i].name_project;
                    tbl.TextWrapping = TextWrapping.Wrap;
                    tbl.TextAlignment = TextAlignment.Right;
                    Grid.SetRow(tbl, curGridLine);
                    Grid.SetColumn(tbl, 0);
                    gContent.Children.Add(tbl);

                    ComboBox cmb = new ComboBox();
                    cmb.ItemsSource = lM;
                    cmb.DisplayMemberPath = "fuzzyvalue";
                    cmb.Height = 30;
                    cmb.SelectionChanged += Cmb_SelectionChanged;
                    cmb.VerticalAlignment = VerticalAlignment.Top;
                    Grid.SetRow(cmb, curGridLine);
                    Grid.SetColumn(cmb, 2);
                    gContent.Children.Add(cmb);

                    TextBlock tbl2 = new TextBlock();
                    tbl2.Text = UOM;
                    tbl2.TextWrapping = TextWrapping.Wrap;
                    Grid.SetRow(tbl2, curGridLine);
                    Grid.SetColumn(tbl2, 4);
                    gContent.Children.Add(tbl2);

                    curGridLine = curGridLine + 2;
                }
            }
            else // если критерий качественный
            {
                List<string> lValidValues = CurrCrit.CritValues[0].valid_values.Split(new char[] { ';' }).ToList();
                int C = lValidValues.Count();
                lValidValues.RemoveAt(C - 1);
                C = lValidValues.Count();

                List<MarkForExaminationProject> lM = new List<MarkForExaminationProject>();
                for (int i = 0; i < lValidValues.Count(); i++)
                {
                    MarkForExaminationProject M = new MarkForExaminationProject(lValidValues[i].ToString(), i + 1);
                    lM.Add(M);
                }

                for (int i = 0; i < CountProjects + CountProjects - 1; i++)
                {
                    int x;
                    Math.DivRem(i, 2, out x);

                    RowDefinition rd = new RowDefinition();
                    if (x == 0) rd.Height = new GridLength(40); else rd.Height = new GridLength(5);
                    gContent.RowDefinitions.Add(rd);
                }

                int curGridLine = 0;
                for (int i = 0; i < CountProjects; i++)
                {
                    TextBlock tbl = new TextBlock();
                    tbl.Text = arrProjects[i].name_project;
                    tbl.TextWrapping = TextWrapping.Wrap;
                    tbl.Tag = "Left";
                    tbl.TextAlignment = TextAlignment.Right;
                    Grid.SetRow(tbl, curGridLine);
                    Grid.SetColumn(tbl, 0);
                    gContent.Children.Add(tbl);

                    ComboBox cmb = new ComboBox();
                    cmb.ItemsSource = lM;
                    cmb.DisplayMemberPath = "fuzzyvalue";
                    cmb.Height = 30;
                    cmb.SelectionChanged += Cmb_SelectionChanged2;
                    cmb.VerticalAlignment = VerticalAlignment.Top;
                    Grid.SetRow(cmb, curGridLine);
                    Grid.SetColumn(cmb, 2);
                    gContent.Children.Add(cmb);

                    TextBlock tbl2 = new TextBlock();
                    tbl2.Text = "Числовое значение: ";
                    tbl2.TextWrapping = TextWrapping.Wrap;
                    tbl2.Tag = "Right";
                    Grid.SetRow(tbl2, curGridLine);
                    Grid.SetColumn(tbl2, 4);
                    gContent.Children.Add(tbl2);

                    curGridLine = curGridLine + 2;
                }
            }
        }
        void ShowFinishInfo()
        {
            tblCritName.Visibility = Visibility.Hidden;
            btnNext.Visibility = Visibility.Hidden;
            gContent.Children.Clear();
            gContent.RowDefinitions.Clear();
            gContent.ColumnDefinitions.Clear();

            TextBlock tbl = new TextBlock();
            tbl.TextWrapping = TextWrapping.Wrap;
            tbl.HorizontalAlignment = HorizontalAlignment.Center;
            tbl.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(tbl, 0);
            Grid.SetColumn(tbl, 0);
            tbl.FontSize = 20;
            tbl.TextAlignment = TextAlignment.Justify;
            tbl.Text = " Оценивание завершено.\r\n Сохранить выставленные оценки?";
            gContent.Children.Add(tbl);

            btnOK.IsEnabled = true;
        }

        private void Cmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<ComboBox> lcmb = new List<ComboBox>();
            foreach (UIElement pUI in gContent.Children)
            {
                if (pUI is ComboBox) lcmb.Add((ComboBox)pUI);
            }
            foreach(ComboBox pC in lcmb)
            {
                try
                {
                    MarkForExaminationProject SE = (MarkForExaminationProject)pC.SelectedItem;
                    int o = SE.numbervalue - 1;
                    btnNext.IsEnabled = true;
                }
                catch(NullReferenceException)
                {
                    btnNext.IsEnabled = false;
                }
            }
        }
        private void Cmb_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            List<ComboBox> lcmb = new List<ComboBox>();
            List<TextBlock> ltmptbl = new List<TextBlock>();
            List<TextBlock> ltbl = new List<TextBlock>();
            foreach (UIElement pUI in gContent.Children)
            {
                if (pUI is ComboBox)
                {
                    lcmb.Add((ComboBox)pUI);
                    continue;
                }
                if (pUI is TextBlock)
                {
                    ltmptbl.Add((TextBlock)pUI);
                    continue;
                }
            }
            foreach (TextBlock pt in ltmptbl)
            {
                if (pt.Tag.ToString() == "Right")
                {
                    ltbl.Add(pt);
                }
            }

            for (int i = 0; i < CountProjects; i++)
            {
                MarkForExaminationProject CE = (MarkForExaminationProject)lcmb[i].SelectedItem;
                try
                {
                    ltbl[i].Text = string.Format("Числовое значение: {0}", CE.numbervalue.ToString());
                    btnNext.IsEnabled = true;
                }
                catch
                {
                    ltbl[i].Text = "Числовое значение: ";
                    btnNext.IsEnabled = false;
                }
                
            }         
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            List<ComboBox> lcmb = new List<ComboBox>();
            foreach (UIElement pUI in gContent.Children)
            {
                if (pUI is ComboBox) lcmb.Add((ComboBox)pUI);
            }

            for (int i = 0; i < CountProjects; i++)
            {
                MarkForExaminationProject SE = (MarkForExaminationProject)lcmb[i].SelectedItem;
                ServiceReference1.Marks M = new ServiceReference1.Marks();
                M.id_expert = id_expert;
                M.id_crit = arrCriterions[CurrentCriterionsIndex].id_crit;
                M.id_project = arrProjects[i].id_project;
                M.rating = SE.numbervalue-1;
                lMarks.Add(M);
            }

            if (CurrentCriterionsIndex + 1 <= CountCriterions-1)
            {
                CurrentCriterionsIndex = CurrentCriterionsIndex + 1;
                CreateExaminationGrid(CurrentCriterionsIndex);
            }
            else
            {
                ShowFinishInfo();
            }
        }
        //==============================================================================================
        private void Client_AddNewMarkCompleted(object sender, ServiceReference1.AddNewMarkCompletedEventArgs e)
        {
            if (e.Error == null && e.Result)
            {
                client.EditExpertiseStatusToStart(id_expertise);
                DialogResult = true;
                MessageBox.Show("Матрица сравнения критериев сохранена.");
            }
            else
            {
                Waiting(false);
                MessageBox.Show("Ошибка сохранения");
            }
        }
        void SaveMarksInDataBase()
        {
            Waiting(true);
            client.AddNewMarkAsync(id_expertise, lMarks.ToArray());
        }
        //==============================================================================================
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            SaveMarksInDataBase();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        
    }
}
