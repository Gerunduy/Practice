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

        public List<CritValues> GetListCritValues()
        {
            List<CritValues> result = new List<CritValues>();
            List<CritValues> tmplCV = db_AAZ.CritValues.ToList();
            foreach (CritValues pCV in tmplCV)
            {
                CritValues tmpCV = new CritValues();
                tmpCV.id_value = pCV.id_value;
                tmpCV.id_crit = pCV.id_crit;
                tmpCV.valid_values = pCV.valid_values;

                result.Add(tmpCV);
            }
            return result;
        }

        public List<ExpCrit> GetListExpCrit()
        {
            List<ExpCrit> result = new List<ExpCrit>();
            List<ExpCrit> tmplEC = db_AAZ.ExpCrit.ToList();
            foreach (ExpCrit pEC in tmplEC)
            {
                ExpCrit tmpEC = new ExpCrit();
                tmpEC.id_exp_crit = pEC.id_exp_crit;
                tmpEC.id_exp = pEC.id_exp;
                tmpEC.id_crit = pEC.id_crit;

                result.Add(tmpEC);
            }
            return result;
        }

        public List<ExpertFos> GetListExpertFos()
        {
            List<ExpertFos> result = new List<ExpertFos>();
            List<ExpertFos> tmplEF = db_AAZ.ExpertFos.ToList();
            foreach (ExpertFos pEF in tmplEF)
            {
                ExpertFos tmpEF = new ExpertFos();
                tmpEF.id_expert_fos = pEF.id_expert_fos;
                tmpEF.id_expert = pEF.id_expert;
                tmpEF.id_fos = pEF.id_fos;

                result.Add(tmpEF);
            }
            return result;
        }

        public List<ExpertiseMark> GetListExpertiseMark()
        {
            List<ExpertiseMark> result = new List<ExpertiseMark>();
            List<ExpertiseMark> tmplEM = db_AAZ.ExpertiseMark.ToList();
            foreach (ExpertiseMark pEM in tmplEM)
            {
                ExpertiseMark tmpEM = new ExpertiseMark();
                tmpEM.id_expertise_mark = pEM.id_expertise_mark;
                tmpEM.id_expertise = pEM.id_expertise;
                tmpEM.id_mark = pEM.id_mark;

                result.Add(tmpEM);
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

        #region Добавление записей

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

        public bool AddCatCrit(int id_cat, int id_crit)
        {
            try
            {
                CatCrit CC = new CatCrit();
                CC.id_cat = id_cat;
                CC.id_crit = id_crit;

                db_AAZ.CatCrit.Add(CC);
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

        public bool AddCriterions(string name_crit, bool qualit_crit)
        {
            try
            {
                Criterions C = new Criterions();
                C.name_crit = name_crit;
                C.qualit_crit = qualit_crit;

                db_AAZ.Criterions.Add(C);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool AddCritValues(int id_crit, string valid_values)
        {
            try
            {
                CritValues CV = new CritValues();
                CV.id_crit = id_crit;
                CV.valid_values = valid_values;

                db_AAZ.CritValues.Add(CV);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool AddExpCrit(int id_exp, int id_crit)
        {
            try
            {
                ExpCrit EC = new ExpCrit();
                EC.id_exp = id_exp;
                EC.id_crit = id_crit;

                db_AAZ.ExpCrit.Add(EC);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool AddExpertFos(int id_expert, int id_fos)
        {
            try
            {
                ExpertFos EF = new ExpertFos();
                EF.id_expert = id_expert;
                EF.id_fos = id_fos;

                db_AAZ.ExpertFos.Add(EF);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool AddExpertiseMark(int id_expertise, int id_mark)
        {
            try
            {
                ExpertiseMark EM = new ExpertiseMark();
                EM.id_expertise = id_expertise;
                EM.id_mark = id_mark;

                db_AAZ.ExpertiseMark.Add(EM);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool AddExpertises(string name_expertise, DateTime date_expertise)
        {
            try
            {
                Expertises E = new Expertises();
                E.name_expertise = name_expertise;
                E.date_expertise = date_expertise;

                db_AAZ.Expertises.Add(E);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool AddExperts(string surname_expert, string name_expert, string patronymic_expert, string job_expert, string post_expert, string degree_expert, string rank_expert, string contacts_expert)
        {
            try
            {
                Experts E = new Experts();
                E.surname_expert = surname_expert;
                E.name_expert = name_expert;
                E.patronymic_expert = patronymic_expert;
                E.job_expert = job_expert;
                E.post_expert = post_expert;
                E.degree_expert = degree_expert;
                E.rank_expert = rank_expert;
                E.contacts_expert = contacts_expert;

                db_AAZ.Experts.Add(E);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool AddFiledsOfScience(string name_fos)
        {
            try
            {
                FiledsOfScience FOS = new FiledsOfScience();
                FOS.name_fos = name_fos;

                db_AAZ.FiledsOfScience.Add(FOS);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool AddGRNTI(string name_grnti)
        {
            try
            {
                GRNTI grnti = new GRNTI();
                grnti.name_grnti = name_grnti;

                db_AAZ.GRNTI.Add(grnti);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool AddMarks(int id_expert, int id_crit, int id_project, int rating)
        {
            try
            {
                Marks M= new Marks();
                M.id_expert = id_expert;
                M.id_crit = id_crit;
                M.id_project = id_project;
                M.rating = rating;

                db_AAZ.Marks.Add(M);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool AddProjectAuthors(int id_proj, int id_author)
        {
            try
            {
                ProjectAuthors PA = new ProjectAuthors();
                PA.id_proj = id_proj;
                PA.id_author = id_author;

                db_AAZ.ProjectAuthors.Add(PA);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool AddProjectExpertise(int id_expertise, int id_project, bool accept)
        {
            try
            {
                ProjectExpertise PE = new ProjectExpertise();
                PE.id_expertise = id_expertise;
                PE.id_project = id_project;
                PE.accept = accept;

                db_AAZ.ProjectExpertise.Add(PE);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool AddProjectFos(int id_project, int id_fos)
        {
            try
            {
                ProjectFos PF = new ProjectFos();
                PF.id_project = id_project;
                PF.id_fos = id_fos;

                db_AAZ.ProjectFos.Add(PF);
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool AddProjects(string name_project, string lead_project, string grnti_project, DateTime begin_project, DateTime end_project, string money_project, string email_project)
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
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        #endregion

        #region Редактирование таблиц

        public bool EditAuthors(int id_author, string surname_author, string name_author, string patronymic_author)
        {
            try
            {
                Authors A = db_AAZ.Authors.Where(p => p.id_author == id_author).FirstOrDefault();
                A.surname_author = surname_author;
                A.name_author = name_author;
                A.patronymic_author = patronymic_author;

                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditCatCrit(int id_cat_crit, int id_cat, int id_crit)
        {
            try
            {
                CatCrit CC = db_AAZ.CatCrit.Where(p => p.id_cat_crit == id_cat_crit).FirstOrDefault();
                CC.id_cat = id_cat;
                CC.id_crit = id_crit;

                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditCategories(int id_category, string name_category)
        {
            try
            {
                Categories C = db_AAZ.Categories.Where(p => p.id_category == id_category).FirstOrDefault();
                C.name_category = name_category;

                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditCriterions(int id_crit, string name_crit, bool qualit_crit)
        {
            try
            {
                Criterions C = db_AAZ.Criterions.Where(p => p.id_crit == id_crit).FirstOrDefault();
                C.name_crit = name_crit;
                C.qualit_crit = qualit_crit;

                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditCritValues(int id_value, int id_crit, string valid_values)
        {
            try
            {
                CritValues CV = db_AAZ.CritValues.Where(p => p.id_value == id_value).FirstOrDefault();
                CV.id_crit = id_crit;
                CV.valid_values = valid_values;

                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditExpCrit(int id_exp_crit, int id_exp, int id_crit)
        {
            try
            {
                ExpCrit EC = db_AAZ.ExpCrit.Where(p => p.id_exp_crit == id_exp_crit).FirstOrDefault();
                EC.id_exp = id_exp;
                EC.id_crit = id_crit;

                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditExpertFos(int id_expert_fos, int id_expert, int id_fos)
        {
            try
            {
                ExpertFos EF = db_AAZ.ExpertFos.Where(p => p.id_expert_fos == id_expert_fos).FirstOrDefault();
                EF.id_expert = id_expert;
                EF.id_fos = id_fos;

                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditExpertiseMark(int id_expertise_mark, int id_expertise, int id_mark)
        {
            try
            {
                ExpertiseMark EM = db_AAZ.ExpertiseMark.Where(p => p.id_expertise_mark == id_expertise_mark).FirstOrDefault();
                EM.id_expertise = id_expertise;
                EM.id_mark = id_mark;

                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditExpertises(int id_expertise, string name_expertise, DateTime date_expertise)
        {
            try
            {
                Expertises E = db_AAZ.Expertises.Where(p => p.id_expertise == id_expertise).FirstOrDefault();
                E.name_expertise = name_expertise;
                E.date_expertise = date_expertise;

                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditExperts(int id_expert, string surname_expert, string name_expert, string patronymic_expert, string job_expert, string post_expert, string degree_expert, string rank_expert, string contacts_expert)
        {
            try
            {
                Experts E = db_AAZ.Experts.Where(p => p.id_expert == id_expert).FirstOrDefault();
                E.surname_expert = surname_expert;
                E.name_expert = name_expert;
                E.patronymic_expert = patronymic_expert;
                E.job_expert = job_expert;
                E.post_expert = post_expert;
                E.degree_expert = degree_expert;
                E.rank_expert = rank_expert;
                E.contacts_expert = contacts_expert;


                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditFiledsOfScience(int id_fos, string name_fos)
        {
            try
            {
                FiledsOfScience FOS = db_AAZ.FiledsOfScience.Where(p => p.id_fos == id_fos).FirstOrDefault();
                FOS.name_fos = name_fos;


                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditGRNTI(string code_grnti, string name_grnti)
        {
            try
            {
                GRNTI grnti = db_AAZ.GRNTI.Where(p => p.code_grnti == code_grnti).FirstOrDefault();
                grnti.name_grnti = name_grnti;


                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditMarks(int id_mark, int id_expert, int id_crit, int id_project, int rating)
        {
            try
            {
                Marks M = db_AAZ.Marks.Where(p => p.id_mark == id_mark).FirstOrDefault();
                M.id_expert = id_expert;
                M.id_crit = id_crit;
                M.id_project = id_project;
                M.rating = rating;


                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditProjectAuthors(int id_proj_author, int id_proj, int id_author)
        {
            try
            {
                ProjectAuthors PA = db_AAZ.ProjectAuthors.Where(p => p.id_proj_author == id_proj_author).FirstOrDefault();
                PA.id_proj = id_proj;
                PA.id_author = id_author;


                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditProjectExpertise(int id_project_expertise, int id_expertise, int id_project, bool accept)
        {
            try
            {
                ProjectExpertise PE = db_AAZ.ProjectExpertise.Where(p => p.id_project_expertise == id_project_expertise).FirstOrDefault();
                PE.id_expertise = id_expertise;
                PE.id_project = id_project;
                PE.accept = accept;


                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditProjectFos(int id_project_fos, int id_project, int id_fos)
        {
            try
            {
                ProjectFos PF = db_AAZ.ProjectFos.Where(p => p.id_project_fos == id_project_fos).FirstOrDefault();
                PF.id_project = id_project;
                PF.id_fos = id_fos;


                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool EditProjects(int id_project, string name_project, string lead_project, string grnti_project, DateTime begin_project, DateTime end_project, string money_project, string email_project)
        {
            try
            {
                Projects P = db_AAZ.Projects.Where(p => p.id_project == id_project).FirstOrDefault();
                P.name_project = name_project;
                P.lead_project = lead_project;
                P.grnti_project = grnti_project;
                P.begin_project = begin_project;
                P.end_project = end_project;
                P.money_project = money_project;
                P.email_project = email_project;


                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        #endregion



        public string Gethello()
        {
            string a;
            return a = "awda";
        }
    }
}
