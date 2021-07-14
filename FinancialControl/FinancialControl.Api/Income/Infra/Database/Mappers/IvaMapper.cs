using FinancialControl.Api.Income.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinancialControl.Api.Income.Infra.Database.Mappers
{
    public class IvaMapper
    {
        [BsonRepresentation(BsonType.Double)]
        public double Amount { get; set; }
        public IvaStatus Status { get; set; }

        public IvaMapper(double amount, IvaStatus status)
        {
            Amount = amount;
            Status = status;
        }
    }
}