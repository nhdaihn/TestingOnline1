using System;
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

		public ExamsController(IUserService user, IExamService examService, IExamPaperService examPaperService) : base(user)
		{
			this.examService = examService;
			this.examPaperService = examPaperService;
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
			ViewBag.listAllExamPaper = examPaperService.GetAll();
			var listExamPaperByExamID = examService.GetExamPaperByExamID(id);
			ViewBag.listExamPaperByExamID = listExamPaperByExamID;
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

		public ActionResult RemoveExamPaperInExams(List<int> ids, int examID)
		{
			try
			{
				if (ids.Count > 0)
				{
					int i = 0;
					foreach (var id in ids)
					{
						if (examService.RemoveExamPaperInExams(id) > 0)
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
						return RedirectToAction("UpdateExamPaper", "Exams", new { id = examID });
					}
				}
				Failure = "Something went wrong, please try again!";
				return RedirectToAction("UpdateExamPaper", "Exams", new { id = examID });
			}
			catch (System.Exception exception)
			{
				Failure = exception.Message;
				return RedirectToAction("UpdateExamPaper", "Exams", new { id = examID });
			}
		}

		//public ActionResult GetExamPaperByExamID(int examID)
		//{
		//	var examPaper = new List<Models.ExamPaper>();
		//	examPaper = examService.GetExamPaperByExamID(examID).ToList();

		//	return Json(new { data = examPaper }, JsonRequestBehavior.AllowGet);
		//}
		public ActionResult UpdateExamPaper(int id)
		{
			var listExamPaperIsActive = examPaperService.GetAllExamPapersIsActive();
			ViewBag.listExamPaperIsActive = listExamPaperIsActive;
			//
			ViewBag.examID = id;
			//
			var countlistExamPaperIsActive = listExamPaperIsActive.Count();
			ViewBag.countlistExamPaperIsActive = countlistExamPaperIsActive;

			var listExamPaperByExamID = examService.GetExamPaperByExamID(id);
			ViewBag.listExamPaperByExamID = listExamPaperByExamID;

			ViewBag.CountExamPaperInExam = listExamPaperByExamID.Count();

			return View();
		}
		[HttpPost]
		public ActionResult UpdateExamPaper(int id,string keySearch)
		{
			var listExamPaperIsActive = examPaperService.SearchExamPapersIsActive(keySearch);
			ViewBag.listExamPaperIsActive = listExamPaperIsActive;
			//
			ViewBag.examID = id;
			//
			var countlistExamPaperIsActive = listExamPaperIsActive.Count();
			ViewBag.countlistExamPaperIsActive = countlistExamPaperIsActive;

			var listExamPaperByExamID = examService.GetExamPaperByExamID(id);
			ViewBag.listExamPaperByExamID = listExamPaperByExamID;

			ViewBag.CountExamPaperInExam = listExamPaperByExamID.Count();

			return View();
		}


		public ActionResult AddExamPaperInExams(int examID, int examPaperID)
		{
			try
			{
				examService.AddExamPaperIntoExams(examPaperID, examID);
				return RedirectToAction("UpdateExamPaper", "Exams", new { id = examID });
			}
			catch (Exception e)
			{
				Failure = "Something went wrong, please try again!";
				return RedirectToAction("UpdateExamPaper", "Exams", new { id = examID });
			}
			
		}
	}
}