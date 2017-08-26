using ApplicantProfile.Data.Helper;
using ApplicantProfile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicantProfile.Data.Abstract
{
  
    public interface IApplicantRepository : IEntityBaseRepository<Applicant> { }
    public interface IEducationtRepository : IEntityBaseRepository<Education> { }
    public interface IExperienceRepository : IEntityBaseRepository<Experience> { }
    public interface IGenderRepository : IEntityBaseRepository<Gender> { }
    public interface IInstituteRepository : IEntityBaseRepository<Institute>
    {
        bool isInstituteExist(string name);
        PagedList<Institute> GetInstitutes(LocationResourceParameter locationResourceParameter);

    }
    public interface IJobTitleRepository : IEntityBaseRepository<JobTitle>
    {
        bool isJobTitleExist(string name);
        PagedList<JobTitle> GetJobTitles(LocationResourceParameter locationResourceParameter);
    }
    public interface ILocationRepository: IEntityBaseRepository<Location>
    {
        bool isLocationExist(string name);
        PagedList<Location> GetLocations(LocationResourceParameter locationResourceParameter);
    }
    public interface IQualificationRepository : IEntityBaseRepository<Qualification>
    {
        bool isQualificationExist(string name);
        PagedList<Qualification> GetQualifications(LocationResourceParameter locationResourceParameter);
    }
    public interface IStudyFieldRepository : IEntityBaseRepository<StudyField>
    {
        bool isStudyFieldExist(string field);
        PagedList<StudyField> GetStudyFields(LocationResourceParameter locationResourceParameter);
    }
    public interface IVacancyRepsitory : IEntityBaseRepository<Vacancy>
    {
        bool isVacancyExist(string name);
        PagedList<Vacancy> GetVacancies(LocationResourceParameter locationResourceParameter);
        Vacancy GetSingle(string id);
    }
  
}
