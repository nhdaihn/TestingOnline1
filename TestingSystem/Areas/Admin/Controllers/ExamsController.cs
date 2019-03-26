﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using TestingSystem.BaseController;
using TestingSystem.Models;
using TestingSystem.Sevice;

namespace TestingSystem.Areas.Admin.Controllers
{
	public class ExamsController : AdminController
	{
		private readonly IExamService examService;
		private readonly IExamPaperService examPaperService;
		private readonly ITestService testService;

		public ExamsController(IUserService user, IExamService examService, IExamPaperService examPaperService, ITestService testService) : base(user)
		{
			this.examService = examService;
			this.examPaperService = examPaperService;
			this.testService = testService;
		}
		// GET: Admin/Exams
		public ActionResult Index()
		{
			var listExams = examService.GetAllExams();
			ViewBag.listExams = listExams;
			return View(listExams);
		}
		[HttpPost]
		public ActionResult Index(string keySearch)
		{
			var listExams = examService.SearchExams(keySearch);
			return View(listExams);
		}
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(Exam exam)
		{
			try
			{
				if (examService.AddExam(exam) > 0)
				{
					Success = "Insert Exam successfully!";
					return RedirectToAction("Index", "Exams");
				}
				else
				{
					Failure = "Something went wrong, please try again!";
					return RedirectToAction("Create", "Exams");
				}
			}
			catch (Exception e)
			{
				Failure = "Something went wrong, please try again!";
				return RedirectToAction("Create", "Exams");
			}
		}
		public ActionResult Edit(int id)
		{
			ViewBag.listAllTest = testService.GetAllTests();
			var listTestByExamID = examService.GetTestByExamID(id);
			//foreach (var item in listTestByExamID)
			//{
			//	item.NumberOfQuestion = examPaperService.GetNumberOfQuestionByExamPaperId(item.ExamPaperID);
			//}
			ViewBag.listTestByExamID = listTestByExamID;
			var exam = examService.GetExamsByID(id);
			return View(exam);
		}

		[HttpPost]
		public ActionResult Edit(Exam exam)
		{
			try
			{
				if (examService.UpdateExam(exam))
				{
					Success = "Update Exam successfully!";
					return RedirectToAction("Index", "Exams");
				}
				else
				{
					Failure = "Something went wrong, please try again!";
					return RedirectToAction("Edit", "Exams");
				}
			}
			catch (Exception e)
			{
				Failure = "Something went wrong, please try again!";
				return RedirectToAction("Edit", "Exams");
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
						if (examService.DeleteExam(id) > 0)
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
						Success = "Delete Exam successfully!";
						return RedirectToAction("Index", "Exams");
					}
				}
				Failure = "Something went wrong, please try again!";
				return RedirectToAction("Index", "Exams");
			}
			catch (System.Exception exception)
			{
				Failure = exception.Message;
				return RedirectToAction("Index", "Exams");
			}
		}
		public JsonResult _CheckExamsAvailableCreate(string userdata)
		{
			try
			{
				var searchData = examService.SearchExams(userdata);
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

		public ActionResult RemoveTestInExams(List<int> ids, int examID)
		{
			try
			{
				if (ids.Count > 0)
				{
					int i = 0;
					foreach (var id in ids)
					{
						if (examService.RemoveTestInExams(id) > 0)
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
						Success = "Delete ExamPaper successfully!";
						return RedirectToAction("UpdateTest", "Exams", new { id = examID });
					}
				}
				Failure = "Something went wrong, please try again!";
				return RedirectToAction("UpdateTest", "Exams", new { id = examID });
			}
			catch (System.Exception exception)
			{
				Failure = exception.Message;
				return RedirectToAction("UpdateTest", "Exams", new { id = examID });
			}
		}

		//public ActionResult GetExamPaperByExamID(int examID)
		//{
		//	var examPaper = new List<Models.ExamPaper>();
		//	examPaper = examService.GetExamPaperByExamID(examID).ToList();

		//	return Json(new { data = examPaper }, JsonRequestBehavior.AllowGet);
		//}
		public ActionResult UpdateTest(int id)
		{
			var listTestActive = testService.GetAllTestIsActive();
			ViewBag.listTestActive = listTestActive;
			//
			ViewBag.examID = id;
			//
			var countlistTestIsActive = listTestActive.Count();
			ViewBag.countlistTestIsActive = countlistTestIsActive;

			var listTestByExamID = examService.GetTestByExamID(id);
			ViewBag.listTestByExamID = listTestByExamID;

			ViewBag.CountTestInExam = listTestByExamID.Count();

			return View();
		}
		[HttpPost]
		public ActionResult UpdateTest(int id, string keySearch)
		{
			var listTestActive = testService.GetAllTestIsActiveByKeySearch(keySearch);
			ViewBag.listTestActive = listTestActive;
			//
			ViewBag.examID = id;
			//
			var countlistTestIsActive = listTestActive.Count();
			ViewBag.countlistTestIsActive = countlistTestIsActive;

			var listTestByExamID = examService.GetTestByExamID(id);
			ViewBag.listTestByExamID = listTestByExamID;

			ViewBag.CountTestInExam = listTestByExamID.Count();

			return View();
		}


		public ActionResult AddTestInExams(int examID, int testID)
		{
			try
			{
				examService.AddTestIntoExams(testID, examID);
				return RedirectToAction("UpdateTest", "Exams", new { id = examID });
			}
			catch (Exception e)
			{
				Failure = "Something went wrong, please try again!";
				return RedirectToAction("UpdateTest", "Exams", new { id = examID });
			}

		}
	}
}