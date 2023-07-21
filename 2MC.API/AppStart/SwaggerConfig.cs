using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using _2MC.BusinessTier.Commons;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Reso.Core.Custom;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace _2MC.API.AppStart
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwaggerServices(this IServiceCollection services)
        {
            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mentor-Mentee-Connect.API", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Mentor-Mentee-Connect.API", Version = "v2" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer iJIUzI1NiIsInR5cCI6IkpXVCGlzI9d9zc2'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                c.IncludeXmlComments(cmlCommentsFullPath);
                c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.EnableAnnotations();
            });

            services.AddSwaggerGenNewtonsoftSupport();
            services.TryAddEnumerable(ServiceDescriptor
                .Transient<IApiDescriptionProvider, SnakeCaseQueryParametersApiDescriptionProvider>());
        }

        public static void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
                c.DocExpansion(DocExpansion.None);
            });
        }

        public class AuthorizeCheckOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var hidden =
                    context.MethodInfo.CustomAttributes.FirstOrDefault(a =>
                        a.AttributeType == typeof(HiddenParamsAttribute));
                if (hidden != null)
                {
                    var parameters = ((string)hidden.ConstructorArguments.FirstOrDefault().Value)?.Split(",");
                    if (parameters != null)
                        foreach (var parameter in parameters)
                        {
                            if (operation.Parameters.Any(a => a.Name.ToSnakeCase() == parameter.ToSnakeCase()))
                            {
                                operation.Parameters.Remove(operation.Parameters.FirstOrDefault(a =>
                                    a.Name.ToSnakeCase() == parameter.ToSnakeCase()));
                            }
                        }
                }
            }
        }
    }
}