using Microsoft.EntityFrameworkCore;
using NguyenThanhLong_SE18C.NET_A01.Models;

namespace NguyenThanhLong_SE18C.NET_A01.Repositories
{
    public class NewsArticleRepository : GenericRepository<NewsArticle>, INewsArticleRepository
    {
        private readonly FUNewsManagementContext _context;

        public NewsArticleRepository(FUNewsManagementContext context) : base(context)
        {
            _context = context;
        }

        public List<NewsArticle> GetActiveNewsArticles()
        {
            return _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.NewsTags)
                    .ThenInclude(nt => nt.Tag)
                .Where(n => n.NewsStatus == true)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();
        }

        public List<NewsArticle> GetNewsArticlesByCreator(short creatorId)
        {
            return _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.NewsTags)
                    .ThenInclude(nt => nt.Tag)
                .Where(n => n.CreatedByID == creatorId)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();
        }

        public List<NewsArticle> GetNewsArticlesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();
        }

        public List<NewsArticle> SearchNewsArticles(string searchTerm)
        {
            return _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.NewsTags)
                    .ThenInclude(nt => nt.Tag)
                .Where(n => n.NewsTitle!.Contains(searchTerm) || 
                           n.Headline.Contains(searchTerm) || 
                           n.NewsContent!.Contains(searchTerm))
                .OrderByDescending(n => n.CreatedDate)
                .ToList();
        }
    }
}
