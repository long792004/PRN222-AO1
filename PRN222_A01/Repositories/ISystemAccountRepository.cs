using NguyenThanhLong_SE18C.NET_A01.Models;

namespace NguyenThanhLong_SE18C.NET_A01.Repositories
{
    public interface ISystemAccountRepository : IGenericRepository<SystemAccount>
    {
        SystemAccount? GetByEmailAndPassword(string email, string password);
    }
}
