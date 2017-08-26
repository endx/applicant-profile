using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicantProfile.Model;

namespace ApplicantProfile.Data
{
    public class ApplicantProfileDbInitializer
    {
        private static ApplicantProfileContext context;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            context = (ApplicantProfileContext)serviceProvider.GetService(typeof(ApplicantProfileContext));

            InitializeApplicantProflieDB();
        }

        private static void InitializeApplicantProflieDB()
        {
            if (!context.Genders.Any())
            {
                Gender gender_1 = new Gender
                {
                    Name = "Male",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                Gender gender_2 = new Gender
                {
                    Name = "Female",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                context.Genders.Add(gender_1); context.Genders.Add(gender_2);
                context.SaveChanges();
            }

            if (!context.Genders.Any())
            {
                Location loc_1= new Location
                {
                    Name = "Addis Ababa",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                Location Loc_2 = new Location
                {
                    Name = "Hawassa",
                    AddedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                context.Locations.Add(loc_1); context.Locations.Add(Loc_2);
                context.SaveChanges();
            }
        }
    }
}
