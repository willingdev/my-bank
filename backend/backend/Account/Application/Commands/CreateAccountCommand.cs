using MediatR;
using MyBank.Account.Domain.Models;
using System.Runtime.Serialization;

namespace MyBank.Account.Application.Commands
{


    public class CreateAccountCommmand : IRequest<AccountModel>
    {
        public string CustomerId { get; set; }

    }
}
