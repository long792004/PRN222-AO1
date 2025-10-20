using NguyenThanhLong_SE18C.NET_A01.Models;
using NguyenThanhLong_SE18C.NET_A01.Repositories;

namespace NguyenThanhLong_SE18C.NET_A01.Services
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        List<Category> GetActiveCategories();
        Category? GetCategoryById(short id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        bool DeleteCategory(short id);
        bool IsCategoryUsedInNews(short categoryId);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public List<Category> GetAllCategories()
        {
            return _repository.GetAll();
        }

        public List<Category> GetActiveCategories()
        {
            return _repository.GetActiveCategories();
        }

        public Category? GetCategoryById(short id)
        {
            return _repository.GetById(id);
        }

        public void AddCategory(Category category)
        {
            _repository.Add(category);
            _repository.Save();
        }

        public void UpdateCategory(Category category)
        {
            _repository.Update(category);
            _repository.Save();
        }

        public bool DeleteCategory(short id)
        {
            if (_repository.IsCategoryUsedInNews(id))
            {
                return false;
            }

            var category = _repository.GetById(id);
            if (category != null)
            {
                _repository.Delete(category);
                _repository.Save();
                return true;
            }
            return false;
        }

        public bool IsCategoryUsedInNews(short categoryId)
        {
            return _repository.IsCategoryUsedInNews(categoryId);
        }
    }
}
