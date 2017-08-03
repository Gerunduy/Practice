using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ExpertiseWCFService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        myExpertiseExaminationTables GetExpertiseExaminationTablesByID(int id_expertise, int id_expert);

        [OperationContract]
        bool AddNewCritCompare(CritCompare[] arrCompare);

        [OperationContract]
        bool AddNewMark(int id_expertise, Marks[] arrMarks);

        [OperationContract]
        bool EditExpertiseExpertStatus(int id_expertise, int id_expert);
        [OperationContract]
        bool EditExpertiseStatusToComplete(int id_expertise);
        [OperationContract]
        bool EditExpertiseStatusToStart(int id_expertise);

        #region Получение таблиц

        [OperationContract]
        List<myAuthors> GetListAuthors();

        [OperationContract]
        List<myAuthors> GetListAuthorsForProject(int id_project);

        [OperationContract]
        List<Experts> GetListExpertForProject(int id_project);

        [OperationContract]
        List<CatCrit> GetListCatCrit();

        [OperationContract]
        List<Categories> GetListCategories();

        [OperationContract]
        List<Criterions> GetListCriterions(int id_category);

        [OperationContract]
        List<CritValues> GetListCritValues(int id_crit);

        [OperationContract]
        List<GRNTI> GetListGRNTI();

        [OperationContract]
        List<Experts> GetListExperts();

        [OperationContract]
        List<ExpertsWithCountExpertise> GetListExpertsWithCountExpertise();

        [OperationContract]
        List<FiledsOfScience> GetListFOS();

        [OperationContract]
        List<myProject> GetListProjects();

        [OperationContract]
        List<ProjectFos> GetListProjectFos();

        [OperationContract]
        List<Expertises> GetListExpertisesForExpert(int id_expert);

        //[OperationContract]
        //List<myCurrentexpertises> GetListCurrentExpertises();

        #endregion

        #region Получение полных таблиц
        [OperationContract]
        List<Authors> GetListAllAuthors();

        [OperationContract]
        List<CatCrit> GetListAllCatCrit();

        [OperationContract]
        List<Categories> GetListAllCategories();

        [OperationContract]
        List<Criterions> GetListAllCriterions();

        [OperationContract]
        List<CritValues> GetListAllCritValues();

        [OperationContract]
        List<ExpCrit> GetListAllExpCrit();

        [OperationContract]
        List<ExpertFos> GetListAllExpertFos();

        [OperationContract]
        List<ExpertiseExpert> GetListAllExpertiseExpert();

        [OperationContract]
        List<ExpertiseMark> GetListAllExpertiseMark();

        [OperationContract]
        List<Expertises> GetListAllExpertises();

        [OperationContract]
        List<Experts> GetListAllExperts();

        [OperationContract]
        List<FiledsOfScience> GetListAllFiledsOfScience();

        [OperationContract]
        List<GRNTI> GetListAllGRNTI();

        [OperationContract]
        List<Marks> GetListAllMarks();

        [OperationContract]
        List<ProjectAuthors> GetListAllProjectAuthors();

        [OperationContract]
        List<ProjectExpertise> GetListAllProjectExpertise();

        [OperationContract]
        List<ProjectFos> GetListAllProjectFos();

        [OperationContract]
        List<Projects> GetListAllProjects();

        #endregion

        #region Добавление записей

        [OperationContract]
        bool AddNewAuthors(string surname_author, string name_author, string patronymic_author);

        [OperationContract]
        bool AddNewCatCrit(int id_cat, int id_crit);

        [OperationContract]
        bool AddNewCategories(string name_category);

        [OperationContract]
        bool AddNewCriterions(string name_crit, bool qualit_crit);

        [OperationContract]
        bool AddNewCritValues(int id_crit, string valid_values);

        [OperationContract]
        bool AddNewExpCrit(int id_exp, int id_crit);

        [OperationContract]
        bool AddNewExpertFos(int id_expert, int id_fos);

        [OperationContract]
        bool AddNewExpertiseExpert(int id_expertise, int id_expert);

        [OperationContract]
        bool AddNewExpertiseMark(int id_expertise, int id_mark);

        [OperationContract]
        bool AddNewExpertises(string name_expertise, DateTime date_expertise);

        [OperationContract]
        bool AddNewExperts(string surname_expert, string name_expert, string patronymic_expert, string job_expert, string post_expert, string degree_expert, string rank_expert, string contacts_expert);

        [OperationContract]
        bool AddNewFiledsOfScience(string name_fos);

        [OperationContract]
        bool AddNewGRNTI(string name_grnti);

        [OperationContract]
        bool AddNewMarks(int id_expert, int id_crit, int id_project, int rating);

        [OperationContract]
        bool AddNewProjectAuthors(int id_proj, int id_author);

        [OperationContract]
        bool AddNewProjectExpertise(int id_expertise, int id_project, bool accept);

        [OperationContract]
        bool AddNewProjectFos(int id_project, int id_fos);

        [OperationContract]
        bool AddNewProjects(string name_project, string lead_project, string grnti_project, DateTime begin_project, DateTime end_project, string money_project, string email_project);

        #endregion

        [OperationContract]
        bool CreateNewExpertise(string name_expertise, DateTime date_expertise, int id_fos, int count_proj_expertise, int[] projectsId, int[] critsId, int[] expertsId);

        [OperationContract]
        bool EditExpertiseByID(int id_expertise, string name_expertise, DateTime date_expertise, int id_fos, int count_proj_expertise, ProjectExpertise[] lprojects, ExpCrit[] lcrits, ExpertiseExpert[] lexperts);

        [OperationContract]
        myExpertiseForCard GetMyExpertiseForCardByID(int id_expertise);

        [OperationContract]
        List<myCompletedexpertises> GetListCompletedExpertises();

        [OperationContract]
        List<myCurrentexpertises> GetListCurrentExpertises();

        [OperationContract]
        TablesForEditExpertise GetTabelsForEditExpertiseByID(int id_expertise);

        [OperationContract]
        bool UpdateProjectExpertise(int id_project_expertise, int id_expertise, int id_project, bool accept);

        [OperationContract]
        TablesForExpertise GetTablesForExpertise();

        [OperationContract]
        List<myRaitinfExpert> GetListRaitingForExpertise(int id_project);



        [OperationContract]
        List<ProjectExpertise> test();
        [OperationContract]
        void test2();

        [OperationContract]
        void UpdateExpertCard(int id_expert, string surname_expert, string name_expert, string patronymic_expert,
            string job_expert, string post_expert, string degree_expert, string rank_expert, 
            Boolean delete_expert, string contacts_expert, int[]  ListFOS,string login_expert, string password_expert);
        [OperationContract]
        bool EditCriterions(int id_crit, string name_crit, bool qualit_crit);

        [OperationContract]
        bool EditCritValues(int id_value, int id_crit, string valid_values);

        [OperationContract]
        bool EditFiledsOfScience(int id_fos, string name_fos);

        [OperationContract]
        bool EditProject(int id_project,string name_project, string lead_project, string grnti_project, DateTime begin_project,
            DateTime end_project, string money_project, string email_project, int[] listauthor, int fos);

        [OperationContract]
        List<Expertise_Expert> Expertise_Expert(int id_expert);


        [OperationContract]
        bool AddProjects(string name_project, string lead_project, string grnti_project, DateTime begin_project,
            DateTime end_project, string money_project, string email_project, int[] listauthor, int fos);

        [OperationContract]
        bool AddAuthors(string surname_author, string name_author, string patronymic_author);

        [OperationContract]
        bool AddCategories(string name_category);

        [OperationContract]
        bool AddExpertises(string name_expertise, DateTime date_expertise, int[] arrExperts);


        [OperationContract]
        bool DeleteExpert(int id_expert);

        [OperationContract]
        bool DeleteProject(int id_project);

        [OperationContract]
        void AddExpert(string surname_expert, string name_expert, string patronymic_expert,
          string job_expert, string post_expert, string degree_expert, string rank_expert
        , string contacts_expert, int[] ListFOS, string login_expert, string password_expert, bool comission_chairman);

       

        [OperationContract]
        bool AddCriterions(string name_crit, bool qualit_crit, string valid_values,int id_category);

        [OperationContract]
        bool AddFiledsOfScience(string name_fos);

        [OperationContract]
        Experts Authorization(string Login, string Password);


        [OperationContract]
        string Gethello();
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Добавьте здесь операции служб
    }


    #region Классы для карточки экспертизы
    public class myExpertiseForCard
    {
        public int id_expertise { get; set; }
        public string name_expertise { get; set; }
        public string fos_expertise { get; set; }
        public string status { get; set; }
        public System.DateTime date_expertise { get; set; }
        public System.DateTime end_date_expertise { get; set; }
        public List<myProjectForExpertiseCard> ListProjects { get; set; }
        public List<Criterions> ListCriterions { get; set; }
        public List<CatCrit> ListCatCrit { get; set; }
        public List<Categories> ListCategories { get; set; }
        public List<Experts> ListExperts { get; set; }

        public List<Marks> ListMark { get; set; }
        public List<CritCompare> ListCritCompare { get; set; }

        public int count_project_expertise { get; set; }
        public bool MarkIsCompleted { get; set; }
        public bool begin_expertise { get; set; }
    }
    public class myProjectForExpertiseCard
    {
        public int id_project { get; set; }
        public string name_project { get; set; }
        public string lead_project { get; set; }
        public string organization { get; set; }
        public string Rating { get; set; } // возможно будет другой тип
        public string accept { get; set; }
    }
    #endregion

    #region Классы для завершенных экспертиз
    public class myCompletedexpertises
    {
        public int id_expertise { get; set; }
        public string status { get; set; }
        public string name_expertise { get; set; }
        public System.DateTime date_expertise { get; set; }
        public System.DateTime end_date_expertise { get; set; }
        public List<myCompletedexpertisesProject> ListProject { get; set; }
        public List<string> ListExperts { get; set; }
    }
    public class myCompletedexpertisesProject
    {
        public int id_project { get; set; }
        public string name_project { get; set; }
        public string lead_project { get; set; }
        public string grnti_project { get; set; }
        public System.DateTime begin_project { get; set; }
        public System.DateTime end_project { get; set; }
        public string money_project { get; set; }
        public string email_project { get; set; }
        public bool delete_project { get; set; }
        public string is_accept { get; set; }
    }
    #endregion

    #region Классы для текущих экспертиз
    public class myCurrentexpertises
    {
        public int id_expertise { get; set; }
        public string status { get; set; }
        public string name_expertise { get; set; }
        public System.DateTime date_expertise { get; set; }
        public List<myCurrentexpertisesProject> ListProject { get; set; }
        public List<string> ListExperts { get; set; }
    }
    public class myCurrentexpertisesProject
    {
        public int id_project { get; set; }
        public string name_project { get; set; }
        public string lead_project { get; set; }
        public string grnti_project { get; set; }
        public System.DateTime begin_project { get; set; }
        public System.DateTime end_project { get; set; }
        public string money_project { get; set; }
        public string email_project { get; set; }
        public bool delete_project { get; set; }
    }
    #endregion

    #region Классы для редактирования экспертиз
    public class TablesForEditExpertise
    {
        public Expertises Expertise { get; set; }
        public List<ProjectExpertise> lProjectExpertise { get; set; }
        public List<ExpCrit> lExpCrit { get; set; }
        public List<ExpertiseExpert> lExpertiseExpert { get; set; }
        // === === ===
        public List<FiledsOfScience> lFOS { get; set; }
        public List<Projects> lProjects { get; set; }
        public List<ProjectFos> lProjectFos { get; set; }
        public List<Categories> lCatigories { get; set; }
        public List<CatCrit> lCatCrit { get; set; }
        public List<Criterions> lCriterions { get; set; }
        public List<CritValues> lCritValues { get; set; }
        public List<Experts> lExperts { get; set; }
        public List<ExpertFos> lExpertFos { get; set; }
        public bool Err { get; set; }
    }
    #endregion

    #region Классы для проведения экспертизы
    public class myExpertiseExaminationTables
    {
        public Expertises expertise { get; set; }
        public Experts expert { get; set; }
        public List<Criterions> ListCriterions { get; set; }
        public List<Projects> ListProjects { get; set; }
        public List<CritCompare> ListCritCompare { get; set; }
        //public List<CritCompareCrit> ListCritCompareCrit { get; set; }
        public List<Marks> ListMark { get; set; }


        public bool Err { get; set; }
    }
    #endregion


    //public class myCurrentexpertises
    //{
    //    public int number { get; set; }
    //    public int id_expertise { get; set; }
    //    public Boolean end_expertise { get; set; }
    //    public string name_expertise { get; set; }
    //    public DateTime date_expertise { get; set; }
    //    public int count_project { get; set; }
    //    public List<string> ListExperts { get; set; }

    //}

    public class myRaitinfExpert
    {
        public string name_crit { get; set; }
        public string raiting_crit { get; set; }
    }

    public class myAuthors
    {
        public string FIO { get; set; }
        public int id_author { get; set; }
        public string surname_author { get; set; }
        public string name_author { get; set; }
        public string patronymic_author { get; set; }
    }
    public class myProject
    {
        public int number { get; set; }
        public int id_project { get; set; }
        public string name_project { get; set; }
        public string lead_project { get; set; }
        public string grnti_project { get; set; }
        public DateTime begin_project { get; set; }
        public DateTime end_project { get; set; }
        public string money_project { get; set; }
        public string email_project { get; set; }
        public string fos { get; set; }
        public Boolean expertisa { get; set; }
        public DateTime date_expertise { get; set; }
        public string name_expertise { get; set; }

    }

    public class ExpertsWithCountExpertise
    {
        public int number { get; set; }
        public int id_expert { get; set; }
        public string surname_expert { get; set; }
        public string name_expert { get; set; }
        public string patronymic_expert { get; set; }
        public string FIO { get; set; }
        public string job_expert { get; set; }//место работы
        public string post_expert { get; set; }//должность
        public string degree_expert { get; set; }//степень
        public string rank_expert { get; set; }//звание
        public string degree_rank_expert { get; set; }//звание + степень
        public Boolean delete_expert { get; set; }//удален\активен
        public string contacts_expert { get; set; }
        public string login_expert { get; set; }
        public string password_expert { get; set; }
        public int countexpertise { get; set; }
        public List<FiledsOfScience> ListFOS { get; set; }
        
    }

    public class Expertise_Expert
    {
        public int number { get; set; }
        public int id_expertise { get; set; }
        public string name_expertise { get; set; }
        public string name_fos { get; set; }
        public DateTime date_expertise { get; set; }
        public  List<string> victory_project { get; set; }
        public string status_expertise { get; set; }
    }

    // объект класса хранит в себе таблицы необходимые для отображения информации в окне создания экспертизы
    public class TablesForExpertise
    {
        public List<FiledsOfScience> lFOS { get; set; }
        public List<Projects> lProjects { get; set; }
        public List<ProjectFos> lProjectFos { get; set; }
        public List<Categories> lCatigories { get; set; }
        public List<CatCrit> lCatCrit { get; set; }
        public List<Criterions> lCriterions { get; set; }
        public List<CritValues> lCritValues { get; set; }
        public List<Experts> lExperts { get; set; }
        public List<ExpertFos> lExpertFos { get; set; }
        public bool Err { get; set; }
    }

    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
