
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyBank.Account.Domain.Aggregates;
using MyBank.Account.Domain.Models;

namespace MyBank.Account.Application.Commands
{

    public class DepositCommandHandler : IRequestHandler<DepositCommand, AccountModel>
    {
        private readonly IAccountRepository _accountRepository;
        private double DEPOSIT_FEE = 0.001;

        public DepositCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<AccountModel> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            string accountId = request.AccountId;
            double actualDeposit = request.DepositAmount - request.DepositAmount * DEPOSIT_FEE;
            AccountModel account = await _accountRepository.GetAsync(accountId);
            account.TotalMoney = actualDeposit;
            await _accountRepository.Update(account);
            //TODO: Should create transaction record for deposit
            return account;
        }
    }
}
