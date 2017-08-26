using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.Model
{
    public class Qualification : IEntityBase
    {
        public Qualification()
        {
            this.JobTitles = new HashSet<JobTitle>();
            this.Educations = new HashSet<Education>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<JobTitle> JobTitles { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
    }
}
