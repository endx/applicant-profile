using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicantProfile.Model;
using ApplicantProfile.Data.Abstract;

namespace ApplicantProfile.Data.Repositories
{
    public class GenderRepository:EntityBaseRepository<Gender>,IGenderRepository
    {
        public GenderRepository(ApplicantProfileContext context)
        : base(context)
    { }
    }
}
