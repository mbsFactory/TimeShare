using TimeShare.Data;
using TimeShare.Data.Repositories;
using TimeShare.Data.Abstract;
using TimeShare.Api.ViewModels.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using TimeShare.Api.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace TimeShare.Api
{
    public class Startup
    {

        private static string _contentRootPath = string.Empty;
        private static string _applicationPath = string.Empty;
        //bool useInMemoryProvider = false;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddDbContext<SchedulerContext>(options =>
            //    options.UseSqlServer(Configuration["Data:SchedulerConnection:ConnectionString"],
            //    b => b.MigrationsAssembly("Scheduler.API")));

            // Connectionstring di default
            services.AddDbContext<TimeShareContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("TimeShare.Data")));

            //services.AddDbContext<TimeShareContext>(opt => opt.UseInMemoryDatabase("TimeShareDb"));

            // Repositories
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAttendeeRepository, AttendeeRepository>();

            // Automapper Configuration
            services.AddAutoMapper();

            // Enable Cors
            services.AddCors();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opts =>
                {
                    // Force Camel Case to JSON
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            _applicationPath = env.WebRootPath;
            // Setup configuration sources.

            //var builder1 = new ConfigurationBuilder()
            //    .SetBasePath(env.ContentRootPath)
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            //    .AddEnvironmentVariables();


            // Enable the use of static file such as images, css, js, ... 
            app.UseStaticFiles();

            // Enable Cross-Origin Requests (CORS) in ASP.NET Core
            app.UseCors(builder =>
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());

            app.UseExceptionHandler(builder =>
            {
                builder.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                        }
                    });
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //builder1.AddUserSecrets<Startup>();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                //routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });

            //Configuration = builder1.Build();

            TimeShareDbInizializer.Initialize(app.ApplicationServices);
            //SchedulerDbInitializer.Initialize(app.ApplicationServices);

        }
    }
}
