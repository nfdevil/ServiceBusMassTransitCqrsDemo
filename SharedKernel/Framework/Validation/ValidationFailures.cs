using System.Collections.Generic;

using FluentValidation.Results;

namespace SharedKernel.Framework.Validation
{
    public class ValidationFailures
    {
        public IEnumerable<ValidationFailure> Failures { get; }

        public ValidationFailures(IEnumerable<ValidationFailure> validationFailures)
        {
            Failures = validationFailures;
        }
    }
}