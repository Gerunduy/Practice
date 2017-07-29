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
    /// Логика взаимодействия для Examination.xaml
    /// </summary>
    public partial class Examination : Window
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        ServiceReference1.myExpertiseExaminationTables Tables;
        int id_expertise;
        public Examination(int id_expertise)
        {
            InitializeComponent();

            client.GetExpertiseExaminationTablesByIDCompleted += Client_GetExpertiseExaminationTablesByIDCompleted;

            this.id_expertise = id_expertise;
            client.GetExpertiseExaminationTablesByIDAsync(id_expertise);
            Waiting(true);
        }
        //=======================================================================================
        private void Client_GetExpertiseExaminationTablesByIDCompleted(object sender, ServiceReference1.GetExpertiseExaminationTablesByIDCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Tables = new ServiceReference1.myExpertiseExaminationTables();
                Tables = e.Result;
                FirstFillFileds();

                Waiting(false);
            }
            else
            {
                Waiting(false);

            }
        }
        //=======================================================================================
        private void FirstFillFileds()
        {
            List<int[]> lLineCoords = new List<int[]>(); 

            SolidColorBrush ExpertiseNameBckgrnd = new SolidColorBrush();
            ExpertiseNameBckgrnd.Color = Color.FromRgb(243, 109, 109);
            TextBlock tblExpertiseName = new TextBlock();
            tblExpertiseName.Background = ExpertiseNameBckgrnd;
            tblExpertiseName.Text = Tables.expertise.name_expertise;
            tblExpertiseName.FontSize = 20;
            tblExpertiseName.TextAlignment = TextAlignment.Center;
            tblExpertiseName.Width = (tblExpertiseName.Text.Count() * 15) + 50;
            tblExpertiseName.SetValue(Canvas.LeftProperty, ((Math.Round(cnvsHierarchy.Width / 2, 0) )- Math.Round(tblExpertiseName.Width / 2, 0)));
            tblExpertiseName.SetValue(Canvas.TopProperty, (20.0));
            cnvsHierarchy.Children.Add(tblExpertiseName);

            int[] ExpertiseCoord = new int[2];
            ExpertiseCoord[0] = int.Parse(Math.Round(cnvsHierarchy.Width / 2, 0).ToString());
            ExpertiseCoord[1] = 45;

            List<int[]> lCritCoords = new List<int[]>();
            double Left = 20.0;
            foreach (ServiceReference1.Criterions pC in Tables.ListCriterions)
            {
                SolidColorBrush CritBckgrnd = new SolidColorBrush();
                CritBckgrnd.Color = Color.FromRgb(234, 150, 30);
                TextBlock tblCrit = new TextBlock();
                tblCrit.Background = CritBckgrnd;
                tblCrit.Text = pC.name_crit;
                tblCrit.FontSize = 14;
                tblCrit.TextAlignment = TextAlignment.Center;
                tblCrit.Width = (tblCrit.Text.Count()*10) + 50;
                tblCrit.SetValue(Canvas.LeftProperty, (Left));
                tblCrit.SetValue(Canvas.TopProperty, (120.0));
                Left = Left + tblCrit.Width + 20.0;
                cnvsHierarchy.Children.Add(tblCrit);

                int[] Coord = new int[2];
                Coord[0] = int.Parse((Left - 20 - Math.Round(tblCrit.Width / 2, 0)).ToString());
                Coord[1] = 140;
                lCritCoords.Add(Coord);

                Line MyLine = new Line();
                MyLine.X1 = ExpertiseCoord[0];// BeginPoint.X;
                MyLine.Y1 = ExpertiseCoord[1];// BeginPoint.Y;
                MyLine.X2 = Left-20-Math.Round(tblCrit.Width / 2, 0);// EndPoint.X;
                MyLine.Y2 = 120;// EndPoint.Y;
                MyLine.Stroke = System.Windows.Media.Brushes.Black;
                MyLine.StrokeThickness = 2;
                cnvsHierarchy.Children.Add(MyLine);
            }

            Left = 20.0;
            foreach (ServiceReference1.Projects pP in Tables.ListProjects)
            {
                SolidColorBrush ProjBckgrnd = new SolidColorBrush();
                ProjBckgrnd.Color = Color.FromRgb(109, 139, 243);
                TextBlock tblProj = new TextBlock();
                tblProj.Background = ProjBckgrnd;
                tblProj.Text = pP.name_project;
                tblProj.FontSize = 16;
                tblProj.TextAlignment = TextAlignment.Center;
                tblProj.Width = (tblProj.Text.Count() * 11) + 50;
                tblProj.SetValue(Canvas.LeftProperty, (Left));
                tblProj.SetValue(Canvas.TopProperty, (220.0));
                Left = Left + tblProj.Width + 20.0;
                cnvsHierarchy.Children.Add(tblProj);

                foreach (int[] pC in lCritCoords)
                {
                    Line MyLine = new Line();
                    MyLine.X1 = pC[0];// BeginPoint.X;
                    MyLine.Y1 = pC[1];// BeginPoint.Y;
                    MyLine.X2 = Left - 20 - Math.Round(tblProj.Width / 2, 0);// EndPoint.X;
                    MyLine.Y2 = 220;// EndPoint.Y;
                    MyLine.Stroke = System.Windows.Media.Brushes.Black;
                    MyLine.StrokeThickness = 2;
                    cnvsHierarchy.Children.Add(MyLine);
                }
            }


        }
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
        //=======================================================================================
    }
}
