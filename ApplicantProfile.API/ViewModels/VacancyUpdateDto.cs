using ApplicantProfile.API.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.ViewModels
{
    public class VacancyUpdateDto : IValidatableObject
    {
        public DateTime VDate { get; set; }
        public int Qty { get; set; }
        public bool Active { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int SelectedJobTitle { get; set; }
        public int SelectedLocation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new VacancyUpdateValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
