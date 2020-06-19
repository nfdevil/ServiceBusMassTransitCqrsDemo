using CSharpFunctionalExtensions;

using MediatR;

namespace SharedKernel.Framework
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommand { }
}