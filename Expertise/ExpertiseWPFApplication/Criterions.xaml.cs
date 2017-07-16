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
    /// Логика взаимодействия для Criterions.xaml
    /// </summary>
    public partial class Criterions : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        AddCriterions_categories _AddCriterions_categories;
        public Criterions()
        {
            InitializeComponent();
            client.GetListCategoriesCompleted += Client_GetListCategoriesCompleted;
            client.GetListCategoriesAsync();
            client.GetListCriterionsCompleted += Client_GetListCriterionsCompleted;
        }

        private void Client_GetListCriterionsCompleted(object sender, ServiceReference1.GetListCriterionsCompletedEventArgs e)
        {
            if (e.Error == null)
            {

                dataGrid1.ItemsSource = e.Result.ToList();
                //MessageBox.Show(e.Result.ToList()[0].name_crit);
            }
            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_GetListCategoriesCompleted(object sender, ServiceReference1.GetListCategoriesCompletedEventArgs e)
        {
            if (e.Error == null)
            {

                dataGrid.ItemsSource = e.Result.ToList();
            }
            else
                MessageBox.Show(e.Error.Message);
        }

       
       
        //карточка критерия
        private void bt_criterion_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ServiceReference1.Categories temp = dataGrid.SelectedItem as ServiceReference1.Categories;
            client.GetListCriterionsAsync(temp.id_category);
            //MessageBox.Show(temp.id_category.ToString());
        }
        //добавить категорий
        private void button_Click(object sender, RoutedEventArgs e)
        {
            _AddCriterions_categories = new AddCriterions_categories();
            _AddCriterions_categories.Owner = this;
            _AddCriterions_categories.tabControl.SelectedIndex = 0;
            if (_AddCriterions_categories.ShowDialog() == true)
            {
                client.GetListCategoriesAsync();
            }
            else
            {
                client.GetListCategoriesAsync();
            }
               
        }
        //добавить критерий
        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            ServiceReference1.Categories temp = dataGrid.SelectedItem as ServiceReference1.Categories;
            _AddCriterions_categories = new AddCriterions_categories();
            _AddCriterions_categories.Owner = this;
            _AddCriterions_categories.tabControl.SelectedIndex = 1;
            _AddCriterions_categories.id_cat = temp.id_category;


            if (temp == null)
            {
                MessageBox.Show("сначала выберите категорию");
            }
            else
            {
                if (_AddCriterions_categories.ShowDialog() == true)
                {
                    client.GetListCriterionsAsync(temp.id_category);
                }
                    
            }
            
        }
    }
}
