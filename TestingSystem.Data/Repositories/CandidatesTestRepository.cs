﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Models;

namespace TestingSystem.Data.Repositories
{
	public interface ICandidatesTestRepository : IRepository<CandidatesTest>
	{
		IEnumerable<Candidate> GetAllCandidatesByTestID(int testID);

		int AddCandidatesIntoTest(int candidatesID, int testID);
		int RemoveCadidatesFromTest(int cadidatesID);
	}
	public class CandidatesTestRepository : RepositoryBase<CandidatesTest>, ICandidatesTestRepository
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public CandidatesTestRepository(IDbFactory dbFactory) : base(dbFactory)
		{

		}

		public IEnumerable<Candidate> GetAllCandidatesByTestID(int testID)
		{
			var listCandidatesTestByTestID = DbContext.CandidatesTests.Where(x => x.TestID == testID).ToList();
			List<Candidate> listCandidates = new List<Candidate>();
			foreach (var item in listCandidatesTestByTestID)
			{
				var candidate = DbContext.Candidates.SingleOrDefault(x => x.CandidateID == item.CandidateID);
				listCandidates.Add(candidate);
			}

			return listCandidates.AsEnumerable();

		}

		public int AddCandidatesIntoTest(int candidatesID, int testID)
		{
			try
			{
				CandidatesTest candidatesTest = new CandidatesTest();
				candidatesTest.CandidateID = candidatesID;
				candidatesTest.TestID = testID;
				DbContext.CandidatesTests.Add(candidatesTest);
				DbContext.SaveChanges();
				return candidatesTest.CandidatesTestID;
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return 0;
			}
		}

		public int RemoveCadidatesFromTest(int cadidatesID)
		{
			try
			{
				var candidates = DbContext.CandidatesTests.FirstOrDefault(x => x.CandidateID==cadidatesID);
				if (candidates != null)
				{
					this.DbContext.CandidatesTests.Remove(candidates);
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