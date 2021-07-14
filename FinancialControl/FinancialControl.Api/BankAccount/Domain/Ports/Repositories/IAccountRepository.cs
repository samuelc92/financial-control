using System.Collections.Generic;
using FinancialControl.Api.BankAccount.Domain.Models;

namespace FinancialControl.Api.BankAccount.Domain.Ports.Repositories
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        IEnumerable<Account> GetAll();
    }
}