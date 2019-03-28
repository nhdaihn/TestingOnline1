using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Models;

namespace TestingSystem.Data.Repositories
{
    public interface ITestResultRepository : IRepository<TestResult>
    {
        int AddTestResult(TestResult testResult);
        IEnumerable<TestResult> GetQuestionByCount(int countQ);
        IEnumerable<TestResult> GetALl();
    }
    public class TestResultRepository : RepositoryBase<TestResult>, ITestResultRepository
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public TestResultRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        public int AddTestResult(TestResult testResult)
        {
            try
            {
                DbContext.TestResults.Add(testResult);
                DbContext.SaveChanges();
                return testResult.TestResultID;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                return 0;
            }
        }
        public IEnumerable<TestResult> GetQuestionByCount(int countQ)
        {
            var listQ = DbContext.TestResults.OrderByDescending(s => s.TestResultID).Take(countQ).ToList();
            return listQ;
        }

        public IEnumerable<TestResult> GetALl()
        {
            
            var model = DbContext.TestResults.ToList();
            
            return model;
        }
    }
}
