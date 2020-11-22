using MediatR;
using MyBank.Account.Domain.Models;
using System.Runtime.Serialization;

namespace MyBank.Account.Application.Commands
{


    public class GetAccountCommand : IRequest<AccountModel>
    {


        public string AccountId { get; set; }


    }
}
