﻿using System;
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

        List<ServiceReference1.CategoryForCritDirectory> lCategory;
        ServiceReference1.CategoryForCritDirectory CurrentCategory;
        ServiceReference1.Criterions CurrentCriterions;

        AddCategory _AddCategory;
        AddCriterions _AddCriterions;
        CriterionsCard _CriterionsCard;
        
        public Criterions()
        {
            InitializeComponent();
            //client.GetListCategoriesCompleted += Client_GetListCategoriesCompleted;
            //client.GetListCriterionsCompleted += Client_GetListCriterionsCompleted;
            //client.GetListCritValuesCompleted += Client_GetListCritValuesCompleted;
            //client.GetListCategoriesAsync();

            client.GetListCategoryForCritDirectoryCompleted += Client_GetListCategoryForCritDirectoryCompleted;

            Waiting(true);
            client.GetListCategoryForCritDirectoryAsync();
        }
        // ======================================================================================================================
        private void Client_GetListCritValuesCompleted(object sender, ServiceReference1.GetListCritValuesCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                _AddCriterions_categories.button1.Content = "Изменить";
                _AddCriterions_categories.id_value = e.Result.ToList()[0].id_value;
                if (_AddCriterions_categories.comboBox.SelectedIndex == 0)//количественный
                {
                       string s = e.Result.ToList()[0].valid_values;
                        String[] words = s.Split(new char[] { ';' });
                    //MessageBox.Show(words.Length.ToString());
                    _AddCriterions_categories.textBox2.Text = words[0];
                    _AddCriterions_categories.textBox3.Text = words[1];
                    _AddCriterions_categories.textBox4.Text = words[2];
                    _AddCriterions_categories.ShowDialog();
                }
                else if (_AddCriterions_categories.comboBox.SelectedIndex == 1)//качественный
                {
                    string s = e.Result.ToList()[0].valid_values;
                    String[] words = s.Split(new char[] { ';' });
                    for (int i = 0; i < words.Length - 1; i++)
                    {
                        AddCriterions_categories.term temp = new AddCriterions_categories.term();
                        temp.name_term = words[i];
                        temp.value_term = i + 1;
                        _AddCriterions_categories.ListTerm.Add(temp);
                    }
                    _AddCriterions_categories.dataGrid.ItemsSource = _AddCriterions_categories.ListTerm;
                    _AddCriterions_categories.ShowDialog();
                }


            }
            else
                MessageBox.Show(e.Error.Message);
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
        // ======================================================================================================================
        private void Client_GetListCategoryForCritDirectoryCompleted(object sender, ServiceReference1.GetListCategoryForCritDirectoryCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                lCategory = e.Result.ToList();
                dataGrid.ItemsSource = lCategory;

                Waiting(false);
            }
            else
            {
                MessageBox.Show("Ошибка получения данных");
                this.Close();                
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
        // ======================================================================================================================
        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //ServiceReference1.Categories temp = dataGrid.SelectedItem as ServiceReference1.Categories;
            //if(temp != null)
            //{
            //    client.GetListCriterionsAsync(temp.id_category);
            //}
            //MessageBox.Show(temp.id_category.ToString());

            CurrentCategory = dataGrid.SelectedItem as ServiceReference1.CategoryForCritDirectory;
            if (CurrentCategory != null)
            {
                dataGrid1.ItemsSource = null;
                dataGrid1.ItemsSource = CurrentCategory.ListCriterions;
            }
        }
        
        private void bt_criterion_Click(object sender, RoutedEventArgs e) //карточка критерия
        {
            //ServiceReference1.Categories temp = dataGrid.SelectedItem as ServiceReference1.Categories;
            //ServiceReference1.Criterions temp2 = dataGrid1.SelectedItem as ServiceReference1.Criterions;
            //_AddCriterions_categories = new AddCriterions_categories();
            //_AddCriterions_categories.Owner = this;
            //_AddCriterions_categories.tabControl.SelectedIndex = 1;
            //_AddCriterions_categories.textBox1.Text = temp2.name_crit;

            //if (temp2.qualit_crit == true)//количественный
            //{

            //    _AddCriterions_categories.comboBox.SelectedIndex = 0;
            //}
            //else if(temp2.qualit_crit == false)
            //{

            //    _AddCriterions_categories.comboBox.SelectedIndex = 1;
            //}
            //_AddCriterions_categories.id_cat = temp.id_category;
            //_AddCriterions_categories.id_crit = temp2.id_crit;

            //client.GetListCritValuesAsync(temp2.id_crit);

            CurrentCriterions = dataGrid1.SelectedItem as ServiceReference1.Criterions;
            _CriterionsCard = new CriterionsCard(CurrentCriterions.id_crit);
            _CriterionsCard.Owner = this;
            _CriterionsCard.Show();

        }
        private void button_Click(object sender, RoutedEventArgs e) //добавить категорию
        {
            //_AddCriterions_categories = new AddCriterions_categories();
            //_AddCriterions_categories.Owner = this;
            //_AddCriterions_categories.tabControl.SelectedIndex = 0;
            //if (_AddCriterions_categories.ShowDialog() == true)
            //{
            //    client.GetListCategoriesAsync();
            //}
            //else
            //{
            //    client.GetListCategoriesAsync();
            //}
            _AddCategory = new AddCategory();
            _AddCategory.Owner = this;
            if (_AddCategory.ShowDialog() == true)
            {
                Waiting(true);
                dataGrid.ItemsSource = null;
                client.GetListCategoryForCritDirectoryAsync();
            }
            else
            {
                //Waiting(true);
                //dataGrid.ItemsSource = null;
                //client.GetListCategoriesAsync();
            }
        }
        private void button1_Click_1(object sender, RoutedEventArgs e) //добавить критерий
        {
            //ServiceReference1.Categories temp = dataGrid.SelectedItem as ServiceReference1.Categories;
            //_AddCriterions_categories = new AddCriterions_categories();
            //_AddCriterions_categories.Owner = this;
            //_AddCriterions_categories.tabControl.SelectedIndex = 1;

            //if (temp == null)
            //{
            //    MessageBox.Show("сначала выберите категорию");
            //}
            //else
            //{
            //    _AddCriterions_categories.id_cat = temp.id_category;
            //    if (_AddCriterions_categories.ShowDialog() == true)
            //    {
            //        client.GetListCriterionsAsync(temp.id_category);
            //    }

            //}

            if (CurrentCategory != null)
            {
                _AddCriterions = new AddCriterions(CurrentCategory.Category.id_category, CurrentCategory.Category.name_category);
                _AddCriterions.Owner = this;
                if (_AddCriterions.ShowDialog() == true)
                {
                    Waiting(true);
                    dataGrid1.ItemsSource = null;
                    client.GetListCategoryForCritDirectoryAsync();
                }
            }
            else
            {
                MessageBox.Show("Сначала выберите категорию");
            }
        }
        // ======================================================================================================================
    }
}
