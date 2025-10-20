using Microsoft.EntityFrameworkCore;
using NguyenThanhLong_SE18C.NET_A01.Models;

namespace NguyenThanhLong_SE18C.NET_A01.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly FUNewsManagementContext _context;

        public CategoryRepository(FUNewsManagementContext context) : base(context)
        {
            _context = context;
        }

        public List<Category> GetActiveCategories()
        {
            return _context.Categories.Where(c => c.IsActive == true).ToList();
        }

        public bool IsCategoryUsedInNews(short categoryId)
        {
            return _context.NewsArticles.Any(n => n.CategoryID == categoryId);
        }
    }
}
