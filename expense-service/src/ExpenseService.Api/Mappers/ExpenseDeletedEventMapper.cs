using ExpenseService.Api.Ports.Events;
using Paramore.Brighter;
using System.Text.Json;

namespace ExpenseService.Api.Mappers;

public class ExpenseDeletedEventMapper : IAmAMessageMapper<ExpenseDeletedEvent>
{
    public Message MapToMessage(ExpenseDeletedEvent request)
    {
        var header = new MessageHeader(messageId: request.Id, topic: nameof(ExpenseDeletedEvent), messageType: MessageType.MT_EVENT, contentType: "application/json");
        var body = new MessageBody(JsonSerializer.Serialize(request, new JsonSerializerOptions(JsonSerializerDefaults.General)));
        return new Message(header, body);
    }

    public ExpenseDeletedEvent MapToRequest(Message message) => throw new NotImplementedException();
}