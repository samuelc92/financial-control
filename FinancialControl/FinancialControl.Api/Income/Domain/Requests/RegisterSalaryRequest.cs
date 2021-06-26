using MediatR;

namespace FinancialControl.Api.Income.Domain.Requests
{
    public class RegisterSalaryRequest : IRequest
    {
        public double BaseSalary { get; set; }
        public double AmountReceived { get; set; } 
    }
}