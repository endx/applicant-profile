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
    public class ApplicantViewModel: IValidatableObject
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int SelectedVacancy { get; set; }
        public SelectList VacancyItems { get; set; }
        public int SelectedGender { get; set; }
        public SelectList GenderItems { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual Vacancy Vacancy { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new ApplicantViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }


}
