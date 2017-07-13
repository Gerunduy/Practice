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


        #region Получение таблиц

        [OperationContract]
        List<Authors> GetListAuthors();

        [OperationContract]
        List<CatCrit> GetListCatCrit();

        [OperationContract]
        List<Categories> GetListCategories();

        [OperationContract]
        List<Criterions> GetListCriterions();

        [OperationContract]
        List<CritValues> GetListCritValues();

        [OperationContract]
        List<ExpCrit> GetListExpCrit();

        [OperationContract]
        List<ExpertFos> GetListExpertFos();

        [OperationContract]
        List<ExpertiseMark> GetListExpertiseMark();

        [OperationContract]
        List<Expertises> GetListExpertises();

        [OperationContract]
        List<Experts> GetListExperts();

        [OperationContract]
        List<FiledsOfScience> GetListFiledsOfScience();

        [OperationContract]
        List<GRNTI> GetListGRNTI();

        [OperationContract]
        List<Marks> GetListMarks();

        [OperationContract]
        List<ProjectAuthors> GetListProjectAuthors();

        [OperationContract]
        List<ProjectExpertise> GetListProjectExpertise();

        [OperationContract]
        List<ProjectFos> GetListProjectFos();

        [OperationContract]
        List<Projects> GetListProjects();

        #endregion

        #region Добавление записей

        [OperationContract]
        bool AddAuthors(string surname_author, string name_author, string patronymic_author);

        [OperationContract]
        bool AddCatCrit(int id_cat, int id_crit);

        [OperationContract]
        bool AddCategories(string name_category);

        [OperationContract]
        bool AddCriterions(string name_crit, bool qualit_crit);

        [OperationContract]
        bool AddCritValues(int id_crit, string valid_values);

        [OperationContract]
        bool AddExpCrit(int id_exp, int id_crit);

        [OperationContract]
        bool AddExpertFos(int id_expert, int id_fos);

        [OperationContract]
        bool AddExpertiseMark(int id_expertise, int id_mark);

        [OperationContract]
        bool AddExpertises(string name_expertise, DateTime date_expertise);

        [OperationContract]
        bool AddExperts(string surname_expert, string name_expert, string patronymic_expert, string job_expert, string post_expert, string degree_expert, string rank_expert, string contacts_expert);

        [OperationContract]
        bool AddFiledsOfScience(string name_fos);

        [OperationContract]
        bool AddGRNTI(string name_grnti);

        [OperationContract]
        bool AddMarks(int id_expert, int id_crit, int id_project, int rating);

        [OperationContract]
        bool AddProjectAuthors(int id_proj, int id_author);

        [OperationContract]
        bool AddProjectExpertise(int id_expertise, int id_project, bool accept);

        [OperationContract]
        bool AddProjectFos(int id_project, int id_fos);

        [OperationContract]
        bool AddProjects(string name_project, string lead_project, string grnti_project, DateTime begin_project, DateTime end_project, string money_project, string email_project);

        #endregion

        #region Редактирование таблиц

        [OperationContract]
        bool EditAuthors(int id_author, string surname_author, string name_author, string patronymic_author);

        [OperationContract]
        bool EditCatCrit(int id_cat_crit, int id_cat, int id_crit);

        [OperationContract]
        bool EditCategories(int id_category, string name_category);

        [OperationContract]
        bool EditCriterions(int id_crit, string name_crit, bool qualit_crit);

        [OperationContract]
        bool EditCritValues(int id_value, int id_crit, string valid_values);

        [OperationContract]
        bool EditExpCrit(int id_exp_crit, int id_exp, int id_crit);

        [OperationContract]
        bool EditExpertFos(int id_expert_fos, int id_expert, int id_fos);

        [OperationContract]
        bool EditExpertiseMark(int id_expertise_mark, int id_expertise, int id_mark);

        [OperationContract]
        bool EditExpertises(int id_expertise, string name_expertise, DateTime date_expertise);

        [OperationContract]
        bool EditExperts(int id_expert, string surname_expert, string name_expert, string patronymic_expert, string job_expert, string post_expert, string degree_expert, string rank_expert, string contacts_expert);

        [OperationContract]
        bool EditFiledsOfScience(int id_fos, string name_fos);

        [OperationContract]
        bool EditGRNTI(string code_grnti, string name_grnti);

        [OperationContract]
        bool EditMarks(int id_mark, int id_expert, int id_crit, int id_project, int rating);

        [OperationContract]
        bool EditProjectAuthors(int id_proj_author, int id_proj, int id_author);

        [OperationContract]
        bool EditProjectExpertise(int id_project_expertise, int id_expertise, int id_project, bool accept);

        [OperationContract]
        bool EditProjectFos(int id_project_fos, int id_project, int id_fos);

        [OperationContract]
        bool EditProjects(int id_project, string name_project, string lead_project, string grnti_project, DateTime begin_project, DateTime end_project, string money_project, string email_project);

        #endregion


        [OperationContract]
        string Gethello();
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Добавьте здесь операции служб
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
