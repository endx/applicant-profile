using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicantProfile.Model;
using ApplicantProfile.Data.Abstract;

namespace ApplicantProfile.Data.Repositories
{
    public class EducationRepository:EntityBaseRepository<Education>,IEducationtRepository
    {
        public EducationRepository(ApplicantProfileContext context)
        : base(context)
    { }
    }
}
