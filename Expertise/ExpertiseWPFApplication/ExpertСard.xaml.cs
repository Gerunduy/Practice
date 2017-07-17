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
    /// Логика взаимодействия для ExpertСard.xaml
    /// </summary>
    public partial class ExpertСard : Window
    {
        
        public ExpertСard()
        {
            InitializeComponent();
        }

       

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(button.Content.ToString()== "Редактировать")
            {
                button.Content = "Сохранить";
                listBox2.Visibility = Visibility.Visible;
                listBox1.Visibility = Visibility.Hidden;
                textBox.IsReadOnly = false;
                textBox1.IsReadOnly = false;
                textBox2.IsReadOnly = false;
                textBox3.IsReadOnly = false;
                textBox4.IsReadOnly = false;
                textBox5.IsReadOnly = false;
                textBox6.IsReadOnly = false;
                textBox7.IsReadOnly = false;
            }
            else
            {
                button.Content = "Редактировать";
                listBox2.Visibility = Visibility.Hidden;
                listBox1.Visibility = Visibility.Visible;
                textBox.IsReadOnly = true;
                textBox1.IsReadOnly = true;
                textBox2.IsReadOnly = true;
                textBox3.IsReadOnly = true;
                textBox4.IsReadOnly = true;
                textBox5.IsReadOnly = true;
                textBox6.IsReadOnly = true;
                textBox7.IsReadOnly = true;
            }
            
        }
    }
}
