using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class StudyFieldViewModelValidator : AbstractValidator<StudyFieldViewModel>
    {
        public StudyFieldViewModelValidator()
        {
            RuleFor(sf => sf.Field).NotEmpty().WithMessage("Field of Study cannot be empty");
        }
    }
}
