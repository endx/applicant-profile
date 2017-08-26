using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class JobTitleCreateValidator: AbstractValidator<JobTitleInsertDto>
    {
        public JobTitleCreateValidator()
        {
            RuleFor(jobtitle => jobtitle.Title).NotEmpty().WithMessage("Job Title cannot be empty");
            RuleFor(jobtitle => jobtitle.AddedDate).NotEmpty().WithMessage("Added Date cannot be empty");
            RuleFor(jobtitle => jobtitle.ExpYears).GreaterThan(0).WithMessage("year of Experience cannot be empty or less than zero");
            RuleFor(jobtitle => jobtitle.SelectedField).NotEmpty().WithMessage("Study Field can not be empty");
            RuleFor(jobtitle => jobtitle.SelectedQLevel).NotEmpty().WithMessage("Qualification cannot be empty");
        }
    }
}
