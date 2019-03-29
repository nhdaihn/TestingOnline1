using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSystem.BaseController;
using TestingSystem.Sevice;

namespace TestingSystem.Areas.Admin.Controllers
{
    public class ExamClientController : AdminController
    {
        
        private IExamPaperService examPaperService;
        private IExamService examService;

        public ExamClientController(IUserService userService, IExamService examService, IExamPaperService examPaperService) : base(userService)
        {
            this.examPaperService = examPaperService;
            this.examService = examService;
        }
        //GET: Admin/ExamClient
        public ActionResult ListExamClient(int idExam = 0)
        {
            int idUser = int.Parse(Session["Name"].ToString());
            var listExam = examService.GetAllExams().ToList();
            if (idExam == 0)
            {
                idExam = listExam[0].ExamID;
            }
            if (idExam != 0)
            {
                ViewBag.ListExam = listExam;
                ViewBag.ListExamPaperInExam = examService.GetTestByExamID(idExam, idUser);
            }
            else
            {
                ViewBag.ListExam = listExam;
                ViewBag.ListExamPaperInExam = examService.GetTestByExamID(idExam, idUser);
            }
            ViewBag.IdExam = idExam;
            return View(listExam);
        }
    }
}