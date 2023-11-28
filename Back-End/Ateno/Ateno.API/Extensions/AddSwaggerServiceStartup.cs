using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Ateno.API.Extensions
{
    public static class AddSwaggerServiceStartup
    {
        public static IServiceCollection AddSwaggerGeneration(this IServiceCollection services, string version)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc($"v{version}", new OpenApiInfo
                {
                    Title = "Ateno API",
                    Version = $"Versão RELEASE {version}",
                    Description = $"API <br/> RELEASE {version} lançado em {DateTime.Now.ToString("dd/MM/yyyy")}"
                });

                // Habilitando Swagger para autenticação Jwt Bearer
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = @"JWT Authorization. Bearer token.<br/>
                    <br/>Entre com 'Bearer'[space] e depois digite o valor do token obtido no login.
                    <br/>Exemplo: \""'Bearer 12345abcdef\"""
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
            });

            return services;
        }
    }
}
