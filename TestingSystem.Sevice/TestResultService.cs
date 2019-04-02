using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Repositories;
using TestingSystem.DataTranferObject;
using TestingSystem.Models;

namespace TestingSystem.Sevice
{
    public interface ITestResultService
    {
        int AddTestResult(TestResult testResult);
        IEnumerable<TestResult> GetALl(int Id);
        IEnumerable<TestResult> GetQuestionByCount(int countQ);
        int ReturnTurn(int testId, DateTime dateTest);
        IEnumerable<ReviewTestResult> ListAllTestByDedicateId(int dedicateId);
        IEnumerable<ResultCheckId> ListAllQuestionIdAndAnswerIdByTestIdChecked(int testId, int turn);

        int CountUsed(int Id);
    }
    public class TestResultService : ITestResultService
    {
        private readonly ITestResultRepository testResultRepository;
        public TestResultService(ITestResultRepository testResultRepository)
        {
            this.testResultRepository = testResultRepository;
        }
        public int AddTestResult(TestResult testResult)
        {
            return testResultRepository.AddTestResult(testResult);
        }

        public int CountUsed(int Id)
        {
            return testResultRepository.CountUsed(Id);
        }

        public IEnumerable<TestResult> GetALl(int Id)
        {
            return testResultRepository.GetALl(Id);
        }

        public IEnumerable<TestResult> GetQuestionByCount(int countQ)
        {
            return testResultRepository.GetQuestionByCount(countQ);
        }

        public IEnumerable<ResultCheckId> ListAllQuestionIdAndAnswerIdByTestIdChecked(int testId, int turn)
        {
            return testResultRepository.ListAllQuestionIdAndAnswerIdByTestIdChecked(testId, turn);
        }

        public IEnumerable<ReviewTestResult> ListAllTestByDedicateId(int dedicateId)
        {
            return testResultRepository.ListAllTestByDedicateId(dedicateId);
        }

        public int ReturnTurn(int testId, DateTime dateTest)
        {
            return testResultRepository.ReturnTurn(testId, dateTest);
        }
    }
}
