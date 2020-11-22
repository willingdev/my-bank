using System.Threading.Tasks;
using MyBank.Account.Domain.Aggregates;
using MyBank.Account.Domain.Models;

namespace MyBank.Account.Infrastructure
{
    public class AccountRepository : IAccountRepository
    {
        public Task<AccountModel> Add(AccountModel account)
        {
            AccountModel accountModel = new AccountModel();
            accountModel.id = "abc";
            return Task.FromResult(accountModel);
        }

        public Task<AccountModel> GetAsync(string account)
        {
            throw new System.NotImplementedException();
        }

        public void Update(AccountModel account)
        {
            throw new System.NotImplementedException();
        }
    }
}
