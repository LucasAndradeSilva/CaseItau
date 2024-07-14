using CaseItau.Data;
using CaseItau.Domain.Interfaces;
using CaseItau.Domain.Services;
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
    }
}
