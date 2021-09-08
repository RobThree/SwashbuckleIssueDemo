using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;
using System.Reflection;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using SwashbuckleTest.Infrastructure.ObjectId;

namespace SwashbuckleTest.Infrastructure
{
    public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly SwaggerOptions _config;

        public ConfigureSwaggerGenOptions(IOptions<SwaggerOptions> config)
            => _config = config?.Value ?? throw new ArgumentNullException(nameof(config));

        public void Configure(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", _config.SwaggerDoc);

            options.OperationFilter<ObjectIdOperationFilter>();

            options.MapType<ObjectId.ObjectId>(() => new OpenApiSchema { Type = "string", Example = new OpenApiString(ObjectId.ObjectId.Empty) });
            options.CustomSchemaIds(type => type.Name.EndsWith("DTO") ? type.Name.Replace("DTO", string.Empty) : type.Name);

            // Set the comments path for the Swagger JSON and UI.
            var docfile = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            if (File.Exists(docfile))
                options.IncludeXmlComments(docfile);
        }
    }
}
