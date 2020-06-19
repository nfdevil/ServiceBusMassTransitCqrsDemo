using System;

namespace ServiceBusMassTransitCqrsDemo.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; }

        public User(string emailAddress)
        {
            Id = Guid.NewGuid();
            EmailAddress = emailAddress;
        }

    }
}