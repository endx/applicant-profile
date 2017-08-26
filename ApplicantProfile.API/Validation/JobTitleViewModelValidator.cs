using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class JobTitleViewModelValidator : AbstractValidator<JobTitleViewModel>
    {
        public JobTitleViewModelValidator()
        {
            RuleFor(jobtitle => jobtitle.Title).NotEmpty().WithMessage("Job Title cannot be empty");
            RuleFor(jobtitle => jobtitle.ExpYears).NotEmpty().WithMessage("Year of Experience cannot be empty");
            RuleFor(jobtitle => jobtitle.SelectedField).NotEmpty().WithMessage("Field of Sutdy cannot be empty");
            RuleFor(jobtitle => jobtitle.SelectedQLevel).NotEmpty().WithMessage("Qualification cannot be empty");
        }
    }
}
