using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class EducationViewModelValidator: AbstractValidator<EducationViewModel>
    {
        public EducationViewModelValidator()
        {
            RuleFor(education => education.DateGraduated).NotEmpty().WithMessage("Date Graduate cannot be empty");
            RuleFor(education => education.SelectedInstitute).NotEmpty().WithMessage("Institute cannot be empty");
            RuleFor(education => education.SelectedQualification).NotEmpty().WithMessage("Qualification cannot be empty");
            RuleFor(education => education.SelectedStudyField).NotEmpty().WithMessage("Field of Study cannot be empty");
            RuleFor(education => education.SelectedApplicant).NotEmpty().WithMessage("Applicant cannot be empty");
        }
    }
}
