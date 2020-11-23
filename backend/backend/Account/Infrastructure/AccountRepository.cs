using System.Collections.Generic;
using System.Threading.Tasks;
using MyBank.Account.Domain.Aggregates;
using MyBank.Account.Domain.Models;

namespace MyBank.Account.Infrastructure
{



    public class AccountRepository : IAccountRepository
    {
        private string[] availableAccountIds = { "NL81RABO8479662646", "NL96INGB7843842861", "NL77ABNA7852130259" };
        private Dictionary<string, AccountModel> database = new Dictionary<string, AccountModel>();
        private int accountIdIndex = 0;
        public Task<AccountModel> Add(AccountModel account)
        {

            AccountModel accountModel = new AccountModel();
            accountModel.Id = availableAccountIds[accountIdIndex++];
            accountModel.CustomerId = account.CustomerId;
            database.Add(accountModel.Id, accountModel);
            return Task.FromResult(accountModel);
        }

        public Task<AccountModel> GetAsync(string account)
        {

            try
            {
                return Task.FromResult(database[account]);
            }
            catch (System.Collections.Generic.KeyNotFoundException e)
            {
                return null;
            }

        }

        public Task<AccountModel> Update(AccountModel account)
        {
            throw new System.NotImplementedException();
        }
    }
}
