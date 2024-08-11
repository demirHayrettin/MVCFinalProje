using Microsoft.AspNetCore.Mvc;

namespace MVCFinalProje.UI.Areas.Customer.Controllers
{
    public class HomeController : CustomerBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
