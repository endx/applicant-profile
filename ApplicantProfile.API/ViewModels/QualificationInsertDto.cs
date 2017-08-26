using ApplicantProfile.API.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApplicantProfile.API.ViewModels
{
    public class QualificationInsertDto: IValidatableObject
    {
        public string Name { get; set; }
        public DateTime AddedDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new QualificationCreateValidation();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
