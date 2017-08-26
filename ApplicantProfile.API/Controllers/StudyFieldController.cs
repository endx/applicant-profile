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
    public class StudyFieldController : Controller
    {
        private IStudyFieldRepository _studyfieldRepository;
        private ILogger<StudyFieldController> _logger;
        private IUrlHelper _urlHelper;
        public StudyFieldController(IStudyFieldRepository _studyfieldRepository, ILogger<StudyFieldController> _logger, IUrlHelper _urlHelper)
        {
            this._studyfieldRepository = _studyfieldRepository;
            this._logger = _logger;
            this._urlHelper = _urlHelper;
        }


        [HttpGet(Name ="GetStudyFields")]
        public IActionResult GetStudyField(LocationResourceParameter locationResourceParameter)
        {

            CreateResourceUri cru = new CreateResourceUri(this._urlHelper);

            var studyfieldfromRepo = _studyfieldRepository.GetStudyFields(locationResourceParameter);

            var previousPageLink = studyfieldfromRepo.HasPrevious ?
                cru.CreateUri(locationResourceParameter, ResourceUriType.PreviousPage, "GetStudyFields") : null;

            var nextPageLink = studyfieldfromRepo.HasNext ?
                cru.CreateUri(locationResourceParameter, ResourceUriType.NextPage, "GetStudyFields") : null;

            var paginationMetadata = new
            {
                totalcount = studyfieldfromRepo.TotalCount,
                pageSize = studyfieldfromRepo.PageSize,
                currentPage = studyfieldfromRepo.CurrentPage,
                totalPages = studyfieldfromRepo.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            var studyfields = Mapper.Map<IEnumerable<StudyFieldViewModel>>(studyfieldfromRepo);

            return Ok(studyfields);

        }

        [HttpGet("{id}", Name = "GetStudyField")]
        public IActionResult GetStudyField(int id)
        {
            var _studyfield = _studyfieldRepository.GetSingle(s => s.Id == id);

            if (_studyfield == null)
            {
                return NotFound();
            }
            var studyfield = Mapper.Map<StudyFieldViewModel>(_studyfield);

            return Ok(studyfield);

        }

        [HttpPost]
        public IActionResult CreateStudyField([FromBody]StudyFieldInsertDto studyfield)
        {
            if (studyfield == null)
            {
                return BadRequest();
            }

            if (_studyfieldRepository.isStudyFieldExist(studyfield.Field))
            {
                ModelState.AddModelError(nameof(StudyFieldInsertDto), "Study Field Name Already Exist");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var studyfieldEntity = Mapper.Map<StudyField>(studyfield);

            _studyfieldRepository.Add(studyfieldEntity);

            if (!_studyfieldRepository.Commit())
            {
                throw new Exception("Creating Study Field Failed");
            }

            var studyfieldtoReturn = Mapper.Map<StudyFieldViewModel>(studyfieldEntity);

            return CreatedAtRoute("GetStudyField", new { id = studyfieldtoReturn.Id }, studyfieldtoReturn);

        }

        [HttpPost("{id}")]
        public IActionResult CreateStudyField(int id)
        {
            if (_studyfieldRepository.isExists(id))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return NotFound();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateLocation(int id, [FromBody] JsonPatchDocument<StudyFieldUpdateDto> studyfield)
        {
            if (studyfield == null)
            {
                return BadRequest();
            }

            var studyfieldFromRepo = _studyfieldRepository.GetSingle(id);

            if (studyfieldFromRepo == null)
            {
                return NotFound();
            }

            var studyfieldToPatch = Mapper.Map<StudyFieldUpdateDto>(studyfieldFromRepo);

            studyfield.ApplyTo(studyfieldToPatch, ModelState);

            TryValidateModel(studyfieldToPatch);

            if (!ModelState.IsValid)
            {
                return new InputValidation(ModelState);
            }

            Mapper.Map(studyfieldToPatch, studyfieldFromRepo);

            _studyfieldRepository.Update(studyfieldFromRepo);

            if (!_studyfieldRepository.Commit())
            {
                throw new Exception("Creating Study Field Failed");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudyField(int id)
        {
            var _studyfieldDb = _studyfieldRepository.GetSingle(id);

            if (_studyfieldDb == null)
            {
                return NotFound();
            }

            _studyfieldRepository.Delete(_studyfieldDb);

            if (!_studyfieldRepository.Commit())
            {
                throw new Exception($"Deleting Study Field {id} failed on commit");
            }

            _logger.LogInformation(100, $"Study Field {id} was deleted.");

            return NoContent();
        }
    }
}
