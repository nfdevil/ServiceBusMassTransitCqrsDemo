using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Autofac;
using Autofac.Core;

using MassTransit;

using MediatR;

using Newtonsoft.Json;

using SharedKernel.Events;
using SharedKernel.Framework.Data;

namespace SharedKernel.Framework.DomainEventsDispatching
{
    public interface IDomainEventDispatcher
    {
        Task DispatchEventsAsync();
    }

    public class DomainEventsDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly IBus _bus;
        private readonly ILifetimeScope _scope;
        private readonly IDomainEventsAccessor _domainEventsProvider;

        public DomainEventsDispatcher(
            IMediator mediator,
            IBus bus,
            ILifetimeScope scope,
            IDomainEventsAccessor domainEventsProvider)
        {
            _mediator = mediator;
            _bus = bus;
            _scope = scope;
            _domainEventsProvider = domainEventsProvider;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEvents = _domainEventsProvider.GetAllDomainEvents();


            var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();
            foreach (var domainEvent in domainEvents)
            {
                Type domainEvenNotificationType = typeof(IDomainEventNotification<>);
                var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
                var domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
                {
                    new NamedParameter("domainEvent", domainEvent),
                    new NamedParameter("id", Guid.NewGuid())
                });

                if (domainNotification != null)
                {
                    domainEventNotifications.Add(domainNotification as IDomainEventNotification<IDomainEvent>);
                }
            }

            _domainEventsProvider.ClearAllDomainEvents();

            IEnumerable<Task> tasks = domainEvents
                .Select(async domainEvent =>
                {
                    await _mediator.Publish(domainEvent);
                });
            tasks = tasks.Concat(domainEventNotifications.Select(async notification =>
            {
                await _bus.Publish(notification);
            }));

            await Task.WhenAll(tasks);

            /*
            foreach (var domainEventNotification in domainEventNotifications)
            {
                string type = domainEventNotification.GetType().FullName;
                var data = JsonConvert.SerializeObject(domainEventNotification, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                });
                OutboxMessage outboxMessage = new OutboxMessage(
                    domainEventNotification.Id,
                    domainEventNotification.DomainEvent.OccurredOn,
                    type,
                    data);
                _outbox.Add(outboxMessage);
            }
             */
        }
    }
}