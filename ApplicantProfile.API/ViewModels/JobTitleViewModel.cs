using ApplicantProfile.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApplicantProfile.API.Validation;

namespace ApplicantProfile.API.ViewModels
{
    public class JobTitleViewModel: IValidatableObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ExpYears { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int SelectedQLevel { get; set; }
        public SelectList QLevelItems { get; set; }
        public int SelectedField { get; set; }
        public SelectList FieldItems { get; set; }

        public virtual Qualification Qualification { get; set; }
        public virtual StudyField StudyField { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new JobTitleViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
