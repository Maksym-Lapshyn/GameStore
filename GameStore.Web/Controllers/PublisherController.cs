using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Web.Models;

namespace GameStore.Web.Controllers
{
    public class PublisherController : Controller
    {
        [HttpPost]
        public ActionResult NewPublisher()
        {
            return View(new PublisherViewModel());
        }

        public ActionResult ShowPublisher(string companyName)
        {
            return View();
        }
    }
}