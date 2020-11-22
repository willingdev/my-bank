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


    }
}
