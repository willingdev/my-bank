
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyBank.Account.Domain.Aggregates;
using MyBank.Account.Domain.Models;

namespace MyBank.Account.Application.Commands
{

    public class GetAccountCommandHandler : IRequestHandler<GetAccountCommand, AccountModel>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<AccountModel> Handle(GetAccountCommand request, CancellationToken cancellationToken)
        {
            return await _accountRepository.GetAsync(request.AccountId);
        }
    }
}
