using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.Model
{
    public class Education : IEntityBase
    {
        public int Id { get; set; }
        public int QualificationId { get; set; }
        public int StudyFieldId { get; set; }
        public System.DateTime DateGraduated { get; set; }
        public int InstituteId { get; set; }
        public int ApplicantId { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Institute Institute { get; set; }
        public virtual StudyField StudyField { get; set; }
        public virtual Qualification Qualification { get; set; }
        public virtual Applicant Applicant { get; set; }
    }
}
