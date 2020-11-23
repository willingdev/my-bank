using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBank.Account.Application.Commands;
using MyBank.Account.Domain.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<AccountModel>> CreateAccountAsync([FromBody] CreateAccountCommmand createAccountCommmand)
        {
            return await _mediator.Send(createAccountCommmand);
        }
        [Route("deposit")]
        [HttpPost]
        public async Task<ActionResult<AccountModel>> DepositAsync([FromBody] DepositCommand depositCommand)
        {
            return await _mediator.Send(depositCommand);
        }
        [Route("transfer")]
        [HttpPost]
        public async Task<ActionResult<AccountModel>> DepositAsync([FromBody] TransferCommand transferCommand)
        {
            return await _mediator.Send(transferCommand);
        }
        [Route("get")]
        [HttpPost]
        public async Task<ActionResult<AccountModel>> GetAccountAsync([FromBody] GetAccountCommand getAccountCommmand)
        {
            return await _mediator.Send(getAccountCommmand);
        }

    }
}
