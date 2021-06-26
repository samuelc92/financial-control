using FinancialControl.Api.BankAccount.Domain.Models;
using FinancialControl.Api.BankAccount.Domain.Ports.Repositories;
using FinancialControl.Api.Utils;
using MongoDB.Driver;

namespace FinancialControl.Api.BankAccount.Infra.Database.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseClass
    {
        private readonly IMongoClient _mongoClient;
        private readonly string _databaseName;
        private readonly string _collectionName;

        protected BaseRepository(IMongoClient mongoClient, IFinanceControlDatabaseSettings financeControlDatabaseSettings,
            string collectionName)
        {
            _mongoClient = mongoClient;
            _databaseName = financeControlDatabaseSettings.DatabaseName;
            _collectionName = collectionName;
        }

        public void Insert(TEntity entity) =>
            Collection.InsertOne(entity);

        public TEntity GetById(string id) =>
            Collection.Find(p => p.Id.Equals(id)).FirstOrDefault();
        
        protected virtual IMongoCollection<TEntity> Collection =>
            _mongoClient.GetDatabase(_databaseName).GetCollection<TEntity>(_collectionName);
    }
}