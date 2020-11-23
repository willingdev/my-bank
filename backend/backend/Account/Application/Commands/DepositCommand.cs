using MediatR;
using MyBank.Account.Domain.Models;
using System.Runtime.Serialization;

namespace MyBank.Account.Application.Commands
{


    public class DepositCommand : IRequest<AccountModel>
    {


        public string AccountId { get; set; }
        public double DepositAmount {get;set;}


    }
}
