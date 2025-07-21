using BankingSystem.Models;
using BankingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace BankingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly BankingContext _context;

        public AccountController(BankingContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new AccountViewModel
            {
                AccountTypes = await _context.AccountTypes
                    .Select(a => new SelectListItem
                    {
                        Value = a.AccountTypeId.ToString(),
                        Text = a.TypeName
                    }).ToListAsync(),

                Branches = await _context.Branches
                    .Select(b => new SelectListItem
                    {
                        Value = b.BranchId.ToString(),
                        Text = b.BranchName
                    }).ToListAsync(),
                Currencies = await _context.Currencies
                    .Select(c => new SelectListItem
                    {
                        Value = c.CurrencyId.ToString(),
                        Text = c.Code
                    }).ToListAsync()
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountViewModel model)
        {
            var idd = HttpContext.Session.GetString("UserId");
            if (!long.TryParse(idd, out long userId))
            {
                ModelState.AddModelError("", "Your session expired. Please log in again.");
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                var account = new Account
                {
                    AccountTypeId = model.AccountTypeId,
                    AccountStatusId = 3,
                    BranchId = model.BranchId,
                    CurrencyId = model.CurrencyId,
                    Balance = 1000,
                    DateOpened = DateTime.Now,
                    UserId = userId
                };
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync(); // add error view
                return RedirectToAction("Index","Home");
            }

            model.AccountTypes = await _context.AccountTypes
                .Select(a => new SelectListItem
                {
                    Value = a.AccountTypeId.ToString(),
                    Text = a.TypeName
                }).ToListAsync();

            model.Branches = await _context.Branches
                .Select(b => new SelectListItem
                {
                    Value = b.BranchId.ToString(),
                    Text = b.BranchName
                }).ToListAsync();

            model.Currencies = await _context.Currencies
                .Select(c => new SelectListItem
                {
                    Value = c.CurrencyId.ToString(),
                    Text = c.Code
                }).ToListAsync();

            return View(model);
        }

    }
}
