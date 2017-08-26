using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class InstituteUpdateValidation : AbstractValidator<InstituteUpdateDto>
    {
        public InstituteUpdateValidation()
        {
            RuleFor(institute => institute.Name).NotEmpty().WithMessage("Institute Name cannot be empty");
            RuleFor(institute => institute.ModifiedDate).NotEmpty().WithMessage("Modified Date cannot be empty");
        }
    }
}
