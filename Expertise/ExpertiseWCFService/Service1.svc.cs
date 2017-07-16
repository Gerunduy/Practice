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

        public List<myAuthors> GetListAuthors()
        {
            try
            {
                List<myAuthors> result = new List<myAuthors>();
                List<Authors> tmplA = db_AAZ.Authors.ToList();
                foreach (Authors pA in tmplA)
                {
                    myAuthors tmpA = new myAuthors();
                    tmpA.id_author = pA.id_author;
                    tmpA.surname_author = pA.surname_author;
                    tmpA.name_author = pA.name_author;
                    tmpA.patronymic_author = pA.patronymic_author;
                    tmpA.FIO = pA.surname_author + " " + pA.name_author + " " + pA.patronymic_author;

                    result.Add(tmpA);
                }
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<myAuthors> result = new List<myAuthors>();
                myAuthors tmpA = new myAuthors();
                tmpA.id_author = -1;
                tmpA.surname_author = "Содержимое таблицы не получено";
                result.Add(tmpA);

                return result;
            }
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
            try
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
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<Categories> result = new List<Categories>();
                Categories tmpC = new Categories();
                tmpC.id_category = -1;
                result.Add(tmpC);

                return result;
            }
        }
        public List<Criterions> GetListCriterions(int id_category)
        {
            List<Criterions> result = new List<Criterions>();
            List<CatCrit> tmplC = db_AAZ.CatCrit.Where(o=>o.id_cat== id_category).ToList();
            foreach (CatCrit pC in tmplC)
            {
                Criterions tmpC = db_AAZ.Criterions.FirstOrDefault(o=>o.id_crit==pC.id_crit);
                Criterions tmp = new Criterions();

                tmp.id_crit = tmpC.id_crit;
                tmp.name_crit = tmpC.name_crit;
                tmp.qualit_crit = tmpC.qualit_crit;

                result.Add(tmp);
            }
            return result;
        }

        public List<GRNTI> GetListGRNTI()
        {
            try
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
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<GRNTI> result = new List<GRNTI>();
                GRNTI temp = new GRNTI();
                temp.code_grnti = "-1";
                result.Add(temp);

                return result;
            }
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
            try
            {
                List<FiledsOfScience> result = new List<FiledsOfScience>();
                List<FiledsOfScience> tmplFOS = db_AAZ.FiledsOfScience.ToList();
                foreach (FiledsOfScience pFOS in tmplFOS)
                {
                    FiledsOfScience tmpFOS = new FiledsOfScience();
                    tmpFOS.id_fos = pFOS.id_fos;
                    tmpFOS.name_fos = pFOS.name_fos;

                    result.Add(tmpFOS);
                }
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<FiledsOfScience> result = new List<FiledsOfScience>();
                FiledsOfScience tmpFOS = new FiledsOfScience();
                tmpFOS.id_fos = -1;
                result.Add(tmpFOS);

                return result;
            }
        }

        public List<myProject> GetListProjects()
        {
            try
            {
                int t = 1;
                List<myProject> result = new List<myProject>();
                List<Projects> tmplP = db_AAZ.Projects.ToList();
                foreach (Projects pP in tmplP)
                {
                    myProject tmpP = new myProject();
                    tmpP.id_project = pP.id_project;
                    tmpP.name_project = pP.name_project;
                    tmpP.lead_project = pP.lead_project;
                    tmpP.grnti_project = pP.grnti_project;
                    tmpP.begin_project = pP.begin_project;
                    tmpP.number = t;
                    tmpP.end_project = pP.end_project;
                    tmpP.money_project = pP.money_project;
                    tmpP.email_project = pP.email_project;
                    ProjectExpertise prexp = db_AAZ.ProjectExpertise.FirstOrDefault(o => o.id_project == pP.id_project);
                    if (prexp != null)
                    {
                        tmpP.expertisa = true;
                    }
                    else tmpP.expertisa = false;
                    ProjectFos prfos = db_AAZ.ProjectFos.FirstOrDefault(o=>o.id_project == pP.id_project);
                    if (prfos != null)
                    {
                        FiledsOfScience fos= db_AAZ.FiledsOfScience.FirstOrDefault(o => o.id_fos == prfos.id_fos);
                        tmpP.fos = fos.name_fos;
                    }
                    t = t + 1;
                    result.Add(tmpP);
                }
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<myProject> result = new List<myProject>();
                myProject tmpP = new myProject();
                tmpP.id_project = -1;
                result.Add(tmpP);

                return result;
            }
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

        public void AddExpert(string surname_expert, string name_expert, string patronymic_expert,
           string job_expert, string post_expert, string degree_expert, string rank_expert
         , string contacts_expert, int[] ListFOS)
        {
            Experts expert =new Experts();
            expert.surname_expert = surname_expert;
            expert.name_expert = name_expert;
            expert.patronymic_expert = patronymic_expert;
            expert.job_expert = job_expert;
            expert.post_expert = post_expert;
            expert.degree_expert = degree_expert;
            expert.rank_expert = rank_expert;
            expert.delete_expert =false;
            expert.contacts_expert = contacts_expert;
            db_AAZ.Experts.Add(expert);
            db_AAZ.SaveChanges();
           
            if (ListFOS != null)
            {

                for (int i = 0; i < ListFOS.Length; i++)
                {
                    ExpertFos temp = new ExpertFos();
                    temp.id_expert = expert.id_expert;
                    temp.id_fos = ListFOS[i];
                    db_AAZ.ExpertFos.Add(temp);
                    db_AAZ.SaveChanges();
                }
            }

        }

        public bool DeleteExpert(int id_expert)
        {
            Experts updateexpert = db_AAZ.Experts.FirstOrDefault(o => o.id_expert == id_expert);
            updateexpert.delete_expert = true;
            db_AAZ.SaveChanges();
            return true;
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

        public bool AddProjects(string name_project, string lead_project, string grnti_project, DateTime begin_project, DateTime end_project, string money_project, string email_project,int[] listauthor,int fos)
        {
            try
            {
                Projects P = new Projects();
                P.name_project = name_project;
                P.lead_project = lead_project;
                P.grnti_project = grnti_project;
                P.begin_project = begin_project;
                P.end_project = end_project;
                P.money_project = money_project;
                P.email_project = email_project;

                db_AAZ.Projects.Add(P);
                db_AAZ.SaveChanges();
                ProjectFos PF = new ProjectFos();
                PF.id_project = P.id_project;
                PF.id_fos = fos;
                db_AAZ.ProjectFos.Add(PF);
                db_AAZ.SaveChanges();
                for(int i = 0; i < listauthor.Length; i++)
                {
                    ProjectAuthors temp = new ProjectAuthors();
                    temp.id_proj = P.id_project;
                    temp.id_author = listauthor[i];
                    db_AAZ.ProjectAuthors.Add(temp);
                    db_AAZ.SaveChanges();
                }
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }
        public bool AddAuthors(string surname_author, string name_author, string patronymic_author)
        {
            try
            {
                Authors A = new Authors();
                A.surname_author = surname_author;
                A.name_author = name_author;
                A.patronymic_author = patronymic_author;

                db_AAZ.Authors.Add(A);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool AddCategories(string name_category)
        {
            try
            {
                Categories C = new Categories();
                C.name_category = name_category;

                db_AAZ.Categories.Add(C);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool AddCriterions(string name_crit, bool qualit_crit,string valid_values,int id_category)
        {
            try
            {
                Criterions C = new Criterions();
                C.name_crit = name_crit;
                C.qualit_crit = qualit_crit;

                db_AAZ.Criterions.Add(C);
                db_AAZ.SaveChanges();
                CritValues crval = new CritValues();
                crval.id_crit = C.id_crit;
                crval.valid_values = valid_values;
                db_AAZ.CritValues.Add(crval);
                db_AAZ.SaveChanges();
                CatCrit catcrit = new CatCrit();
                catcrit.id_crit = C.id_crit;
                catcrit.id_cat = id_category;
                db_AAZ.CatCrit.Add(catcrit);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public string Gethello()
        {
            string a;
            return a = "awda";
        }
    }
}
