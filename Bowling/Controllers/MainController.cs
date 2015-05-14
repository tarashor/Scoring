﻿using Bowling.Models;
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

        //
        // GET: /Main/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Count(Game game)//IList<Frame> frames)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                int totalScore = scoreService.GetScore(game.frames);
                return Json(new { score = totalScore });
            }
            else 
            {
                message = "Request is not valid";
            }

            HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return Json(new {message = message});
        }
	}
}