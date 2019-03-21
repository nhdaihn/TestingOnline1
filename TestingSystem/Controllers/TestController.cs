using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TestingSystem.BaseController;
using TestingSystem.Models;
using TestingSystem.Sevice;

namespace TestingSystem.Controllers
{
    public class TestController : ClientController
    {
        public TestController(IUserService userService) : base(userService)
        {
        }
        public ActionResult Index()
        {
            List<Question> listQ = new List<Question>() {
                new Question()
                {
                    QuestionID = 1,
                    Content = "Câu lệnh SQL nào được dùng để chèn thêm dữ liệu vào database?",
                    Image = null,
                    Level = 1,
                    CategoryID = 1,
                    IsActive = true,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = 1,
                    ModifiedDate = DateTime.Now
                },
                new Question()
                {
                    QuestionID = 2,
                    Content = "Câu lệnh SQL nào được dùng để chèn thêm dữ liệu vào database?",
                    Image = null,
                    Level = 1,
                    CategoryID = 1,
                    IsActive = true,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = 1,
                    ModifiedDate = DateTime.Now
                },
                new Question()
                {
                    QuestionID = 3,
                    Content = "Câu lệnh SQL nào được dùng để chèn thêm dữ liệu vào database?",
                    Image = null,
                    Level = 1,
                    CategoryID = 1,
                    IsActive = true,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = 1,
                    ModifiedDate = DateTime.Now
                }
            };
            List<Answer> listA = new List<Answer>() {
                new Answer()
                {
                    IsCorrect = true,
                    QuestionID = 1,
                    AnswerContent = "Select * From Persons WHERE FirstName='%a%'",
                },
                new Answer()
                {
                    IsCorrect = false,
                    QuestionID = 1,
                    AnswerContent = "Select * From Persons WHERE FirstName='%a%'",
                },
                new Answer()
                {
                    IsCorrect = false,
                    QuestionID = 1,
                    AnswerContent = "Select * From Persons WHERE FirstName='%a%'",
                },
                new Answer()
                {
                    IsCorrect = false,
                    QuestionID = 1,
                    AnswerContent = "Select * From Persons WHERE FirstName='%a%'",
                },
                new Answer()
                {
                    IsCorrect = true,
                    QuestionID = 2,
                    AnswerContent = "Select * From Persons WHERE FirstName='%a%'",
                },
                new Answer()
                {
                    IsCorrect = false,
                    QuestionID = 2,
                    AnswerContent = "Select * From Persons WHERE FirstName='%a%'",
                },
                new Answer()
                {
                    IsCorrect = false,
                    QuestionID = 2,
                    AnswerContent = "Select * From Persons WHERE FirstName='%a%'",
                },
                new Answer()
                {
                    IsCorrect = false,
                    QuestionID = 2,
                    AnswerContent = "Select * From Persons WHERE FirstName='%a%'",
                },
                new Answer()
                {
                    IsCorrect = true,
                    QuestionID = 3,
                    AnswerContent = "Select * From Persons WHERE FirstName='%a%'",
                },
                new Answer()
                {
                    IsCorrect = false,
                    QuestionID = 3,
                    AnswerContent = "Select * From Persons WHERE FirstName='%a%'",
                },
                new Answer()
                {
                    IsCorrect = false,
                    QuestionID = 3,
                    AnswerContent = "Select * From Persons WHERE FirstName='%a%'",
                },
                new Answer()
                {
                    IsCorrect = false,
                    QuestionID = 3,
                    AnswerContent = "Select * From Persons WHERE FirstName='%a%'",
                }
            };
            ViewBag.ListQuestion = listQ;
            ViewBag.ListAnswer = listA;
            return View();
        }
    }
}