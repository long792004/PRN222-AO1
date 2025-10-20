using Microsoft.AspNetCore.Mvc;
using NguyenThanhLong_SE18C.NET_A01.Models;
using NguyenThanhLong_SE18C.NET_A01.Services;

namespace NguyenThanhLong_SE18C.NET_A01.Controllers
{
    public class SystemAccountController : Controller
    {
        private readonly ISystemAccountService _accountService;

        public SystemAccountController(ISystemAccountService accountService)
        {
            _accountService = accountService;
        }

        private bool IsAdmin()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "0";
        }

        public IActionResult Index(string searchTerm)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var accounts = _accountService.GetAllAccounts();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accounts = accounts.Where(a =>
                    (a.AccountName != null && a.AccountName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (a.AccountEmail != null && a.AccountEmail.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            ViewBag.SearchTerm = searchTerm;
            return View(accounts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            return PartialView("_CreateEditModal", new SystemAccount());
        }

        [HttpPost]
        public IActionResult Create(SystemAccount account)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            if (ModelState.IsValid)
            {
                _accountService.AddAccount(account);
                return Json(new { success = true });
            }

            return PartialView("_CreateEditModal", account);
        }

        [HttpGet]
        public IActionResult Edit(short id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var account = _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }

            return PartialView("_CreateEditModal", account);
        }

        [HttpPost]
        public IActionResult Edit(SystemAccount account)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            if (ModelState.IsValid)
            {
                _accountService.UpdateAccount(account);
                return Json(new { success = true });
            }

            return PartialView("_CreateEditModal", account);
        }

        [HttpPost]
        public IActionResult Delete(short id)
        {
            if (!IsAdmin())
            {
                return Json(new { success = false, message = "Access denied" });
            }

            try
            {
                _accountService.DeleteAccount(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
