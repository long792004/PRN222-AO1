using Microsoft.AspNetCore.Mvc;
using NguyenThanhLong_SE18C.NET_A01.Services;

namespace NguyenThanhLong_SE18C.NET_A01.Controllers
{
    public class ReportController : Controller
    {
        private readonly INewsArticleService _newsArticleService;

        public ReportController(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        private bool IsAdmin()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "0";
        }

        public IActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            if (startDate.HasValue && endDate.HasValue)
            {
                var articles = _newsArticleService.GetNewsArticlesByDateRange(startDate.Value, endDate.Value);
                return View(articles);
            }

            return View(new List<NguyenThanhLong_SE18C.NET_A01.Models.NewsArticle>());
        }
    }
}
