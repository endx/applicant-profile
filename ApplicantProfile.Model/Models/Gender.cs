using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.Model
{
    public class Gender : IEntityBase
    {
        public Gender()
        {
            this.Applicants = new HashSet<Applicant>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<Applicant> Applicants { get; set; }
    }
}
