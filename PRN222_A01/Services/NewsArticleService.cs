using NguyenThanhLong_SE18C.NET_A01.Models;
using NguyenThanhLong_SE18C.NET_A01.Repositories;

namespace NguyenThanhLong_SE18C.NET_A01.Services
{
    public interface INewsArticleService
    {
        List<NewsArticle> GetAllNewsArticles();
        List<NewsArticle> GetActiveNewsArticles();
        List<NewsArticle> GetNewsArticlesByCreator(short creatorId);
        List<NewsArticle> GetNewsArticlesByDateRange(DateTime startDate, DateTime endDate);
        List<NewsArticle> SearchNewsArticles(string searchTerm);
        NewsArticle? GetNewsArticleById(string id);
        void AddNewsArticle(NewsArticle article, List<int> tagIds);
        void UpdateNewsArticle(NewsArticle article, List<int> tagIds);
        void DeleteNewsArticle(string id);
    }

    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _articleRepository;
        private readonly INewsTagRepository _newsTagRepository;

        public NewsArticleService(INewsArticleRepository articleRepository, INewsTagRepository newsTagRepository)
        {
            _articleRepository = articleRepository;
            _newsTagRepository = newsTagRepository;
        }

        public List<NewsArticle> GetAllNewsArticles()
        {
            return _articleRepository.GetAll();
        }

        public List<NewsArticle> GetActiveNewsArticles()
        {
            return _articleRepository.GetActiveNewsArticles();
        }

        public List<NewsArticle> GetNewsArticlesByCreator(short creatorId)
        {
            return _articleRepository.GetNewsArticlesByCreator(creatorId);
        }

        public List<NewsArticle> GetNewsArticlesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _articleRepository.GetNewsArticlesByDateRange(startDate, endDate);
        }

        public List<NewsArticle> SearchNewsArticles(string searchTerm)
        {
            return _articleRepository.SearchNewsArticles(searchTerm);
        }

        public NewsArticle? GetNewsArticleById(string id)
        {
            return _articleRepository.GetById(id);
        }

        public void AddNewsArticle(NewsArticle article, List<int> tagIds)
        {
            _articleRepository.Add(article);
            _articleRepository.Save();

            // Add tags
            foreach (var tagId in tagIds)
            {
                var newsTag = new NewsTag
                {
                    NewsArticleID = article.NewsArticleID,
                    TagID = tagId
                };
                _newsTagRepository.Add(newsTag);
            }
            _newsTagRepository.Save();
        }

        public void UpdateNewsArticle(NewsArticle article, List<int> tagIds)
        {
            _articleRepository.Update(article);
            _articleRepository.Save();

            // Remove existing tags
            _newsTagRepository.DeleteByNewsArticleId(article.NewsArticleID);
            _newsTagRepository.Save();

            // Add new tags
            foreach (var tagId in tagIds)
            {
                var newsTag = new NewsTag
                {
                    NewsArticleID = article.NewsArticleID,
                    TagID = tagId
                };
                _newsTagRepository.Add(newsTag);
            }
            _newsTagRepository.Save();
        }

        public void DeleteNewsArticle(string id)
        {
            var article = _articleRepository.GetById(id);
            if (article != null)
            {
                _articleRepository.Delete(article);
                _articleRepository.Save();
            }
        }
    }
}
