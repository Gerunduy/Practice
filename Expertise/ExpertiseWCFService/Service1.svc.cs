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

    
        public List<ExpertsWithCountExpertise> GetListExpertsWithCountExpertise()
        {
            List<ExpertsWithCountExpertise> result = new List<ExpertsWithCountExpertise>();
            List<Experts> tmplE = db_AAZ.Experts.ToList();
            int t = 0;
            foreach (Experts pE in tmplE)
            {
                if (pE.delete_expert == false)
                {
                    ExpertsWithCountExpertise tmpE = new ExpertsWithCountExpertise();
                    tmpE.id_expert = pE.id_expert;
                    tmpE.surname_expert = pE.surname_expert;
                    tmpE.name_expert = pE.name_expert;
                    tmpE.patronymic_expert = pE.patronymic_expert;
                    tmpE.FIO = pE.surname_expert + " " + pE.name_expert + " " + pE.patronymic_expert;
                    tmpE.job_expert = pE.job_expert;
                    tmpE.post_expert = pE.post_expert;
                    tmpE.degree_expert = pE.degree_expert;
                    tmpE.degree_rank_expert = pE.rank_expert + "," + pE.degree_expert;
                    tmpE.rank_expert = pE.rank_expert;
                    tmpE.contacts_expert = pE.contacts_expert;
                    tmpE.ListFOS = new List<FiledsOfScience>();
                    List<ExpertFos> expfos = db_AAZ.ExpertFos.Where(o => o.id_expert == pE.id_expert).ToList();
                    List<FiledsOfScience> Listfos = db_AAZ.FiledsOfScience.ToList();
                    List<FiledsOfScience> GetFOSCurrentExpert = new List<FiledsOfScience>();
                    if (expfos != null)
                    {

                        for (int i = 0; i < expfos.Count; i++)
                        {
                            FiledsOfScience fos = new FiledsOfScience();
                            fos = Listfos.Find(c => c.id_fos == expfos[i].id_fos);

                            GetFOSCurrentExpert.Add(fos);
                        }
                        Listfos.Clear();
                        for (int i = 0; i < GetFOSCurrentExpert.Count; i++)
                        {
                            FiledsOfScience temp = new FiledsOfScience();
                            temp.id_fos = GetFOSCurrentExpert[i].id_fos;
                            temp.name_fos = GetFOSCurrentExpert[i].name_fos;

                            tmpE.ListFOS.Add(temp);
                        }
                    }
                    else
                    {
                        tmpE.ListFOS.Clear();
                    }
                    tmpE.delete_expert = pE.delete_expert;
                    List<Marks> tmpMarks = db_AAZ.Marks.Where(o => o.id_expert == pE.id_expert).ToList();
                    List<int> countexpertise = new List<int>();
                    for (int i = 0; i < tmpMarks.Count; i++)
                    {
                        int id_mark = tmpMarks[i].id_mark;
                        ExpertiseMark id_expertise = db_AAZ.ExpertiseMark.FirstOrDefault(o => o.id_mark == id_mark);
                        if (countexpertise.Count == 0)
                        {
                            countexpertise.Add(id_expertise.id_expertise);
                        }
                        else
                        {
                            List<int> idtemplist = new List<int>();
                            for (int j = 0; j < countexpertise.Count; j++)
                            {
                                if (countexpertise[j] != id_expertise.id_expertise)
                                {
                                    idtemplist.Add(id_expertise.id_expertise);
                                }
                            }
                            countexpertise.AddRange(idtemplist);
                            idtemplist.Clear();
                        }
                    }
                    tmpE.countexpertise = countexpertise.Count();
                    tmpE.number = t;
                    result.Add(tmpE);
                    countexpertise.Clear();
                    t++;
                }
            }
            return result;
        }
       

        public List<FiledsOfScience> GetListFOS()
        {
            List<FiledsOfScience> result = new List<FiledsOfScience>();
            List<FiledsOfScience> tmplE = db_AAZ.FiledsOfScience.ToList();
            foreach (FiledsOfScience pE in tmplE)
            {
                FiledsOfScience tmpE = new FiledsOfScience();
                tmpE.id_fos = pE.id_fos;
                tmpE.name_fos = pE.name_fos;
                result.Add(tmpE);
            }
            return result;
        }
        #endregion
        public List<FiledsOfScience> test()
        {
            //Experts tempexperts = new Experts();
            //tempexperts.contacts_expert = "agfag@adw.ty";
            //tempexperts.degree_expert = "2";
            //tempexperts.delete_expert = false;
            //tempexperts.job_expert = "КемГУ";
            //tempexperts.name_expert = "Алексей";
            //tempexperts.patronymic_expert = "Иванович";
            //tempexperts.post_expert = "менджер";
            //tempexperts.rank_expert = "преподавтель";
            //tempexperts.surname_expert = "Иванов";

            //db_AAZ.Experts.Add(tempexperts);
            //db_AAZ.SaveChanges();

            //FiledsOfScience tempFiledsOfScience = new FiledsOfScience();
            //tempFiledsOfScience.name_fos = "МАТЕМАТИКА, ИНФОРМАТИКА И НАУКИ О СИСТЕМАХ";
            //db_AAZ.FiledsOfScience.Add(tempFiledsOfScience);
            //db_AAZ.SaveChanges();
            //FiledsOfScience tempFiledsOfScience2 = new FiledsOfScience();
            //tempFiledsOfScience2.name_fos = "ФИЗИКА И НАУКИ О КОСМОСЕ";
            //db_AAZ.FiledsOfScience.Add(tempFiledsOfScience2);
            //db_AAZ.SaveChanges();

            List<FiledsOfScience> result = new List<FiledsOfScience>();
            List<FiledsOfScience> tmplE = db_AAZ.FiledsOfScience.ToList();
            foreach (FiledsOfScience pE in tmplE)
            {
                FiledsOfScience tmpE = new FiledsOfScience();
                tmpE.id_fos = pE.id_fos;
                tmpE.name_fos = pE.name_fos;


                result.Add(tmpE);
            }
            return result;
        }
        public void test2()
        {
            FiledsOfScience id_expertise = db_AAZ.FiledsOfScience.FirstOrDefault(o => o.name_fos == "ФИЗИКА И НАУКИ О КОСМОСЕ");
            //db_AAZ.FiledsOfScience.Remove(id_expertise);
            //db_AAZ.SaveChanges();
            Experts id_Experts = db_AAZ.Experts.FirstOrDefault(o => o.name_expert == "Алексей");
            ExpertFos tmpE = new ExpertFos();
            tmpE.id_expert = id_Experts.id_expert;
            tmpE.id_fos = id_expertise.id_fos;
            db_AAZ.ExpertFos.Add(tmpE);
            db_AAZ.SaveChanges();
        }

        public void UpdateExpertCard(int id_expert,string surname_expert, string name_expert, string patronymic_expert,
            string job_expert, string post_expert, string degree_expert, string rank_expert,
            Boolean delete_expert, string contacts_expert, int[] ListFOS)
        {
            Experts updateexpert = db_AAZ.Experts.FirstOrDefault(o => o.id_expert == id_expert);
            updateexpert.surname_expert = surname_expert;
            updateexpert.name_expert = name_expert;
            updateexpert.patronymic_expert = patronymic_expert;
            updateexpert.job_expert = job_expert;
            updateexpert.post_expert = post_expert;
            updateexpert.degree_expert = degree_expert;
            updateexpert.rank_expert = rank_expert;
            updateexpert.delete_expert = delete_expert;
            updateexpert.contacts_expert = contacts_expert;
            db_AAZ.SaveChanges();
            List<ExpertFos> Listexpertfos = db_AAZ.ExpertFos
                         .Where(c => c.id_expert == updateexpert.id_expert).ToList();
            if (Listexpertfos != null)
            {
                for (int i = 0; i < Listexpertfos.Count; i++)
                {
                    db_AAZ.ExpertFos.Remove(Listexpertfos[i]);
                }

            }
            db_AAZ.SaveChanges();
            if (ListFOS != null)
            {

                for (int i = 0; i < ListFOS.Length; i++)
                {
                    ExpertFos temp = new ExpertFos();
                    temp.id_expert = updateexpert.id_expert;
                    temp.id_fos = ListFOS[i];
                    db_AAZ.ExpertFos.Add(temp);
                    db_AAZ.SaveChanges();
                }
            }

        }

        public List<Expertise_Expert> Expertise_Expert(int id_expert)
        {
            List<Expertise_Expert> result = new List<Expertise_Expert>();
            int t = 1;
            List<Marks> tmpMarks = db_AAZ.Marks.Where(o => o.id_expert == id_expert).ToList();
            List<int> countexpertise = new List<int>();
            for (int i = 0; i < tmpMarks.Count; i++)
            {
                int id_mark = tmpMarks[i].id_mark;
                ExpertiseMark id_expertise = db_AAZ.ExpertiseMark.FirstOrDefault(o => o.id_mark == id_mark);
                if (countexpertise.Count == 0)
                {
                    countexpertise.Add(id_expertise.id_expertise);
                }
                else
                {
                    List<int> idtemplist = new List<int>();
                    for (int j = 0; j < countexpertise.Count; j++)
                    {
                        if (countexpertise[j] != id_expertise.id_expertise)
                        {
                            idtemplist.Add(id_expertise.id_expertise);
                        }
                    }
                    countexpertise.AddRange(idtemplist);
                    idtemplist.Clear();
                }
            }
            List<Expertise_Expert> listExpertise_Expert = new List<Expertise_Expert>();
            for (int i = 0; i < countexpertise.Count; i++)
            {
                int id = countexpertise[i];
                Expertises exp = db_AAZ.Expertises.FirstOrDefault(o => o.id_expertise == id);
                Expertise_Expert temp = new Expertise_Expert();
                temp.date_expertise = exp.date_expertise;
                temp.name_expertise = exp.name_expertise;
                ProjectExpertise prexp = db_AAZ.ProjectExpertise.FirstOrDefault(o => o.id_expertise == id);
                ProjectFos prfos= db_AAZ.ProjectFos.FirstOrDefault(o => o.id_project == prexp.id_project);
                FiledsOfScience fieldfos= db_AAZ.FiledsOfScience.FirstOrDefault(o => o.id_fos == prfos.id_fos);
                temp.name_fos = fieldfos.name_fos;
                List<ProjectExpertise> lisrprexp = db_AAZ.ProjectExpertise.Where(o => o.id_expertise == id).ToList();
                for (int j=0;j< lisrprexp.Count; j++)
                {
                    ProjectExpertise tempexppr = db_AAZ.ProjectExpertise.FirstOrDefault(o => o.id_project == lisrprexp[j].id_project);
                    if (tempexppr.accept == true)
                    {
                        Projects prtemp = db_AAZ.Projects.FirstOrDefault(o => o.id_project == tempexppr.id_project);
                        temp.victory_project.Add(prtemp.name_project);
                    }
                }
                temp.number = t;
                t = t + 1;
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
