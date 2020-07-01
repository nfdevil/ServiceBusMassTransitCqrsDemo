using System;
using System.Collections.Generic;
using System.Linq;

using SharedKernel.Events;
using SharedKernel.Framework.Data;

using ChatMessageSent = ServiceBusMassTransitCqrsDemo.Domain.Chat.DomainEvents.ChatMessageSent;
using Message = ServiceBusMassTransitCqrsDemo.Domain.Chat.DomainEvents.Message;

namespace ServiceBusMassTransitCqrsDemo.Domain
{
    public class User : Entity
    {
        public string EmailAddress { get; }
        private readonly ICollection<ChatMessage> _chatMessages = new List<ChatMessage>();
        public IReadOnlyList<ChatMessage> ChatMessages => _chatMessages.ToList();

        private User(){}
        public User(string emailAddress) : this()
        {
            EmailAddress = emailAddress;
        }

        public void SendChatMessage(Message message)
        {
            var chatMessage = new ChatMessage(Guid.NewGuid(), message);
            RaiseDomainEvent(new ChatMessageSent(Id, chatMessage.Message, chatMessage.Created));

            //Do this in UoW
            // await _bus.Publish(new ChatMessageSent(Id, chatMessage.Message, chatMessage.Created), cancellationToken);

            _chatMessages.Add(chatMessage);
        }
    }
}