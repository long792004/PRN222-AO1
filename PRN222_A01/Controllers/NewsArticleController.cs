using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NguyenThanhLong_SE18C.NET_A01.Models;
using NguyenThanhLong_SE18C.NET_A01.Services;

namespace NguyenThanhLong_SE18C.NET_A01.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public NewsArticleController(INewsArticleService newsArticleService, ICategoryService categoryService, ITagService tagService)
        {
            _newsArticleService = newsArticleService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        private bool IsStaff()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "1";
        }

        private short? GetCurrentUserId()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (short.TryParse(userId, out short id))
            {
                return id;
            }
            return null;
        }

        public IActionResult Index(string searchTerm)
        {
            if (!IsStaff())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var userId = GetCurrentUserId();
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var articles = _newsArticleService.GetNewsArticlesByCreator(userId.Value);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                articles = articles.Where(a =>
                    (a.NewsTitle != null && a.NewsTitle.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    a.Headline.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    (a.NewsContent != null && a.NewsContent.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            ViewBag.SearchTerm = searchTerm;
            return View(articles);
        }

        public IActionResult MyHistory()
        {
            if (!IsStaff())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var userId = GetCurrentUserId();
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var articles = _newsArticleService.GetNewsArticlesByCreator(userId.Value);
            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsStaff())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            ViewBag.Categories = new SelectList(_categoryService.GetActiveCategories(), "CategoryID", "CategoryName");
            ViewBag.Tags = _tagService.GetAllTags();
            return PartialView("_CreateEditModal", new NewsArticle());
        }

        [HttpPost]
        public IActionResult Create(NewsArticle article, List<int> selectedTags)
        {
            if (!IsStaff())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var userId = GetCurrentUserId();
            if (!userId.HasValue)
            {
                return Json(new { success = false, message = "User not logged in" });
            }

            if (ModelState.IsValid)
            {
                article.CreatedByID = userId.Value;
                article.CreatedDate = DateTime.Now;
                article.ModifiedDate = DateTime.Now;
                
                if (selectedTags == null || !selectedTags.Any())
                {
                    selectedTags = new List<int>();
                }

                _newsArticleService.AddNewsArticle(article, selectedTags);
                return Json(new { success = true });
            }

            ViewBag.Categories = new SelectList(_categoryService.GetActiveCategories(), "CategoryID", "CategoryName");
            ViewBag.Tags = _tagService.GetAllTags();
            return PartialView("_CreateEditModal", article);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (!IsStaff())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var article = _newsArticleService.GetNewsArticleById(id);
            if (article == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(_categoryService.GetActiveCategories(), "CategoryID", "CategoryName", article.CategoryID);
            ViewBag.Tags = _tagService.GetAllTags();
            ViewBag.SelectedTags = article.NewsTags.Select(nt => nt.TagID).ToList();
            return PartialView("_CreateEditModal", article);
        }

        [HttpPost]
        public IActionResult Edit(NewsArticle article, List<int> selectedTags)
        {
            if (!IsStaff())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var userId = GetCurrentUserId();
            if (!userId.HasValue)
            {
                return Json(new { success = false, message = "User not logged in" });
            }

            if (ModelState.IsValid)
            {
                article.UpdatedByID = userId.Value;
                article.ModifiedDate = DateTime.Now;
                
                if (selectedTags == null || !selectedTags.Any())
                {
                    selectedTags = new List<int>();
                }

                _newsArticleService.UpdateNewsArticle(article, selectedTags);
                return Json(new { success = true });
            }

            ViewBag.Categories = new SelectList(_categoryService.GetActiveCategories(), "CategoryID", "CategoryName", article.CategoryID);
            ViewBag.Tags = _tagService.GetAllTags();
            return PartialView("_CreateEditModal", article);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (!IsStaff())
            {
                return Json(new { success = false, message = "Access denied" });
            }

            try
            {
                _newsArticleService.DeleteNewsArticle(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult Details(string id)
        {
            var article = _newsArticleService.GetNewsArticleById(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }
    }
}
