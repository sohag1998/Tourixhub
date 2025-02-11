using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Application.Helpers
{
    public class AtLeastOneRequiredAttribute: ValidationAttribute
    {
        private readonly string _otherProperty;
        public AtLeastOneRequiredAttribute(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherValue = validationContext.ObjectType.GetProperty(_otherProperty)?
                .GetValue(validationContext.ObjectInstance);
            if (string.IsNullOrWhiteSpace(value?.ToString()) && string.IsNullOrWhiteSpace(otherValue?.ToString()))
            {
                return new ValidationResult($"Either {validationContext.MemberName} or {_otherProperty} must be provided.");
            }
            return ValidationResult.Success;
        }
    }
}
