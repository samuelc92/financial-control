using ExpenseService.Api.Ports.Requests;
using Paramore.Brighter;
using System.Text.Json;

namespace ExpenseService.Api.Mappers;

public class ExpenseCreatedEventMapper : IAmAMessageMapper<ExpenseCreatedEvent>
{
    public Message MapToMessage(ExpenseCreatedEvent request)
    {
        var header = new MessageHeader(messageId: request.Id, topic: "expense-created", messageType: MessageType.MT_EVENT);
        var body = new MessageBody(JsonSerializer.Serialize(request, new JsonSerializerOptions(JsonSerializerDefaults.General)));
        return new Message(header, body);
    }

    public ExpenseCreatedEvent MapToRequest(Message message) => throw new NotImplementedException();
}