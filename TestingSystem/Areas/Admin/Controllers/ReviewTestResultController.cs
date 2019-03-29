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
    public class ReviewTestResultController : AdminController
    {
        public ICandidateService candidateService;
        public IExamPaperQuestionService examPaperQuestionService;
        public IExamPaperService examPaperService;
        public ITestService testService;
        public ITestResultService testResultService;
        public IQuestionService questionService;
        public IAnswerService answerService;
        public ReviewTestResultController(ICandidateService candidateService, ITestResultService testResultService, ITestService testService, IUserService userService, IExamPaperQuestionService examPaperQuestionService, IQuestionService questionService, IAnswerService answerService, IExamPaperService examPaperService) : base(userService)
        {
            this.examPaperQuestionService = examPaperQuestionService;
            this.questionService = questionService;
            this.answerService = answerService;
            this.examPaperService = examPaperService;
            this.testService = testService;
            this.testResultService = testResultService;
            this.candidateService = candidateService;
        }
        public ActionResult ShowAllTestByDedicateId()
        {
            int idUser = int.Parse(Session["Name"].ToString());
            List<ReviewTestResult> listReviewTestResult = new List<ReviewTestResult>();
            ViewBag.ListReviewTestResult = testResultService.ListAllTestByDedicateId(idUser).ToList();
            ViewBag.NameDedicate = userService.GetUserById(idUser).Name;
            return View();
        }
        public ActionResult ShowTestResultById(int idTest, DateTime dateTest)
        {
            int idExamPaper = testService.GetExamPaperIdByTestId(idTest);
            /// danh sach cau hoi trong de thi
            var listExamPaperQuesions = questionService.GetQuestionsByExamPaperId(idExamPaper);

            

            // so luong cau hoi
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

            List<QuestionCheckMulti> listQuestionCheckMulti = new List<QuestionCheckMulti>();
            List<Answer> listAnswerByQuestionId = new List<Answer>();
            
            // get multi choice in all question
            foreach (var item in listExamPaperQuesions)
            {
                var checkcount = 0;
                bool multichoice = false;
                listAnswerByQuestionId = answerService.GetAnswersByQuestionID(item.QuestionID);
                foreach (var item2 in listAnswerByQuestionId)
                {
                    if(answerService.GetAnswerCorrect(item2.AnswerID) != null)
                    {
                        checkcount++;
                    }
                }
                if(checkcount > 1)
                {
                    multichoice = true;
                }
                listQuestionCheckMulti.Add(new QuestionCheckMulti() { QuestionID = item.QuestionID, CheckMulti = multichoice });
                multichoice = false;
                checkcount = 0;
            }
            ViewBag.ListQuestionCheckMulti = listQuestionCheckMulti;
            ViewBag.IdExamPaper = idExamPaper;
            TempData["idExamPaper"] = idExamPaper;
            ViewBag.TitleTest = examPaperService.GetExamPaperById(idExamPaper).Title;
            // id bai thi
            ViewBag.IdTest = idTest;
            // lay bai thi theo id
            Test test = new Test();
            test = testService.GetTestByID(idTest);
            //pass score bai thi
            ViewBag.PassScore = test.PassingScore;
            return View();
        }
    }
}