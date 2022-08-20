// Licensed under the Apache License, Version 1.0 (the "License").

using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Scorecard.Web.Api.Extensions;

public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services) {
        services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReverseProxy Management Web API", Version = "v1" });
            c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "Please enter into field the word 'Bearer' following by space and JWT. Example: Bearer {token}",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Name = "Authorization",
                        Scheme = "Bearer"
                    });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.Http,
                            Name = "Authorization",
                            Scheme = "Bearer",
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            var filePath = Path.Combine(System.AppContext.BaseDirectory, "Scorecard.Management.xml");
            c.IncludeXmlComments(filePath);
        });
        services.AddSwaggerGenNewtonsoftSupport();
        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app) {
        //app.UseSwagger(c =>
        //{
        //    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
        //    {
        //        swaggerDoc.Servers.Add(new OpenApiServer() { Url = $"dev" });
        //    });
        //});
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"v1/swagger.json", "ReverseProxy Management Web API");
            c.DocExpansion(DocExpansion.List);
        });
        return app;
    }
}
