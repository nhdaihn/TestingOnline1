using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Repositories;
using TestingSystem.Models;

namespace TestingSystem.Sevice
{
	public interface ITestService
	{
		IEnumerable<Test> GetAllTests();
		bool UpdateTest(Test test);
		int AddTest(Test test);
		int DeleteTest(int id);
		Test GetTestByID(int id);
	}
	public class TestService : ITestService
	{
		private readonly ITestRepository testRepository;
		public TestService(ITestRepository TestRepository)
		{
			this.testRepository = TestRepository;
		}

		public int AddTest(Test test)
		{
			return testRepository.AddTest(test);
		}

		public int DeleteTest(int id)
		{
			return testRepository.DeleteTest(id);
		}

		public IEnumerable<Test> GetAllTests()
		{
			return testRepository.GetAllTest();
		}

		public Test GetTestByID(int id)
		{
			return testRepository.GetTestByID(id);
		}

		public bool UpdateTest(Test test)
		{
			return testRepository.UpdateTest(test);
		}
	}
}
