using ApplicantProfile.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Validation
{
    public class StudyFieldUpdateValidator: AbstractValidator<StudyFieldUpdateDto>
    {
        public StudyFieldUpdateValidator()
        {
            RuleFor(studyfield => studyfield.Field).NotEmpty().WithMessage("Study Field Name cannot be empty");
            RuleFor(studyfield => studyfield.Field).Length(1, 20).WithMessage("CHeck Min/Max Length of Study Field Name");
            RuleFor(studyfield => studyfield.ModifiedDate).NotEmpty().WithMessage("Modified Date cannot be empty");
        }
    }
}
