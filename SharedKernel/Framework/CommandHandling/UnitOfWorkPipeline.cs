using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using SharedKernel.Framework.Data;
using SharedKernel.Framework.DomainEventsDispatching;

namespace SharedKernel.Framework.CommandHandling
{
    public class DomainEventHandlingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand
    {
        private readonly DbContext _dbContext;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public DomainEventHandlingPipelineBehavior(DbContext dbContext, IDomainEventDispatcher domainEventDispatcher)
        {
            _dbContext = dbContext;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            TResponse response = await next();
            await _domainEventDispatcher.DispatchEventsAsync();
            await _dbContext.SaveChangesAsync(cancellationToken);
            return response;
        }
    }
}
