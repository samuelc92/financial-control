using System.Threading.Tasks;
using FinancialControl.Api.Income.Domain.Ports.Repositories;
using FinancialControl.Api.Income.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISalaryRepository _repository;

        public SalaryController(IMediator mediator,
            ISalaryRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpPost]
        public async Task Insert([FromBody] RegisterSalaryRequest request)
        {
            await _mediator.Send(request);
            NoContent();
        }

        [HttpGet("iva/unpaid/total")]
        public ActionResult GetTotalIvaUnpaid() =>
            Ok(_repository.GetTotalIvaAmountUnpaid());
        
        [HttpGet("iva/unpaid")]
        public ActionResult GetUnpaidIva() =>
            Ok(_repository.GetUnpaidIva());
    }
}