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

        public myExpertiseExaminationTables GetExpertiseExaminationTablesByID(int id_expertise, int id_expert)
        {
            try
            {
                myExpertiseExaminationTables result = new myExpertiseExaminationTables();
                // = expertise
                Expertises E = db_AAZ.Expertises.Where(p => p.id_expertise == id_expertise).FirstOrDefault();
                result.expertise = new Expertises();
                result.expertise.id_expertise = E.id_expertise;
                result.expertise.name_expertise = E.name_expertise;
                result.expertise.date_expertise = E.date_expertise;
                result.expertise.end_date_expertise = E.end_date_expertise;
                result.expertise.count_proj_expertise = E.count_proj_expertise;
                result.expertise.id_fos = E.id_fos;
                result.expertise.end_expertise = E.end_expertise;
                // = expert
                Experts Exp = db_AAZ.Experts.Where(x => x.id_expert == id_expert).FirstOrDefault();
                result.expert = new Experts();
                result.expert.id_expert = Exp.id_expert;
                result.expert.surname_expert = Exp.surname_expert;
                result.expert.name_expert = Exp.name_expert;
                result.expert.patronymic_expert = Exp.patronymic_expert;
                result.expert.job_expert = Exp.job_expert;
                result.expert.post_expert = Exp.post_expert;
                result.expert.degree_expert = Exp.degree_expert;
                result.expert.rank_expert = Exp.rank_expert;
                result.expert.contacts_expert = Exp.contacts_expert;
                // = ListCriterions
                result.ListCriterions = new List<Criterions>();
                foreach (ExpCrit pEC in E.ExpCrit)
                {
                    Criterions tC = db_AAZ.Criterions.Where(o => o.id_crit == pEC.id_crit).FirstOrDefault();
                    Criterions C = new Criterions();
                    C.id_crit = tC.id_crit;
                    C.name_crit = tC.name_crit;
                    C.qualit_crit = tC.qualit_crit;
                    foreach (CritValues ptCV in tC.CritValues)
                    {
                        CritValues CV = new CritValues();
                        CV.id_value = ptCV.id_value;
                        CV.id_crit = ptCV.id_crit;
                        CV.valid_values = ptCV.valid_values;
                        C.CritValues.Add(CV);
                    }
                    result.ListCriterions.Add(C);
                }
                // = ListProjects
                result.ListProjects = new List<Projects>();
                foreach (ProjectExpertise pPE in E.ProjectExpertise)
                {
                    Projects tP = db_AAZ.Projects.Where(z => z.id_project == pPE.id_project).FirstOrDefault();
                    Projects P = new Projects();
                    P.id_project = tP.id_project;
                    P.name_project = tP.name_project;
                    P.lead_project = tP.lead_project;
                    P.grnti_project = tP.grnti_project;
                    P.begin_project = tP.begin_project;
                    P.email_project = tP.email_project;
                    P.money_project = tP.money_project;
                    P.email_project = tP.email_project;
                    result.ListProjects.Add(P);
                }
                // = ListCritCompare
                result.ListCritCompare = new List<CritCompare>();
                foreach (CritCompare pCC in E.CritCompare)
                {
                    //CritCompare tCC = db_AAZ.CritCompare.Where(s => s.id_compare == pCC.id_compare).FirstOrDefault();
                    if (pCC.id_expert == id_expert)
                    {
                        CritCompare CC = new CritCompare();
                        CC.id_compare = pCC.id_compare;
                        CC.id_expertise = pCC.id_expertise;
                        CC.id_expert = pCC.id_expert;
                        CC.mark_compare = pCC.mark_compare;
                        List<CritCompareCrit> lCCC = new List<CritCompareCrit>();
                        foreach (CritCompareCrit pCCC in pCC.CritCompareCrit)
                        {
                            CritCompareCrit CCC = new CritCompareCrit();
                            CCC.id_crit_crcompare = pCCC.id_crit_crcompare;
                            CCC.id_compare = pCCC.id_compare;
                            CCC.id_crit = pCCC.id_crit;
                            lCCC.Add(CCC);
                        }
                        CC.CritCompareCrit = lCCC.ToArray();

                        result.ListCritCompare.Add(CC);
                    }
                }
                // = ListMark
                result.ListMark = new List<Marks>();
                foreach (ExpertiseMark pEM in E.ExpertiseMark)
                {
                    Marks tM = db_AAZ.Marks.Where(f => f.id_mark == pEM.id_mark).FirstOrDefault();
                    Marks M = new Marks();
                    M.id_mark = tM.id_mark;
                    M.id_expert = tM.id_expert;
                    M.id_crit = tM.id_crit;
                    M.id_project = tM.id_project;
                    M.rating = tM.rating;

                    result.ListMark.Add(M);
                }

                // = Err
                result.Err = false;

                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                myExpertiseExaminationTables result = new myExpertiseExaminationTables();
                result.Err = true;
                return result;
            }
        }
        public bool AddNewCritCompare(CritCompare[] arrCompare)
        {
            try
            {
                foreach (CritCompare pCC in arrCompare)
                {
                    db_AAZ.CritCompare.Add(pCC);
                }
                
                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }
        public bool AddNewMark(int id_expertise, Marks[] arrMarks)
        {
            try
            {
                foreach (Marks pM in arrMarks)
                {
                    ExpertiseMark EM = new ExpertiseMark();
                    EM.id_expertise = id_expertise;
                    EM.Marks = pM;
                    db_AAZ.ExpertiseMark.Add(EM);
                }

                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }
        public bool EditExpertiseExpertStatus(int id_expertise, int id_expert)
        {
            try
            {
                ExpertiseExpert EE = db_AAZ.ExpertiseExpert.Where(p => p.id_expertise == id_expertise).Where(o => o.id_expert == id_expert).FirstOrDefault();
                if (!EE.end_marking)
                {
                    EE.end_marking = true;
                    db_AAZ.SaveChanges();
                    return true;
                }
                else return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }
        public bool EditExpertiseStatusToComplete(int id_expertise)
        {
            try
            {
                Expertises E = db_AAZ.Expertises.Where(p => p.id_expertise == id_expertise).FirstOrDefault();
                if (!E.end_expertise)
                {
                    E.end_expertise = true;
                    db_AAZ.SaveChanges();
                    return true;
                }
                else return true;

            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }
        public bool EditExpertiseStatusToStart(int id_expertise)
        {
            try
            {
                Expertises E = db_AAZ.Expertises.Where(p => p.id_expertise == id_expertise).FirstOrDefault();
                if (!E.begin_expertise)
                {
                    E.begin_expertise = true;
                    db_AAZ.SaveChanges();
                    return true;
                }
                else return true;
                
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
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
                    tmpE.degree_rank_expert = pE.rank_expert + ", " + pE.degree_expert;
                    tmpE.rank_expert = pE.rank_expert;
                    tmpE.contacts_expert = pE.contacts_expert;
                    tmpE.login_expert = pE.login_expert;
                    tmpE.password_expert = pE.password_expert;
                    tmpE.commision_chairman = pE.comission_chairman;



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
                    if (pE.delete_expert) tmpE.status = "Удалён"; else tmpE.status = "Активен";
                    //tmpE.delete_expert = pE.delete_expert;
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
                    tmpP.org_project = pP.org_project;
                    tmpP.money_project = pP.money_project;
                    tmpP.email_project = pP.email_project;


                    ProjectExpertise prexp = db_AAZ.ProjectExpertise.FirstOrDefault(o => o.id_project == pP.id_project);
                    if (prexp != null)
                    {
                        tmpP.expertisa = true;
                        tmpP.expertise = "Проводилась";
                        Expertises ex = db_AAZ.Expertises.FirstOrDefault(o => o.id_expertise == prexp.id_expertise);
                        tmpP.date_expertise = ex.date_expertise;
                        tmpP.name_expertise = ex.name_expertise;
                    }
                    else
                    {
                        tmpP.expertisa = false;
                        tmpP.expertise = "Не проводилась";
                    }
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
                    tmpExp.end_date_expertise = pE.end_date_expertise;
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
                        if (temp != null)
                        {
                            myraex.raiting_crit += temp.rating + ";";
                        }
                        else
                        {
                            myraex.raiting_crit +=";" ;
                        }
                       
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

        public List<ExpertiseExpert> GetListAllExpertiseExpert()
        {
            try
            {
                List<ExpertiseExpert> result = new List<ExpertiseExpert>();
                List<ExpertiseExpert> tmplEE = db_AAZ.ExpertiseExpert.ToList();
                foreach (ExpertiseExpert pEE in tmplEE)
                {
                    ExpertiseExpert tmpEE = new ExpertiseExpert();
                    tmpEE.id_expertise_expert = pEE.id_expertise_expert;
                    tmpEE.id_expertise = pEE.id_expertise;
                    tmpEE.id_expert = pEE.id_expert;

                    result.Add(tmpEE);
                }
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<ExpertiseExpert> result = new List<ExpertiseExpert>();
                ExpertiseExpert tmpEE = new ExpertiseExpert();
                tmpEE.id_expertise_expert = -1;
                result.Add(tmpEE);

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
                    tmpE.end_expertise = pE.end_expertise;


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
                    tmpE.comission_chairman = pE.comission_chairman;

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

        public bool AddNewExpertiseExpert(int id_expertise, int id_expert)
        {
            try
            {
                ExpertiseExpert EE = new ExpertiseExpert();
                EE.id_expertise = id_expertise;
                EE.id_expert = id_expert;
                

                db_AAZ.ExpertiseExpert.Add(EE);
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

        public bool CreateNewExpertise(string name_expertise, DateTime date_expertise, int id_fos, int count_proj_expertise, int[] projectsId, int[] critsId, int[] expertsId)
        {
            try
            {
                Expertises E = new Expertises();
                E.name_expertise = name_expertise;
                E.date_expertise = date_expertise;
                E.end_expertise = false;
                E.id_fos = id_fos;
                E.count_proj_expertise = count_proj_expertise;
                E.end_date_expertise = DateTime.MinValue;

                foreach (int i in projectsId)
                {
                    ProjectExpertise PE = new ProjectExpertise();
                    PE.id_project = i;
                    E.ProjectExpertise.Add(PE);
                    Projects P = db_AAZ.Projects.Where(p => p.id_project == i).FirstOrDefault();
                    P.exsist_exp_project = true;
                }

                foreach (int i in critsId)
                {
                    ExpCrit EC = new ExpCrit();
                    EC.id_crit = i;
                    E.ExpCrit.Add(EC);
                }

                foreach (int i in expertsId)
                {
                    ExpertiseExpert EE = new ExpertiseExpert();
                    EE.id_expert = i;
                    E.ExpertiseExpert.Add(EE);
                }

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

        public bool EditExpertiseByID(int id_expertise, string name_expertise, DateTime date_expertise, int id_fos, int count_proj_expertise, ProjectExpertise[] lprojects, ExpCrit[] lcrits, ExpertiseExpert[] lexperts)
        {
            try
            {
                Expertises E = db_AAZ.Expertises.Where(p => p.id_expertise == id_expertise).FirstOrDefault();
                E.name_expertise = name_expertise;
                E.date_expertise = date_expertise;
                //E.end_expertise = false;
                E.id_fos = id_fos;
                E.count_proj_expertise = count_proj_expertise;
                //E.end_date_expertise = DateTime.MinValue;

                //==========================================================
                ProjectExpertise[] ePE = E.ProjectExpertise.ToArray();
                foreach (ProjectExpertise PE in ePE)
                {
                    Projects p = db_AAZ.Projects.Where(o => o.id_project == PE.id_project).FirstOrDefault();
                    p.exsist_exp_project = false;
                    db_AAZ.ProjectExpertise.Remove(PE);
                }
                E.ProjectExpertise = lprojects;
                foreach (ProjectExpertise pe in lprojects)
                {
                    Projects p = db_AAZ.Projects.Where(o => o.id_project == pe.id_project).FirstOrDefault();
                    p.exsist_exp_project = true;
                }
                //==========================================================
                ExpCrit[] eEC = E.ExpCrit.ToArray();
                foreach (ExpCrit EC in eEC)
                {
                    db_AAZ.ExpCrit.Remove(EC);
                }
                E.ExpCrit = lcrits;
                //==========================================================
                ExpertiseExpert[] eEE = E.ExpertiseExpert.ToArray();
                foreach (ExpertiseExpert EE in eEE)
                {
                    db_AAZ.ExpertiseExpert.Remove(EE);
                }
                E.ExpertiseExpert = lexperts;
                //==========================================================

                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public myExpertiseForCard GetMyExpertiseForCardByID(int id_expertise)
        {
            try
            {
                myExpertiseForCard result = new myExpertiseForCard();
                Expertises E = db_AAZ.Expertises.Where(p => p.id_expertise == id_expertise).FirstOrDefault();

                result.id_expertise = E.id_expertise;
                result.name_expertise = E.name_expertise;
                result.fos_expertise = db_AAZ.FiledsOfScience.Where(o => o.id_fos == E.id_fos).FirstOrDefault().name_fos;
                if (E.end_expertise) result.status = "Завершена"; else result.status = "В работе";
                result.end_expertise = E.end_expertise;
                result.date_expertise = E.date_expertise;
                result.end_date_expertise = E.end_date_expertise;
                result.count_project_expertise = E.count_proj_expertise;
                result.begin_expertise = E.begin_expertise;

                result.ListProjects = new List<myProjectForExpertiseCard>();
                foreach (ProjectExpertise PE in E.ProjectExpertise)
                {
                    myProjectForExpertiseCard P = new myProjectForExpertiseCard();
                    Projects proj = db_AAZ.Projects.Where(z => z.id_project == PE.id_project).FirstOrDefault();
                    P.id_project = proj.id_project;
                    P.name_project = proj.name_project;
                    P.lead_project = proj.lead_project;
                    P.organization = proj.org_project;
                    if (PE.accept) P.accept = "Да"; else P.accept = "Нет";
                    result.ListProjects.Add(P);
                }
                // =======================================================================================================
                result.ListCriterions = new List<Criterions>();
                result.ListCatCrit = new List<CatCrit>();
                result.ListCategories = new List<Categories>();
                foreach (ExpCrit EC in E.ExpCrit)
                {
                    Criterions tmpCrit = db_AAZ.Criterions.Where(x => x.id_crit == EC.id_crit).FirstOrDefault();
                    Criterions Crit = new Criterions();
                    Crit.id_crit = tmpCrit.id_crit;
                    Crit.name_crit = tmpCrit.name_crit;
                    result.ListCriterions.Add(Crit);
                    foreach (CatCrit pCC in tmpCrit.CatCrit)
                    {
                        CatCrit CC = new CatCrit();
                        CC.id_cat_crit = pCC.id_cat_crit;
                        CC.id_cat = pCC.id_cat;
                        CC.id_crit = pCC.id_crit;
                        result.ListCatCrit.Add(CC);

                        Categories tmpCat = db_AAZ.Categories.Where(v => v.id_category == CC.id_cat).FirstOrDefault();
                        if (result.ListCategories.Where(q => q.id_category == tmpCat.id_category).FirstOrDefault() == default(Categories))
                        {
                            Categories Cat = new Categories();
                            Cat.id_category = tmpCat.id_category;
                            Cat.name_category = tmpCat.name_category;
                            result.ListCategories.Add(Cat);
                        }
                    }
                }
                // =======================================================================================================
                List<bool> lb = new List<bool>();
                result.ListExperts = new List<Experts>();
                foreach (ExpertiseExpert EE in E.ExpertiseExpert)
                {
                    Experts tmpExpert = db_AAZ.Experts.Where(g => g.id_expert == EE.id_expert).FirstOrDefault();
                    Experts Expert = new Experts();
                    Expert.id_expert = tmpExpert.id_expert;
                    Expert.surname_expert = tmpExpert.surname_expert;
                    Expert.name_expert = tmpExpert.name_expert;
                    Expert.patronymic_expert = tmpExpert.patronymic_expert;
                    result.ListExperts.Add(Expert);

                    lb.Add(EE.end_marking);
                }

                bool tmpMarkIsCompleted = true;
                foreach(bool pb in lb)
                {
                    if (!pb)
                    {
                        tmpMarkIsCompleted = false;
                        break;
                    }
                    else continue;
                }
                result.MarkIsCompleted = tmpMarkIsCompleted;
                // =======================================================================================================
                result.ListMark = new List<Marks>();
                foreach(ExpertiseMark pM in E.ExpertiseMark)
                {
                    Marks tM = db_AAZ.Marks.Where(k => k.id_mark == pM.id_mark).FirstOrDefault();
                    Marks M = new Marks();
                    M.id_mark = tM.id_mark;
                    M.id_expert = tM.id_expert;
                    M.id_crit = tM.id_crit;
                    M.id_project = tM.id_project;
                    M.rating = tM.rating;
                    result.ListMark.Add(M);
                }
                // =======================================================================================================
                result.ListCritCompare = new List<CritCompare>();
                foreach (CritCompare pCC in E.CritCompare)
                {
                    CritCompare CC = new CritCompare();
                    CC.id_compare = pCC.id_compare;
                    CC.id_expertise = pCC.id_expertise;
                    CC.id_expert = pCC.id_expert;
                    CC.mark_compare = pCC.mark_compare;
                    foreach (CritCompareCrit pCCC in pCC.CritCompareCrit)
                    {
                        CritCompareCrit CCC = new CritCompareCrit();
                        CCC.id_crit_crcompare = pCCC.id_crit_crcompare;
                        CCC.id_compare = pCC.id_compare;
                        CCC.id_crit = pCCC.id_crit;
                        CC.CritCompareCrit.Add(CCC);
                    }
                    result.ListCritCompare.Add(CC);
                }
                // =======================================================================================================
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                myExpertiseForCard result = new myExpertiseForCard();
                result.id_expertise = -1;
                result.name_expertise = "Ошибка получения данных";
                return result;
            }
        }

        public List<myCompletedexpertises> GetListCompletedExpertises()
        {
            try
            {
                List<myCompletedexpertises> result = new List<myCompletedexpertises>();
                List<Expertises> tmplE = db_AAZ.Expertises.Where(p => p.end_expertise == true).ToList();
                foreach (Expertises pE in tmplE)
                {
                    myCompletedexpertises tmpE = new myCompletedexpertises();
                    tmpE.id_expertise = pE.id_expertise;
                    if (pE.end_expertise) tmpE.status = "Завершена"; else tmpE.status = "В работе";
                    tmpE.name_expertise = pE.name_expertise;
                    tmpE.date_expertise = pE.date_expertise;
                    tmpE.end_date_expertise = pE.end_date_expertise;

                    tmpE.ListExperts = new List<string>();
                    foreach (ExpertiseExpert EE in pE.ExpertiseExpert)
                    {
                        Experts Exp = db_AAZ.Experts.Where(o => o.id_expert == EE.id_expert).FirstOrDefault();
                        string S = string.Format("{0} {1} {2}", Exp.surname_expert, Exp.name_expert, Exp.patronymic_expert);
                        tmpE.ListExperts.Add(S);
                    }

                    tmpE.ListProject = new List<myCompletedexpertisesProject>();
                    foreach (ProjectExpertise PE in pE.ProjectExpertise)
                    {
                        Projects tmpProj = db_AAZ.Projects.Where(z => z.id_project == PE.id_project).FirstOrDefault();
                        myCompletedexpertisesProject Proj = new myCompletedexpertisesProject();
                        Proj.id_project = tmpProj.id_project;
                        Proj.name_project = tmpProj.name_project;
                        Proj.lead_project = tmpProj.lead_project;
                        Proj.grnti_project = tmpProj.grnti_project;
                        Proj.begin_project = tmpProj.begin_project;
                        Proj.email_project = tmpProj.email_project;
                        Proj.money_project = tmpProj.money_project;
                        Proj.email_project = tmpProj.email_project;
                        Proj.delete_project = tmpProj.delete_project;
                        if (PE.accept) Proj.is_accept = "Принят"; else Proj.is_accept = "Не принят";

                        tmpE.ListProject.Add(Proj);
                    }

                    result.Add(tmpE);
                }
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<myCompletedexpertises> result = new List<myCompletedexpertises>();
                myCompletedexpertises tmpE = new myCompletedexpertises();
                tmpE.id_expertise = -1;
                tmpE.name_expertise = "Содержимое не было получено";
                result.Add(tmpE);

                return result;
            }
        }

        public List<myCurrentexpertises> GetListCurrentExpertises()
        {
            try
            {
                List<myCurrentexpertises> result = new List<myCurrentexpertises>();
                List<Expertises> tmplE = db_AAZ.Expertises.Where(p => p.end_expertise == false).ToList();
                foreach (Expertises pE in tmplE)
                {
                    myCurrentexpertises tmpE = new myCurrentexpertises();
                    tmpE.id_expertise = pE.id_expertise;
                    if (pE.end_expertise) tmpE.status = "Завершена"; else tmpE.status = "В работе";
                    tmpE.name_expertise = pE.name_expertise;
                    tmpE.date_expertise = pE.date_expertise;

                    tmpE.ListExperts = new List<string>();
                    foreach (ExpertiseExpert EE in pE.ExpertiseExpert)
                    {
                        Experts Exp = db_AAZ.Experts.Where(o => o.id_expert == EE.id_expert).FirstOrDefault();
                        string S = string.Format("{0} {1} {2}", Exp.surname_expert, Exp.name_expert, Exp.patronymic_expert);
                        tmpE.ListExperts.Add(S);
                    }

                    tmpE.ListProject = new List<myCurrentexpertisesProject>();
                    foreach (ProjectExpertise PE in pE.ProjectExpertise)
                    {
                        Projects tmpProj = db_AAZ.Projects.Where(z => z.id_project == PE.id_project).FirstOrDefault();
                        myCurrentexpertisesProject Proj = new myCurrentexpertisesProject();
                        Proj.id_project = tmpProj.id_project;
                        Proj.name_project = tmpProj.name_project;
                        Proj.lead_project = tmpProj.lead_project;
                        Proj.grnti_project = tmpProj.grnti_project;
                        Proj.begin_project = tmpProj.begin_project;
                        Proj.email_project = tmpProj.email_project;
                        Proj.money_project = tmpProj.money_project;
                        Proj.email_project = tmpProj.email_project;
                        Proj.delete_project = tmpProj.delete_project;

                        tmpE.ListProject.Add(Proj);
                    }

                    result.Add(tmpE);
                }
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<myCurrentexpertises> result = new List<myCurrentexpertises>();
                myCurrentexpertises tmpE = new myCurrentexpertises();
                tmpE.id_expertise = -1;
                tmpE.name_expertise = "Содержимое не было получено";
                result.Add(tmpE);

                return result;
            }
        }

        public TablesForEditExpertise GetTabelsForEditExpertiseByID(int id_expertise)
        {
            try
            {
                TablesForEditExpertise result = new TablesForEditExpertise();
                Expertises tmpE = db_AAZ.Expertises.Where(p => p.id_expertise == id_expertise).FirstOrDefault();
                result.Expertise = new Expertises();
                result.Expertise.id_expertise = tmpE.id_expertise;
                result.Expertise.name_expertise = tmpE.name_expertise;
                result.Expertise.date_expertise = tmpE.date_expertise;
                result.Expertise.end_expertise = tmpE.end_expertise;
                result.Expertise.count_proj_expertise = tmpE.count_proj_expertise;
                result.Expertise.id_fos = tmpE.id_fos;
                result.Expertise.end_date_expertise = tmpE.end_date_expertise;

                result.lProjectExpertise = new List<ProjectExpertise>();
                foreach (ProjectExpertise pPE in tmpE.ProjectExpertise)
                {
                    ProjectExpertise tmpPE = new ProjectExpertise();
                    tmpPE.id_project_expertise = pPE.id_project_expertise;
                    tmpPE.id_expertise = pPE.id_expertise;
                    tmpPE.id_project = pPE.id_project;
                    tmpPE.accept = pPE.accept;
                    result.lProjectExpertise.Add(tmpPE);
                }

                result.lExpCrit = new List<ExpCrit>();
                foreach (ExpCrit pEC in tmpE.ExpCrit)
                {
                    ExpCrit tmpEC = new ExpCrit();
                    tmpEC.id_exp_crit = pEC.id_exp_crit;
                    tmpEC.id_exp = pEC.id_exp;
                    tmpEC.id_crit = pEC.id_crit;

                    result.lExpCrit.Add(tmpEC);
                }

                result.lExpertiseExpert = new List<ExpertiseExpert>();
                foreach (ExpertiseExpert pEE in tmpE.ExpertiseExpert)
                {
                    ExpertiseExpert tmpEE = new ExpertiseExpert();
                    tmpEE.id_expertise_expert = pEE.id_expertise_expert;
                    tmpEE.id_expertise = pEE.id_expertise;
                    tmpEE.id_expert = pEE.id_expert;

                    result.lExpertiseExpert.Add(tmpEE);
                }

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
                TablesForEditExpertise result = new TablesForEditExpertise();
                result.Err = true;
                return result;
            }
        }

        public bool SupportProject(int id_expertise, int[] arrIdProjects)
        {
            try
            {
                foreach (int idProj in arrIdProjects)
                {
                    ProjectExpertise P = db_AAZ.ProjectExpertise.Where(p => p.id_expertise == id_expertise).Where(p => p.id_project == idProj).FirstOrDefault();
                    P.accept = true;
                }
                Expertises E = db_AAZ.Expertises.Where(e => e.id_expertise == id_expertise).FirstOrDefault();
                E.end_expertise = true;
                E.end_date_expertise = DateTime.Now;

                db_AAZ.SaveChanges();
                return true;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return false;
            }
        }

        public bool UpdateProjectExpertise(int id_project_expertise, int id_expertise, int id_project, bool accept)
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



        public TablesForExpertise GetTablesForExpertise()
        {
            try
            {
                TablesForExpertise result = new TablesForExpertise();
                result.lFOS = GetListAllFiledsOfScience();
                result.lProjects = GetListNotExistProjects();
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

        public List<Projects> GetListNotExistProjects()
        {
            try
            {
                List<Projects> result = new List<Projects>();
                List<Projects> tmplP = db_AAZ.Projects.Where(p => p.exsist_exp_project == false).ToList();
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
            


        public myProject GetmyProjectsfForID(int id_project)
        {
            try
            {
                myProject result = new myProject();
                Projects pP = db_AAZ.Projects.Where(p => p.id_project == id_project).FirstOrDefault();

                result.id_project = pP.id_project;
                result.name_project = pP.name_project;
                result.lead_project = pP.lead_project;
                result.grnti_project = pP.grnti_project;
                result.begin_project = pP.begin_project;
                result.end_project = pP.end_project;
                result.org_project = pP.org_project;
                result.money_project = pP.money_project;
                result.email_project = pP.email_project;


                ProjectExpertise prexp = db_AAZ.ProjectExpertise.FirstOrDefault(o => o.id_project == pP.id_project);
                if (prexp != null)
                {
                    result.expertisa = true;
                    result.expertise = "Проводилась";
                    Expertises ex = db_AAZ.Expertises.FirstOrDefault(o => o.id_expertise == prexp.id_expertise);
                    result.date_expertise = ex.date_expertise;
                    result.name_expertise = ex.name_expertise;
                }
                else
                {
                    result.expertisa = false;
                    result.expertise = "Не проводилась";
                }
                ProjectFos prfos = db_AAZ.ProjectFos.FirstOrDefault(o => o.id_project == pP.id_project);
                if (prfos != null)
                {
                    FiledsOfScience fos = db_AAZ.FiledsOfScience.FirstOrDefault(o => o.id_fos == prfos.id_fos);
                    result.fos = fos.name_fos;
                }
                
                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                myProject result = new myProject();
                result.id_project = -1;
                return result;
            }
        }

        public ExpertsWithCountExpertise GetExpertsWithCountExpertise(int id_experts)
        {
            ExpertsWithCountExpertise result = new ExpertsWithCountExpertise();
            Experts pE = db_AAZ.Experts.Where(p => p.id_expert == id_experts).FirstOrDefault();
            int t = 0;

            if (pE.delete_expert == false)
            {
                result.id_expert = pE.id_expert;
                result.surname_expert = pE.surname_expert;
                result.name_expert = pE.name_expert;
                result.patronymic_expert = pE.patronymic_expert;
                result.FIO = pE.surname_expert + " " + pE.name_expert + " " + pE.patronymic_expert;
                result.job_expert = pE.job_expert;
                result.post_expert = pE.post_expert;
                result.degree_expert = pE.degree_expert;
                result.degree_rank_expert = pE.rank_expert + ", " + pE.degree_expert;
                result.rank_expert = pE.rank_expert;
                result.contacts_expert = pE.contacts_expert;
                result.login_expert = pE.login_expert;
                result.password_expert = pE.password_expert;
                result.commision_chairman = pE.comission_chairman;



                result.ListFOS = new List<FiledsOfScience>();
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

                        result.ListFOS.Add(temp);
                    }
                }
                else
                {
                    result.ListFOS.Clear();
                }
                if (pE.delete_expert) result.status = "Удалён"; else result.status = "Активен";
                //tmpE.delete_expert = pE.delete_expert;
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
                result.countexpertise = countexpertise.Count();
                result.number = t;
                t++;
            }
            
            return result;
        }


        #region Работа с критериями

        public List<CategoryForCritDirectory> GetListCategoryForCritDirectory()
        {
            try
            {
                List<CategoryForCritDirectory> result = new List<CategoryForCritDirectory>();
                List<Categories> tmpCatList = db_AAZ.Categories.ToList();
                foreach (Categories pCat in tmpCatList)
                {
                    CategoryForCritDirectory Category = new CategoryForCritDirectory();
                    Category.Category = new Categories();
                    Category.Category.id_category = pCat.id_category;
                    Category.Category.name_category = pCat.name_category;
                    Category.ListCriterions = new List<Criterions>();
                    foreach (CatCrit pCatCrit in pCat.CatCrit)
                    {
                        Criterions tmpCriterions = db_AAZ.Criterions.Where(p => p.id_crit == pCatCrit.id_crit).FirstOrDefault();
                        Criterions Crit = new Criterions();
                        Crit.id_crit = tmpCriterions.id_crit;
                        Crit.name_crit = tmpCriterions.name_crit;
                        Crit.qualit_crit = tmpCriterions.qualit_crit;
                        CritValues CritVal = new CritValues();
                        CritVal.id_value = tmpCriterions.CritValues.ToArray()[0].id_value;
                        CritVal.id_crit = tmpCriterions.CritValues.ToArray()[0].id_crit;
                        CritVal.valid_values = tmpCriterions.CritValues.ToArray()[0].valid_values;
                        Crit.CritValues.Add(CritVal);
                        Category.ListCriterions.Add(Crit);
                    }
                    result.Add(Category);
                }

                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                List<CategoryForCritDirectory> result = new List<CategoryForCritDirectory>();
                CategoryForCritDirectory Category = new CategoryForCritDirectory();
                Category.Category = new Categories();
                Category.Category.id_category = -1;
                Category.Category.name_category = "Содержимое не было получено";
                result.Add(Category);

                return result;
            }
        }

        public CritForCard GetCriterionsForCard(int id_crit)
        {
            try
            {
                CritForCard result = new CritForCard();
                Criterions tmpCrit = db_AAZ.Criterions.Where(p => p.id_crit == id_crit).FirstOrDefault();
                result.Criterions = new Criterions();
                result.Criterions.id_crit = tmpCrit.id_crit;
                result.Criterions.name_crit = tmpCrit.name_crit;
                result.Criterions.qualit_crit = tmpCrit.qualit_crit;

                result.CritValue = new CritValues();
                result.CritValue.id_value = tmpCrit.CritValues.ToArray()[0].id_value;
                result.CritValue.id_crit = tmpCrit.CritValues.ToArray()[0].id_crit;
                result.CritValue.valid_values = tmpCrit.CritValues.ToArray()[0].valid_values;

                CatCrit CC = tmpCrit.CatCrit.ToArray()[0];
                Categories C = db_AAZ.Categories.Where(o => o.id_category == CC.id_cat).FirstOrDefault();
                result.name_category = C.name_category;


                return result;
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                CritForCard result = new CritForCard();
                result.Criterions = new Criterions();
                result.Criterions.id_crit = -1;
                result.Criterions.name_crit = "Содержимое не было получено";

                return result;
            }
        }

        public EditCriterionsMessage EditCriterions(int id_crit, string name_crit, bool qualit_crit, string valid_values)
        {
            try
            {
                Criterions C = db_AAZ.Criterions.Where(p => p.id_crit == id_crit).FirstOrDefault();

                if (C.ExpCrit.Count() == 0) // Если экспертиз нет
                {
                    C.name_crit = name_crit;
                    C.qualit_crit = qualit_crit;
                    C.CritValues.ToArray()[0].valid_values = valid_values;
                    db_AAZ.SaveChanges();
                    return EditCriterionsMessage.Succes;
                }

                List<Expertises> lEndExp = new List<Expertises>();
                foreach (ExpCrit pEC in C.ExpCrit)
                {
                    Expertises E = db_AAZ.Expertises.Where(o => o.id_expertise == pEC.id_exp).FirstOrDefault();
                    if (!E.end_expertise) return EditCriterionsMessage.ErrParticipant;
                }

                if (C.qualit_crit != qualit_crit)
                {
                    return EditCriterionsMessage.ErrQualit;
                }
                else
                {
                    C.name_crit = name_crit;
                    C.CritValues.ToArray()[0].valid_values = valid_values;
                    db_AAZ.SaveChanges();
                    return EditCriterionsMessage.Succes;
                }
            }
            catch (Exception Ex)
            {
                // тут логируется ошибка
                return EditCriterionsMessage.ErrDataBase;
            }
        }

        #endregion


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
            ExpertiseExpert eee = db_AAZ.ExpertiseExpert.FirstOrDefault(o => o.id_expertise_expert == 7);

            db_AAZ.ExpertiseExpert.Remove(eee);

            ExpertiseExpert eee2 = db_AAZ.ExpertiseExpert.FirstOrDefault(o => o.id_expertise_expert == 8);

            db_AAZ.ExpertiseExpert.Remove(eee2);
            db_AAZ.SaveChanges();
        }

        public void UpdateExpertCard(int id_expert,string surname_expert, string name_expert, string patronymic_expert,
            string job_expert, string post_expert, string degree_expert, string rank_expert,
            Boolean delete_expert, string contacts_expert, int[] ListFOS, string login_expert, string password_expert, bool comission_chairman)
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
            updateexpert.login_expert = login_expert;
            updateexpert.password_expert = password_expert;
            updateexpert.comission_chairman = comission_chairman;
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

        //public bool EditCriterions(int id_crit, string name_crit, bool qualit_crit)
        //{
        //    try
        //    {
        //        Criterions C = db_AAZ.Criterions.Where(p => p.id_crit == id_crit).FirstOrDefault();
        //        C.name_crit = name_crit;
        //        C.qualit_crit = qualit_crit;

        //        db_AAZ.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception Ex)
        //    {
        //        // тут логируется ошибка
        //        return false;
        //    }
        //}

        //public bool EditCritValues(int id_value, int id_crit, string valid_values)
        //{
        //    try
        //    {
        //        CritValues CV = db_AAZ.CritValues.Where(p => p.id_value == id_value).FirstOrDefault();
        //        CV.id_crit = id_crit;
        //        CV.valid_values = valid_values;

        //        db_AAZ.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception Ex)
        //    {
        //        // тут логируется ошибка
        //        return false;
        //    }
        //}


        public void AddExpert(string surname_expert, string name_expert, string patronymic_expert,
           string job_expert, string post_expert, string degree_expert, string rank_expert
         , string contacts_expert, int[] ListFOS, string login_expert, string password_expert, bool comission_chairman)
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
            expert.login_expert = login_expert;
            expert.password_expert = password_expert;
            expert.comission_chairman = comission_chairman;
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
            DateTime end_project, string org_project, string money_project, string email_project, int[] listauthor, int fos)
        {
            try
            {
                Projects P = db_AAZ.Projects.FirstOrDefault(o=>o.id_project== id_project);
                P.name_project = name_project;
                P.lead_project = lead_project;
                P.grnti_project = grnti_project;
                P.begin_project = begin_project;
                P.end_project = end_project;
                P.org_project = org_project;
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


        //public List<myCurrentexpertises> GetListCurrentExpertises()
        //{
        //    List<myCurrentexpertises> result = new List<myCurrentexpertises>();
        //    List<Expertises> awd = db_AAZ.Expertises.ToList();
        //    List <Expertises> ListE = db_AAZ.Expertises.Where(o => o.end_expertise == false).ToList();
        //    int t = 1;
        //    for(int i = 0; i<ListE.Count; i++)
        //    {
        //        myCurrentexpertises temp = new myCurrentexpertises();
        //        temp.ListExperts = new List<string>();
        //        temp.number = t + i;
        //        temp.id_expertise = ListE[i].id_expertise;
        //        temp.end_expertise = ListE[i].end_expertise;
        //        temp.name_expertise = ListE[i].name_expertise;
        //        temp.date_expertise = ListE[i].date_expertise;
        //        int id_expertise = ListE[i].id_expertise;
        //        List<ProjectExpertise> listPE = db_AAZ.ProjectExpertise.Where(o => o.id_expertise == id_expertise).ToList();
        //        temp.count_project = listPE.Count;
        //        List<ExpertiseExpert> listEE = db_AAZ.ExpertiseExpert.Where(o=>o.id_expertise== id_expertise).ToList();

        //        for(int j = 0; j < listEE.Count; j++)
        //        {
        //            int id_expert = listEE[j].id_expert;
        //            Experts temp_expert = db_AAZ.Experts.FirstOrDefault(o => o.id_expert == id_expert);
        //            temp.ListExperts.Add(temp_expert.surname_expert + " " + temp_expert.name_expert + " " + temp_expert.patronymic_expert);
        //        }
        //        result.Add(temp);
        //    }
        //    return result;
        //}

        public List<Expertise_Expert> Expertise_Expert(int id_expert)
        {
            List<Expertise_Expert> result = new List<Expertise_Expert>();
            List<ExpertiseExpert> e = db_AAZ.ExpertiseExpert.ToList();
            List< ExpertiseExpert> EE=db_AAZ.ExpertiseExpert.Where( o => o.id_expert == id_expert).ToList();
            int t = 1;
            for(int i = 0; i < EE.Count; i++)
            {
                int id_expertise = EE[i].id_expertise;
                ProjectExpertise PE = db_AAZ.ProjectExpertise.FirstOrDefault(o => o.id_expertise == id_expertise);
                int id_projecttemp = PE.id_project;
                ProjectFos PF = db_AAZ.ProjectFos.FirstOrDefault(o => o.id_project == id_projecttemp);
                int id_fos = PF.id_fos;
                FiledsOfScience FOS = db_AAZ.FiledsOfScience.FirstOrDefault(o => o.id_fos == id_fos);
                Expertise_Expert temp = new Expertise_Expert();
                temp.name_fos = FOS.name_fos;
                temp.id_expertise = id_expertise;
                Expertises E = db_AAZ.Expertises.FirstOrDefault(o => o.id_expertise == id_expertise);
                temp.name_expertise = E.name_expertise;
                temp.date_expertise = E.date_expertise;
                temp.number = t + i;
                if (E.end_expertise == true)
                {
                    temp.status_expertise = "Завершена";
                }
                else
                {
                    temp.status_expertise = "В работе";
                }
                
                List<ProjectExpertise> ListPE= db_AAZ.ProjectExpertise.Where(o => o.id_expertise == id_expertise).ToList();
                for(int j = 0; j < ListPE.Count; j++)
                {
                    if (ListPE[j].accept == true)
                    {
                        int id_project = ListPE[j].id_project;
                        Projects P = db_AAZ.Projects.FirstOrDefault(o => o.id_project == id_project);
                        temp.victory_project.Add(P.name_project);
                    }
                }
                result.Add(temp);
            }
            return result;
           

        }

        public bool AddProjects(string name_project, string lead_project, string grnti_project, DateTime begin_project, DateTime end_project, string org_project, string money_project, string email_project,int[] listauthor,int fos)
        {
            try
            {
                Projects P = new Projects();
                P.name_project = name_project;
                P.lead_project = lead_project;
                P.grnti_project = grnti_project;
                P.begin_project = begin_project;
                P.end_project = end_project;
                P.org_project = org_project;
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
                            expert.comission_chairman = tmpExp.comission_chairman;

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
