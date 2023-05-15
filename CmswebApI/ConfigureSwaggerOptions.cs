using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CmswebApI
{

    [ExcludeFromCodeCoverage]
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private const string ApiTitle = "CmsWebApi presented by Divya(new developer) and supported by Pankaj(DevOps expert)";

        private const string ApiDescription = "**CmsWebApi**\n\n" +
        "<br/>provides details of:\n\n" +
            "<br><ul>"+
            "<li>Courses</li>" +
            "<li>Students</li>";           

        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        }
        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options); 
        }       

        public void Configure(SwaggerGenOptions options)
        {
            //add a swaager document for each discoved API version
            //note: you might choose to skip or document depreceted API version differently
            foreach (var item in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
              options.SwaggerDoc(item.GroupName, CreateVersionInfo(item));
            }
        }

        private static OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo 
            {
              Title = "CmsWebApi by Divya",
              Version = description.ApiVersion.ToString(),
              Description = ApiDescription
            };
            return info;
        }
    }
}
