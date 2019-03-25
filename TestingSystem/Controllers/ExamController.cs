using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSystem.BaseController;
using TestingSystem.Data.Repositories;
using TestingSystem.Sevice;

namespace TestingSystem.Controllers
{
	public class ExamController : ClientController
	{
		private readonly IExamPaperService examPaperService;
        private readonly IExamPaperExamService examPaperExamService;
		public ExamController(IExamPaperService examPaperService, IUserService userService, IExamPaperExamService examPaperExamService):base(userService)
		{
            this.examPaperExamService = examPaperExamService;

            this.examPaperService = examPaperService;
        }
		// GET: Exam
		public ActionResult Index()
		{
			var model = examPaperService.ListExamPapersTop();

            return PartialView(model);
		}

	}
}