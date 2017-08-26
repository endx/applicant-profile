using ApplicantProfile.API.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.ViewModels
{
    public class JobTitleInsertDto: IValidatableObject
    {
        public string Title { get; set; }
        public int ExpYears { get; set; }
        public DateTime AddedDate { get; set; }
        public int SelectedQLevel { get; set; }
        public int SelectedField { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new JobTitleCreateValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
