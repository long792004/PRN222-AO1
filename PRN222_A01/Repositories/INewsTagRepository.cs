using NguyenThanhLong_SE18C.NET_A01.Models;

namespace NguyenThanhLong_SE18C.NET_A01.Repositories
{
    public interface INewsTagRepository : IGenericRepository<NewsTag>
    {
        List<NewsTag> GetTagsByNewsArticleId(string newsArticleId);
        void DeleteByNewsArticleId(string newsArticleId);
    }
}
