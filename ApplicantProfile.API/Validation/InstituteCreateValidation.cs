using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class InstituteCreateValidation : AbstractValidator<InstituteCreateDto>
    {
        public InstituteCreateValidation()
        {
            RuleFor(institute => institute.Name).NotEmpty().WithMessage("Institute Name cannot be empty");
            RuleFor(institute => institute.AddedDate).NotEmpty().WithMessage("Added Date cannot be empty");
        }
    }
}
