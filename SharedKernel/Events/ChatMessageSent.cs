using System;

using MediatR;

namespace SharedKernel.Events
{
    public class ChatMessageSent : INotification
    {
        public ChatMessageSent(Guid userId, string message, DateTime postTime)
        {
            UserId = userId;
            Message = message;
            PostTime = postTime;
        }

        public string Message { get; }
        public Guid UserId { get; }
        public DateTime PostTime { get; }
    }
}