using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class ExperienceViewModelValidator: AbstractValidator<ExperienceViewModel>
    {
        public ExperienceViewModelValidator()
        {
            RuleFor(experience => experience.Position).NotEmpty().WithMessage("Position cannot be empty");
            RuleFor(experience => experience.CurrentPos).NotEmpty().WithMessage("Current Position cannot be empty");
            RuleFor(experience => experience.BankingExp).NotEmpty().WithMessage("Banking Experience cannot be empty");
            RuleFor(experience => experience.CurrentPos).NotEmpty().WithMessage("Company cannot be empty");
            RuleFor(experience => experience.SelectedApplicant).NotEmpty().WithMessage("Applicant cannot be empty");

            RuleFor(experience => experience.ToDate).Must((start, end) =>
            {
                return DateTimeIsGreater(start.FromDate, end);
            }).WithMessage("ToDate  must be greater than FromDate");
        }

        private bool DateTimeIsGreater(DateTime start, DateTime? end)
        {
            return end > start;
        }
    }
}
