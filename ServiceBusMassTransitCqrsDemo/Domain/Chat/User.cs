using System;
using System.Collections.Generic;
using System.Linq;

using SharedKernel.Framework.Data;

using ChatMessageSent = ServiceBusMassTransitCqrsDemo.Domain.Chat.DomainEvents.ChatMessageSent;
using Message = ServiceBusMassTransitCqrsDemo.Domain.Chat.DomainEvents.Message;

namespace ServiceBusMassTransitCqrsDemo.Domain.Chat
{
    public class User : AggregateRoot
    {
        public string EmailAddress { get; }

        private User(){}
        public User(string emailAddress) : this()
        {
            EmailAddress = emailAddress;
        }

        
    }
}