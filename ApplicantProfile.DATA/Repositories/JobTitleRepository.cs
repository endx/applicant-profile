using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicantProfile.Model;
using ApplicantProfile.Data.Abstract;
using ApplicantProfile.Data.Helper;

namespace ApplicantProfile.Data.Repositories
{

    public class JobTitleRepository:EntityBaseRepository<JobTitle>,IJobTitleRepository
    {
        private ApplicantProfileContext _context;
        public JobTitleRepository(ApplicantProfileContext context)
        : base(context)
        {
            this._context = context;
        }

        public virtual bool isJobTitleExist(string name)
        {
            return _context.Set<JobTitle>().Count(x => x.Title == name) > 0;
        }

        public virtual PagedList<JobTitle> GetJobTitles(LocationResourceParameter locationResourceParameter)
        {

            var collectionBeforePaging = _context.JobTitles
                .OrderBy(a => a.Title).AsQueryable();

            if (!string.IsNullOrEmpty(locationResourceParameter.Name))
            {
                //trim & ignore casing
                var nameForWhereClause = locationResourceParameter.Name.Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Title.ToLowerInvariant() == nameForWhereClause);
            }

            if (!string.IsNullOrEmpty(locationResourceParameter.SearchQuery))
            {
                //trim & ignore casing
                var searchQueryorWhereClause = locationResourceParameter.SearchQuery.Trim().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Title.ToLowerInvariant().Contains(searchQueryorWhereClause));
            }

            return PagedList<JobTitle>.Create(collectionBeforePaging,
                locationResourceParameter.PageNumber, locationResourceParameter.PageSize);

        }
    }
}
