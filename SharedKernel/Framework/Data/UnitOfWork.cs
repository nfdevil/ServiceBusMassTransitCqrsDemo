using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace SharedKernel.Framework.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        //private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(
            DbContext context
            /*IDomainEventsDispatcher domainEventsDispatcher*/) 
            // Implement https://github.com/kgrzybek/modular-monolith-with-ddd/tree/master/src/BuildingBlocks/Infrastructure/DomainEventsDispatching
        {
            this._context = context;
            //this._domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            //await this._domainEventsDispatcher.DispatchEventsAsync();
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
