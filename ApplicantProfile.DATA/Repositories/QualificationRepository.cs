using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicantProfile.Model;
using ApplicantProfile.Data.Abstract;
using ApplicantProfile.Data.Helper;

namespace ApplicantProfile.Data.Repositories
{
    public class QualificationRepository:EntityBaseRepository<Qualification>,IQualificationRepository
    {
        private ApplicantProfileContext _context;
        public QualificationRepository(ApplicantProfileContext context)
        : base(context)
        {
            _context = context;
        }

        public virtual bool isQualificationExist(string name)
        {
            return _context.Set<Qualification>().Count(x => x.Name == name) > 0;
        }

        public virtual PagedList<Qualification> GetQualifications(LocationResourceParameter locationResourceParameter)
        {

            var collectionBeforePaging = _context.Qualifications
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

            return PagedList<Qualification>.Create(collectionBeforePaging,
                locationResourceParameter.PageNumber, locationResourceParameter.PageSize);

        }
    }
}
