using CSharpFunctionalExtensions;

using MediatR;

namespace SharedKernel.Framework
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
        where TQuery : IQuery<TResult> { }
}