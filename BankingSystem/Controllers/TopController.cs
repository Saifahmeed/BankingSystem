using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Controllers
{
    public class TopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
