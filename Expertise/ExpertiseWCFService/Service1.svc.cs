using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ExpertiseWCFService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        public db_AAZEntities db_AAZ = new db_AAZEntities();
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        public List<GRNTI> GetListGRNTI()
        {
            List<GRNTI> result = new List<GRNTI>();
            List<GRNTI> lv = db_AAZ.GRNTI.ToList();
            for (int i = 0; i < lv.Count; i++)
            {
                GRNTI temp = new GRNTI();
                    temp.code_grnti = lv[i].code_grnti;
                    temp.name_grnti = lv[i].name_grnti;
                   
                    result.Add(temp);
                

            }
            return result;
        }
        public string Gethello()
        {
            string a;
            return a = "awda";
        }
    }
}
