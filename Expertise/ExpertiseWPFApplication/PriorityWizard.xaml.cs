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
        ServiceReference1.Criterions[] arrCriterions;
        ServiceReference1.CritCompare[,] SaatiMatrix;
        List<ServiceReference1.CritCompare> lCompare;

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
        public PriorityWizard(ServiceReference1.Criterions[] arrCriterions)
        {
            InitializeComponent();
            btnOK.IsEnabled = false;

            this.arrCriterions = arrCriterions;
            SetCompare();
            cmbMark.ItemsSource = SaatiMarks;
            ShowCompare(CurrentCompare);
        }
        //==============================================================================================
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
                SetPareCompareMatrix();
                ShowPareCompareMatrix();

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
        void SetPareCompareMatrix()
        {

        }
        void ShowPareCompareMatrix()
        {

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

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        //==============================================================================================
        //==============================================================================================
        //==============================================================================================
    }
}
