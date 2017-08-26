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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ApplicantProfile.API.Controllers
{
    [Route("api/[controller]")]
    public class GenderController : Controller
    {
        private IGenderRepository _genderRepository;

        public GenderController(IGenderRepository _genderRepository)
        {
            this._genderRepository = _genderRepository;
        }

        [HttpGet(Name = "GetGender")]
        public IActionResult GetGender()
        {

            var genderFromRepo = _genderRepository.GetAll();

            var genders = Mapper.Map<IEnumerable<GenderViewModel>>(genderFromRepo);

            return new JsonResult(genders);

        }
        
    }
}
