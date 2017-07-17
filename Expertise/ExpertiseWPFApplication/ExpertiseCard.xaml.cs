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
        public ExpertiseCard()
        {
            InitializeComponent();
            List<te> nads = new List<te>();
            for(int i = 0; i < 5; i++)
            {
                te temp = new te();
                temp.name = i.ToString();
                temp.tr = false;
                nads.Add(temp);
            }
            dataGrid.ItemsSource = nads;
        }
        public class te
        {
           public string name { get; set; }
            public bool tr { get; set; }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            //for (int i = 0; i < dataGrid.Items.Count; i++)
            //{
            //    if (dataGrid.Items[i] is CheckBox)
            //    {
            //       if( ((CheckBox)dataGrid.Items[i]).IsChecked==true)
            //        {
            //            MessageBox.Show("чекнут");
            //        }
            //    }
            //}
            

        }
    }
}
