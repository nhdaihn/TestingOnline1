using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Data.Repositories;
using TestingSystem.Models;

namespace TestingSystem.Sevice
{
	public interface IExamService
	{
		IEnumerable<Exam> GetAllExams();
		bool UpdateExam(Exam exam);
		int AddExam(Exam exam);
		int DeleteExam(int id);
		Exam GetExamsByID(int id);
		IEnumerable<Exam> SearchExams(string txtSearch);
		//IEnumerable<ExamPaper> GetExamPaperByExamID(int examID);
		//int RemoveExamPaperInExams(int id);
		//int AddExamPaperIntoExams(int examPaperID, int examID);
	}

	public class ExamService : IExamService
	{
		private readonly IExamRepository examRepository;
		private readonly IUnitOfWork unitOfWork;

		public ExamService(IExamRepository examRepository, IUnitOfWork unitOfWork)
		{
			this.examRepository = examRepository;
			this.unitOfWork = unitOfWork;
		}
		public int AddExam(Exam exam)
		{
			return examRepository.AddExam(exam);
		}

		public int DeleteExam(int id)
		{
			return examRepository.DeleteExam(id);
		}

		public IEnumerable<Exam> GetAllExams()
		{
			return examRepository.GetAllExams();
		}

		public Exam GetExamsByID(int id)
		{
			return examRepository.GetExamsByID(id);
		}

		public IEnumerable<Exam> SearchExams(string txtSearch)
		{
			return examRepository.SearchExams(txtSearch);
		}

		//public IEnumerable<ExamPaper> GetExamPaperByExamID(int examID)
		//{
		//	return examRepository.GetExamPaperByExamID(examID);
		//}

		//public int RemoveExamPaperInExams(int id)
		//{
		//	return examRepository.RemoveExamPaperInExams(id);
		//}

		//public int AddExamPaperIntoExams(int examPaperID, int examID)
		//{
		//	return examRepository.AddExamPaperIntoExams(examPaperID, examID);
		//}

		public bool UpdateExam(Exam exam)
		{
			return examRepository.UpdateExam(exam);
		}
	}
}
