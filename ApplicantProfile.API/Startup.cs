using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using ApplicantProfile.Data;
using ApplicantProfile.Data.Repositories;
using ApplicantProfile.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using ApplicantProfile.API.Mappings;
using ApplicantProfile.API.Core;
using Microsoft.AspNetCore.Mvc.Formatters;
using NLog.Extensions.Logging;
using NLog.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ApplicantProfile.API
{
    public class Startup
    {
        private static string _applicationPath = string.Empty;
        private static string _contentRootPath = string.Empty;
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            _applicationPath = env.WebRootPath;
            _contentRootPath = env.ContentRootPath;
            // Setup configuration sources.

            var builder = new ConfigurationBuilder()
                .SetBasePath(_contentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicantProfileContext>(options =>
                options.UseSqlServer(Configuration["Data:ApplicantProfileConnection:ConnectionString"],
                b => b.MigrationsAssembly("ApplicantProfile.API")));

            // Repositories
            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            services.AddScoped<IEducationtRepository, EducationRepository>();
            services.AddScoped<IExperienceRepository, ExperienceRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IInstituteRepository, InstituteRepository>();
            services.AddScoped<IJobTitleRepository, JobTitleRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IQualificationRepository, QualificationRepository>();
            services.AddScoped<IVacancyRepsitory, VacancyRepository>();
            services.AddScoped<IStudyFieldRepository, StudyFieldRepository>();
       
            // Automapper Configuration
            AutoMapperConfiguration.Configure();

            // Enable Cors
            services.AddCors();

            // Add MVC services to the services container.
            services.AddMvc(setupAction => {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                setupAction.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
            })
                .AddJsonOptions(opts =>
                {
                    // Force Camel Case to JSON
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            //URI helper    
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IUrlHelper, UrlHelper> (implementationFactory =>
            {
                var actioncontext = implementationFactory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actioncontext);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
       // public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
      public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug(LogLevel.Information);
            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(
              builder =>
              {
                  builder.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global Exception Logger");
                            logger.LogError(500, error.Error, error.Error.Message);

                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                        }
                    });
              });
            }
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            app.UseStaticFiles();
            // Add MVC to the request pipeline.
            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            //app.UseExceptionHandler(
            //  builder =>
            //  {
            //      builder.Run(
            //        async context =>
            //        {
            //            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            //            var error = context.Features.Get<IExceptionHandlerFeature>();
            //            if (error != null)
            //            {
            //                var logger = loggerFactory.CreateLogger("Global Exception Logger");
            //                logger.LogError(500, error.Error, error.Error.Message);

            //                context.Response.AddApplicationError(error.Error.Message);
            //                await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
            //            }
            //        });
            //  });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                //routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });

            ApplicantProfileDbInitializer.Initialize(app.ApplicationServices);
        }
    }
}
