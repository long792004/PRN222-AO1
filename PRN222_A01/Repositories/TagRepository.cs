using NguyenThanhLong_SE18C.NET_A01.Models;

namespace NguyenThanhLong_SE18C.NET_A01.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        public TagRepository(FUNewsManagementContext context) : base(context)
        {
        }
    }
}
