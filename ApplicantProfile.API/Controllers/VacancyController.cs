using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApplicantProfile.Data.Abstract;
using Microsoft.Extensions.Logging;
using ApplicantProfile.Data.Helper;
using ApplicantProfile.API.Helper;
using ApplicantProfile.API.ViewModels;
using AutoMapper;
using ApplicantProfile.Model;
using Microsoft.AspNetCore.JsonPatch;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ApplicantProfile.API.Controllers
{
    [Route("api/[controller]")]
    public class VacancyController : Controller
    {
        private ILocationRepository _locationRepository;
        private ILogger<LocationController> _logger;
        private IUrlHelper _urlHelper;
        private IJobTitleRepository _jobtitleRepository;
        private IVacancyRepsitory _vacancyRepository;

        public VacancyController(ILocationRepository _locationRepository, ILogger<LocationController> logger, 
            IUrlHelper urlHelper, IJobTitleRepository _jobtitleRepository, IVacancyRepsitory _vacancyRepository)
        {
            this._locationRepository = _locationRepository;
            this._logger = logger;
            this._urlHelper = urlHelper;
            this._jobtitleRepository = _jobtitleRepository;
            this._vacancyRepository = _vacancyRepository;
        }


        [HttpGet(Name = "GetVacancies")]
        public IActionResult GetVacancy(LocationResourceParameter locationResourceParameter)
        {
            CreateResourceUri cru = new CreateResourceUri(this._urlHelper);

            var vacancyfromRepo = _vacancyRepository.GetVacancies(locationResourceParameter);

            var previousPageLink = vacancyfromRepo.HasPrevious ?
                cru.CreateUri(locationResourceParameter, ResourceUriType.PreviousPage, "GetVacancies") : null;

            var nextPageLink = vacancyfromRepo.HasNext ?
                cru.CreateUri(locationResourceParameter, ResourceUriType.NextPage, "GetVacancies") : null;

            var paginationMetadata = new
            {
                totalcount = vacancyfromRepo.TotalCount,
                pageSize = vacancyfromRepo.PageSize,
                currentPage = vacancyfromRepo.CurrentPage,
                totalPages = vacancyfromRepo.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            var vacancies = Mapper.Map<IEnumerable<VacancyViewModel>>(vacancyfromRepo);

            return Ok(vacancies);
        }


        //[HttpGet("{id}", Name = "GetVacancy")]
        //public IActionResult GetVacancy(int id)
        //{
        //    var _vacancy = _vacancyRepository.GetSingle(s => s.Id == id);

        //    if (_vacancy == null)
        //    {
        //        return NotFound();
        //    }
        //    var vacancy = Mapper.Map<VacancyViewModel>(_vacancy);

        //    return Ok(vacancy);

        //}

        [HttpGet("{id}", Name = "GetVacancy")]
        public IActionResult GetVacancy(string id)
        {
            var _vacancy = _vacancyRepository.GetSingle(s => s.VacancyNumber == id);

            if (_vacancy == null)
            {
                return NotFound();
            }
            var vacancy = Mapper.Map<VacancyViewModel>(_vacancy);

            return Ok(vacancy);

        }

        [HttpPost]
        public IActionResult CreateVacancy([FromBody]VacancyCreateDto vacancy)
        {
            if (vacancy == null)
            {
                return BadRequest();
            }

            if (_locationRepository.isLocationExist(vacancy.VacancyNumber))
            {
                ModelState.AddModelError(nameof(LocationInsertDto), "Vacany Number Already Exist");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vacancyEntity = Mapper.Map<Vacancy>(vacancy);

            _vacancyRepository.Add(vacancyEntity);

            if (!_vacancyRepository.Commit())
            {
                throw new Exception("Creating a Vacancy Failed");
            }

            var vacancytoReturn = Mapper.Map<VacancyViewModel>(vacancyEntity);

            return CreatedAtRoute("GetVacancy", new { id = vacancytoReturn.VacancyNumber }, vacancytoReturn);

        }

        [HttpPatch("{id}")]
        public IActionResult UpdateLocation(int id, [FromBody] JsonPatchDocument<VacancyUpdateDto> vacancy)
        {
            if (vacancy == null)
            {
                return BadRequest();
            }

            var vacancyFromRepo = _vacancyRepository.GetSingle(id);

            if (vacancyFromRepo == null)
            {
                return NotFound();
            }


            var vacancyToPatch = Mapper.Map<VacancyUpdateDto>(vacancyFromRepo);

            vacancy.ApplyTo(vacancyToPatch, ModelState);

            TryValidateModel(vacancyToPatch);

            if (!ModelState.IsValid)
            {
                return new InputValidation(ModelState);
            }

            Mapper.Map(vacancyToPatch, vacancyFromRepo);

            _vacancyRepository.Update(vacancyFromRepo);

            if (!_vacancyRepository.Commit())
            {
                throw new Exception("Updating a vacancy Failed");
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteVacancy(string id)
        {
            var _vacancyDb = _vacancyRepository.GetSingle(id);

            if (_vacancyDb == null)
            {
                return NotFound();
            }

            _vacancyRepository.Delete(_vacancyDb);

            if (!_vacancyRepository.Commit())
            {
                throw new Exception($"Deleting Vacancy {id} failed on commit");
            }

            _logger.LogInformation(100, $"Vacancy {id} was deleted.");

            return NoContent();
        }
    }
}
