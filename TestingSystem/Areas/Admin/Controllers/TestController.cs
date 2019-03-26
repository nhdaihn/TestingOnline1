using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSystem.BaseController;
using TestingSystem.Data.Repositories;
using TestingSystem.Models;
using TestingSystem.Sevice;

namespace TestingSystem.Areas.Admin.Controllers
{
    public class TestController : AdminController
    {
	    private readonly ITestService testService;

        private readonly IExamPaperService examPaperService;

		public TestController(ITestService testService, IExamPaperService examPaperService,IUserService user):base(user)
		{
			this.testService = testService;
            this.examPaperService = examPaperService;
		}
        // GET: Admin/Test
        public ActionResult Index()
        {
	        var model = testService.GetAllTests();
            return View(model);
        }
        public ActionResult Create()
        {
            var model = examPaperService.GetAll();
            ViewBag.ExamPaperId = new SelectList(model, "ExamPaperId", "Title");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Test test, int Status1)
        {
            test.Status = true;//
            if (Status1 == 1)
            {
                test.IsActive = true;
            }
            else if(Status1 == 2)
            {
                test.IsActive = false;
            }
            test.CreatedBy= int.Parse(Session["Name"].ToString());
            try
            {
                if (testService.AddTest(test) > 0)
                {
                    Success = "Insert Exam successfully!";
                    return RedirectToAction("Index", "Test");
                }
                else
                {
                    Failure = "Something went wrong, please try again!";
                    return RedirectToAction("Create", "Test");
                }
            }
            catch (Exception e)
            {
                Failure = "Something went wrong, please try again!";
                return RedirectToAction("Create", "Test");
            }
        }
        public JsonResult _CheckExamsAvailableCreate(string userdata)
        {
            try
            {
                var searchData = testService.SearchExams(userdata);
                if (searchData.Count() > 0)
                {
                    return Json(1);
                }
                else
                {
                    return Json(0);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}