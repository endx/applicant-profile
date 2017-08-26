using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class QualificationViewModelValidator : AbstractValidator<QualificationViewModel>
    {
        public QualificationViewModelValidator()
        {
            RuleFor(qual => qual.Name).NotEmpty().WithMessage("Qualification cannot be empty");
        }
    }
}
