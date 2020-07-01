using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

using SharedKernel.Framework.Functional;

namespace SharedKernel.Framework.Validation
{
    public class ValidatorPipelineBehavior<TRequest> : IPipelineBehavior<TRequest, Result<Unit, ValidationFailures>>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<Result<Unit, ValidationFailures>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Result<Unit, ValidationFailures>> next)
        {
            List<ValidationFailure> failures = _validators
                                               .Select(validator => validator.Validate(request))
                                               .SelectMany(result => result.Errors)
                                               .Where(error => error != null)
                                               .ToList();
            return failures.Any() ? Task.FromResult(Result.Failure<Unit, ValidationFailures>(new ValidationFailures(failures))) : next();
        }
    }
}