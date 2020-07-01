using System;

using JetBrains.Annotations;

using SharedKernel.Events;
using SharedKernel.Framework.Data;

using Message = ServiceBusMassTransitCqrsDemo.Domain.Chat.DomainEvents.Message;

namespace ServiceBusMassTransitCqrsDemo.Domain
{
    public class ChatMessage : Entity
    {
        [UsedImplicitly]
        private ChatMessage() { }

        public ChatMessage(Guid id, Message message) : base(id)
        {
            Message = message;
            Created = DateTime.UtcNow;
        }

        public Message Message { get; }
        public DateTime Created { get; }
    }
}
