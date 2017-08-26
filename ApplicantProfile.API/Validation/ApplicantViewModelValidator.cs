using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class ApplicantViewModelValidator : AbstractValidator<ApplicantViewModel>
    {
        public ApplicantViewModelValidator()
        {
            RuleFor(applicant => applicant.FirstName).NotEmpty().WithMessage("First Name cannot be empty");
            RuleFor(applicant => applicant.SecondName).NotEmpty().WithMessage("Second Name cannot be empty");
            RuleFor(applicant => applicant.LastName).NotEmpty().WithMessage("Last Name cannot be empty");
            RuleFor(applicant => applicant.BirthDate).NotEmpty().WithMessage("Birth Date cannot be empty");
            RuleFor(applicant => applicant.SelectedGender).NotEmpty().WithMessage("Gender cannot be empty");
            RuleFor(applicant => applicant.SelectedVacancy).NotEmpty().WithMessage("Vacancy cannot be empty");
        }
    }
}
