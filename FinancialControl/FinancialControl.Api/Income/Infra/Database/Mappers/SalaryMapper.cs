using System;
using FinancialControl.Api.Income.Domain.Models;
using FinancialControl.Api.Income.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinancialControl.Api.Income.Infra.Database.Mappers
{
    public class SalaryMapper
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonRepresentation(BsonType.Double)]
        public double BaseSalary { get; set; }
        
        [BsonRepresentation(BsonType.Double)]
        public double Irs { get; set; }
        
        [BsonRepresentation(BsonType.Double)]
        public double AmountReceived { get; set; } 
        
        public IvaMapper Iva { get; set; }
        
        public DateTime CreateDateTime { get; protected set; }
        
        public DateTime? UpdateDateTime { get; protected set; }

        public static string CollectionName
        {
            get
            {
                return "Salary";
            }
        }

        public SalaryMapper(string id, double baseSalary, double amountReceived, double irs, IvaMapper iva,
            DateTime createDateTime, DateTime? updateDateTime)
        {
            Id = id;
            BaseSalary = baseSalary;
            AmountReceived = amountReceived;
            Irs = irs;
            Iva = iva;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
        }

        public static SalaryMapper BuildFrom(Salary salary) =>
            new SalaryMapper(salary.Id, salary.BaseSalary, salary.AmountReceived, salary.Irs, new IvaMapper(
                salary.Iva.Amount, salary.Iva.Status), salary.CreateDateTime, salary.UpdateDateTime);   
        
        public Salary ConvertToSalary() =>
            new Salary(Id, BaseSalary, AmountReceived, Irs, new Iva(Iva.Amount, Iva.Status), CreateDateTime);
    }
}