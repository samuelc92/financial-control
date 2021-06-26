using System.Collections.Generic;
using FinancialControl.Api.BankAccount.Domain.Models;
using FinancialControl.Api.BankAccount.Domain.Ports.Repositories;
using FinancialControl.Api.Utils;
using MongoDB.Driver;

namespace FinancialControl.Api.BankAccount.Infra.Database.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IMongoClient mongoClient, IFinanceControlDatabaseSettings financeControlDatabaseSettings) 
            : base(mongoClient, financeControlDatabaseSettings, nameof(Account))
        { }

        public IEnumerable<Account> GetAll() =>
            Collection.Find(Builders<Account>.Filter.Empty).ToList();
    }
}