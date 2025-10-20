using Microsoft.AspNetCore.Mvc;
using NguyenThanhLong_SE18C.NET_A01.Models;
using NguyenThanhLong_SE18C.NET_A01.Services;

namespace NguyenThanhLong_SE18C.NET_A01.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISystemAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(ISystemAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Email and password are required.";
                return View();
            }

            // Check admin account from appsettings.json
            var adminEmail = _configuration["AdminAccount:Email"];
            var adminPassword = _configuration["AdminAccount:Password"];

            if (email == adminEmail && password == adminPassword)
            {
                HttpContext.Session.SetString("UserEmail", email);
                HttpContext.Session.SetString("UserRole", "0"); // Admin role
                HttpContext.Session.SetString("UserName", "Administrator");
                return RedirectToAction("Index", "SystemAccount");
            }

            // Check regular user
            var user = _accountService.Login(email, password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserEmail", user.AccountEmail!);
                HttpContext.Session.SetString("UserRole", user.AccountRole.ToString()!);
                HttpContext.Session.SetString("UserName", user.AccountName!);
                HttpContext.Session.SetString("UserId", user.AccountID.ToString());

                // Redirect based on role
                if (user.AccountRole == 1) // Staff
                {
                    return RedirectToAction("Index", "NewsArticle");
                }
                else if (user.AccountRole == 2) // Lecturer
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.ErrorMessage = "Invalid email or password.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
