using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmswebApI.Repository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using CmswebApI.DTOs;
using CmswebApI.Validators;
using System.IO;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace CmswebApI
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
            // AddSingleton: This lifetime creates a single instance of the service that is shared across the entire application. 
            // Once the service is created, it is reused for each subsequent request for that service.

            // AddTransient: This lifetime creates a new instance of the service for each request. 
            // This is useful for services that have a short lifespan and are not thread-safe.

            // AddScoped: This lifetime creates a new instance of the service for each HTTP request. 
            // The instance is shared within the scope of the request, so it can be used by multiple components during the request.
            services.AddSingleton<ICmsrepository, InMemoryCmsRepository>();
            // Registering all fluent validators that are in assembly which have CourseDtoValidation (including)
            services.AddControllers()
                .AddFluentValidation(fv =>
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<CourseDtoValidator>();
                    });
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml", SearchOption.AllDirectories);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CmswebApI", Version = "v1" });
                foreach (var name in files) c.IncludeXmlComments(name);
            });
            services.AddApiVersioning(setupAction =>
             {
                 setupAction.AssumeDefaultVersionWhenUnspecified = true;
                 setupAction.DefaultApiVersion = new ApiVersion(1, 0);
                 //This one is for QueryString Versioning
                 //setupAction.ApiVersionReader = new QueryStringApiVersionReader("v");

                 //This One is for URL Versioning
                 // setupAction.ApiVersionReader = new UrlSegmentApiVersionReader();
                 // this is used for Header Version
                // setupAction.ApiVersionReader = new HeaderApiVersionReader("X-Version");
                  
                  setupAction.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("v"),
                    new HeaderApiVersionReader("X-Version")
                  );

             });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CmswebApI v1"));
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // redirect the root URL to the Swagger UI URL
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger");
                    return Task.CompletedTask;
                });
            });

        }
    }
}


