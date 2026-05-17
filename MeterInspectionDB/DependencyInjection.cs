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
            services.AddScoped<SyncRepository>();
            services.AddScoped<OFFline_Online>();
            services.AddScoped<CorrectiveActionRepository>();
            services.AddScoped<LabCenterRepository>();
            services.AddScoped<CompanySectorDeptRepository>();
            services.AddScoped<TestResultRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<ErrorRepository>();
            services.AddScoped<MaintenanceRecordRepository>();
            services.AddScoped<MaintenanceRecordDetailRepository>();

            return services;
        }
    }
}
