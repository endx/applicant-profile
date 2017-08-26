using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.Model
{
    public class JobTitle : IEntityBase
    {
        public JobTitle()
        {
            this.Vacancies = new HashSet<Vacancy>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int QualificationId { get; set; }
        public int ExpYears { get; set; }
        public int StudyFieldId { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Qualification Qualification { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
        public virtual StudyField StudyField { get; set; }
    }
}
