using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CSharpFunctionalExtensions;

using FluentValidation.Results;

using Newtonsoft.Json;

namespace SharedKernel.Framework.Functional
{
    public static class ResultExtensions
    {
        private static string _validationFailuresCheck = "[[ValidationFailures]]";
        public static Result ToResult(this IEnumerable<ValidationFailure> validationFailures)
            => Result.Failure(_validationFailuresCheck + JsonConvert.SerializeObject(validationFailures.Select(x => x.ErrorMessage)));
        public static IEnumerable<string> GetErrorMessages(this Result result) 
            => result.Error.StartsWith(_validationFailuresCheck) 
                   ? JsonConvert.DeserializeObject<IEnumerable<string>>(result.Error.Substring(_validationFailuresCheck.Length)) 
                   : new []{ result.Error };
    }
}
