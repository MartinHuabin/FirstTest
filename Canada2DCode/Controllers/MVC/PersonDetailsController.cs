using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Canada2DCode.Controllers.MVC
{
    public class PersonDetailsController : Controller
    {
        // GET: PersonDetails
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PersonDetails()
        {
            ViewBag.Message = "PersonDetails";

            return View();
        }
    }
}