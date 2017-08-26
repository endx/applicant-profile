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
    public class ExperienceViewModel: IValidatableObject
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public System.DateTime FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public bool CurrentPos { get; set; }
        public bool BankingExp { get; set; }
        public string Duties { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Company { get; set; }
        public int SelectedApplicant { get; set; }
        public SelectList ApplicantItems { get; set; }

        public virtual Applicant Applicant { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new ExperienceViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
