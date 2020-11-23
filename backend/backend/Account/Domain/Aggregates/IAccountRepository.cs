using System.Threading.Tasks;
using MyBank.Account.Domain.Models;

namespace MyBank.Account.Domain.Aggregates
{
    public interface IAccountRepository
    {
        Task<AccountModel> Add(AccountModel account);
        Task<AccountModel> Update(AccountModel account);
        Task<AccountModel> Update(AccountModel[] account);
        Task<AccountModel> GetAsync(string account);
    }
}
