using FinancialControl.Api.Income.Domain.Enums;
using FinancialControl.Api.Utils;

namespace FinancialControl.Api.Income.Domain.ValueObjects
{
    public class Iva
    {
        private double TAX_IVA_PERCENT = 23;
        
        public double Amount { get; private set; }
        public IvaStatus Status { get; private set; }
        public Iva(double baseSalary)
        {
            Status = IvaStatus.UNPAID;
            CalculateIva(baseSalary);
        }

        public Iva(double amount, IvaStatus status)
        {
            Amount = amount;
            Status = status;
        }

        private void CalculateIva(double baseSalary)
        {
            Amount = CalculateUtils.CalculaValueOfPercent(baseSalary, TAX_IVA_PERCENT);
        }
    }
}