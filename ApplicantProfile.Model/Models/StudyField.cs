using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.Model
{
    public class StudyField:IEntityBase
    {
        public StudyField()
        {
            this.Educations = new HashSet<Education>();
            this.JobTitles = new HashSet<JobTitle>();
        }

        public int Id { get; set; }
        public string Field { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<JobTitle> JobTitles { get; set; }
    }
}
