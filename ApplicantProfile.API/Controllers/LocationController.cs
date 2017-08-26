using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ApplicantProfile.Data.Abstract;
using ApplicantProfile.Model;
using AutoMapper;
using ApplicantProfile.API.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ApplicantProfile.API.Helper;
using ApplicantProfile.Data.Helper;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ApplicantProfile.API.Controllers
{
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private ILocationRepository _locationRepository;
        private ILogger<LocationController> _logger;
        private IUrlHelper _urlHelper;
        public LocationController(ILocationRepository _locationRepository, ILogger<LocationController> logger, IUrlHelper urlHelper)
        {
            this._locationRepository = _locationRepository;
            this._logger = logger;
            this._urlHelper = urlHelper;
        }

        [HttpGet(Name ="GetLocations")]
        public IActionResult GetLocation(LocationResourceParameter locationResourceParameter)
        {

            CreateResourceUri cru = new CreateResourceUri(this._urlHelper);

            var locationsfromRepo = _locationRepository.GetLocations(locationResourceParameter);

            var previousPageLink = locationsfromRepo.HasPrevious ?
                cru.CreateUri(locationResourceParameter, ResourceUriType.PreviousPage, "GetLocations") : null;

            var nextPageLink = locationsfromRepo.HasNext ?
                cru.CreateUri(locationResourceParameter, ResourceUriType.NextPage, "GetLocations") : null;

            var paginationMetadata = new
            {
                totalcount = locationsfromRepo.TotalCount,
                pageSize = locationsfromRepo.PageSize,
                currentPage = locationsfromRepo.CurrentPage,
                totalPages = locationsfromRepo.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            var locations = Mapper.Map<IEnumerable<LocationViewModel>>(locationsfromRepo);
            
            return Ok(locations);

        }

        [HttpGet("{id}", Name = "GetLocation")]
        public IActionResult GetLocation(int id)
        {
            var _location = _locationRepository.GetSingle(s => s.Id == id);

            if (_location == null)
            {
                return NotFound();
            }
            var location = Mapper.Map<LocationViewModel>(_location);

            return Ok(location);
            
        }

        [HttpPost]
        public IActionResult CreateLocation([FromBody]LocationInsertDto location)
        {
            if (location == null)
            {
                return BadRequest();
            }

            if (_locationRepository.isLocationExist(location.Name))
            {
                ModelState.AddModelError(nameof(LocationInsertDto), "Locaiton Name Already Exist");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationEntity = Mapper.Map<Location>(location);

            _locationRepository.Add(locationEntity);

            if (!_locationRepository.Commit())
            {
                throw new Exception("Creating a Locaiton Failed");
            }

            var locationtoReturn = Mapper.Map<LocationViewModel>(locationEntity);

            return CreatedAtRoute("GetLocation", new { id = locationtoReturn.Id }, locationtoReturn);

        }

        [HttpPost("{id}")]
        public IActionResult CreateLocation(int id)
        {
            if (_locationRepository.isExists(id))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return NotFound();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateLocation(int id, [FromBody] JsonPatchDocument<LocationUpdateDto> location)
        {
            if (location==null)
            {
                return BadRequest();
            }

            var locationFromRepo = _locationRepository.GetSingle(id);

            if(locationFromRepo == null)
            {
                return NotFound();
            }
            

            var locationToPatch = Mapper.Map<LocationUpdateDto>(locationFromRepo);

           location.ApplyTo(locationToPatch, ModelState);

            TryValidateModel(locationToPatch);

            if (!ModelState.IsValid)
            {
                return new InputValidation(ModelState);
            }

            Mapper.Map(locationToPatch, locationFromRepo);

            _locationRepository.Update(locationFromRepo);

            if (!_locationRepository.Commit())
            {
                throw new Exception("Updating a Locaiton Failed");
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteLocation(int id)
        {
            var _locationDb = _locationRepository.GetSingle(id);

            if (_locationDb == null)
            {
                return NotFound();
            }
           
             _locationRepository.Delete(_locationDb);

            if (!_locationRepository.Commit())
            {
                throw new Exception($"Deleting Locaiton {id} failed on commit");
            }

            _logger.LogInformation(100,$"Locaiton {id} was deleted.");

            return NoContent();
        }
    }
}
