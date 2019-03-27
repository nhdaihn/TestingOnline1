using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestingSystem.BaseController;
using TestingSystem.DataTranferObject;
using TestingSystem.DataTranferObject.Question;
using TestingSystem.Models;
using TestingSystem.Sevice;

namespace TestingSystem.Areas.Admin.Controllers
{
    public class TestLoginController : AdminController
    {
        public ICandidateService candidateService;
        public IExamPaperQuestionService examPaperQuestionService;
        public IExamPaperService examPaperService;
        public ITestService testService;
        public ITestResultService testResultService;
        public IQuestionService questionService;
        public IAnswerService answerService;
        public TestLoginController(ICandidateService candidateService, ITestResultService testResultService, ITestService testService, IUserService userService, IExamPaperQuestionService examPaperQuestionService, IQuestionService questionService, IAnswerService answerService, IExamPaperService examPaperService) : base(userService)
        {
            this.examPaperQuestionService = examPaperQuestionService;
            this.questionService = questionService;
            this.answerService = answerService;
            this.examPaperService = examPaperService;
            this.testService = testService;
            this.testResultService = testResultService;
            this.candidateService = candidateService;
        }

        // hieu la lay id bai test theo idExamPaper va idTest
        [HttpGet]
        public ActionResult ShowExamPaperById(int idExamPaper, int idExam, int idTest)
        {
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
            ViewBag.IdExam = idExam;
            return View();
        }
        [HttpPost]
        public JsonResult _RepostTest(IEnumerable<ResultTest> fruits, int exampaperid,int examid, int passscore, int idtest)
        {
           
	        int idUser = int.Parse(Session["Name"].ToString());
            // fruits : chua danh sach tat ca id question va id answer da check
			var list = fruits;
            int countAnswer = fruits.Count();
            List<Answer> listAnswerCorrect = new List<Answer>();
            foreach (var item in list)
            {
                // id: answer id, name: question id
                // lay answer dung trong fruits
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
            // get all question include not check
            List<QuestionDto> listQuestion = new List<QuestionDto>();
            listQuestion = questionService.GetQuestionsByExamPaperId(exampaperid).ToList();


            double percent = numberOfCorrectAnswer / countAnswer;

            Models.ExamPaper examPaper = new Models.ExamPaper();
            examPaper = examPaperService.GetExamPaperById(exampaperid);
            

            // lay exampaper theo id
            // listquestion: so cau hoi trong de
            foreach (var item in listQuestion)
            {
                Candidate newCandidate = new Candidate();
                newCandidate.CandidateID = idUser;
                candidateService.AddCandidate(newCandidate);
                TestResult testResult = new TestResult();
                testResult.TestID = idtest;
                testResult.CandidateID = idUser;
                testResult.TestName = examPaper.Title;
                testResult.Description = "description note";
                testResult.CreatedDate = DateTime.Now;
                testResult.Score = numberOfCorrectAnswer;
                // list: so cau hoi da check
                bool checkAvailable = false;
                foreach(var item2 in list)
                {
                    // item2 co 2 attribute: id(answerid) va name(questionid)
                    if(item2.name == item.QuestionID)
                    {
                        testResult.QuestionID = item2.name;
                        testResult.AnswerID = item2.id;
                        checkAvailable = true;
                        break;
                    }
                }
                if(checkAvailable == false)
                {
                    testResult.QuestionID = item.QuestionID;
                    testResult.AnswerID = -1;
                }
                testResultService.AddTestResult(testResult);
            }
            return Json(listQuestion.Count());
        }

        public ActionResult _ShowResult(int countQ, int passscore, string title)
        {
            int idUser = int.Parse(Session["Name"].ToString());
            List<Models.TestResult> listQ= new List<Models.TestResult>();
            listQ = testResultService.GetQuestionByCount(countQ).ToList();
            ViewBag.DedicateName = userService.GetUserById(idUser).Name;
            ViewBag.DedicateEmail = userService.GetUserById(idUser).Email;
            ViewBag.TestTitle = title;
            int score = 0;
            bool checkPass = false; 
            foreach(var item in listQ)
            {
                score = item.Score;
                break;
            }
            double percent = (score / countQ) * 100;
            percent = Math.Round(percent);
            if (percent >= passscore)
            {
                checkPass = true;
            }
            ViewBag.Score = score;
            ViewBag.CheckPass = checkPass;
            ViewBag.CountQ = countQ;
            return View();
        }
    }
}