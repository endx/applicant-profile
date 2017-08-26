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
    public class JobTitleController : Controller
    {
        private IJobTitleRepository _jobtitleRepository;
        private IQualificationRepository _qualificationRepository;
        private IStudyFieldRepository _studyfieldRepository;
        private ILogger<JobTitleController> _logger;
        private IUrlHelper _urlHelper;

        public JobTitleController(IJobTitleRepository _jobtitleRepository, IQualificationRepository _qualificationRepository, 
            IStudyFieldRepository _studyfieldRepository, ILogger<JobTitleController> _logger, IUrlHelper _urlHelper)
        {
            this._jobtitleRepository = _jobtitleRepository;
            this._qualificationRepository = _qualificationRepository;
            this._studyfieldRepository = _studyfieldRepository;
            this._logger = _logger;
            this._urlHelper = _urlHelper;
        }
        
        [HttpGet (Name ="GetJobTitles")]
        public IActionResult GetJobTitle(LocationResourceParameter locationResourceParameter)
        {
            CreateResourceUri cru = new CreateResourceUri(this._urlHelper);

            var jobtitlefromRepo = _jobtitleRepository.GetJobTitles(locationResourceParameter);
            
            var previousPageLink = jobtitlefromRepo.HasPrevious ?
                cru.CreateUri(locationResourceParameter, ResourceUriType.PreviousPage, "GetJobTitles") : null;

            var nextPageLink = jobtitlefromRepo.HasNext ?
                cru.CreateUri(locationResourceParameter, ResourceUriType.NextPage, "GetJobTitles") : null;

            var paginationMetadata = new
            {
                totalcount = jobtitlefromRepo.TotalCount,
                pageSize = jobtitlefromRepo.PageSize,
                currentPage = jobtitlefromRepo.CurrentPage,
                totalPages = jobtitlefromRepo.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            var jobtitles = Mapper.Map<IEnumerable<JobTitleViewModel>>(jobtitlefromRepo);
            
            return Ok(jobtitles);
        }

        [HttpGet("{id}", Name = "GetJobTitle")]
        public IActionResult GetJobTitle(int id)
        {
            var _jobtitle= _jobtitleRepository.GetSingle(s => s.Id == id);

            if (_jobtitle == null)
            {
                return NotFound();
            }
            var jobtitle = Mapper.Map<JobTitleViewModel>(_jobtitle);

            return Ok(jobtitle);
        }

        [HttpPost]
        public IActionResult CreateJobTitle([FromBody]JobTitleInsertDto jobtitle)
        {
            if (jobtitle == null)
            {
                return BadRequest();
            }

            if (_jobtitleRepository.isJobTitleExist(jobtitle.Title))
            {
                ModelState.AddModelError(nameof(LocationInsertDto), "Job Title Already Exist");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobtitleEntity = Mapper.Map<JobTitle>(jobtitle);

            _jobtitleRepository.Add(jobtitleEntity);

            if (!_jobtitleRepository.Commit())
            {
                throw new Exception("Creating Job Title Failed");
            }

            var jobtitletoReturn = Mapper.Map<JobTitleViewModel>(jobtitleEntity);

            return CreatedAtRoute("GetJobTitle", new { id = jobtitletoReturn.Id }, jobtitletoReturn);

        }


        [HttpPost("{id}")]
        public IActionResult CreateJobTitle(int id)
        {
            if (_jobtitleRepository.isExists(id))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return NotFound();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateJobTitle(int id, [FromBody] JsonPatchDocument<JobTitleUpdateDto> jobtitle)
        {
            if (jobtitle == null)
            {
                return BadRequest();
            }

            var jobtitleFromRepo = _jobtitleRepository.GetSingle(id);

            if (jobtitleFromRepo == null)
            {
                return NotFound();
            }

            var jobtitleToPatch = Mapper.Map<JobTitleUpdateDto>(jobtitleFromRepo);

            jobtitle.ApplyTo(jobtitleToPatch, ModelState);

            TryValidateModel(jobtitleToPatch);

            if (!ModelState.IsValid)
            {
                return new InputValidation(ModelState);
            }

            Mapper.Map(jobtitleToPatch, jobtitleFromRepo);

            _jobtitleRepository.Update(jobtitleFromRepo);

            if (!_jobtitleRepository.Commit())
            {
                throw new Exception("Creating Job Title Failed");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var _jobtitleDb = _jobtitleRepository.GetSingle(id);

            if (_jobtitleDb == null)
            {
                return NotFound();
            }

            _jobtitleRepository.Delete(_jobtitleDb);

            if (!_jobtitleRepository.Commit())
            {
                throw new Exception($"Deleting Job Title {id} failed on commit");
            }

            _logger.LogInformation(100, $"Job Title {id} was deleted.");

            return NoContent();
        }
    }
}
