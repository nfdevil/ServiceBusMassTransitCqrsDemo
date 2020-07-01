using System;

using SharedKernel.Framework.Data;

namespace ServiceBusMassTransitCqrsDemo.Domain.Chat.DomainEvents
{
    public class ChatMessageSent : DomainEventBase
    {
        public ChatMessageSent(Guid userId, Message message, DateTime postTime)
        {
            UserId = userId;
            Message = message;
            PostTime = postTime;
        }

        public Message Message { get; }
        public Guid UserId { get; }
        public DateTime PostTime { get; }
    }
}