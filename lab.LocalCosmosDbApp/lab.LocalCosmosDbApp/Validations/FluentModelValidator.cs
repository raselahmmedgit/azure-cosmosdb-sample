using FluentValidation;
using lab.LocalCosmosDbApp.Helpers;
using lab.LocalCosmosDbApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab.LocalCosmosDbApp.Validations
{
    public class ToolInfoApproverSourceSearchValidator : AbstractValidator<ToolInfoApproverSourceSearch>
    {
        public ToolInfoApproverSourceSearchValidator()
        {
            RuleFor(x => x.BeginDate).Must(BeAValidDate)
                .WithMessage(string.Format(MessageHelper.InvalidDateTime, nameof(ToolInfoApproverSourceSearch.BeginDate)));
            RuleFor(x => x.EndDate).Must(BeAValidDate)
                .WithMessage(string.Format(MessageHelper.InvalidDateTime, nameof(ToolInfoApproverSourceSearch.EndDate)));
            RuleFor(x => x).Must(IsValidBeginEndDateFormat).OverridePropertyName(x => x.BeginDate)
                .WithMessage(string.Format(MessageHelper.InvalidBeginEndDateTime, nameof(ToolInfoApproverSourceSearch.BeginDate), nameof(ToolInfoApproverSourceSearch.EndDate)));
            RuleFor(x => x).Must(IsBeginDateProvided).OverridePropertyName(x => x.BeginDate)
                .WithMessage(string.Format(MessageHelper.InvalidEndDateTime, nameof(ToolInfoApproverSourceSearch.BeginDate)));

        }

        private bool BeAValidDate(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return DateTime.TryParse(value, out DateTime date);
            }
            return true;
        }

        private bool IsValidBeginEndDateFormat(ToolInfoApproverSourceSearch toolInfoApproverSourceSearch)
        {
            DateTime? beginDate = null, endDate = null;
            if (!string.IsNullOrEmpty(toolInfoApproverSourceSearch.BeginDate)
                && DateTime.TryParse(toolInfoApproverSourceSearch.BeginDate, out DateTime begin))
            {
                beginDate = begin;
            }
            if (!string.IsNullOrEmpty(toolInfoApproverSourceSearch.EndDate)
                && DateTime.TryParse(toolInfoApproverSourceSearch.EndDate, out DateTime end))
            {
                endDate = end;
            }

            // if one of begin and end date is null then there no require to check greater or less than datetime validation
            if (beginDate != null && endDate != null)
            {
                if (beginDate.Value >= endDate.Value)
                {
                    return false;
                }
            }
            return true;
        }
        
        private bool IsBeginDateProvided(ToolInfoApproverSourceSearch toolInfoApproverSourceSearch)
        {

            if (!string.IsNullOrEmpty(toolInfoApproverSourceSearch.EndDate) &&
                 string.IsNullOrEmpty(toolInfoApproverSourceSearch.BeginDate))
            {
                return false;
            }
            return true;
        }
    }
}
