using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class LocationViewModelValidator : AbstractValidator<LocationViewModel>
    {
        public LocationViewModelValidator()
        {
            RuleFor(location => location.Name).NotEmpty().WithMessage("Location Name cannot be empty");

        }
    }
}
