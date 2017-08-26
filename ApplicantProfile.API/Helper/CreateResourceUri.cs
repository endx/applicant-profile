using ApplicantProfile.Data.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Helper
{
    public class CreateResourceUri
    {
        private IUrlHelper _urlHelper;
        public CreateResourceUri(IUrlHelper _urlHelper)
        {
            this._urlHelper = _urlHelper;
        }

        public string CreateUri(LocationResourceParameter locationResourceParameter, ResourceUriType type, string linkValue)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link(linkValue,
                        new
                        {
                            searchQuery = locationResourceParameter.SearchQuery,
                            name = locationResourceParameter.Name,
                            pageNumber = locationResourceParameter.PageNumber - 1,
                            pageSize = locationResourceParameter.PageSize

                        });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link(linkValue,
                        new
                        {
                            searchQuery = locationResourceParameter.SearchQuery,
                            name = locationResourceParameter.Name,
                            pageNumber = locationResourceParameter.PageNumber + 1,
                            pageSize = locationResourceParameter.PageSize

                        });
                default:
                    return _urlHelper.Link(linkValue,
                        new
                        {
                            searchQuery = locationResourceParameter.SearchQuery,
                            name = locationResourceParameter.Name,
                            pageNumber = locationResourceParameter.PageNumber,
                            pageSize = locationResourceParameter.PageSize

                        });
            }
        }
    }
}