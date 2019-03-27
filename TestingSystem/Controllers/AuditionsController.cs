using System;
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
			var model = testService.GetAllTetByExamCode(code);
			try
			{
				if (model != null)
				{
					return View(model);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			return View();

		}
	}
}