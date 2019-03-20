using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSystem.BaseController;
using TestingSystem.Data.Repositories;
using TestingSystem.Sevice;

namespace TestingSystem.Areas.Admin.Controllers
{
    public class TestController : AdminController
    {
	    private readonly ITestService testService;

		public TestController(ITestService testService,IUserService user):base(user)
		{
			this.testService = testService;
		}
        // GET: Admin/Test
        public ActionResult Index()
        {
	        var model = testService.GetAllTests();
            return View(model);
        }
    }
}