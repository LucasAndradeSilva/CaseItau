using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseItau.IOC
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder AddCustomAppBuilder(this IApplicationBuilder app)
        {
            app.UseCustomSwagger();

            return app;
        }

        private static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            return app;
        }
    }
}
