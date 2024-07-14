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
            return app;
        }
    }
}
