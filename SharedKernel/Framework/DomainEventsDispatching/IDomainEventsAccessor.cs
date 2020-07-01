using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using SharedKernel.Framework.Data;

namespace SharedKernel.Framework.DomainEventsDispatching
{
    public interface IDomainEventsAccessor
    {
        List<IDomainEvent> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}
