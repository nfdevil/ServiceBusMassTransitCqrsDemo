using System;

namespace SharedKernel.Framework.Data
{
    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase()
        {
            OccuredOn = DateTime.UtcNow;
        }
        public DateTime OccuredOn { get; }
    }
}