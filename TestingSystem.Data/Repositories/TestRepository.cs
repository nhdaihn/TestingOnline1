using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Models;

namespace TestingSystem.Data.Repositories
{
	public interface ITestRepository :IRepository<Test>
	{
		IEnumerable<Test> GetAllTest();
		bool UpdateTest(Test entity);
		Test GetTestByID(int id);
		int AddTest(Test entity);
		int DeleteTest(int id);
	}
	public class TestRepository : RepositoryBase<Test>, ITestRepository
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public TestRepository(IDbFactory dbFactory) : base(dbFactory)
		{

		}
		public int AddTest(Test entity)
		{
			try
			{
				entity.CreateDate = DateTime.Now;
				DbContext.Tests.Add(entity);
				DbContext.SaveChanges();
				return entity.ExamID;
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return 0;
			}
		}

		public int DeleteTest(int id)
		{
			try
			{
				var test = DbContext.Tests.Find(id);
				if (test != null)
				{
					this.DbContext.Tests.Remove(test);
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

		public IEnumerable<Test> GetAllTest()
		{
			try
			{
				var listTest = DbContext.Tests.ToList();
				return listTest;
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return null;
			}
		}

		public Test GetTestByID(int id)
		{
			var model = DbContext.Tests.Find(id);
			return model;
		}

		public bool UpdateTest(Test entity)
		{
			try
			{
				var objExam = this.DbContext.Tests.Find(entity.TestID);
				if (objExam != null)
				{
					objExam.TestName = entity.TestName;
					objExam.CreateDate = DateTime.Now;
					objExam.Description = entity.Description;
					objExam.StartDate = entity.StartDate;
					objExam.EndDate = entity.EndDate;
					objExam.Status = entity.Status;
					objExam.PassingScore = entity.PassingScore;
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
	}
}
