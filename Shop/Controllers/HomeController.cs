using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }
}