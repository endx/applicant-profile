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
    public class VacancyViewModel: IValidatableObject
    {
        public int Id { get; set; }
        public string VacancyNumber { get; set; }
        public DateTime VDate { get; set; }
        public int Qty { get; set; }
        public bool Active { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int SelectedJobTitle { get; set; }
        public SelectList JobTitleItems { get; set; }
        public int SelectedLocation { get; set; }
        public SelectList LocationItems { get; set; }

        public virtual JobTitle JobTitle { get; set; }
        public virtual Location Location { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new VacancyViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
