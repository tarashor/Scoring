using Bowling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bowling.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Count(IList<Frame> frames)
        {
            return Json(new {score = 10});
        }
	}
}