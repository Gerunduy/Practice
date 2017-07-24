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

        public List<CritValues> GetListCritValues(int id_crit)
        {
            try
            {
                List<CritValues> result = new List<CritValues>();
                List<CritValues> tmplCV = db_AAZ.CritValues.Where(o=>o.id_crit== id_crit).ToList();
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
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<CritValues> result = new List<CritValues>();
                CritValues tmpCV = new CritValues();
                tmpCV.id_value = -1;
                result.Add(tmpCV);

                return result;
            }
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
                tmpE.login_expert = pE.login_expert;
                tmpE.password_expert = pE.password_expert;

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

        public List<ProjectFos> GetListProjectFos()
        {
            try
            {
                List<ProjectFos> result = new List<ProjectFos>();
                List<ProjectFos> tmplPF = db_AAZ.ProjectFos.ToList();
                foreach (ProjectFos pPF in tmplPF)
                {
                    ProjectFos tmpPF = new ProjectFos();
                    tmpPF.id_project_fos = pPF.id_project_fos;
                    tmpPF.id_project = pPF.id_project;
                    tmpPF.id_fos = pPF.id_fos;

                    result.Add(tmpPF);
                }
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<ProjectFos> result = new List<ProjectFos>();
                ProjectFos tmpPF = new ProjectFos();
                tmpPF.id_project_fos = -1;
                result.Add(tmpPF);

                return result;
            }
        }

        public List<Expertises> GetListExpertisesForExpert(int id_expert)
        {
            try
            {
                List<Expertises> result = new List<Expertises>();
                List<Expertises> tmplExp = new List<Expertises>();
                List<ExpertiseExpert> lEE = db_AAZ.ExpertiseExpert.Where(p => p.id_expert == id_expert).ToList();
                foreach (ExpertiseExpert pEE in lEE)
                {
                    tmplExp.Add(db_AAZ.Expertises.Where(o => o.id_expertise == pEE.id_expertise).FirstOrDefault());
                }

                foreach (Expertises pE in tmplExp)
                {
                    Expertises tmpExp = new Expertises();
                    tmpExp.id_expertise = pE.id_expertise;
                    tmpExp.name_expertise = pE.name_expertise;
                    tmpExp.date_expertise = pE.date_expertise;
                    tmpExp.end_expertise = pE.end_expertise;
                    result.Add(tmpExp);
                }
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<Expertises> result = new List<Expertises>();
                Expertises tmpExp = new Expertises();
                tmpExp.id_expertise = -1;
                tmpExp.name_expertise = "Ошибка получения данных";
                result.Add(tmpExp);

                return result;
            }
        }

        public List<myAuthors> GetListAuthorsForProject(int id_project)
        {
            List<ProjectAuthors> PA = db_AAZ.ProjectAuthors.Where(p => p.id_proj == id_project).ToList();
           List<myAuthors> ListAuthor = new List<myAuthors>();
            for(int i = 0; i < PA.Count; i++)
            {
                int id_author = PA[i].id_author;
                Authors temp = db_AAZ.Authors.FirstOrDefault(o => o.id_author == id_author);
                myAuthors author = new myAuthors();
                author.id_author = temp.id_author;
                author.name_author = temp.name_author;
                author.patronymic_author = temp.patronymic_author;
                author.surname_author = temp.surname_author;
                author.FIO = temp.surname_author + " " + temp.name_author + " " + temp.patronymic_author;
                ListAuthor.Add(author);
            }
            return ListAuthor;
        }

        public List<Experts> GetListExpertForProject(int id_project)
        {
            ProjectExpertise PE = db_AAZ.ProjectExpertise.FirstOrDefault(o => o.id_project == id_project);
            List<Experts> listExperts = new List<Experts>();
            if (PE != null)
            {
                int id_expertise = PE.id_expertise;
                List<ExpertiseExpert> lEE = db_AAZ.ExpertiseExpert.Where(o => o.id_expertise == id_expertise).ToList();
                
                for (int i = 0; i < lEE.Count; i++)
                {
                    int  id_expert = lEE[i].id_expert;
                    Experts expert = db_AAZ.Experts.FirstOrDefault(o => o.id_expert == id_expert);
                    Experts temp = new Experts();
                    temp.id_expert = expert.id_expert;
                    temp.name_expert = expert.name_expert;
                    temp.patronymic_expert = expert.patronymic_expert;
                    temp.surname_expert = expert.surname_expert;
                    listExperts.Add(temp);
                }
                return listExperts;
            }
            return listExperts;
        }

        public List<myRaitinfExpert> GetListRaitingForExpertise(int id_project)
        {
            ProjectExpertise PE = db_AAZ.ProjectExpertise.FirstOrDefault(o => o.id_project == id_project);
            List<myRaitinfExpert> ListRatingforproject = new List<myRaitinfExpert>();
            int id_expertise = PE.id_expertise;
            List<Experts> listExperts = new List<Experts>();
            if (PE != null)
            {
                List<ExpertiseExpert> lEE = db_AAZ.ExpertiseExpert.Where(o => o.id_expertise == id_expertise).ToList();
                for (int i = 0; i < lEE.Count; i++)
                {
                    int id_expert = lEE[i].id_expert;
                    Experts expert = db_AAZ.Experts.FirstOrDefault(o => o.id_expert == id_expert);
                    Experts temp = new Experts();
                    temp.id_expert = expert.id_expert;
                    temp.name_expert = expert.name_expert;
                    temp.patronymic_expert = expert.patronymic_expert;
                    temp.surname_expert = expert.surname_expert;
                    listExperts.Add(temp);
                }
                List< ExpCrit> ListEC= db_AAZ.ExpCrit.Where(o => o.id_exp == id_expertise).ToList();
                
                for(int i = 0; i < ListEC.Count; i++)
                {
                    int id_crit = ListEC[i].id_crit;
                    myRaitinfExpert myraex = new myRaitinfExpert();
                    Criterions crit = db_AAZ.Criterions.FirstOrDefault(o => o.id_crit == id_crit);
                    myraex.name_crit = crit.name_crit;
                    for(int j = 0; j < listExperts.Count; j++)
                    {
                        int id_expert = listExperts[j].id_expert;
                        Marks temp = db_AAZ.Marks.FirstOrDefault(o => o.id_crit == id_crit && o.id_expert== id_expert && o.id_project==id_project);
                        myraex.raiting_crit += temp.rating + ";";
                    }
                    ListRatingforproject.Add(myraex);
                }
               
            }
            return ListRatingforproject;

        }

        #endregion

        #region Получение полных таблиц
        public List<Authors> GetListAllAuthors()
        {
            try
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
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<Authors> result = new List<Authors>();
                Authors tmpA = new Authors();
                tmpA.id_author = -1;
                tmpA.surname_author = "Содержимое таблицы не получено";
                result.Add(tmpA);

                return result;
            }
        }

        public List<CatCrit> GetListAllCatCrit()
        {
            try
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
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<CatCrit> result = new List<CatCrit>();
                CatCrit tmpCC = new CatCrit();
                tmpCC.id_cat_crit = -1;
                result.Add(tmpCC);

                return result;
            }
        }

        public List<Categories> GetListAllCategories()
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

        public List<Criterions> GetListAllCriterions()
        {
            try
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
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<Criterions> result = new List<Criterions>();
                Criterions tmpC = new Criterions();
                tmpC.id_crit = -1;
                result.Add(tmpC);

                return result;
            }
        }

        public List<CritValues> GetListAllCritValues()
        {
            try
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
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<CritValues> result = new List<CritValues>();
                CritValues tmpCV = new CritValues();
                tmpCV.id_value = -1;
                result.Add(tmpCV);

                return result;
            }
        }

        public List<ExpCrit> GetListAllExpCrit()
        {
            try
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
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<ExpCrit> result = new List<ExpCrit>();
                ExpCrit tmpEC = new ExpCrit();
                tmpEC.id_exp_crit = -1;
                result.Add(tmpEC);

                return result;
            }
        }

        public List<ExpertFos> GetListAllExpertFos()
        {
            try
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
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<ExpertFos> result = new List<ExpertFos>();
                ExpertFos tmpEF = new ExpertFos();
                tmpEF.id_expert_fos = -1;
                result.Add(tmpEF);

                return result;
            }
        }

        public List<ExpertiseMark> GetListAllExpertiseMark()
        {
            try
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
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<ExpertiseMark> result = new List<ExpertiseMark>();
                ExpertiseMark tmpEM = new ExpertiseMark();
                tmpEM.id_expertise_mark = -1;
                result.Add(tmpEM);

                return result;
            }
        }

        public List<Expertises> GetListAllExpertises()
        {
            try
            {
                List<Expertises> result = new List<Expertises>();
                List<Expertises> tmplE = db_AAZ.Expertises.ToList();
                foreach (Expertises pE in tmplE)
                {
                    Expertises tmpE = new Expertises();
                    tmpE.id_expertise = pE.id_expertise;
                    tmpE.name_expertise = pE.name_expertise;
                    tmpE.date_expertise = pE.date_expertise;


                    result.Add(tmpE);
                }
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<Expertises> result = new List<Expertises>();
                Expertises tmpE = new Expertises();
                tmpE.id_expertise = -1;
                result.Add(tmpE);

                return result;
            }
        }

        public List<Experts> GetListAllExperts()
        {
            try
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
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<Experts> result = new List<Experts>();
                Experts tmpE = new Experts();
                tmpE.id_expert = -1;
                result.Add(tmpE);

                return result;
            }
        }

        public List<FiledsOfScience> GetListAllFiledsOfScience()
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

        public List<GRNTI> GetListAllGRNTI()
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

        public List<Marks> GetListAllMarks()
        {
            try
            {
                List<Marks> result = new List<Marks>();
                List<Marks> tmplM = db_AAZ.Marks.ToList();
                foreach (Marks pM in tmplM)
                {
                    Marks tmpM = new Marks();
                    tmpM.id_mark = pM.id_mark;
                    tmpM.id_expert = pM.id_expert;
                    tmpM.id_crit = pM.id_crit;
                    tmpM.id_project = pM.id_project;
                    tmpM.rating = pM.rating;

                    result.Add(tmpM);
                }
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<Marks> result = new List<Marks>();
                Marks tmpM = new Marks();
                tmpM.id_mark = -1;
                result.Add(tmpM);

                return result;
            }
        }

        public List<ProjectAuthors> GetListAllProjectAuthors()
        {
            try
            {
                List<ProjectAuthors> result = new List<ProjectAuthors>();
                List<ProjectAuthors> tmplPA = db_AAZ.ProjectAuthors.ToList();
                foreach (ProjectAuthors pPA in tmplPA)
                {
                    ProjectAuthors tmpPA = new ProjectAuthors();
                    tmpPA.id_proj_author = pPA.id_proj_author;
                    tmpPA.id_proj = pPA.id_proj;
                    tmpPA.id_author = pPA.id_author;

                    result.Add(tmpPA);
                }
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<ProjectAuthors> result = new List<ProjectAuthors>();
                ProjectAuthors tmpPA = new ProjectAuthors();
                tmpPA.id_proj_author = -1;
                result.Add(tmpPA);

                return result;
            }
        }

        public List<ProjectExpertise> GetListAllProjectExpertise()
        {
            try
            {
                List<ProjectExpertise> result = new List<ProjectExpertise>();
                List<ProjectExpertise> tmplPE = db_AAZ.ProjectExpertise.ToList();
                foreach (ProjectExpertise pPE in tmplPE)
                {
                    ProjectExpertise tmpPE = new ProjectExpertise();
                    tmpPE.id_project_expertise = pPE.id_project_expertise;
                    tmpPE.id_expertise = pPE.id_expertise;
                    tmpPE.id_project = pPE.id_project;
                    tmpPE.accept = pPE.accept;

                    result.Add(tmpPE);
                }
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<ProjectExpertise> result = new List<ProjectExpertise>();
                ProjectExpertise tmpPE = new ProjectExpertise();
                tmpPE.id_project_expertise = -1;
                result.Add(tmpPE);

                return result;
            }
        }

        public List<ProjectFos> GetListAllProjectFos()
        {
            try
            {
                List<ProjectFos> result = new List<ProjectFos>();
                List<ProjectFos> tmplPF = db_AAZ.ProjectFos.ToList();
                foreach (ProjectFos pPF in tmplPF)
                {
                    ProjectFos tmpPF = new ProjectFos();
                    tmpPF.id_project_fos = pPF.id_project_fos;
                    tmpPF.id_project = pPF.id_project;
                    tmpPF.id_fos = pPF.id_fos;

                    result.Add(tmpPF);
                }
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<ProjectFos> result = new List<ProjectFos>();
                ProjectFos tmpPF = new ProjectFos();
                tmpPF.id_project_fos = -1;
                result.Add(tmpPF);

                return result;
            }
        }

        public List<Projects> GetListAllProjects()
        {
            try
            {
                List<Projects> result = new List<Projects>();
                List<Projects> tmplP = db_AAZ.Projects.ToList();
                foreach (Projects pP in tmplP)
                {
                    Projects tmpP = new Projects();
                    tmpP.id_project = pP.id_project;
                    tmpP.name_project = pP.name_project;
                    tmpP.lead_project = pP.lead_project;
                    tmpP.grnti_project = pP.grnti_project;
                    tmpP.begin_project = pP.begin_project;
                    tmpP.email_project = pP.email_project;
                    tmpP.money_project = pP.money_project;
                    tmpP.email_project = pP.email_project;

                    result.Add(tmpP);
                }
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<Projects> result = new List<Projects>();
                Projects tmpP = new Projects();
                tmpP.id_project = -1;
                result.Add(tmpP);

                return result;
            }
        }

        #endregion

        #region Добавление записей

        public bool AddNewAuthors(string surname_author, string name_author, string patronymic_author)
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

        public bool AddNewCatCrit(int id_cat, int id_crit)
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

        public bool AddNewCategories(string name_category)
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

        public bool AddNewCriterions(string name_crit, bool qualit_crit)
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

        public bool AddNewCritValues(int id_crit, string valid_values)
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

        public bool AddNewExpCrit(int id_exp, int id_crit)
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

        public bool AddNewExpertFos(int id_expert, int id_fos)
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

        public bool AddNewExpertiseMark(int id_expertise, int id_mark)
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

        public bool AddNewExpertises(string name_expertise, DateTime date_expertise)
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

        public bool AddNewExperts(string surname_expert, string name_expert, string patronymic_expert, string job_expert, string post_expert, string degree_expert, string rank_expert, string contacts_expert)
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

        public bool AddNewFiledsOfScience(string name_fos)
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

        public bool AddNewGRNTI(string name_grnti)
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

        public bool AddNewMarks(int id_expert, int id_crit, int id_project, int rating)
        {
            try
            {
                Marks M = new Marks();
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

        public bool AddNewProjectAuthors(int id_proj, int id_author)
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

        public bool AddNewProjectExpertise(int id_expertise, int id_project, bool accept)
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

        public bool AddNewProjectFos(int id_project, int id_fos)
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

        public bool AddNewProjects(string name_project, string lead_project, string grnti_project, DateTime begin_project, DateTime end_project, string money_project, string email_project)
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

        public TablesForExpertise GetTablesForExpertise()
        {
            try
            {
                TablesForExpertise result = new TablesForExpertise();
                result.lFOS = GetListAllFiledsOfScience();
                result.lProjects = GetListAllProjects();
                result.lProjectFos = GetListAllProjectFos();
                result.lCatigories = GetListAllCategories();
                result.lCatCrit = GetListAllCatCrit();
                result.lCriterions = GetListAllCriterions();
                result.lCritValues = GetListAllCritValues();
                result.lExperts = GetListAllExperts();
                result.lExpertFos = GetListAllExpertFos();
                result.Err = false;
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                TablesForExpertise result = new TablesForExpertise();
                result.Err = true;
                return result;
            }
        }

        public List<ProjectExpertise> test()
        {
            Projects pr = db_AAZ.Projects.FirstOrDefault(o => o.name_project == "Test_Proj2");
            int id = pr.id_project;
            List <ProjectExpertise> temp = db_AAZ.ProjectExpertise.Where(o => o.id_project == id).ToList();

            //List<ProjectExpertise> result = new List<ProjectExpertise>();
            //List<ProjectExpertise> tmplE = db_AAZ.ProjectExpertise.ToList();
            //foreach (ProjectExpertise pE in tmplE)
            //{
            //    ProjectExpertise tmpE = new ProjectExpertise();
            //    //tmpE.id_fos = pE.id_fos;
            //    //tmpE.name_fos = pE.name_fos;


            //    result.Add(tmpE);
            //}
            return temp;
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


        public void AddExpert(string surname_expert, string name_expert, string patronymic_expert,
           string job_expert, string post_expert, string degree_expert, string rank_expert
         , string contacts_expert, int[] ListFOS)
        {
            Experts expert = new Experts();
            expert.surname_expert = surname_expert;
            expert.name_expert = name_expert;
            expert.patronymic_expert = patronymic_expert;
            expert.job_expert = job_expert;
            expert.post_expert = post_expert;
            expert.degree_expert = degree_expert;
            expert.rank_expert = rank_expert;
            expert.delete_expert = false;
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
        public void AddExpert_new(string surname_expert, string name_expert, string patronymic_expert,
           string job_expert, string post_expert, string degree_expert, string rank_expert
         , string contacts_expert, int[] ListFOS, string login_expert, string password_expert)
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
            expert.login_expert = login_expert;
            expert.password_expert = password_expert;
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

        public bool EditProject(int id_project, string name_project, string lead_project, string grnti_project, DateTime begin_project,
            DateTime end_project, string money_project, string email_project, int[] listauthor, int fos)
        {
            try
            {
                Projects P = db_AAZ.Projects.FirstOrDefault(o=>o.id_project== id_project);
                P.name_project = name_project;
                P.lead_project = lead_project;
                P.grnti_project = grnti_project;
                P.begin_project = begin_project;
                P.end_project = end_project;
                P.money_project = money_project;
                P.email_project = email_project;

              
                db_AAZ.SaveChanges();
                ProjectFos PF = db_AAZ.ProjectFos.FirstOrDefault(o=>o.id_project== id_project) ;
                PF.id_project = P.id_project;
                PF.id_fos = fos;
                
                db_AAZ.SaveChanges();
               List< ProjectAuthors> listoldauthor = db_AAZ.ProjectAuthors.Where(o => o.id_proj == id_project).ToList();
                for (int i = 0; i < listoldauthor.Count; i++)
                {
                    db_AAZ.ProjectAuthors.Remove(listoldauthor[i]);
                }
                for (int i = 0; i < listauthor.Length; i++)
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


        public bool DeleteExpert(int id_expert)
        {
            Experts updateexpert = db_AAZ.Experts.FirstOrDefault(o => o.id_expert == id_expert);
            updateexpert.delete_expert = true;
            db_AAZ.SaveChanges();
            return true;
        }

        public bool DeleteProject(int id_project)
        {
            List<ProjectExpertise> PE = db_AAZ.ProjectExpertise.Where(o => o.id_project == id_project).ToList();
            if (PE.Count == 0)
            {
                List<ProjectAuthors> temp = db_AAZ.ProjectAuthors.Where(o => o.id_proj == id_project).ToList();
                for (int i = 0; i < temp.Count; i++)
                {
                    db_AAZ.ProjectAuthors.Remove(temp[i]);
                }
                ProjectFos PF = db_AAZ.ProjectFos.FirstOrDefault(o => o.id_project == id_project);
                db_AAZ.ProjectFos.Remove(PF);
                Projects p = db_AAZ.Projects.FirstOrDefault(o => o.id_project == id_project);
                db_AAZ.Projects.Remove(p);
                db_AAZ.SaveChanges();
                return true;
            }
            else
            {
                return false;
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

        public bool AddExpertises(string name_expertise, DateTime date_expertise, int[] arrExperts)
        {
            try
            {
                Expertises E = new Expertises();
                E.name_expertise = name_expertise;
                E.date_expertise = date_expertise;
                E.end_expertise = false;

                List<ExpertiseExpert> lEE = new List<ExpertiseExpert>();
                foreach (int i in arrExperts)
                {
                    ExpertiseExpert EE = new ExpertiseExpert();
                    EE.id_expert = i;
                    lEE.Add(EE);
                }
                E.ExpertiseExpert = lEE;

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

        public Experts Authorization(string Login, string Password)
        {
            try
            {
                Experts tmpExp = db_AAZ.Experts.Where(p => p.login_expert == Login).FirstOrDefault();
                if (tmpExp != null) // если эксперт с таким логином найден
                {
                    if (!tmpExp.delete_expert) // если не удалён
                    {
                        if (tmpExp.password_expert == Password) // если пароль введён верно
                        {
                            Experts expert = new Experts();
                            expert.id_expert = tmpExp.id_expert;
                            expert.surname_expert = tmpExp.surname_expert;
                            expert.name_expert = tmpExp.name_expert;
                            expert.patronymic_expert = tmpExp.patronymic_expert;
                            expert.job_expert = tmpExp.job_expert;
                            expert.post_expert = tmpExp.post_expert;
                            expert.degree_expert = tmpExp.degree_expert;
                            expert.rank_expert = tmpExp.rank_expert;
                            expert.delete_expert = tmpExp.delete_expert;
                            expert.contacts_expert = tmpExp.contacts_expert;

                            return expert;
                        }
                        else // если пароль введён неверно
                        {
                            Experts expert = new Experts();
                            expert.id_expert = -1;
                            expert.surname_expert = "Неверный Login или Password";
                            return expert;
                        } 
                    }
                    else // если удалён
                    {
                        Experts expert = new Experts();
                        expert.id_expert = -1;
                        expert.surname_expert = "Неверный Login или Password";
                        return expert;
                    }
                }
                else // если эксперт с таким логином не найден
                {
                    Experts expert = new Experts();
                    expert.id_expert = -1;
                    expert.surname_expert = "Неверный Login или Password";
                    return expert;
                }
            }
            catch (Exception Ex)
            {
                // логируем
                Experts expert = new Experts();
                expert.id_expert = -2;
                expert.surname_expert = "Ошибка";
                return expert;
            }
        }


        public string Gethello()
        {
            string a;
            return a = "awda";
        }
    }
}
