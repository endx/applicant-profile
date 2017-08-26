using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class VacancyCreateValidator: AbstractValidator<VacancyCreateDto>
    {
        public VacancyCreateValidator()
        {
            RuleFor(vacancy => vacancy.VacancyNumber).NotEmpty().WithMessage("Vacancy Number cannot be empty");
            RuleFor(vacancy => vacancy.VDate).NotEmpty().WithMessage("Vacancy Date cannot be empty");
            RuleFor(vacancy => vacancy.AddedDate).NotEmpty().WithMessage("Added Date cannot be empty");
            RuleFor(vacancy => vacancy.Qty).NotEmpty().WithMessage("Quantity cannot be empty");
            RuleFor(vacancy => vacancy.SelectedJobTitle).NotEmpty().WithMessage("Job Title cannot be empty");
            RuleFor(vacancy => vacancy.SelectedLocation).NotEmpty().WithMessage("Location cannot be empty");
        }
    }
}
