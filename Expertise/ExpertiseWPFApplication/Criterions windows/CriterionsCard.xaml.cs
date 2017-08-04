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
    /// Логика взаимодействия для CriterionsCard.xaml
    /// </summary>
    public partial class CriterionsCard : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        ServiceReference1.CritForCard Criterions;
        ServiceReference1.CritForCard CloneCriterions; // для отката изменений без повторного запроса в базу на получения критерия
        int id_crit;

        string name_crit;
        bool qualit_crit;
        string valid_values;
        string UOM;

        bool IsEditMode = false;

        List<QuantitativeRow> lQuantitativeRow;
        List<QualitativeRow> lQualitativeRow;
        public CriterionsCard(int id_crit)
        {
            InitializeComponent();
            this.id_crit = id_crit;
            client.GetCriterionsForCardCompleted += Client_GetCriterionsForCardCompleted;
            client.EditCriterionsCompleted += Client_EditCriterionsCompleted;

            ActivateEdit(false);
            Waiting(true);
            client.GetCriterionsForCardAsync(id_crit);
        }
        // ======================================================================================================================
        private void Client_GetCriterionsForCardCompleted(object sender, ServiceReference1.GetCriterionsForCardCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Criterions = e.Result;
                CloneCriterions = e.Result;
                FillFileds();
                Waiting(false);
            }
            else
            {
                this.Close();
                MessageBox.Show(e.Error.Message);
            }
        }
        // ======================================================================================================================
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
        private void ActivateEdit(bool Active)
        {
            if (Active)
            {
                tbxName.IsEnabled = true;
                cmbIndex.IsEnabled = true;
                dgValues.IsReadOnly = false;
                gUOM.IsEnabled = true;
                btnCancel.Visibility = Visibility.Visible;
                btnOk.Content = "Сохранить";
            }
            else
            {
                tbxName.IsEnabled = false;
                cmbIndex.IsEnabled = false;
                dgValues.IsReadOnly = true;
                gUOM.IsEnabled = false;
                btnCancel.Visibility = Visibility.Hidden;
                btnOk.Content = "Редактировать";
            }
        }
        private void FillFileds()
        {
            tbxName.Text = Criterions.Criterions.name_crit;
            if (Criterions.Criterions.qualit_crit)
            {
                cmbIndex.SelectedIndex = 1;
                ParseCritValue();
                dgValues.ItemsSource = null;
                dgValues.ItemsSource = lQualitativeRow;
            }
            else
            {
                cmbIndex.SelectedIndex = 0;
                ParseCritValue();
                tbxUOM.Text = UOM;
                dgValues.ItemsSource = null;
                dgValues.ItemsSource = lQuantitativeRow;
                
            }
            tblNameCategory.Text = string.Format("Критерий относится к категории \"{0}\"", Criterions.name_category);
        }
        private void ParseCritValue()
        {
            if (Criterions.Criterions.qualit_crit)
            {
                lQualitativeRow = new List<QualitativeRow>();

                string[] ltmpValidValues = Criterions.CritValue.valid_values.Split(new char[] { ';' });
                for (int i = 0; i < 5; i++)
                {
                    QualitativeRow Row = new QualitativeRow(ltmpValidValues[i], i + 1);
                    lQualitativeRow.Add(Row);
                }
            }
            else
            {
                lQuantitativeRow = new List<QuantitativeRow>();

                string[] ltmpValidValues = Criterions.CritValue.valid_values.Split(new char[] { ';' });
                for (int i = 0; i < 5; i++)
                {
                    QuantitativeRow Row = new QuantitativeRow(double.Parse(ltmpValidValues[i]), i + 1);
                    lQuantitativeRow.Add(Row);
                }
                UOM = ltmpValidValues[5];
            }
        }

        void ShowQuantitativeContent()
        {
            dgValues.Columns.Clear();
            tbxUOM.Text = "";
            gUOM.Visibility = Visibility.Visible;

            DataGridTextColumn Col1 = new DataGridTextColumn();
            Col1.Header = "Значение";
            Col1.Width = new DataGridLength(195);
            Col1.Binding = new Binding("name_value");
            dgValues.Columns.Add(Col1);

            DataGridTextColumn Col2 = new DataGridTextColumn();
            Col2.Header = "Численное выражение";
            Col1.Width = new DataGridLength(180);
            Col2.IsReadOnly = true;
            Col2.Binding = new Binding("value");
            dgValues.Columns.Add(Col2);

            lQuantitativeRow = new List<QuantitativeRow>();
            for (int i = 1; i < 6; i++)
            {
                QuantitativeRow Row = new QuantitativeRow(i, i);
                lQuantitativeRow.Add(Row);
            }

            dgValues.ItemsSource = null;
            dgValues.ItemsSource = lQuantitativeRow;
        }
        void ShowQualitativeContent()
        {
            dgValues.Columns.Clear();
            tbxUOM.Text = "";
            gUOM.Visibility = Visibility.Hidden;

            DataGridTextColumn Col1 = new DataGridTextColumn();
            Col1.Header = "Название терма";
            Col1.Width = new DataGridLength(195);
            Col1.Binding = new Binding("name_value");
            dgValues.Columns.Add(Col1);

            DataGridTextColumn Col2 = new DataGridTextColumn();
            Col2.Header = "Численное выражение";
            Col1.Width = new DataGridLength(180);
            Col2.IsReadOnly = true;
            Col2.Binding = new Binding("value");
            dgValues.Columns.Add(Col2);

            lQualitativeRow = new List<QualitativeRow>();
            for (int i = 1; i < 6; i++)
            {
                QualitativeRow Row = new QualitativeRow("", i);
                lQualitativeRow.Add(Row);
            }

            dgValues.ItemsSource = null;
            dgValues.ItemsSource = lQualitativeRow;
        }
        string GetValidValues()
        {
            if (cmbIndex.SelectedIndex == 0)
            {
                return string.Format("{0};{1};{2};{3};{4};{5};", lQuantitativeRow[0].name_value, lQuantitativeRow[1].name_value, lQuantitativeRow[2].name_value, lQuantitativeRow[3].name_value, lQuantitativeRow[4].name_value, tbxUOM.Text);
            }
            else
            {
                return string.Format("{0};{1};{2};{3};{4};", lQualitativeRow[0].name_value, lQualitativeRow[1].name_value, lQualitativeRow[2].name_value, lQualitativeRow[3].name_value, lQualitativeRow[4].name_value);
            }
        }

        bool CheckDataGrid()
        {
            if (cmbIndex.SelectedIndex == 0)
            {
                foreach (QuantitativeRow pR in lQuantitativeRow)
                {
                    double d;
                    if (double.TryParse(pR.name_value.ToString(), out d)) continue;
                    else return false;
                }
                return true;
            }
            else
            {
                foreach (QualitativeRow pR in lQualitativeRow)
                {
                    if (pR.name_value.Trim(' ') != "") continue;
                    else return false;
                }
                return true;
            }
        }
        bool Check()
        {
            if (cmbIndex.SelectedIndex == 0)
            {
                if (tbxName.Text.Trim(' ') != "" && tbxUOM.Text.Trim(' ') != "" && CheckDataGrid()) return true;
                else return false;
            }
            else
            {
                if (tbxName.Text.Trim(' ') != "" && CheckDataGrid()) return true;
                else return false;
            }
        }
        // ======================================================================================================================
        private void cmbIndex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbIndex.SelectedIndex == 0) ShowQuantitativeContent();
            if (cmbIndex.SelectedIndex == 1) ShowQualitativeContent();
        }
        // ======================================================================================================================
        private void Client_EditCriterionsCompleted(object sender, ServiceReference1.EditCriterionsCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                switch (e.Result)
                {
                    case ServiceReference1.EditCriterionsMessage.Succes:
                        Waiting(false);
                        IsEditMode = false;
                        ActivateEdit(false);
                        MessageBox.Show("Изменения внесены.");
                        IsEditMode = false;
                        break;
                    case ServiceReference1.EditCriterionsMessage.ErrParticipant:
                        Waiting(false);
                        MessageBox.Show("Нельзя редактировать, т.к. есть незавершенные экспертизы!");
                        break;
                    case ServiceReference1.EditCriterionsMessage.ErrQualit:
                        Waiting(false);
                        MessageBox.Show("Нельзя менять природу критерия т.к он учавствует или учавствовал в экспертизах!");
                        break;
                    case ServiceReference1.EditCriterionsMessage.ErrDataBase:
                        Waiting(false);
                        MessageBox.Show("Ошибка работы с базой во время редактирования!");
                        break;
                }
            }
            else
            {
                Waiting(false);
                MessageBox.Show(e.Error.Message);
            }
        }
        // ======================================================================================================================
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (!IsEditMode)
            {
                ActivateEdit(true);
                IsEditMode = true;
            }
            else
            {
                if (Check())
                {
                    name_crit = tbxName.Text;
                    if (cmbIndex.SelectedIndex == 0) qualit_crit = false; else qualit_crit = true;
                    valid_values = GetValidValues();
                    tblWait.Text = "Редактирование...";
                    Waiting(true);
                    client.EditCriterionsAsync(id_crit, name_crit, qualit_crit, valid_values);
                }
                else
                {
                    MessageBox.Show("Информация введена некорректно");
                }
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            IsEditMode = false;
            Criterions = CloneCriterions;
            FillFileds();
            ActivateEdit(false);
        }
    }
}
