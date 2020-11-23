
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyBank.Account.Domain.Aggregates;
using MyBank.Account.Domain.Models;

namespace MyBank.Account.Application.Commands
{

    public class TransferCommandHandler : IRequestHandler<TransferCommand, AccountModel>
    {
        private readonly IAccountRepository _accountRepository;

        public TransferCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<AccountModel> Handle(TransferCommand request, CancellationToken cancellationToken)
        {
            AccountModel fromAccount = await _accountRepository.GetAsync(request.FromAccountId);
            AccountModel toAccount = await _accountRepository.GetAsync(request.ToAccountId);
            fromAccount.TotalMoney = fromAccount.TotalMoney - request.TransfertAmount;
            toAccount.TotalMoney += request.TransfertAmount;
            await _accountRepository.Update(new AccountModel[] { fromAccount, toAccount });
             //TODO: Should create transaction record for deposit
            return fromAccount;
        }
    }
}
