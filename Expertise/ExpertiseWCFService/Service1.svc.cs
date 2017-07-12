﻿using System;
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
            return string.Format("You entered  : {0}", value);
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

        #region Получение таблиц

        public List<Authors> GetListAuthors()
        {
            List<Authors> result = new List<Authors>();
            List<Authors> tmplA = db_AAZ.Authors.ToList();
            foreach (Authors pA in tmplA)
            {
                Authors tmpA = new Authors();
                tmpA.id_author = pA.id_author;
                tmpA.surname_author = pA.surname_author;
                tmpA.name_author = pA.name_author;
                tmpA.patronymic_author = pA.patronymic_author;

                result.Add(tmpA);
            }
            return result;
        }

        public List<CatCrit> GetListCatCrit()
        {
            List<CatCrit> result = new List<CatCrit>();
            List<CatCrit> tmplCC = db_AAZ.CatCrit.ToList();
            foreach (CatCrit pCC in tmplCC)
            {
                CatCrit tmpCC = new CatCrit();
                tmpCC.id_cat_crit = pCC.id_cat_crit;
                tmpCC.id_cat = pCC.id_cat;
                tmpCC.id_crit = pCC.id_crit;

                result.Add(tmpCC);
            }
            return result;
        }

        public List<Categories> GetListCategories()
        {
            List<Categories> result = new List<Categories>();
            List<Categories> tmplC = db_AAZ.Categories.ToList();
            foreach (Categories pC in tmplC)
            {
                Categories tmpC = new Categories();
                tmpC.id_category = pC.id_category;
                tmpC.name_category = pC.name_category;

                result.Add(tmpC);
            }
            return result;
        }

        public List<Criterions> GetListCriterions()
        {
            List<Criterions> result = new List<Criterions>();
            List<Criterions> tmplC = db_AAZ.Criterions.ToList();
            foreach (Criterions pC in tmplC)
            {
                Criterions tmpC = new Criterions();
                tmpC.id_crit = pC.id_crit;
                tmpC.name_crit = pC.name_crit;
                tmpC.qualit_crit = pC.qualit_crit;

                result.Add(tmpC);
            }
            return result;
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

        public List<Experts> GetListExperts()
        {
            List<Experts> result = new List<Experts>();
            List<Experts> tmplE = db_AAZ.Experts.ToList();
            foreach (Experts pE in tmplE)
            {
                Experts tmpE = new Experts();
                tmpE.id_expert = pE.id_expert;
                tmpE.surname_expert = pE.surname_expert;
                tmpE.name_expert = pE.name_expert;
                tmpE.patronymic_expert = pE.patronymic_expert;
                tmpE.job_expert = pE.job_expert;
                tmpE.post_expert = pE.post_expert;
                tmpE.degree_expert = pE.degree_expert;
                tmpE.rank_expert = pE.rank_expert;
                tmpE.contacts_expert = pE.contacts_expert;

                result.Add(tmpE);
            }
            return result;
        }

        #endregion



        public string Gethello()
        {
            string a;
            return a = "awda";
        }
    }
}
