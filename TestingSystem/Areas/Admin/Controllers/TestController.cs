﻿using System;
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
        public ActionResult Create(Test test)
        {
          
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
        public ActionResult Edit(int Id)
        {
            var test = testService.GetTestByID(Id);
            var model = examPaperService.GetAll();
            ViewBag.ExamPaperId = new SelectList(model, "ExamPaperId", "Title");
            return View(test);
        }
        [HttpPost]
        public ActionResult Edit(Test test)
        {
            try
            {
                if (testService.UpdateTest(test))
                {
                    Success = "Update Test successfully!";
                    return RedirectToAction("Index", "Test");
                }
                else
                {
                    Failure = "Something went wrong, please try again!";
                    return RedirectToAction("Edit", "Test");
                }
            }
            catch (Exception e)
            {
                Failure = "Something went wrong, please try again!";
                return RedirectToAction("Edit", "Test");
            }
        }

        public ActionResult Delete(List<int> ids)
        {
            try
            {
                if (ids.Count > 0)
                {
                    int i = 0;
                    foreach (var id in ids)
                    {
                        if (testService.DeleteTest(id) > 0)
                        {
                            i++;
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (i > 0)
                    {
                        Success = "Delete test successfully!";
                        return RedirectToAction("Index", "Test");
                    }
                }
                Failure = "Something went wrong, please try again!";
                return RedirectToAction("Index", "Test");
            }
            catch (System.Exception exception)
            {
                Failure = exception.Message;
                return RedirectToAction("Index", "Test");
            }
        }
    }
}