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
    public class EducationViewModel: IValidatableObject
    {
        public int Id { get; set; }
        public System.DateTime DateGraduated { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public int SelectedInstitute { get; set; }
        public SelectList InstituteItems { get; set; }
        public int SelectedStudyField { get; set; }
        public SelectList StudyFieldItems { get; set; }
        public int SelectedQualification { get; set; }
        public SelectList QualificationItems { get; set; }
        public int SelectedApplicant { get; set; }
        public SelectList ApplicantItems { get; set; }

        public virtual Institute Institute { get; set; }
        public virtual StudyField StudyField { get; set; }
        public virtual Qualification Qualification { get; set; }
        public virtual Applicant Applicant { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new EducationViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
