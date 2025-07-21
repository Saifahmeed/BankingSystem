using BankingSystem.Models;
using BankingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;


namespace BankingSystem.Controllers
{
    public class TransactionController : Controller
    {
        private readonly BankingContext _context;

        public TransactionController(BankingContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var sessionId = HttpContext.Session.GetString("UserId");
            if (!long.TryParse(sessionId, out var userId))
                return RedirectToAction("Index", "Login");

            var accounts = await _context.Accounts
                .Include(a => a.Currency)
                .Include(a => a.AccountType)
                .Where(a => a.UserId == userId && a.AccountStatusId == 3) // Active only
                .ToListAsync();

            var viewModel = new TransactionViewModel
            {
                SenderAccounts = accounts.Select(a => new SelectListItem
                {
                    Value = a.AccountId.ToString(),
                    Text = $"{a.AccountId} - {a.AccountType?.TypeName} - {a.Currency.Code}"
                }).ToList(),
                AccountBalances = accounts.ToDictionary(a => a.AccountId, a => a.Balance ?? 0)
            };

            return View(viewModel);
        }
        [HttpGet]
        public IActionResult GetReceiverName(long accountId)
        {
            var name = _context.Accounts
    .Where(a => a.AccountId == accountId && a.AccountStatusId == 3)
    .Select(a => new { a.User.Fname, a.User.Lname })
    .FirstOrDefault();

            if (name != null)
                return Json(new { name = $"{name.Fname} {name.Lname}" });

            return Json(new { name = "" });
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionViewModel model)
        {
            var sessionId = HttpContext.Session.GetString("UserId");
            if (!long.TryParse(sessionId, out var userId))
            {
                ModelState.AddModelError("", "Your session expired. Please log in again.");
                return RedirectToAction("Index", "Login");
            }

            var sendingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            var sendingAccount = await _context.Accounts
                .Where(a => a.AccountId == model.SenderAccountId)
                .Select(a => new { a.Balance, a.AccountStatusId, a.CurrencyId })
                .FirstOrDefaultAsync();

            var receivingAccount = await _context.Accounts
                .Where(a => a.AccountId == model.ReceiverAccountId)
                .Select(a => new { a.UserId, a.AccountStatusId, a.CurrencyId })
                .FirstOrDefaultAsync();

            var receivingUser = receivingAccount != null
                ? await _context.Users.FirstOrDefaultAsync(u => u.UserId == receivingAccount.UserId)
                : null;

            bool isValid = true;

            if (sendingUser?.UserTypeId == 1)
            {
                ModelState.AddModelError(nameof(model.SenderAccountId), "You are still pending approval.");
                isValid = false;
            }

            if (receivingUser?.UserTypeId == 1)
            {
                ModelState.AddModelError(nameof(model.ReceiverAccountId), "The owner of this account is pending approval.");
                isValid = false;
            }

            if (model.Amount is null || model.Amount <= 0)
            {
                ModelState.AddModelError(nameof(model.Amount), "Amount must be greater than zero.");
                isValid = false;
            }

            if (sendingAccount == null)
            {
                ModelState.AddModelError(nameof(model.SenderAccountId), "Your account does not exist.");
                isValid = false;
            }

            if (receivingAccount == null)
            {
                ModelState.AddModelError(nameof(model.ReceiverAccountId), "Receiver account does not exist or not active.");
                isValid = false;
            }

            if (model.SenderAccountId == model.ReceiverAccountId)
            {
                ModelState.AddModelError(nameof(model.ReceiverAccountId), "Sender and receiver accounts cannot be the same.");
                isValid = false;
            }

            if (sendingAccount?.AccountStatusId != 3)
            {
                ModelState.AddModelError(nameof(model.SenderAccountId), "Your account is not active.");
                isValid = false;
            }

            if (sendingAccount?.Balance < model.Amount)
            {
                ModelState.AddModelError(nameof(model.Amount), "Insufficient balance in your account.");
                isValid = false;
            }

            if (model.Amount > 40000)
            {
                ModelState.AddModelError(nameof(model.Amount), "Transaction amount exceeds the limit of 40,000.");
                isValid = false;
            }

            if (ModelState.IsValid && isValid)
            {
                var rateSender = await _context.Currencies
                    .Where(c => c.CurrencyId == sendingAccount.CurrencyId)
                    .Select(c => c.ExchangeRate)
                    .FirstOrDefaultAsync();

                var rateReceiver = await _context.Currencies
                    .Where(c => c.CurrencyId == receivingAccount.CurrencyId)
                    .Select(c => c.ExchangeRate)
                    .FirstOrDefaultAsync();

                decimal sendingAmount = model.Amount.Value;

                decimal convertedAmount = sendingAmount * (rateSender / rateReceiver);
                decimal factor = (decimal)Math.Pow(10, 4);
                convertedAmount = Math.Truncate(convertedAmount * factor) / factor;

                var transaction = new Transaction
                {
                    SenderAccountId = model.SenderAccountId,
                    ReceiverAccountId = model.ReceiverAccountId,
                    Amount = convertedAmount,
                    EquivalentRate = rateSender / rateReceiver,
                    TimeStamp = DateTime.Now,
                    Status = true
                };

                await _context.Accounts
                    .Where(a => a.AccountId == model.SenderAccountId)
                    .ExecuteUpdateAsync(set => set.SetProperty(a => a.Balance, a => a.Balance - sendingAmount));

                await _context.Accounts
                    .Where(a => a.AccountId == model.ReceiverAccountId)
                    .ExecuteUpdateAsync(set => set.SetProperty(a => a.Balance, a => a.Balance + convertedAmount));

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            var accounts = await _context.Accounts
                .Include(a => a.Currency)
                .Include(a => a.AccountType)
                .Where(a => a.UserId == userId && a.AccountStatusId == 3)
                .ToListAsync();

            model.SenderAccounts = accounts.Select(a => new SelectListItem
            {
                Value = a.AccountId.ToString(),
                Text = $"{a.AccountId} - {a.AccountType?.TypeName} - {a.Currency?.Code}"
            }).ToList();

            model.AccountBalances = accounts.ToDictionary(a => a.AccountId, a => a.Balance ?? 0);

            return View(model);
        }

    }

}
