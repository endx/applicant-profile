using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.Model
{
    public class Institute : IEntityBase
    {
        public Institute()
        {
            this.Educations = new HashSet<Education>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<Education> Educations { get; set; }
    }
}
