using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicantProfile.Model;
using ApplicantProfile.Data.Abstract;
using ApplicantProfile.Data.Helper;

namespace ApplicantProfile.Data.Repositories
{
    public class VacancyRepository:EntityBaseRepository<Vacancy>, IVacancyRepsitory
    {
        private ApplicantProfileContext _context;
        public VacancyRepository(ApplicantProfileContext context)
        : base(context)
         {
            _context = context;
        }

        public virtual bool isVacancyExist(string name)
        {
            return _context.Set<Vacancy>().Count(x => x.VacancyNumber == name) > 0;
        }

        public virtual PagedList<Vacancy> GetVacancies(LocationResourceParameter locationResourceParameter)
        {

            var collectionBeforePaging = _context.Vacancies
                .OrderBy(a => a.VacancyNumber).AsQueryable();

            if (!string.IsNullOrEmpty(locationResourceParameter.Name))
            {
                //trim & ignore casing
                var nameForWhereClause = locationResourceParameter.Name.Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.VacancyNumber.ToLowerInvariant() == nameForWhereClause);
            }

            if (!string.IsNullOrEmpty(locationResourceParameter.SearchQuery))
            {
                //trim & ignore casing
                var searchQueryorWhereClause = locationResourceParameter.SearchQuery.Trim().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.VacancyNumber.ToLowerInvariant().Contains(searchQueryorWhereClause));
            }

            return PagedList<Vacancy>.Create(collectionBeforePaging,
                locationResourceParameter.PageNumber, locationResourceParameter.PageSize);

        }

        public Vacancy GetSingle(string id)
        {
            return _context.Set<Vacancy>().FirstOrDefault(x => x.VacancyNumber == id);
        }
    }
}
