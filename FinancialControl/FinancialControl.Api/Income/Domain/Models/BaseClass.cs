using System;

namespace FinancialControl.Api.Income.Domain.Models
{
    public class BaseClass
    {
        public string Id { get; protected set; } 
        public DateTime CreateDateTime { get; protected set; }
        public DateTime UpdateDateTime { get; protected set; }

        protected BaseClass()
        {
            this.CreateDateTime = DateTime.UtcNow;
        }
    }
}