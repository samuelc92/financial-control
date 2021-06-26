using FinancialControl.Api.BankAccount.Domain.Enums;

namespace FinancialControl.Api.BankAccount.Domain.Models
{
    public class Card : BaseClass
    {
        public string BankId { get; private set; }
        public CardType Type { get; private set; }

        public Card(string bankId, CardType type)
        {
            BankId = bankId;
            Type = type;
        }
    }
}