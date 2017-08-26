using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.Model
{
    public class Vacancy:IEntityBase
    {
        public Vacancy()
        {
            this.Applicants = new HashSet<Applicant>();
            
        }

        public int Id { get; set; }
        public string VacancyNumber { get; set; }
        public DateTime VDate { get; set; }
        public int JobTitleId { get; set; }
        public int LocationId { get; set; }
        public int Qty { get; set; }
        public bool Active { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }


        public virtual ICollection<Applicant> Applicants { get; set; }
        public virtual JobTitle JobTitle { get; set; }
        public virtual Location Location { get; set; }
    }
}
