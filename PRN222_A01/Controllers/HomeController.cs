using Microsoft.AspNetCore.Mvc;
using NguyenThanhLong_SE18C.NET_A01.Models;
using NguyenThanhLong_SE18C.NET_A01.Services;
using System.Diagnostics;

namespace NguyenThanhLong_SE18C.NET_A01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsArticleService _newsArticleService;

        public HomeController(ILogger<HomeController> logger, INewsArticleService newsArticleService)
        {
            _logger = logger;
            _newsArticleService = newsArticleService;
        }

        public IActionResult Index()
        {
            var activeNews = _newsArticleService.GetActiveNewsArticles();
            return View(activeNews);
        }

        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var article = _newsArticleService.GetNewsArticleById(id);
            if (article == null || article.NewsStatus != true)
            {
                return NotFound();
            }

            return View(article);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
