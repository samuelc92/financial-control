using System.Threading;
using System.Threading.Tasks;
using FinancialControl.Api.BankAccount.Domain.Models;
using FinancialControl.Api.BankAccount.Domain.Ports.Repositories;
using FinancialControl.Api.BankAccount.Domain.Requests;
using MediatR;

namespace FinancialControl.Api.BankAccount.Domain.Handlers
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountRequest>
    {
        private readonly IAccountRepository _repository;

        public CreateAccountHandler(IAccountRepository repository)
        {
            _repository = repository;
        }
        
        public Task<Unit> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
        {
            var account = new Account(request.Bank, request.Iban);
           _repository.Insert(account);
           return Task.FromResult(Unit.Value);
        }
    }
}