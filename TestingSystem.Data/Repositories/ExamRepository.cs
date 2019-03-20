﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Models;

namespace TestingSystem.Data.Repositories
{
	public interface IExamRepository : IRepository<Exam>
	{
		IEnumerable<Exam> GetAllExams();
		bool UpdateExam(Exam exam);
		Exam GetExamsByID(int id);
		int AddExam(Exam exam);
		int DeleteExam(int id);

	}
	public class ExamRepository : RepositoryBase<Exam>, IExamRepository
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public ExamRepository(IDbFactory dbFactory) : base(dbFactory)
		{

		}

		public IEnumerable<Exam> GetAllExams()
		{
			try
			{
				var listExam = DbContext.Exams.ToList();
				return listExam;
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return null;
			}

		}

		public bool UpdateExam(Exam exam)
		{
			try
			{
				var objExam = this.DbContext.Exams.Find(exam.ExamID);
				if (objExam != null)
				{
					objExam.ExamName = exam.ExamName;
					objExam.ExamCode = exam.ExamCode;
					objExam.Description = exam.Description;
					objExam.StartDate = exam.StartDate;
					objExam.EndDate = exam.EndDate;
					objExam.CreateDate = objExam.CreateDate;
					objExam.Status = exam.Status;

					this.DbContext.SaveChanges();
					return true;
				}
				return false;
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return false;
			}

		}

		public Exam GetExamsByID(int id)
		{
			try
			{
				var exam = DbContext.Exams.SingleOrDefault(x => x.ExamID == id);
				return exam;
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return null;
			}
			
		}

		public int AddExam(Exam exam)
		{
			try
			{
				exam.ExamCode = Guid.NewGuid().ToString("n");
				exam.CreateDate = DateTime.Now;
				DbContext.Exams.Add(exam);
				DbContext.SaveChanges();
				return exam.ExamID;
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return 0;
			}

		}

		public int DeleteExam(int id)
		{
			try
			{
				var exam = DbContext.Exams.Find(id);
				if (exam != null)
				{
					this.DbContext.Exams.Remove(exam);
					return DbContext.SaveChanges();
				}
				else
				{
					return 0;
				}
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return 0;
			}

		}
	}
}
