using FinancialControl.Api.BankAccount.Domain.Enums;
using MediatR;

namespace FinancialControl.Api.BankAccount.Domain.Requests
{
    public class CreateAccountRequest : IRequest
    {
        public Banks Bank { get; set; }
        
        public string Iban { get; set; }
    }
}