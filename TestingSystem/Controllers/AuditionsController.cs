﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using TestingSystem.Models;
using TestingSystem.Sevice;

namespace TestingSystem.Controllers
{
	public class AuditionsController : Controller
	{
		public string Success { set { TempData["Success"] = ViewData["Success"] = value; } }
		public string Failure { set { TempData["Failure"] = ViewData["Failure"] = value; } }

		private readonly IExamPaperService examPaperService;
		private readonly IQuestionService questionService;
		private readonly IAnswerService answerService;
		private readonly IExamPaperQuestionService examPaperQuestionService;
		private readonly ITestService testService;
		private readonly IQuestionCategorySevice questionCategorySevice;

		public AuditionsController(
			IExamPaperService examPaperService,
			IQuestionService questionService,
			IAnswerService answerService,
			IExamPaperQuestionService examPaperQuestionService,
			IQuestionCategorySevice questionCategorySevice,
			ITestService testService)
		{
			this.examPaperService = examPaperService;
			this.questionService = questionService;
			this.answerService = answerService;
			this.examPaperQuestionService = examPaperQuestionService;
			this.questionCategorySevice = questionCategorySevice;
			this.testService = testService;
		}

		public ActionResult AuditionsTest()
		{
			return View();
		}

		public ActionResult MyAuditionsTest()
		{

			return View();
		}

		[HttpPost]
		public ActionResult MyAuditionsTest(string code)
		{
			
			try
			{
				var model = testService.GetAllTetByExamCode(code);
				if (model != null)
				{
					return View(model);
				}
			}
			catch (Exception e)
			{
				Failure = "Code not exist!";
				return RedirectToAction("AuditionsTest");

			}

			return View();

		}
		public ActionResult ShowExamPaperById(int idExamPaper)
		{
			var listExamPaperQuesions = questionService.GetQuestionsByExamPaperId(idExamPaper);
			var countQuestion = listExamPaperQuesions.Count();
			ViewBag.CountQuestion = countQuestion;
			ViewBag.Time = examPaperService.GetExamPaperById(idExamPaper).Time;
			List<Answer> listAnswer = new List<Answer>();
			List<Answer> listAnswerInExamPaper = new List<Answer>();
			foreach (var item in listExamPaperQuesions)
			{
				listAnswerInExamPaper.AddRange(answerService.GetAnswersByQuestionID(item.QuestionID));
			}
			List<Answer> listA = new List<Answer>();
			ViewBag.ListQuestion = listExamPaperQuesions;
			ViewBag.ListAnswer = listAnswerInExamPaper;
			ViewBag.IdExamPaper = idExamPaper;
			TempData["idExamPaper"] = idExamPaper;
			return View();
		}
	}
}