using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class InstituteViewModelValidator : AbstractValidator<InstituteViewModel>
    {
        public InstituteViewModelValidator()
        {
            RuleFor(institute => institute.Name).NotEmpty().WithMessage("Name cannot be empty");
        }
    }
}
