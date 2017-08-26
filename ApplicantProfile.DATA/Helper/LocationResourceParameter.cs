using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.Data.Helper
{
    public class LocationResourceParameter
    {
        const int maxPageSize = 20;

        public int PageNumber { get; set; } = 1;
        public int _pageSize = 10;

        public int PageSize {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
         }

        public string Name { get; set; }

        public string SearchQuery { get; set; }

        public string OrderBy { get; set; } = "Name";
    }
}
