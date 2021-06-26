using System.Threading;
using System.Threading.Tasks;
using FinancialControl.Api.Income.Domain.Models;
using FinancialControl.Api.Income.Domain.Ports.Repositories;
using FinancialControl.Api.Income.Domain.Requests;
using MediatR;

namespace FinancialControl.Api.Income.Domain.Handlers
{
    public class RegisterSalaryHandler: IRequestHandler<RegisterSalaryRequest>
    {
        private readonly ISalaryRepository _repository;

        public RegisterSalaryHandler(ISalaryRepository repository)
        {
            _repository = repository;
        }
        
        public Task<Unit> Handle(RegisterSalaryRequest request, CancellationToken cancellationToken)
        {
           var salary = new Salary(request.BaseSalary, request.AmountReceived);
            _repository.Insert(salary);    
           return Unit.Task;
        }
    }
}