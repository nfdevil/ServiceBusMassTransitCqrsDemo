using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel.Framework.Data
{
    public abstract class AggregateRoot : Entity
    {
        public byte[] RowVersion { get; }

        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
