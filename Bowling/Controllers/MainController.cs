using Bowling.Models;
using Bowling.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Bowling.Controllers
{
    public class MainController : Controller
    {
        private ScoreService scoreService;

        public MainController() {
            scoreService = new ScoreService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Count(Game game)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                int totalScore = scoreService.GetScore(game);
                return Json(new { score = totalScore });
            }
            else
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        message += error.ErrorMessage;
                    }
                }
            }

            HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return Json(new {message = message});
        }
	}
}