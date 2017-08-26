using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicantProfile.Model;
using ApplicantProfile.Data.Abstract;
using ApplicantProfile.Data.Helper;

namespace ApplicantProfile.Data.Repositories
{
    public class StudyFieldRepository:EntityBaseRepository<StudyField>,IStudyFieldRepository
    {
        private ApplicantProfileContext _context;
        public StudyFieldRepository(ApplicantProfileContext context)
        : base(context)
        {
            this._context = context;
        }

        public virtual bool isStudyFieldExist(string name)
        {
            return _context.Set<StudyField>().Count(x => x.Field == name) > 0;
        }

        public virtual PagedList<StudyField> GetStudyFields(LocationResourceParameter locationResourceParameter)
        {

            var collectionBeforePaging = _context.StudyFields
                .OrderBy(a => a.Field).AsQueryable();

            if (!string.IsNullOrEmpty(locationResourceParameter.Name))
            {
                //trim & ignore casing
                var nameForWhereClause = locationResourceParameter.Name.Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Field.ToLowerInvariant() == nameForWhereClause);
            }

            if (!string.IsNullOrEmpty(locationResourceParameter.SearchQuery))
            {
                //trim & ignore casing
                var searchQueryorWhereClause = locationResourceParameter.SearchQuery.Trim().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Field.ToLowerInvariant().Contains(searchQueryorWhereClause));
            }

            return PagedList<StudyField>.Create(collectionBeforePaging,
                locationResourceParameter.PageNumber, locationResourceParameter.PageSize);

        }
    }
}
