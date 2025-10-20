using NguyenThanhLong_SE18C.NET_A01.Models;
using NguyenThanhLong_SE18C.NET_A01.Repositories;

namespace NguyenThanhLong_SE18C.NET_A01.Services
{
    public interface ITagService
    {
        List<Tag> GetAllTags();
        Tag? GetTagById(int id);
    }

    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }

        public List<Tag> GetAllTags()
        {
            return _repository.GetAll();
        }

        public Tag? GetTagById(int id)
        {
            return _repository.GetById(id);
        }
    }
}
