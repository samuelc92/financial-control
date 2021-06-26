using FinancialControl.Api.BankAccount.Domain.Models;

namespace FinancialControl.Api.BankAccount.Domain.Ports.Repositories
{
    public interface IBaseRepository<TEntity> 
        where TEntity: BaseClass
    {
        public void Insert(TEntity entity);
        public TEntity GetById(string id);
    }
}