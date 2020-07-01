using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using SharedKernel.Framework.Data;

namespace SharedKernel.Framework.DomainEventsDispatching
{
    public class DomainEventsAccessor : IDomainEventsAccessor
    {
        private readonly DbContext _meetingsContext;

        public DomainEventsAccessor(DbContext meetingsContext)
        {
            _meetingsContext = meetingsContext;
        }

        public List<IDomainEvent> GetAllDomainEvents()
        {
            List<EntityEntry<AggregateRoot>> aggregates = _meetingsContext.ChangeTracker
                                                                          .Entries<AggregateRoot>()
                                                                          .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            return aggregates
                   .SelectMany(x => x.Entity.DomainEvents)
                   .ToList();
        }

        public void ClearAllDomainEvents()
        {
            List<EntityEntry<AggregateRoot>> aggregates = _meetingsContext.ChangeTracker
                                                                          .Entries<AggregateRoot>()
                                                                          .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            aggregates
                .ForEach(aggregate => aggregate.Entity.ClearDomainEvents());
        }
    }
}