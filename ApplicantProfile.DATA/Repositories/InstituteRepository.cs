using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicantProfile.Model;
using ApplicantProfile.Data.Abstract;
using ApplicantProfile.Data.Helper;

namespace ApplicantProfile.Data.Repositories
{
    public class InstituteRepository:EntityBaseRepository<Institute>,IInstituteRepository
    {
        private ApplicantProfileContext _context;
        public InstituteRepository(ApplicantProfileContext context)
        : base(context)
       {
            _context = context;
       }

        public virtual bool isInstituteExist(string name)
        {
            return _context.Set<Institute>().Count(x => x.Name == name) > 0;
        }

        public virtual PagedList<Institute> GetInstitutes(LocationResourceParameter locationResourceParameter)
        {

            var collectionBeforePaging = _context.Institutes
                .OrderBy(a => a.Name).AsQueryable();

            if (!string.IsNullOrEmpty(locationResourceParameter.Name))
            {
                //trim & ignore casing
                var nameForWhereClause = locationResourceParameter.Name.Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Name.ToLowerInvariant() == nameForWhereClause);
            }

            if (!string.IsNullOrEmpty(locationResourceParameter.SearchQuery))
            {
                //trim & ignore casing
                var searchQueryorWhereClause = locationResourceParameter.SearchQuery.Trim().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Name.ToLowerInvariant().Contains(searchQueryorWhereClause));
            }

            return PagedList<Institute>.Create(collectionBeforePaging,
                locationResourceParameter.PageNumber, locationResourceParameter.PageSize);

        }
    }
}
