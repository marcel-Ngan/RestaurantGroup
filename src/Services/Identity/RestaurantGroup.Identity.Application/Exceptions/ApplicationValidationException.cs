using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace RestaurantGroup.Identity.Application.Exceptions
{
    public class ApplicationValidationException : Exception
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }

        public ApplicationValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ApplicationValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(
                    failureGroup => failureGroup.Key, 
                    failureGroup => failureGroup.ToArray()
                );
        }
    }
}