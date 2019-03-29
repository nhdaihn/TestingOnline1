using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingSystem.BaseController;
using TestingSystem.Sevice;

namespace TestingSystem.Areas.Admin.Controllers
{
    public class CandidatatesTestController : AdminController
    {
	    private readonly ICandidatesTestService _candidatesTestService;
	    public CandidatatesTestController(IUserService userService, ICandidatesTestService candidatesTestService) : base(userService)
	    {
		    _candidatesTestService = candidatesTestService;
	    }
		// GET: Admin/CandidatatesTest
		public ActionResult Index()
        {
            return View();
        }
		public ActionResult RemoveCandidatesFromTest(List<int> ids, int testID)
		{
			try
			{
				if (ids.Count > 0)
				{
					int i = 0;
					foreach (var id in ids)
					{
						if (_candidatesTestService.RemoveCadidatesFromTest(id) > 0)
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
						Success = "Delete Candidate successfully!";
						return RedirectToAction("UpdateCandidates", "CandidatatesTest", new { id = testID });
					}
				}
				Failure = "Something went wrong, please try again!";
				return RedirectToAction("UpdateCandidates", "CandidatatesTest", new { id = testID });
			}
			catch (System.Exception exception)
			{
				Failure = exception.Message;
				return RedirectToAction("UpdateCandidates", "CandidatatesTest", new { id = testID });
			}
		}
		public ActionResult UpdateCandidates(int id)
		{
			var listUser = userService.ListAll();
			ViewBag.listUser = listUser;
			ViewBag.countUser = listUser.Count;

			//var listTestActive = testService.GetAllTestIsActive();
			//ViewBag.listTestActive = listTestActive;
			//
			ViewBag.TestID = id;
			//
			//var countlistTestIsActive = listTestActive.Count();
			//ViewBag.countlistTestIsActive = countlistTestIsActive;

			var listCandidatesByTestID = _candidatesTestService.GetAllCandidatesByTestID(id);
			ViewBag.listCandidatesByTestID = listCandidatesByTestID;

			ViewBag.CountlistCandidatesByTestID = listCandidatesByTestID.Count();

			return View();
		}
		[HttpPost]
		public ActionResult UpdateTest(int id, string keySearch)
		{
			var listUser = userService.ListAll();
			ViewBag.listUser = listUser;
			ViewBag.countUser = listUser.Count;

			//var listTestActive = testService.GetAllTestIsActive();
			//ViewBag.listTestActive = listTestActive;
			//
			ViewBag.TestID = id;
			//
			//var countlistTestIsActive = listTestActive.Count();
			//ViewBag.countlistTestIsActive = countlistTestIsActive;

			var listCandidatesByTestID = _candidatesTestService.GetAllCandidatesByTestID(id);
			ViewBag.listCandidatesByTestID = listCandidatesByTestID;

			ViewBag.CountlistCandidatesByTestID = listCandidatesByTestID.Count();

			return View();
		}


		public ActionResult AddCandidatesIntoTest(int candidatesID, int testID)
		{
			try
			{
				_candidatesTestService.AddCandidatesIntoTest(candidatesID, testID);
				return RedirectToAction("UpdateCandidates", "CandidatatesTest", new { id = testID });
			}
			catch (Exception e)
			{
				Failure = "Something went wrong, please try again!";
				return RedirectToAction("UpdateCandidates", "CandidatatesTest", new { id = testID });
			}

		}
	}
}