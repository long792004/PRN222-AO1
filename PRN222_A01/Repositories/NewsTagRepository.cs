using Microsoft.EntityFrameworkCore;
using NguyenThanhLong_SE18C.NET_A01.Models;

namespace NguyenThanhLong_SE18C.NET_A01.Repositories
{
    public class NewsTagRepository : GenericRepository<NewsTag>, INewsTagRepository
    {
        private readonly FUNewsManagementContext _context;

        public NewsTagRepository(FUNewsManagementContext context) : base(context)
        {
            _context = context;
        }

        public List<NewsTag> GetTagsByNewsArticleId(string newsArticleId)
        {
            return _context.NewsTags
                .Include(nt => nt.Tag)
                .Where(nt => nt.NewsArticleID == newsArticleId)
                .ToList();
        }

        public void DeleteByNewsArticleId(string newsArticleId)
        {
            var tags = _context.NewsTags.Where(nt => nt.NewsArticleID == newsArticleId);
            _context.NewsTags.RemoveRange(tags);
        }
    }
}
