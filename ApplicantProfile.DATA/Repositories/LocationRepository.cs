using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicantProfile.Model;
using ApplicantProfile.Data.Abstract;
using ApplicantProfile.Data.Helper;

namespace ApplicantProfile.Data.Repositories
{
    public class LocationRepository:EntityBaseRepository<Location>,ILocationRepository
    {
        private ApplicantProfileContext _context;
        public LocationRepository(ApplicantProfileContext context)
        : base(context)
        {
            _context = context;
        }

        public virtual bool isLocationExist(string name)
        {
            return _context.Set<Location>().Count(x => x.Name == name) > 0;
        }

        public virtual PagedList<Location> GetLocations(LocationResourceParameter locationResourceParameter)
        {

            var collectionBeforePaging = _context.Locations
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

            return PagedList<Location>.Create(collectionBeforePaging,
                locationResourceParameter.PageNumber, locationResourceParameter.PageSize);
            
        }
    }
}
