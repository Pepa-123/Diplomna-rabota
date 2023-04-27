using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
