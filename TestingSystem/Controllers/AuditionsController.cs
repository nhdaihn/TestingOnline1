using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestingSystem.Controllers
{
    public class AuditionsController : Controller
    {
        // GET: Auditions
        public ActionResult Index()
        {
            return View();
        }
    }
}