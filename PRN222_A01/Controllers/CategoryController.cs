using Microsoft.AspNetCore.Mvc;
using NguyenThanhLong_SE18C.NET_A01.Models;
using NguyenThanhLong_SE18C.NET_A01.Services;

namespace NguyenThanhLong_SE18C.NET_A01.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        private bool IsStaff()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "1";
        }

        public IActionResult Index(string searchTerm)
        {
            if (!IsStaff())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var categories = _categoryService.GetAllCategories();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                categories = categories.Where(c =>
                    c.CategoryName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    c.CategoryDesciption.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            ViewBag.SearchTerm = searchTerm;
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!IsStaff())
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            return PartialView("_CreateEditModal", new Category());
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!IsStaff())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            if (ModelState.IsValid)
            {
                _categoryService.AddCategory(category);
                return Json(new { success = true });
            }

            return PartialView("_CreateEditModal", category);
        }

        [HttpGet]
        public IActionResult Edit(short id)
        {
            if (!IsStaff())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return PartialView("_CreateEditModal", category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!IsStaff())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(category);
                return Json(new { success = true });
            }

            return PartialView("_CreateEditModal", category);
        }

        [HttpPost]
        public IActionResult Delete(short id)
        {
            if (!IsStaff())
            {
                return Json(new { success = false, message = "Access denied" });
            }

            try
            {
                bool deleted = _categoryService.DeleteCategory(id);
                if (deleted)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Cannot delete category that is used in news articles." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
