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
    /// Логика взаимодействия для ExpertRoom.xaml
    /// </summary>
    public partial class ExpertRoom : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public ServiceReference1.Experts User;

        List<ServiceReference1.Expertises> lAllExpertises = new List<ServiceReference1.Expertises>();
        List<ServiceReference1.Expertises> lCurExpertises = new List<ServiceReference1.Expertises>();
        List<ServiceReference1.Expertises> lCompExpertises = new List<ServiceReference1.Expertises>();

        int SelectedCurExpertiseID;
        int SelectedCompExpertiseID;
        ExpertiseCard _ExpertiseCard;
        Examination _Examination;
        public ExpertRoom(ServiceReference1.Experts User)
        {
            InitializeComponent();
            client.GetListExpertisesForExpertCompleted += Client_GetListExpertisesForExpertCompleted;
            this.User = User;
            tblExpertFullName.Text = string.Format("Эксперт: {0} {1} {2} ", User.surname_expert, User.name_expert, User.patronymic_expert);
            client.GetListExpertisesForExpertAsync(User.id_expert);
            Waiting(true);
        }
        // ===================================================================
        private void Client_GetListExpertisesForExpertCompleted(object sender, ServiceReference1.GetListExpertisesForExpertCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result != null)
                {
                    lAllExpertises = e.Result.ToList();
                    foreach (ServiceReference1.Expertises pE in lAllExpertises)
                    {
                        if (!pE.end_expertise) lCurExpertises.Add(pE);
                        else lCompExpertises.Add(pE);
                    }
                    Waiting(false);
                    if (lCurExpertises.Count == 0 && lCompExpertises.Count == 0)
                    {
                        ShowInfo("Экспертиз не найдено", "Экспертиз не найдено");
                    }
                    else
                    {
                        if (lCurExpertises.Count == 0 && lCompExpertises.Count > 0) ShowInfo("Экспертиз не найдено", "");
                        if (lCurExpertises.Count > 0 && lCompExpertises.Count == 0) ShowInfo("", "Экспертиз не найдено");
                        if (lCurExpertises.Count > 0 && lCompExpertises.Count > 0) ShowInfo("", "");
                    }
                }
                else
                {
                    Waiting(false);
                    ShowInfo("Экспертиз не найдено", "Экспертиз не найдено");
                }
            }
            else
            {
                Waiting(false);
                ShowInfo("Ошибка!", "Ошибка!");
            }      
        }
        // ===================================================================
        private void dgCurrentExpertises_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.Expertises CurrentCurExpertise = dgCurrentExpertises.CurrentCell.Item as ServiceReference1.Expertises;
                SelectedCurExpertiseID = CurrentCurExpertise.id_expertise;
            }
            catch { }
        }
        private void dgCompletedExpertises_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.Expertises CurrentCompExpertise = dgCompletedExpertises.CurrentCell.Item as ServiceReference1.Expertises;
                SelectedCompExpertiseID = CurrentCompExpertise.id_expertise;
            }
            catch { }
        }
        // ===================================================================
        private void Waiting(bool Wait)
        {
            if (Wait)
            {
                dgCurrentExpertises.Visibility = Visibility.Hidden;
                dgCompletedExpertises.Visibility = Visibility.Hidden;
                tblWaitInfo1.Visibility = Visibility.Visible;
                tblWaitInfo2.Visibility = Visibility.Visible;
            }
            else
            {
                dgCurrentExpertises.Visibility = Visibility.Visible;
                dgCompletedExpertises.Visibility = Visibility.Visible;
                tblWaitInfo1.Visibility = Visibility.Hidden;
                tblWaitInfo2.Visibility = Visibility.Hidden;
            }
        }
        private void ShowInfo(string Info1, string Info2)
        {
            if (Info1 != "")
            {
                tblInfo1.Text = Info1;
                dgCurrentExpertises.Visibility = Visibility.Hidden;
                tblInfo1.Visibility = Visibility.Visible;
            }
            else
            {
                dgCurrentExpertises.Visibility = Visibility.Visible;
                dgCurrentExpertises.ItemsSource = lCurExpertises;
                tblInfo1.Visibility = Visibility.Hidden;
            }
            

            if (Info2 != "")
            {
                tblInfo2.Text = Info2;
                dgCompletedExpertises.Visibility = Visibility.Hidden;
                tblInfo2.Visibility = Visibility.Visible;
            }
            else
            {
                dgCompletedExpertises.Visibility = Visibility.Visible;
                dgCompletedExpertises.ItemsSource = lCompExpertises;
                tblInfo2.Visibility = Visibility.Hidden;
            }
        }
        private void btnGoToCurExpertiseCard_Click(object sender, RoutedEventArgs e)
        {
            _ExpertiseCard = new ExpertiseCard(SelectedCurExpertiseID);
            _ExpertiseCard.Owner = App.Current.MainWindow;
            _ExpertiseCard.Show();
        }
        private void btnGoToCompExpertiseCard_Click(object sender, RoutedEventArgs e)
        {
            _ExpertiseCard = new ExpertiseCard(SelectedCompExpertiseID);
            _ExpertiseCard.Owner = App.Current.MainWindow;
            _ExpertiseCard.Show();
        }

        private void btnGoToExamination_Click(object sender, RoutedEventArgs e)
        {
            _Examination = new Examination(SelectedCurExpertiseID, User.id_expert);
            _Examination.Owner = App.Current.MainWindow;
            _Examination.ShowDialog();
        }
        // ===================================================================
    }
}
