using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApplicantProfile.Data.Abstract;
using ApplicantProfile.Model;
using ApplicantProfile.API.Core;
using AutoMapper;
using ApplicantProfile.API.ViewModels;
using Microsoft.Extensions.Logging;
using ApplicantProfile.Data.Helper;
using ApplicantProfile.API.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ApplicantProfile.API.Controllers
{
    [Route("api/[controller]")]
    public class InstituteController : Controller
    {
        private IInstituteRepository _instituteRepository;
        private ILogger<InstituteController> _logger;
        private IUrlHelper _urlHelper;
        public InstituteController(IInstituteRepository _instituteRepository, ILogger<InstituteController> _logger, IUrlHelper _urlHelper)
        {
            this._instituteRepository = _instituteRepository;
            this._logger = _logger;
            this._urlHelper = _urlHelper;
        }

        [HttpGet(Name = "GetInstitutes")]
        public IActionResult GetInstitute(LocationResourceParameter locationResourceParameter)
        {
            //var institutefromRepo = _instituteRepository.GetAll();

            //var institutes = Mapper.Map<IEnumerable<InstituteViewModel>>(institutefromRepo);

            //return new JsonResult(institutes);

            CreateResourceUri cru = new CreateResourceUri(this._urlHelper);
            var instituteFromRepo = _instituteRepository.GetInstitutes(locationResourceParameter);

            var previousPageLink = instituteFromRepo.HasPrevious ?
               cru.CreateUri(locationResourceParameter, ResourceUriType.PreviousPage, "GetInstitutes") : null;

            var nextPageLink = instituteFromRepo.HasNext ?
                cru.CreateUri(locationResourceParameter, ResourceUriType.NextPage, "GetInstitutes") : null;

            var paginationMetadata = new
            {
                totalcount = instituteFromRepo.TotalCount,
                pageSize = instituteFromRepo.PageSize,
                currentPage = instituteFromRepo.CurrentPage,
                totalPages = instituteFromRepo.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            var locations = Mapper.Map<IEnumerable<InstituteViewModel>>(instituteFromRepo);

            return Ok(locations);

        }

        [HttpGet("{id}", Name = "GetInstitute")]
        public IActionResult GetInstitute(int id)
        {
            var _institute = _instituteRepository.GetSingle(i => i.Id == id);

            if (_institute != null)
            {
                var institute = Mapper.Map<InstituteViewModel>(_institute);

                return new JsonResult(institute);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        public IActionResult CreateInstitute([FromBody]InstituteCreateDto institute)
        {
            if (institute == null)
            {
                return BadRequest();
            }

            if (_instituteRepository.isInstituteExist(institute.Name))
            {
                ModelState.AddModelError(nameof(InstituteCreateDto), "Institute Name Already Exist");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instituteEntity = Mapper.Map<Institute>(institute);

            _instituteRepository.Add(instituteEntity);
            if (!_instituteRepository.Commit())
            {
                throw new Exception("Creating an Institute Failed");
            }

            var instituteReturn = Mapper.Map<InstituteViewModel>(instituteEntity);

            return CreatedAtRoute("GetInstitute", new { id = instituteReturn.Id }, instituteReturn);

        }

        [HttpPost("{id}")]
        public IActionResult CreateInstitute(int id)
        {
            if (_instituteRepository.isExists(id))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return NotFound();
        }


        [HttpPatch("{id}")]
        public IActionResult UpdateInstitute(int id, [FromBody] JsonPatchDocument<InstituteUpdateDto> institute)
        {
            if (institute == null)
            {
                return BadRequest();
            }

            var instituteFromRepo = _instituteRepository.GetSingle(id);

            if (instituteFromRepo == null)
            {
                return NotFound();
            }

            var instituteToPatch = Mapper.Map<InstituteUpdateDto>(instituteFromRepo);

            institute.ApplyTo(instituteToPatch, ModelState);

            TryValidateModel(instituteToPatch);

            if (!ModelState.IsValid)
            {
                return new InputValidation(ModelState);
            }

            Mapper.Map(instituteToPatch, instituteFromRepo);

            _instituteRepository.Update(instituteFromRepo);

            if (!_instituteRepository.Commit())
            {
                throw new Exception("Creating Institute Failed");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var _instituteDb = _instituteRepository.GetSingle(id);

            if (_instituteDb == null)
            {
                return NotFound();
            }
            
            _instituteRepository.Delete(_instituteDb);
            if (!_instituteRepository.Commit())
            {
                throw new Exception($"Deleting Institute {id} failed on commit");
            }

            _logger.LogInformation(100, $"Institute {id} was deleted.");

            return NoContent();
        }
    }
}
