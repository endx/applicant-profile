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
    public class QualificationController : Controller
    {
        private IQualificationRepository _qualificationRepository;
        private ILogger<QualificationController> _logger;
        private IUrlHelper _urlHelper;
        public QualificationController(IQualificationRepository _qualificationRepository, ILogger<QualificationController> _logger, IUrlHelper _urlHelper)
        {
            this._qualificationRepository = _qualificationRepository;
            this._logger = _logger;
            this._urlHelper = _urlHelper;
        }
        
        [HttpGet(Name ="GetQualifications")]
        public IActionResult GetQualification(LocationResourceParameter locationResourceParameter)
        {
            CreateResourceUri cru = new CreateResourceUri(this._urlHelper);

            var qualificationfromRepo = _qualificationRepository.GetQualifications(locationResourceParameter);

            var previousPageLink = qualificationfromRepo.HasPrevious ?
                cru.CreateUri(locationResourceParameter, ResourceUriType.PreviousPage, "GetQualifications") : null;

            var nextPageLink = qualificationfromRepo.HasNext ?
                cru.CreateUri(locationResourceParameter, ResourceUriType.NextPage, "GetQualifications") : null;

            var paginationMetadata = new
            {
                totalcount = qualificationfromRepo.TotalCount,
                pageSize = qualificationfromRepo.PageSize,
                currentPage = qualificationfromRepo.CurrentPage,
                totalPages = qualificationfromRepo.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            var qualifications = Mapper.Map<IEnumerable<QualificationViewModel>>(qualificationfromRepo);

            return Ok(qualifications);
        }

        [HttpGet("{id}", Name = "GetQualification")]
        public IActionResult GetQualification(int id)
        {
            var _qualification = _qualificationRepository.GetSingle(i => i.Id == id);

            if (_qualification == null)
            {
                return NotFound();
            }

            var qualification = Mapper.Map<QualificationViewModel>(_qualification);

            return Ok(qualification);
        }

        [HttpPost]
        public IActionResult CreateQualification([FromBody]QualificationInsertDto qualificaiton)
        {
            if (qualificaiton == null)
            {
                return BadRequest();
            }

            if (_qualificationRepository.isQualificationExist(qualificaiton.Name))
            {
                ModelState.AddModelError(nameof(QualificationInsertDto), "Qualification Name Already Exist");

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var qualificaitonEntity = Mapper.Map<Qualification>(qualificaiton);

            _qualificationRepository.Add(qualificaitonEntity);
            if (!_qualificationRepository.Commit())
            {
                throw new Exception("Creating a Locaiton Failed");
            }

            var qualificaitonReturn = Mapper.Map<QualificationViewModel>(qualificaitonEntity);

            return CreatedAtRoute("GetQualification", new { id = qualificaitonReturn.Id }, qualificaitonReturn);

        }

        [HttpPost("{id}")]
        public IActionResult CreateQualification(int id)
        {
            if (_qualificationRepository.isExists(id))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return NotFound();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateQualification(int id, [FromBody] JsonPatchDocument<QualificationUpdateDto> qualification)
        {
            if (qualification == null)
            {
                return BadRequest();
            }

            var qualificationFromRepo = _qualificationRepository.GetSingle(id);

            if (qualificationFromRepo == null)
            {
                return NotFound();
            }


            var qualificationToPatch = Mapper.Map<QualificationUpdateDto>(qualificationFromRepo);

            qualification.ApplyTo(qualificationToPatch, ModelState);

            TryValidateModel(qualificationToPatch);

            if (!ModelState.IsValid)
            {
                return new InputValidation(ModelState);
            }

            Mapper.Map(qualificationToPatch, qualificationFromRepo);

            _qualificationRepository.Update(qualificationFromRepo);

            if (!_qualificationRepository.Commit())
            {
                throw new Exception("Creating Qualification Failed");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQualification(int id)
        {
            var _qualificationDb = _qualificationRepository.GetSingle(id);

            if (_qualificationDb == null)
            {
                return NotFound();
            }
           
            _qualificationRepository.Delete(_qualificationDb);

            if (!_qualificationRepository.Commit())
            {
                throw new Exception($"Deleting Qualification {id} failed on commit");
            }

            _logger.LogInformation(100, $"Qualification {id} was deleted.");

            return NoContent();
        }
    }
}
