using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.Model
{
    public class Applicant : IEntityBase
    {
        public Applicant()
        {
            this.Experiences = new HashSet<Experience>();
            this.Educations = new HashSet<Education>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int VacancyId { get; set; }
        public int GenderId { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual Vacancy Vacancy { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
    }
}
