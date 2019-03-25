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
<<<<<<< HEAD
	public class ExamController :ClientController
	{
		private readonly IExamPaperService examPaperService;
		public ExamController(IExamPaperService examPaperService, IUserService userService):base(userService)
		{
			this.examPaperService = examPaperService;
		}
=======
	public class ExamController : ClientController
	{
		private readonly IExamPaperService examPaperService;
        private readonly IExamPaperExamService examPaperExamService;
		public ExamController(IExamPaperService examPaperService, IUserService userService, IExamPaperExamService examPaperExamService):base(userService)
		{
            this.examPaperExamService = examPaperExamService;

            this.examPaperService = examPaperService;
        }
>>>>>>> origin/vananh
		// GET: Exam
		public ActionResult Index()
		{
			var model = examPaperService.ListExamPapersTop();
<<<<<<< HEAD
			return PartialView(model);
=======

            return PartialView(model);
>>>>>>> origin/vananh
		}

	}
}