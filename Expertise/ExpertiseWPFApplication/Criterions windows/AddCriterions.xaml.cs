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
    /// Логика взаимодействия для AddCriterions.xaml
    /// </summary>
    public partial class AddCriterions : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        string name_crit;
        bool qualit_crit;
        string valid_values;
        int id_category;

        string name_category;

        List<QuantitativeRow> lQuantitativeRow;
        List<QualitativeRow> lQualitativeRow;

        public AddCriterions(int id_category, string name_category)
        {
            InitializeComponent();
            client.AddCriterionsCompleted += Client_AddCriterionsCompleted;
            this.id_category = id_category;
            this.name_category = name_category;
            tblNameCategory.Text = string.Format("Будет добавлен в категорию \"{0}\"",name_category);
            cmbIndex.SelectedIndex = 0;

            Waiting(false);
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
        private void Client_AddCriterionsCompleted(object sender, ServiceReference1.AddCriterionsCompletedEventArgs e)
        {
            if (e.Error == null && e.Result)
            {
                this.DialogResult = true;
                MessageBox.Show("Критерий добавлен");
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
            if (Check())
            {
                name_crit = tbxName.Text;
                if (cmbIndex.SelectedIndex == 0) qualit_crit = false; else qualit_crit = true;
                valid_values = GetValidValues();
                Waiting(true);
                client.AddCriterionsAsync(name_crit, qualit_crit, valid_values, id_category);
            }
            else
            {
                MessageBox.Show("Информация введена некорректно");
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }

    public class QuantitativeRow
    {
        public QuantitativeRow(double name_value, int value)
        {
            this.name_value = name_value;
            this.value = value; 
        }
        public double name_value { get; set; }
        public int value { get; set; }
    }
    public class QualitativeRow
    {
        public QualitativeRow(string name_value, int value)
        {
            this.name_value = name_value;
            this.value = value;
        }
        public string name_value { get; set; }
        public int value { get; set; }
    }
}
