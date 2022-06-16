
using Paramore.Brighter;

namespace ExpenseService.Api.Ports.Requests;

public class ExpenseCreatedEvent : Event
{
    public string Category { get; set; }

    public ExpenseCreatedEvent(Guid id, string category) : base(id)
    {
       Category = category;
    }
}