using AppointmentScheduler.Core.Mapper;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core
{
    public static class CoreServiceExtensions
    {
        public static void ConfigureExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            var mappingConfig = new MapperConfiguration(map =>
            {
                map.AddProfile<ApplicationMappingProfile>();
            });

            services.AddSingleton(mappingConfig.CreateMapper());
        }
    }
}
