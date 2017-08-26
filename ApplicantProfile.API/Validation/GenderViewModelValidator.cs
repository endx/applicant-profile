using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class GenderViewModelValidator: AbstractValidator<GenderViewModel>
    {
        public GenderViewModelValidator()
        {
            RuleFor(gender => gender.Name).NotEmpty().WithMessage("Gender cannot be empty");
        }
    }
}
