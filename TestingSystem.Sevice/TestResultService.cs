using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Repositories;
using TestingSystem.Models;

namespace TestingSystem.Sevice
{
    public interface ITestResultService
    {
        int AddTestResult(TestResult testResult);
        IEnumerable<TestResult> GetALl();
        IEnumerable<TestResult> GetQuestionByCount(int countQ);
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

        public IEnumerable<TestResult> GetALl()
        {
            return testResultRepository.GetALl();
        }

        public IEnumerable<TestResult> GetQuestionByCount(int countQ)
        {
            return testResultRepository.GetQuestionByCount(countQ);
        }
    }
}
