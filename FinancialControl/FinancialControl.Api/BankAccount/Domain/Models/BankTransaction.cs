using FinancialControl.Api.BankAccount.Domain.Enums;

namespace FinancialControl.Api.BankAccount.Domain.Models
{
    public class BankTransaction : BaseClass
    {
        public string BankId { get; private set; }
        public TransactionType Type { get; private set; }
        public double Amount { get; private set; }

        public BankTransaction(string bankId, TransactionType type, double amount)
        {
            amount = SetAmountNegativeIfTypeIsDebit(type, amount); 
            ConstructorDefault(string.Empty, bankId, type, amount); 
        }

        private void ConstructorDefault(string id, string bankId, TransactionType type, double amount)
        {
            Id = id;
            BankId = bankId;
            Type = type;
            Amount = amount;
        }

        private double SetAmountNegativeIfTypeIsDebit(TransactionType type, double amount) =>
            TransactionType.Debit.Equals(type) ? amount * -1 : amount;
    }
}