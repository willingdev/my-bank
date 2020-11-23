using MediatR;
using MyBank.Account.Domain.Models;
using System.Runtime.Serialization;

namespace MyBank.Account.Application.Commands
{


    public class TransferCommand : IRequest<AccountModel>
    {
        public string FromAccountId { get; set; }
        public string ToAccountId { get; set; }
        public double TransfertAmount { get; set; }


    }
}
