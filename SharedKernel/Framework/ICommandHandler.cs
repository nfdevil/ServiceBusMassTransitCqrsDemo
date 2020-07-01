using CSharpFunctionalExtensions;

using MediatR;

using SharedKernel.Framework.Validation;

namespace SharedKernel.Framework
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result<Unit, ValidationFailures>>
        where TCommand : ICommand { }
}