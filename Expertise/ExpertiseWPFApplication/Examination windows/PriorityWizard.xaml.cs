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
    /// Логика взаимодействия для PriorityWizard.xaml
    /// </summary>
    public partial class PriorityWizard : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        ServiceReference1.Criterions[] arrCriterions;

        int id_expertise;
        int id_expert;

        double[,] SaatiMatrix;
        List<ServiceReference1.CritCompare> lCompare;

        DataGrid dgSaatiMatrix;

        int CountCriterions;

        int CountCompare;
        int CurrentCompare;

        SaatiMark[] SaatiMarks = new SaatiMark[]
        {
            new SaatiMark(SaatiValue.not_selected),
            new SaatiMark(SaatiValue.v9_Absolutely_superior),
            new SaatiMark(SaatiValue.v8_Intermediate_value),
            new SaatiMark(SaatiValue.v7_Explicitly_exceeds),
            new SaatiMark(SaatiValue.v6_Intermediate_value),
            new SaatiMark(SaatiValue.v5_Significantly_superior),
            new SaatiMark(SaatiValue.v4_Intermediate_value),
            new SaatiMark(SaatiValue.v3_Slightly_superior),
            new SaatiMark(SaatiValue.v2_Intermediate_value),
            new SaatiMark(SaatiValue.v1_Equivalent),
            new SaatiMark(SaatiValue.v_2_Intermediate_value),
            new SaatiMark(SaatiValue.v_3_Slightly_inferior),
            new SaatiMark(SaatiValue.v_4_Intermediate_value),
            new SaatiMark(SaatiValue.v_5_Significantly_inferior),
            new SaatiMark(SaatiValue.v_6_Intermediate_value),
            new SaatiMark(SaatiValue.v_7_Obviously_inferior),
            new SaatiMark(SaatiValue.v_8_Intermediate_value),
            new SaatiMark(SaatiValue.v_9_Absolutely_inferior)
        };
        //==============================================================================================
        public PriorityWizard(int id_expertise, int id_expert, ServiceReference1.Criterions[] arrCriterions)
        {
            InitializeComponent();
            client.AddNewCritCompareCompleted += Client_AddNewCritCompareCompleted;
            btnOK.IsEnabled = false;

            this.id_expertise = id_expertise;
            this.id_expert = id_expert;
            this.arrCriterions = arrCriterions;
            CountCriterions = arrCriterions.Count();

            SetCompare();
            cmbMark.ItemsSource = SaatiMarks;
            ShowCompare(CurrentCompare);
            Waiting(false);
        }
        //==============================================================================================
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
        void SetCompare()
        {
            lCompare = new List<ServiceReference1.CritCompare>();
            for (int i = 0; i<arrCriterions.Count(); i++)
            {
                for (int j = i+1; j < arrCriterions.Count(); j++)
                {
                    ServiceReference1.CritCompare Compare = new ServiceReference1.CritCompare();

                    ServiceReference1.CritCompareCrit CompareCrit1 = new ServiceReference1.CritCompareCrit();
                    CompareCrit1.id_crit = arrCriterions[i].id_crit;

                    ServiceReference1.CritCompareCrit CompareCrit2 = new ServiceReference1.CritCompareCrit();
                    CompareCrit2.id_crit = arrCriterions[j].id_crit;

                    Compare.CritCompareCrit = new ServiceReference1.CritCompareCrit[2];
                    Compare.CritCompareCrit[0] = CompareCrit1;
                    Compare.CritCompareCrit[1] = CompareCrit2;

                    lCompare.Add(Compare);
                }
            }
            CountCompare = lCompare.Count();
            CurrentCompare = 1;
        }
        void ShowCompare(int number)
        {
            if (number<=CountCompare)
            {
                btnOK.IsEnabled = false;
                tblCrit1.Text = arrCriterions.ToList().Where(p => p.id_crit == lCompare[number-1].CritCompareCrit[0].id_crit).FirstOrDefault().name_crit;
                cmbMark.SelectedIndex = 0;
                tblCrit2.Text = arrCriterions.ToList().Where(p => p.id_crit == lCompare[number-1].CritCompareCrit[1].id_crit).FirstOrDefault().name_crit;
            }
            else
            {
                SetPairCompareMatrix();
                ShowPairCompareMatrix();

                btnNext.Visibility = Visibility.Hidden;
                btnOK.IsEnabled = true;
            }
        }
        void SaveCompare(int number)
        {
            if (number <= CountCompare)
            {
                SaatiMark CurrMark = (SaatiMark)cmbMark.SelectedItem;
                if (CurrMark.Mark != 0)
                {
                    lCompare[number-1].mark_compare = CurrMark.Mark;
                }
            }
        }
        void SetPairCompareMatrix()
        {
            int CurContextCompare = 0;
            SaatiMatrix = new double[CountCriterions,CountCriterions];
            for (int i = 0; i < CountCriterions; i++) //for (int i = 0; i < lCompare.Count(); i++)
            {
                for (int j = 0; j < CountCriterions; j++) //for (int j = 0; j < lCompare.Count(); j++)
                {
                    if (j == i) { SaatiMatrix[i, j] = 1; }
                    else
                    {
                        if (j > i)
                        {
                            SaatiMatrix[i, j] = lCompare[CurContextCompare].mark_compare;
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
                            //SaatiMatrix[i, j] = 1 / SaatiMatrix[j, i];
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
                Column.Header = arrCriterions[i].name_crit;
                Column.Binding = new Binding(string.Format("content.[{0}]",i));
                dgSaatiMatrix.Columns.Add(Column);
            }

            List<myCompareRow> lRow = new  List<myCompareRow>();
            // === Формируем строку ===
            for (int i = 0; i < CountCriterions; i++)
            {
                double[] arrMark = new double[CountCriterions];
                for (int j = 0; j < CountCriterions; j++)
                {
                    arrMark[j] = SaatiMatrix[i, j];
                }
                myCompareRow Row = new myCompareRow(arrCriterions[i].name_crit, arrMark);
                lRow.Add(Row);
            }
            // === === ===
            dgSaatiMatrix.ItemsSource = lRow;

            gContent.Children.Clear();
            gContent.RowDefinitions.Clear();
            gContent.ColumnDefinitions.Clear();
            tblTitle.Text = "  Получена матрица сравнения критериев:  ";
            tblTitle.Background = Brushes.LightGray;
            gContent.Children.Add(dgSaatiMatrix);
        }
        void SaveCompareInDataBase()
        {
            foreach (ServiceReference1.CritCompare pCC in lCompare)
            {
                pCC.id_expertise = id_expertise;
                pCC.id_expert = id_expert;
            }
            Waiting(true);
            client.AddNewCritCompareAsync(lCompare.ToArray());
        }
        private void Client_AddNewCritCompareCompleted(object sender, ServiceReference1.AddNewCritCompareCompletedEventArgs e)
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
        //==============================================================================================
        private void cmbMark_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbMark.SelectedIndex != 0) btnNext.IsEnabled = true; else btnNext.IsEnabled = false;
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            SaveCompare(CurrentCompare);
            CurrentCompare = CurrentCompare + 1;
            ShowCompare(CurrentCompare);
        }
        //==============================================================================================
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            SaveCompareInDataBase();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        //==============================================================================================
    }
}
