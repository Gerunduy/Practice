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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExpertiseWPFApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ServiceReference1.Experts User;

        GRNTI _GRNTI;
        Experts _Experts;
        ProjectCard _ProjectCard;
        ExpertCard _ExpertCard;
        Projects _Projects;
        Criterions _Criterions;
        FiledsOfScience _FiledsOfScience;
        ExpertiseCard _ExpertiseCard;
        NewExpertiseWindow _NewExpertiseWindow;
        Authorization _Authorization;
        ExpertRoom _ExpertRoom;
        CurrentExpertises _CurrentExpertises;
        CompletedExpertises _CompletedExpertises;
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public MainWindow()
        {
            InitializeComponent();
            client.GetListGRNTICompleted += Client_GetListGRNTICompleted;
            client.GethelloCompleted += Client_GethelloCompleted;
            client.GetListAuthorsCompleted += Client_GetListAuthorsCompleted;
            client.GetListFOSCompleted += Client_GetListFOSCompleted;

            client.testCompleted += Client_testCompleted;
            //client.testAsync();

            client.test2Completed += Client_test2Completed;
            //client.test2Async();

            ShowUserInfo();
        }

        private void Client_GetListFOSCompleted(object sender, ServiceReference1.GetListFOSCompletedEventArgs e)
        {
            if (e.Error == null)
            {

                _ProjectCard.comboBox1.ItemsSource = e.Result.ToList();
            }
            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_GetListAuthorsCompleted(object sender, ServiceReference1.GetListAuthorsCompletedEventArgs e)
        {
            if (e.Error == null)
            {

                _ProjectCard.comboBox.ItemsSource = e.Result.ToList();
            }
            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_test2Completed(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                
                    MessageBox.Show("gfh");
                

            }


            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_testCompleted(object sender, ServiceReference1.testCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                for(int i = 0; i < e.Result.ToList().Count; i++)
                {
                    //MessageBox.Show(e.Result[i].name_fos);
                }
                
            }


            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_GethelloCompleted(object sender, ServiceReference1.GethelloCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                
                MessageBox.Show(e.Result);
            }


            else
                MessageBox.Show(e.Error.Message);
        }

        private void Client_GetListGRNTICompleted(object sender, ServiceReference1.GetListGRNTICompletedEventArgs e)
        {
            if (e.Error == null)
            {
                _ProjectCard.comboBox2.ItemsSource = e.Result.ToList();
            }


            else
                MessageBox.Show(e.Error.Message);
        }

        private void bt_update_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("test");
            //client.GethelloAsync();
            //client.GetListGRNTIAsync();
            _GRNTI = new GRNTI();
            _GRNTI.Owner = this;
            _GRNTI.ShowDialog();
        }
        //ГРНТИ
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            _GRNTI = new GRNTI();
            _GRNTI.Owner = this;
            _GRNTI.ShowDialog();
            
        }
        //Эксперты
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            _Experts = new Experts();
            _Experts.Owner = this;
            _Experts.ShowDialog();
        }
        //новый проект
        private void button_Click(object sender, RoutedEventArgs e)
        {
            _ProjectCard = new ProjectCard();
            _ProjectCard.Owner = this;
            client.GetListAuthorsAsync();
            client.GetListFOSAsync();
            client.GetListGRNTIAsync();
            _ProjectCard.ShowDialog();
        }
        //проект
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            
            _Projects = new Projects();
            _Projects.Owner = this;
            _Projects.ShowDialog();
        }
        //новый эксперт
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _ExpertCard = new ExpertCard();
            _ExpertCard.Owner = this;
            _ExpertCard.tabControl.Visibility = Visibility.Hidden;
            _ExpertCard.comboBox.Visibility = Visibility.Visible;
            _ExpertCard.button1.Visibility = Visibility.Visible;
            _ExpertCard.button2.Visibility = Visibility.Hidden;
            _ExpertCard.button.Content = "Сохранить";
            _ExpertCard.textBox.IsReadOnly = false;
            _ExpertCard.textBox1.IsReadOnly = false;
            _ExpertCard.textBox2.IsReadOnly = false;
            _ExpertCard.textBox3.IsReadOnly = false;
            _ExpertCard.textBox4.IsReadOnly = false;
            _ExpertCard.textBox5.IsReadOnly = false;
            _ExpertCard.textBox6.IsReadOnly = false;
            _ExpertCard.textBox7.IsReadOnly = false;
            _ExpertCard.textBox8.IsReadOnly = false;
            _ExpertCard.textBox9.IsReadOnly = false;
            _ExpertCard.ShowDialog();
        }
        //критерии
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            _Criterions = new Criterions();
            _Criterions.Owner = this;
            _Criterions.ShowDialog();
        }
        //направление науки
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            _FiledsOfScience = new FiledsOfScience();
            _FiledsOfScience.Owner = this;
            _FiledsOfScience.ShowDialog();
        }
        //new expertise
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            NewExpertiseWindow _NewExpertiseWindow;

            _NewExpertiseWindow = new NewExpertiseWindow();
            _NewExpertiseWindow.Owner = this;
            _NewExpertiseWindow.ShowDialog();
            
        }

        // вход в личный кабинет
        private void button10_Click(object sender, RoutedEventArgs e)
        {
            _ExpertRoom = new ExpertRoom(User);
            _ExpertRoom.Owner = this;
            _ExpertRoom.ShowDialog(); // скорее всего здесь _ExpertRoom.Show();
        }

        // авторизация
        private void button11_Click(object sender, RoutedEventArgs e)
        {
            if (User != null)
            {
                User = null;
                ShowUserInfo();
            }
            else
            {
                _Authorization = new Authorization();
                _Authorization.Owner = this;
                _Authorization.ShowDialog();
                if (_Authorization.DialogResult == true)
                {
                    this.User = _Authorization.User;
                    ShowUserInfo();
                }
            }  
        }
        private void ShowUserInfo()
        {
            if (User != null)
            {
                string FullNameUser = string.Format("Эксперт: {0} {1} {2} ", User.surname_expert, User.name_expert, User.patronymic_expert);
                tblUserInfo.Text = FullNameUser;
                button10.IsEnabled = true;
                button11.Content = "Выход";
            }
            else
            {
                string FullNameUser = string.Format("Вход не выполнен ");
                tblUserInfo.Text = FullNameUser;
                button10.IsEnabled = false;
                button11.Content = "Вход";
            }
        }
        //текущие эксертизы
        private void button9_Click(object sender, RoutedEventArgs e)
        {
            _CurrentExpertises = new CurrentExpertises();
            _CurrentExpertises.Owner = this;
            _CurrentExpertises.ShowDialog();
        }
        // завершенные экспертизы
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            _CompletedExpertises = new CompletedExpertises();
            _CompletedExpertises.Owner = this;
            _CompletedExpertises.ShowDialog(); // скорее всего здесь _CompletedExpertises.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            foreach (Window pW in App.Current.Windows)
            {
                pW.Close();
            }
        }
    }
}
