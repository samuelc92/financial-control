using System.Threading.Tasks;
using FinancialControl.Api.BankAccount.Domain.Ports.Repositories;
using FinancialControl.Api.BankAccount.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAccountRepository _repository;

        public AccountController(IMediator mediator, IAccountRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpGet]
        public ActionResult GetAll() =>
            Ok(_repository.GetAll());
        
        [HttpPost]
        public async Task Insert([FromBody] CreateAccountRequest request)
        {
            await _mediator.Send(request);
            NoContent();
        }
    }
}