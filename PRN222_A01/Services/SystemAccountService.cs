using NguyenThanhLong_SE18C.NET_A01.Models;
using NguyenThanhLong_SE18C.NET_A01.Repositories;

namespace NguyenThanhLong_SE18C.NET_A01.Services
{
    public interface ISystemAccountService
    {
        List<SystemAccount> GetAllAccounts();
        SystemAccount? GetAccountById(short id);
        SystemAccount? Login(string email, string password);
        void AddAccount(SystemAccount account);
        void UpdateAccount(SystemAccount account);
        void DeleteAccount(short id);
    }

    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepository _repository;

        public SystemAccountService(ISystemAccountRepository repository)
        {
            _repository = repository;
        }

        public List<SystemAccount> GetAllAccounts()
        {
            return _repository.GetAll();
        }

        public SystemAccount? GetAccountById(short id)
        {
            return _repository.GetById(id);
        }

        public SystemAccount? Login(string email, string password)
        {
            return _repository.GetByEmailAndPassword(email, password);
        }

        public void AddAccount(SystemAccount account)
        {
            _repository.Add(account);
            _repository.Save();
        }

        public void UpdateAccount(SystemAccount account)
        {
            _repository.Update(account);
            _repository.Save();
        }

        public void DeleteAccount(short id)
        {
            var account = _repository.GetById(id);
            if (account != null)
            {
                _repository.Delete(account);
                _repository.Save();
            }
        }
    }
}
