using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ServiceBusMassTransitCqrsDemo.Domain.Chat.DomainEvents;

using SharedKernel.Framework.Data;

namespace ServiceBusMassTransitCqrsDemo.Domain.Chat
{
    public class Chat : AggregateRoot
    {
        private readonly ICollection<ChatMessage> _chatMessages = new List<ChatMessage>();
        public IReadOnlyList<ChatMessage> ChatMessages => _chatMessages.ToList();

        public void SendChatMessage(User user, Message message)
        {
            var chatMessage = new ChatMessage(Guid.NewGuid(), user, message);
            RaiseDomainEvent(new ChatMessageSent(Id, chatMessage.Message, chatMessage.Created));

            //Do this in UoW
            // await _bus.Publish(new ChatMessageSent(Id, chatMessage.Message, chatMessage.Created), cancellationToken);

            _chatMessages.Add(chatMessage);
        }
    }
}
