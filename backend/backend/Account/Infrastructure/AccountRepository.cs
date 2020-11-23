using System.Collections.Generic;
using System.Threading.Tasks;
using MyBank.Account.Domain.Aggregates;
using MyBank.Account.Domain.Models;
using Dapper;
using System.Data.SqlClient;

namespace MyBank.Account.Infrastructure
{

    public class AccountRepository : IAccountRepository
    {
        private string[] availableAccountIds = { "NL81RABO8479662646", "NL96INGB7843842861", "NL77ABNA7852130259" };
        private Dictionary<string, AccountModel> database = new Dictionary<string, AccountModel>();
        private int accountIdIndex = 0;

        private string connectionString = "Server=172.18.0.2;Database=mybank;User ID=sa;Password=yourStrong(!)Password;";
        public async Task<AccountModel> Add(AccountModel account)
        {

            string sql = $@"INSERT INTO
                            [dbo].[account] (
                                [id],
                                [customer_id],
                                [created],
                                [updated],
                                [total_money]
                            )
                            VALUES
                                (
                                    @account_id,
                                    @customer_id,
                                    getdate(),
                                    getdate(),
                                    0.0
                                );";
            using (var sqlConnection = new SqlConnection(connectionString))
            {

                await sqlConnection.ExecuteAsync(sql, new
                {
                    account_id = account.Id,
                    customer_id = account.CustomerId,
                });

            }
            return account;
        }

        public async Task<AccountModel> GetAsync(string accountId)
        {
            string sql = $@"SELECT
                            id,
                            customer_id as CustomerId,
                            total_money as TotalMoney
                        FROM
                            [dbo].[account]
                        WHERE
                            id = @account_id";

            using (var sqlConnection = new SqlConnection(connectionString))
            {

                AccountModel result = await sqlConnection.QueryFirstOrDefaultAsync<AccountModel>(sql, new
                {
                    account_id = accountId
                });
                return result;
            }
        }

        public Task<AccountModel> Update(AccountModel account)
        {
            throw new System.NotImplementedException();
        }
        public Task<AccountModel> Update(AccountModel[] account)
        {
            throw new System.NotImplementedException();
        }
    }
}
