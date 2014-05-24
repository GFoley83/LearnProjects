using System.Web.Mvc;

namespace Web.Controllers
{
  public class WinSurfaceProController : Controller
  {
    //
    // GET: /WinSurfacePro/
    public ActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Index(string email)
    {
      return View("SurfaceProWon");
    }
  }
}
