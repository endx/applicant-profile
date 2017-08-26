using ApplicantProfile.API.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class LocationUpdateValidation: AbstractValidator<LocationUpdateDto>
    {
        public LocationUpdateValidation()
        {
            RuleFor(location => location.Name).NotEmpty().WithMessage("Location Name cannot be empty");
            RuleFor(location => location.ModifiedDate).NotEmpty().WithMessage("Modified Date cannot be empty");
        }
    }
}
