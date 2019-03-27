using TestingSystem.Data.Repositories;
using TestingSystem.Models;

namespace TestingSystem.Sevice
{
    public interface ICandidateService
    {
        int AddCandidate(Candidate candidate);
    }
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository candidateRepository;
        public CandidateService(ICandidateRepository candidateRepository)
        {
            this.candidateRepository = candidateRepository;
        }
        public int AddCandidate(Candidate candidate)
        {
            return candidateRepository.AddCandidate(candidate);
        }
    }
}
