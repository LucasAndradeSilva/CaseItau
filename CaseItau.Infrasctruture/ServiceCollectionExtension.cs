using CaseItau.Data;
using CaseItau.Domain.Interfaces;
using CaseItau.Domain.Services;
using CaseItau.Repositories;
using CaseItau.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseItau.IOC
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddDatabase();
            services.AddDependencyInjections();
            services.AddCustomSwagger();
            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddScoped(instance => new FundDbContext());
            return services;
        }

        private static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IFundService, FundService>();            
            return services;
        }

        private static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Case Itau Fundos",
                    Version = "v1",
                    Description = "Dotumentação da API para Gerencias Fundos.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Lucas Andrade Silva",
                        Email = "lucasandradesilva2019@gmail.com",
                        Url = new Uri("https://github.com/LucasAndradeSilva"),
                    },
                });
            });

            return services;
        }
    }
}
