using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class QualificationUpdateValidator: AbstractValidator<QualificationUpdateDto>
    {
        public QualificationUpdateValidator()
        {
            RuleFor(qualification => qualification.Name).NotEmpty().WithMessage("Qualification Name cannot be empty");
            RuleFor(qualification => qualification.ModifiedDate).NotEmpty().WithMessage("Modified Date cannot be empty");
            RuleFor(qualification => qualification.Name).Length(1, 10).WithMessage("Length of Qualficaiton Name shall be between 1 & 10");
        }
    }
}
