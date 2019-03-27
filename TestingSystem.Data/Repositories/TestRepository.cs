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
		IEnumerable<Test> GetAllTestIsActive();
		IEnumerable<Test> GetAllTestIsActiveByKeySearch(string keySearch);

        IEnumerable<Test> SearchExams(string txtSearch);

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
                entity.Description = "good";
				entity.CreateDate = DateTime.Now;
				DbContext.Tests.Add(entity);
                return DbContext.SaveChanges();

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

		public IEnumerable<Test> GetAllTestIsActive()
		{
			var listTestActive = DbContext.Tests.Where(x => x.IsActive == true).ToList();
			return listTestActive.AsEnumerable();
		}

		public IEnumerable<Test> GetAllTestIsActiveByKeySearch(string keySearch)
		{
			var listTestActiveByKey = DbContext.Tests.Where(x => x.TestName.Contains(keySearch) && x.IsActive == true);
			return listTestActiveByKey;
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
        public IEnumerable<Test> SearchExams(string txtSearch)
        {
            var listTest = DbContext.Tests.Where(x => x.TestName.Contains(txtSearch)).ToList();
            return listTest;
        }
        public bool UpdateTest(Test entity)
		{
			try
			{
				var objTest = this.DbContext.Tests.Find(entity.TestID);
				if (objTest != null)
				{
                    objTest.TestName = entity.TestName;
					objTest.Description = entity.Description;
					objTest.IsActive = entity.IsActive;
					objTest.PassingScore = entity.PassingScore;
                    objTest.ModifiedDate = DateTime.Now;
                    objTest.StartDate = entity.StartDate;
                    objTest.EndDate = entity.EndDate;
                    objTest.ExamPaperID = entity.ExamPaperID;
                    objTest.Status = entity.Status;
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
