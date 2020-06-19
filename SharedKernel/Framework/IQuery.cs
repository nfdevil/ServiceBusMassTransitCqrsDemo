using CSharpFunctionalExtensions;

using MediatR;

namespace SharedKernel.Framework
{
    public interface IQuery<TResult> : IRequest<Result<TResult>> { }
}