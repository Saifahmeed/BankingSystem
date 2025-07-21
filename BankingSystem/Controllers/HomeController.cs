using BankingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
namespace BankingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BankingContext _context;

        public HomeController(ILogger<HomeController> logger, BankingContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var idd = HttpContext.Session.GetString("UserId");
            if (long.TryParse(idd, out long userId))
            {
                var user = _context.Users
                    .Where(u => u.UserId == userId)
                    .Include(u => u.Accounts)
                        .ThenInclude(a => a.AccountType)
                    .Include(u => u.Accounts)
                        .ThenInclude(a => a.Branch)
                    .Include(u => u.Accounts)
                        .ThenInclude(a => a.Currency)
                    .Include(u => u.Accounts)
                        .ThenInclude(a => a.TransactionSenderAccounts)
                            .ThenInclude(t => t.ReceiverAccount)
                                .ThenInclude(r => r.Currency)
                    .Include(u => u.Accounts)
                        .ThenInclude(a => a.TransactionReceiverAccounts)
                            .ThenInclude(t => t.SenderAccount)
                                .ThenInclude(s => s.Currency)
                    .FirstOrDefault();

                return View(user);
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong, please login again.");
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
