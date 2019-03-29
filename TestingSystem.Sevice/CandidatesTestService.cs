using System.Collections.Generic;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Data.Repositories;
using TestingSystem.Models;

namespace TestingSystem.Sevice
{
	public interface ICandidatesTestService
	{
		IEnumerable<Candidate> GetAllCandidatesByTestID(int testID);
		int AddCandidatesIntoTest(int candidatesID, int testID);
		int RemoveCadidatesFromTest(int cadidatesID);
	}
	public class CandidatesTestService : ICandidatesTestService
	{
		private readonly ICandidatesTestRepository _candidatesTestRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CandidatesTestService(ICandidatesTestRepository candidatesTestRepository, IUnitOfWork unitOfWork)
		{
			_candidatesTestRepository = candidatesTestRepository;
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<Candidate> GetAllCandidatesByTestID(int testID)
		{
			return _candidatesTestRepository.GetAllCandidatesByTestID(testID);
		}

		public int AddCandidatesIntoTest(int candidatesID, int testID)
		{
			return _candidatesTestRepository.AddCandidatesIntoTest(candidatesID, testID);
		}

		public int RemoveCadidatesFromTest(int cadidatesID)
		{
			return _candidatesTestRepository.RemoveCadidatesFromTest(cadidatesID);
		}
	}
}
