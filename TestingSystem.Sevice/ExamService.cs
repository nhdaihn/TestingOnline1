﻿using System;
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

		public bool UpdateExam(Exam exam)
		{
			return examRepository.UpdateExam(exam);
		}
	}
}
