using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Validations
{
    public class FluentValidationResult
    {
        public bool IsValid { get; set; }

        public List<ValidationFailure> ValidationFailures { get; set; }

        public List<string> PropertyNames { get; set; }

        public List<string> ErrorMessages { get; set; }
    }
}
