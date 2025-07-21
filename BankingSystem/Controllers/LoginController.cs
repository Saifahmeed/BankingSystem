using BankingSystem.Models;
using BankingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace BankingSystem.Controllers
{
    public class LoginController : Controller
    {

        private readonly BankingContext _context;

        public LoginController(BankingContext context)
        {
            _context = context;
        }
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToUpper();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> login(LoginViewModel model)
        {
            if (model == null)
            {
                ModelState.AddModelError("", "Username and Password are required.");
                return View("index", model);
            }
            if (ModelState.IsValid)
            {
                if (model.Email == "admin@gmail.com" && model.Password == "1")
                {
                    return RedirectToAction("Index", "Admin");
                }
                string hashedPassword = HashPassword(model.Password);
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == hashedPassword);
                if (user == null)
                {
                    ModelState.AddModelError("", "Username and Password are invalid.");
                    return View("index", model);
                }
                if (user.UserTypeId == 1)
                {
                    ModelState.AddModelError("", "You are still pending approval");
                    return View("index", model);
                }
                else
                {
                    HttpContext.Session.SetString("UserId", user.UserId.ToString());
                    HttpContext.Session.SetString("FirstName", user.Fname ?? "");
                    HttpContext.Session.SetString("LastName", user.Lname ?? "");
                    HttpContext.Session.SetString("IsAuthenticated", "true");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("index", model);
        }
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (model == null || !ModelState.IsValid)
            {
                ModelState.AddModelError("", "Wrong Email or Phone number.");
                return View(model);
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Wrong Email or Phone number.");
                return View(model);
            }
            if (user.PhoneNumber != model.PhoneNumber)
            {
                ModelState.AddModelError("", "Wrong Email or Phone number.");
                return View(model);
            }
            user.Password = HashPassword(model.NewPassword);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            ViewBag.PasswordResetSuccess = true;
            return View("ForgotPassword", model);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}

