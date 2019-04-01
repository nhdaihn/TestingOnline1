using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Infrastructure;
using TestingSystem.DataTranferObject;
using TestingSystem.Models;

namespace TestingSystem.Data.Repositories
{
    public interface ITestResultRepository : IRepository<TestResult>
    {
        int AddTestResult(TestResult testResult);
        IEnumerable<TestResult> GetQuestionByCount(int countQ);
        IEnumerable<TestResult> GetALl(int Id);
        int ReturnTurn(int testId, DateTime dateTest);
        IEnumerable<ReviewTestResult> ListAllTestByDedicateId(int dedicateId);
        IEnumerable<ResultCheckId> ListAllQuestionIdAndAnswerIdByTestIdChecked(int testId, int turn);
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

        public IEnumerable<TestResult> GetALl(int Id)
        {
            var model = DbContext.TestResults.Where(x => x.TestID == Id).GroupBy(x => new { x.TestID, x.Turns }).Select(x => x.FirstOrDefault()).ToList();
            return model.Distinct().AsEnumerable();
        }
        public IEnumerable<ReviewTestResult> ListAllTestByDedicateId(int dedicateId)
        {
            var listAllTestResult = this.DbContext.TestResults.ToList();
            var listAllTestResultDTO = new List<ReviewTestResult>();
            int i = 0;
            foreach (var item in listAllTestResult)
            {
                if (i != item.Turns)
                {
                    ReviewTestResult obj = new ReviewTestResult();
                    obj.TestId = item.TestID;
                    obj.TestName = item.TestName;
                    obj.numRank = item.Turns;
                    obj.dateTest = item.CreatedDate;
                    i = item.Turns;
                    listAllTestResultDTO.Add(obj);
                }
            }
            return listAllTestResultDTO;
        }

        public int ReturnTurn(int testId, DateTime dateTest)
        {
            var item = this.DbContext.TestResults.Where(s => s.TestID == testId && s.CreatedDate.Month == dateTest.Month && s.CreatedDate.Year == dateTest.Year && s.CreatedDate.Day == dateTest.Day).OrderByDescending(s => s.CreatedDate).Take(1).FirstOrDefault();
            if (item != null)
                return item.Turns;
            else return 0;
        }

        public IEnumerable<ResultCheckId> ListAllQuestionIdAndAnswerIdByTestIdChecked(int testId, int turn)
        {
            var list = this.DbContext.TestResults.Where(s => s.TestID == testId && s.Turns == turn).ToList();
            List<ResultCheckId> listResultCheckId = new List<ResultCheckId>();
            foreach (var item in list)
            {
                ResultCheckId obj = new ResultCheckId();
                obj.QuestionId = item.QuestionID;
                obj.AnswerId = item.AnswerID;
                listResultCheckId.Add(obj);
            }
            return listResultCheckId;
        }
    }
}
