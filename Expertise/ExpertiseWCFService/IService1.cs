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
        List<GRNTI> GetListGRNTI();

        [OperationContract]
        List<Experts> GetListExperts();

        [OperationContract]
        List<ExpertsWithCountExpertise> GetListExpertsWithCountExpertise();

        #endregion


        [OperationContract]
        string Gethello();
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Добавьте здесь операции служб
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
        public string contacts_expert { get; set; }
        public int countexpertise { get; set; }
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
