﻿using System;
using System.Collections.Generic;
using System.Linq;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Models;
using TestingSystem.DataTranferObject;
using System.Text;

namespace TestingSystem.Data.Repositories
{

    public interface IExamPaperRepository : IRepository<ExamPaper>
    {
        IQueryable<ExamPaper> Filter(ExamPaperFilterModel examPaperFilterModel);
        List<ExamPaper> Search(string keySearch);
        int Create(ExamPaper examPaper);
        int Edit(ExamPaper examPaper);
        ExamPaper FindById(int id);
        int Delete(int id);
        //IEnumerable<ExamPaper> GetExamPaperByCode(int ExamId, string code);
        IEnumerable<ExamPaper> ListExamPapersTop();

        int GetCode(ExamPaper examPaper);

        int GetNumberOfQuestionByExamPaperId(int examPaperId);

        ExamPaper FindCode(string code);
    }

    public class ExamPaperRepository : RepositoryBase<ExamPaper>, IExamPaperRepository
    {
        public ExamPaperRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public int Create(ExamPaper examPaper)
        {
            try
            {
                DbContext.ExamPapers.Add(examPaper);
                if (DbContext.SaveChanges() > 0)
                {
                    return examPaper.ExamPaperID;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public int Edit(ExamPaper examPaper)
        {
            try
            {
                ExamPaper exam = new ExamPaper();
                exam = DbContext.ExamPapers.Find(examPaper.ExamPaperID);
                exam.Title = examPaper.Title;
                exam.NumberOfQuestion = examPaper.NumberOfQuestion;
                exam.Time = examPaper.Time;
                exam.Status = examPaper.Status;
                exam.IsActive = examPaper.IsActive;
                exam.ModifiedBy = examPaper.ModifiedBy;
                exam.ModifiedDate = examPaper.ModifiedDate;
                return DbContext.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public int Delete(int id)
        {
            try
            {
                ExamPaper objExamPaper = DbContext.ExamPapers.Find(id);
                if (objExamPaper != null)
                {
                    DbContext.ExamPapers.Remove(objExamPaper);
                    return DbContext.SaveChanges();
                }
                return 0;
            }

            catch (Exception e)
            {
                Console.Write(e);
                throw;
            }
        }

        public IQueryable<ExamPaper> Filter(ExamPaperFilterModel examPaperFilterModel)
        {
            try
            {
                var result = DbContext.ExamPapers.AsQueryable();
                if (examPaperFilterModel != null)
                {
                    if (examPaperFilterModel.NumberOfQuestion.HasValue)
                    {
                        result = result.Where(x => x.NumberOfQuestion == examPaperFilterModel.NumberOfQuestion);
                    }

                    if (examPaperFilterModel.CreatedBy.HasValue)
                    {
                        result = result.Where(x => x.CreatedBy == examPaperFilterModel.CreatedBy);
                    }

                    if (examPaperFilterModel.CreatedDate.HasValue)
                    {
                        result = result.Where(x => x.CreatedDate == examPaperFilterModel.CreatedDate);
                    }

                    if (examPaperFilterModel.Status.HasValue)
                    {
                        result = result.Where(x => x.Status == examPaperFilterModel.Status);
                    }

                    if (examPaperFilterModel.Time.HasValue)
                    {
                        result = result.Where(x => x.Time == examPaperFilterModel.Time);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<ExamPaper> Search(string keySearch)
        {
            try
            {
                List<ExamPaper> listeExamPapers = new List<ExamPaper>();
                listeExamPapers = DbContext.ExamPapers.Where(x => x.Title.Contains(keySearch)).ToList();
                return listeExamPapers;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
                throw;
            }
        }

        ExamPaper IExamPaperRepository.FindById(int id)
        {

            try
            {
                ExamPaper exam = new ExamPaper();
                exam = DbContext.ExamPapers.Find(id);
                return exam;
            }
            catch (Exception e)
            {
                e.Message.ToString();
                throw;
            }
        }

        public int GetNumberOfQuestionByExamPaperId(int examPaperId)
        {
            try
            {
                return DbContext.ExamPaperQuesions.Where(e => e.ExamPaperID == examPaperId).Count();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

		public IEnumerable<ExamPaper> ListExamPapersTop()
		{
			return DbContext.ExamPapers.OrderByDescending(x => x.CreatedDate).Take(6).ToList();
		}

        //public IEnumerable<ExamPaper> GetExamPaperByCode(int ExamId, string code)
        //{
        //    var listTestByExamPaperID = DbContext.Exx.Where(x => x.ExamID == ExamId).ToList();
        //    List<ExamPaper> listExamPapers = new List<ExamPaper>();
        //    foreach (var item in listTestByExamPaperID)
        //    {
        //        var examPaper = DbContext.ExamPapers.SingleOrDefault(x => x.ExamPaperID == item.ExamPaperID);
        //        listExamPapers.Add(examPaper);
        //    }
        //    return listExamPapers.AsEnumerable();
        //}

        public ExamPaper FindCode(string code)
        {
            return DbContext.ExamPapers.SingleOrDefault(x => x.ExamPaperCode == code);
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public int GetCode(ExamPaper examPaper)
        {
            ExamPaper exam = new ExamPaper();
            exam = DbContext.ExamPapers.Find(examPaper.ExamPaperID);
            var examcode = RandomString(10, true);
            exam.ExamPaperCode = examcode;
            examcode = examPaper.ExamPaperCode;
            return DbContext.SaveChanges();
        }

        //public IEnumerable<ExamPaper> GetExamPaperByCode(string code)
        //{
        //    var listTestByExamPaperID = DbContext.Tests.Where(x => x.ExamID == examID).ToList();
        //    List<ExamPaper> listExamPapers = new List<ExamPaper>();
        //    foreach (var item in listTestByExamPaperID)
        //    {
        //        var examPaper = DbContext.ExamPapers.SingleOrDefault(x => x.ExamPaperID == item.ExamPaperID);
        //        listExamPapers.Add(examPaper);
        //    }

        //    return listExamPapers.AsEnumerable();
        //}
    }
}