using BankingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly BankingContext _context;

        public AdminController(BankingContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var sessionId = HttpContext.Session.GetString("RoleId");
            if(sessionId != "1")
            {
                return RedirectToAction("Index","Top");
            }
            var bankingContext = _context.Users;
            return View(await bankingContext.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Approve(long id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound(); 
            }

            user.UserTypeId = 2; 

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); 
        }

    }
}
