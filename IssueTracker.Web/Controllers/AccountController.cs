using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IssueTracker.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult List()
        {
            return View();
        }
    }
}