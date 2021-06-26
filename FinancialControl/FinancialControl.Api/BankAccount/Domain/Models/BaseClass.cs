using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FinancialControl.Api.BankAccount.Domain.Models
{
    public abstract class BaseClass
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; protected set; } 
        public DateTime CreateDate { get; protected set; }
        public DateTime? UpdateDate { get; protected set; }

        protected BaseClass()
        {
            this.CreateDate = DateTime.UtcNow;
        }
    }
}