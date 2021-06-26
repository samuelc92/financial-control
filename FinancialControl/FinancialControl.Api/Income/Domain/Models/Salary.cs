using System;
using FinancialControl.Api.Income.Domain.ValueObjects;
using FinancialControl.Api.Utils;

namespace FinancialControl.Api.Income.Domain.Models
{
    public class Salary : BaseClass
    {
        private double TAX_IRS_PERCENT = 25;
        public double BaseSalary { get; private set; }
        
        public double Irs { get; private set; }
        
        public double AmountReceived { get; private set; } 
        
        public Iva Iva { get; private set; }

        public Salary(double baseSalary, double amountReceived)
        {
            BaseSalary = baseSalary;
            AmountReceived = amountReceived;
            CalculateIrs();
            Iva = new Iva(baseSalary);
        }

        public Salary(string id, double baseSalary, double amountReceived, double irs, Iva iva, DateTime createDateTime)
        {
            Id = id;
            BaseSalary = baseSalary;
            AmountReceived = amountReceived;
            Iva = iva;
            Irs = irs;
            CreateDateTime = createDateTime;
        }

        private void CalculateIrs()
        {
            Irs = CalculateUtils.CalculaValueOfPercent(BaseSalary, TAX_IRS_PERCENT);
        }
    }
    
}