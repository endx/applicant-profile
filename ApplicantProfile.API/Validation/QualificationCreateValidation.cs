using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class QualificationCreateValidation: AbstractValidator<QualificationInsertDto>
    {
        public QualificationCreateValidation()
        {
            RuleFor(qualification => qualification.Name).NotEmpty().WithMessage("Qualification Name cannot be empty");
            RuleFor(qualification => qualification.AddedDate).NotEmpty().WithMessage("Added Date cannot be empty");
            RuleFor(qualification => qualification.Name).Length(1,10).WithMessage("Length of Qualficaiton Name shall be between 1 & 10");
        }
    }
}
