using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MeterInspectionDB
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMeterInspectionDB(this IServiceCollection services)
        {
            services.AddScoped<CorrectiveActionRepository>();
            services.AddScoped<LabCenterRepository>();

            return services;
        }
    }
}
