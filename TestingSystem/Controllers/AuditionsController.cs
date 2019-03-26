using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSystem.Models;
using TestingSystem.Sevice;

namespace TestingSystem.Controllers
{
    public class AuditionsController : Controller
    {

        private readonly IExamPaperService examPaperService;

        /// <summary>
        /// Defines the questionService
        /// </summary>
        private readonly IQuestionService questionService;

        /// <summary>
        /// Defines the answerService
        /// </summary>
        private readonly IAnswerService answerService;

        /// <summary>
        /// Defines the examPaperQuestionService
        /// </summary>
        private readonly IExamPaperQuestionService examPaperQuestionService;

        /// <summary>
        /// Defines the questionCategorySevice
        /// </summary>
        private readonly IQuestionCategorySevice questionCategorySevice;
        public AuditionsController(IExamPaperService examPaperService, IQuestionService questionService, IAnswerService answerService, IExamPaperQuestionService examPaperQuestionService, IQuestionCategorySevice questionCategorySevice)
        {
            this.examPaperService = examPaperService;
            this.questionService = questionService;
            this.answerService = answerService;
            this.examPaperQuestionService = examPaperQuestionService;
            this.questionCategorySevice = questionCategorySevice;
        }
        // GET: Auditions
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AuditionsTest()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult AuditionsTest(string code)
        //{
        //    var model = examPaperService.FindCode(code);
        //    var examPaperID = model.ExamPaperID;
        //    return RedirectToAction("ShowExamPaperById", "TestNotLogin",new { idExamPaper = examPaperID });
        //}
    }
}