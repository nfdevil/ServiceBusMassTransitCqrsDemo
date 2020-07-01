using System;
using System.Collections.Generic;

using CSharpFunctionalExtensions;

namespace ServiceBusMassTransitCqrsDemo.Domain.Chat.DomainEvents
{
    public class Message : ValueObject
    {
        public string Text { get; }
        public Message(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(text));
            }

            Text = text;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Text;
        }
    }
}