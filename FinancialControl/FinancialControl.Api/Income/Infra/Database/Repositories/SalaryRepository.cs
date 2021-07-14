using System.Collections.Generic;
using System.Linq;
using FinancialControl.Api.Income.Domain.Enums;
using FinancialControl.Api.Income.Domain.Models;
using FinancialControl.Api.Income.Domain.Ports.Repositories;
using FinancialControl.Api.Income.Infra.Database.Mappers;
using FinancialControl.Api.Utils;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FinancialControl.Api.Income.Infra.Database.Repositories
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly IMongoCollection<SalaryMapper> _collection;

        public SalaryRepository(IFinanceControlDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<SalaryMapper>(SalaryMapper.CollectionName);
        }

        public void Insert(Salary salary) =>
            _collection.InsertOne(SalaryMapper.BuildFrom(salary));

        public void Update(Salary salary) =>
            _collection.ReplaceOne(p => p.Id.Equals(salary.Id), SalaryMapper.BuildFrom(salary));

        public Salary GetById(string id) =>
            _collection.Find(p => p.Id.Equals(id)).FirstOrDefault()?.ConvertToSalary();

        public IEnumerable<Salary> GetUnpaidIva() =>
            _collection.AsQueryable()
                .Where(p => IvaStatus.UNPAID == p.Iva.Status)
                .OrderByDescending(p => p.CreateDateTime)
                .ToList()
                .Select(p => p.ConvertToSalary());
        
        public double GetTotalIvaAmountUnpaid() =>
            _collection.AsQueryable()
                .Where(p => IvaStatus.UNPAID == p.Iva.Status)
                .Sum(p => p.Iva.Amount);
    }
}