﻿using System.Web.Mvc;

namespace Web.Controllers
{
  public class ErrorController : Controller
  {
    public ActionResult Index()
    {
      return View("~/Views/Error/Index.cshtml");
    }
  }
}
