using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicantProfile.Data.Abstract;
using ApplicantProfile.Model;

namespace ApplicantProfile.Data.Repositories
{
    public class ExperienceRepository:EntityBaseRepository<Experience>, IExperienceRepository
    {
        public ExperienceRepository(ApplicantProfileContext context)
        : base(context)
    { }
    }
}
