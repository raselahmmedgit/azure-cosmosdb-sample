using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace lab.LocalCosmosDbApp.Validations
{
    public static class FluentValidationHelper
    {
        public static FluentValidationResult IsValid<T, V>(T model) 
            where T : new()
            where V : AbstractValidator<T>, new()
        {
            var validator = new V();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                List<ValidationFailure> validationFailureList = new List<ValidationFailure>();
                List<string> propertyNameList = new List<string>();
                List<string> errorMessageList = new List<string>();

                foreach (var item in validationResult.Errors)
                {
                    if (!string.IsNullOrEmpty(item.ErrorMessage))
                    {
                        propertyNameList.Add(item.PropertyName);
                        errorMessageList.Add(item.ErrorMessage);
                        validationFailureList.Add(new ValidationFailure(item.PropertyName, item.ErrorMessage));
                    }
                }

                return new FluentValidationResult()
                {
                    IsValid = false,
                    ValidationFailures = validationFailureList,
                    PropertyNames = propertyNameList,
                    ErrorMessages = errorMessageList
                };
            }

            return new FluentValidationResult()
            {
                IsValid = true,
                ValidationFailures = null,
                PropertyNames = null,
                ErrorMessages = null
            };
        }
    }
}
