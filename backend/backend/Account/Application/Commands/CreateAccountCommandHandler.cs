
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyBank.Account.Domain.Aggregates;
using MyBank.Account.Domain.Models;

namespace MyBank.Account.Application.Commands
{

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommmand, AccountModel>
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<AccountModel> Handle(CreateAccountCommmand request, CancellationToken cancellationToken)
        {
            AccountModel account = new AccountModel();
            account.CustomerId = request.CustomerId;
            account.Id = AccountNumberGenerator.Next();
            return await _accountRepository.Add(account);
        }
    }
}
