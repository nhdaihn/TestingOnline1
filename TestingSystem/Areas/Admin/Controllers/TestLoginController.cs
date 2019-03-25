using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSystem.BaseController;
using TestingSystem.DataTranferObject;
using TestingSystem.Models;
using TestingSystem.Sevice;

namespace TestingSystem.Areas.Admin.Controllers
{
    public class TestLoginController : AdminController
    {
        public IExamPaperQuestionService examPaperQuestionService;
        public IExamPaperService examPaperService;
        public IQuestionService questionService;
        public IAnswerService answerService;
        public TestLoginController(IUserService userService, IExamPaperQuestionService examPaperQuestionService, IQuestionService questionService, IAnswerService answerService, IExamPaperService examPaperService) : base(userService)
        {
            this.examPaperQuestionService = examPaperQuestionService;
            this.questionService = questionService;
            this.answerService = answerService;
            this.examPaperService = examPaperService;
        }
        [HttpGet]
        public ActionResult ShowExamPaperById(int idExamPaper, int idExam)
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
            ViewBag.IdExam = idExam;
            return View();
        }
        [HttpPost]
        public JsonResult RepostTest(IEnumerable<ResultTest> fruits, int exampaperid,int examid)
        {
            var list = fruits;
            List<Answer> listAnswerCorrect = new List<Answer>();
            foreach (var item in list)
            {
                var obj = answerService.GetAnswerCorrect(item.id);
                if (obj != null)
                {
                    listAnswerCorrect.Add(obj);
                }
            }
            int numberOfCorrectAnswer = 0;
            foreach (var item in listAnswerCorrect)
            {
                foreach (var item2 in list)
                {
                    if (item.AnswerID == item2.id)
                    {
                        numberOfCorrectAnswer++;
                        continue;
                    }
                }
            }
            return Json(numberOfCorrectAnswer);
        }
    }
}