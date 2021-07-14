using FinancialControl.Api.BankAccount.Domain.Enums;

namespace FinancialControl.Api.BankAccount.Domain.Models
{
    public class Account : BaseClass
    {
        public Banks Bank { get; private set; }
        public string Iban { get; private set; }

        public Account(Banks bank, string iban)
        {
            Bank = bank;
            Iban = iban;
        }
    }
}