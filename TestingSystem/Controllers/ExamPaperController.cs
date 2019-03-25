using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSystem.BaseController;
using TestingSystem.Sevice;

namespace TestingSystem.Controllers
{
    public class ExamPaperController : AdminController
    {
	    public IExamPaperService examPaperService;
		// GET: ExamPaper
		public ExamPaperController(IExamPaperService examPaperService, IUserService userService):base(userService)
		{
			this.examPaperService = examPaperService;
		}
        public ActionResult PartialExamPaper()
        {
	        var model = examPaperService.ListExamPapersTop();
            return PartialView(model);
        }
    }
}