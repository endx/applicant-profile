using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.Model
{
    public class Experience : IEntityBase
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public string Position { get; set; }
        public System.DateTime FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public bool CurrentPos { get; set; }
        public bool BankingExp { get; set; }
        public string Duties { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Company { get; set; }
        public virtual Applicant Applicant { get; set; }
    }
}
