using ApplicantProfile.API.ViewModels;
using ApplicantProfile.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.API.Mappings
{
    public class DomainToViewModelMappingProfile:Profile
    {
        protected override void Configure()
        {

            Mapper.CreateMap<LocationInsertDto, Location>();
            Mapper.CreateMap<LocationUpdateDto, Location>();
            Mapper.CreateMap<Location, LocationUpdateDto>();

            Mapper.CreateMap<InstituteCreateDto, Institute>();
            Mapper.CreateMap<InstituteUpdateDto, Institute>();
            Mapper.CreateMap<Institute, InstituteUpdateDto>();

            Mapper.CreateMap<QualificationInsertDto, Qualification>();
            Mapper.CreateMap<QualificationUpdateDto, Qualification>();
            Mapper.CreateMap<Qualification, QualificationUpdateDto>();

            Mapper.CreateMap<StudyFieldInsertDto, StudyField>();
            Mapper.CreateMap<StudyFieldUpdateDto, StudyField>();
            Mapper.CreateMap<StudyField, StudyFieldUpdateDto>();

            Mapper.CreateMap<VacancyCreateDto, Vacancy>()
                .ForMember(v => v.JobTitleId,
                map => map.MapFrom(vc => vc.SelectedJobTitle))
                     .ForMember(v => v.LocationId,
                map => map.MapFrom(vc => vc.SelectedLocation));

            Mapper.CreateMap<VacancyUpdateDto, Vacancy>()
             .ForMember(v => v.JobTitleId,
                map => map.MapFrom(vc => vc.SelectedJobTitle))
                     .ForMember(v => v.LocationId,
                map => map.MapFrom(vc => vc.SelectedLocation));

            Mapper.CreateMap<Vacancy, VacancyUpdateDto>()
            .ForMember(vm => vm.SelectedJobTitle,
              map => map.MapFrom(v => v.JobTitleId))
          .ForMember(vm => vm.SelectedLocation, map =>
              map.MapFrom(v => v.LocationId));

            Mapper.CreateMap<Vacancy, VacancyViewModel>()
          .ForMember(vm => vm.SelectedJobTitle,
              map => map.MapFrom(v => v.JobTitleId))
          .ForMember(vm => vm.SelectedLocation, map =>
              map.MapFrom(v => v.LocationId));

            Mapper.CreateMap<JobTitle, JobTitleViewModel>()
          .ForMember(vm => vm.SelectedField,
              map => map.MapFrom(j => j.StudyFieldId))
          .ForMember(vm => vm.SelectedQLevel, map =>
              map.MapFrom(j => j.QualificationId));

            Mapper.CreateMap<JobTitleInsertDto, JobTitle>()
               .ForMember(vm => vm.StudyFieldId,
                map => map.MapFrom(j => j.SelectedField))
            .ForMember(vm => vm.QualificationId, map =>
                map.MapFrom(j => j.SelectedQLevel));

            Mapper.CreateMap<JobTitle, JobTitleUpdateDto>()
              .ForMember(vm => vm.SelectedField,
                map => map.MapFrom(j => j.StudyFieldId))
            .ForMember(vm => vm.SelectedQLevel, map =>
                map.MapFrom(j => j.QualificationId));

            Mapper.CreateMap<JobTitleUpdateDto, JobTitle>()
                .ForMember(vm => vm.StudyFieldId,
                   map => map.MapFrom(j => j.SelectedField))
               .ForMember(vm => vm.QualificationId, map =>
                map.MapFrom(j => j.SelectedQLevel));

            Mapper.CreateMap<Applicant, ApplicantViewModel>()
            .ForMember(vm => vm.SelectedGender,
                map => map.MapFrom(a => a.GenderId))
            .ForMember(vm => vm.SelectedVacancy, map =>
                map.MapFrom(a => a.VacancyId));

            Mapper.CreateMap<Education, EducationViewModel>()
            .ForMember(vm => vm.SelectedApplicant,
                map => map.MapFrom(e => e.ApplicantId))
            .ForMember(vm => vm.SelectedInstitute, map =>
                map.MapFrom(e => e.InstituteId))
            .ForMember(vm => vm.SelectedQualification, map =>
                map.MapFrom(e => e.QualificationId))
            .ForMember(vm => vm.SelectedStudyField, map =>
                map.MapFrom(e => e.StudyFieldId));

            Mapper.CreateMap<Experience, ExperienceViewModel>()
            .ForMember(vm => vm.SelectedApplicant,
                map => map.MapFrom(e => e.ApplicantId));

          


           

           

            Mapper.CreateMap<Gender, GenderViewModel>()
          .ForMember(vm => vm.Name,
              map => map.MapFrom(g => g.Name));

            Mapper.CreateMap<Location, LocationViewModel>()
          .ForMember(vm => vm.Name,
             map => map.MapFrom(l => l.Name));

            Mapper.CreateMap<Institute, InstituteViewModel>()
          .ForMember(vm => vm.Name,
             map => map.MapFrom(l => l.Name));

            Mapper.CreateMap<Qualification, QualificationViewModel>()
          .ForMember(vm => vm.Name,
            map => map.MapFrom(l => l.Name));

            Mapper.CreateMap<QualificationInsertDto, Qualification>();
            Mapper.CreateMap<QualificationUpdateDto, Qualification>();

            Mapper.CreateMap<StudyField, StudyFieldViewModel>()
        .ForMember(vm => vm.Field,
           map => map.MapFrom(l => l.Field));

            Mapper.CreateMap<StudyFieldInsertDto, StudyField>();
            Mapper.CreateMap<StudyFieldUpdateDto, StudyField>();
        }
    }
}
