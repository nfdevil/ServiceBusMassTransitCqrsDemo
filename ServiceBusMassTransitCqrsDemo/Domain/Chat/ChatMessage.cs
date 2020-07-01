using System;

using JetBrains.Annotations;

using ServiceBusMassTransitCqrsDemo.Domain.Chat;

using SharedKernel.Events;
using SharedKernel.Framework.Data;

using Message = ServiceBusMassTransitCqrsDemo.Domain.Chat.DomainEvents.Message;

namespace ServiceBusMassTransitCqrsDemo.Domain
{
    public class ChatMessage : Entity
    {
        [UsedImplicitly]
        private ChatMessage() { }

        public ChatMessage(Guid id, User user, Message message) : base(id)
        {
            User = user;
            Message = message;
            Created = DateTime.UtcNow;
        }

        public User User { get; }
        public Message Message { get; }
        public DateTime Created { get; }
    }
}
