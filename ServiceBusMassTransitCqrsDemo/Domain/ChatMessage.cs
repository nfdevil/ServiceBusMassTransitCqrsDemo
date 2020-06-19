using System;

namespace ServiceBusMassTransitCqrsDemo.Domain
{
    public class ChatMessage
    {
        public ChatMessage(User user, string message)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Created = DateTime.UtcNow;
        }

        public string Message { get; }
        public User User { get; }
        public DateTime Created { get; }
    }
}
