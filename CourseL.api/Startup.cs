using AutoMapper;
using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace CourseLibrary.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(setUpAction =>
            {
                setUpAction.ReturnHttpNotAcceptable = true;

            }).AddXmlDataContractSerializerFormatters();
            
            services.AddScoped<ICourseLibraryRepository, CourseLibraryRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("LibraryOpenAPISpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Library API",
                        Version = "1"
                    });
               // var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
               // var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

               // setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });

            services.AddDbContext<CourseLibraryContext>(options =>
            {
                options.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=CourseLibraryDB;Trusted_Connection=True;");
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened . Try again later");
                    });
                });
            }
            app.UseRouting();
            app.UseSwagger();

            app.UseSwaggerUI(setUpAction =>
            {
                setUpAction.SwaggerEndpoint("/swagger/LibraryOpenAPISpecification/swagger.json", "Library API");
                setUpAction.RoutePrefix = "";
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
