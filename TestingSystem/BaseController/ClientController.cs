namespace TestingSystem.BaseController
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using TestingSystem.Models;
    using TestingSystem.Sevice;

    /// <summary>
    /// Defines the <see cref="ClientController" />
    /// </summary>
    public class ClientController : Controller
    {
        /// <summary>
        /// Defines the userService
        /// </summary>
        protected readonly IUserService userService;

        /// <summary>
        /// Sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { set { ViewBag.Title = value; } }

        /// <summary>
        /// Sets the success.
        /// </summary>
        /// <value>The success.</value>
        public string Success { set { TempData["Success"] = ViewData["Success"] = value; } }
        /// <summary>
        /// Sets the failure.
        /// </summary>
        /// <value>The failure.</value>
        public string Failure { set { TempData["Failure"] = ViewData["Failure"] = value; } }


        /// <summary>
        /// Initializes a new instance of the <see cref="ClientController"/> class.
        /// </summary>
        /// <param name="userService">The userService<see cref="IUserService"/></param>
        protected ClientController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// The OnActionExecuting
        /// </summary>
        /// <param name="filterContext">The filterContext<see cref="ActionExecutingContext"/></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// The GetAction
        /// </summary>
        /// <returns>The <see cref="List{RoleAction}"/></returns>
        protected virtual List<RoleAction> GetAction()
        {
            try
            {
                var ss = Session["Name"];
                int id = int.Parse(ss.ToString());
                return userService.GetAction(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
