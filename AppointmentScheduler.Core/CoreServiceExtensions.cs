using AppointmentScheduler.Core.Mapper;
using AppointmentScheduler.Core.Services.AppointmentServices;
using AppointmentScheduler.Core.Services.AppointmentServices.Interfaces;
using AppointmentScheduler.Core.Services.JWTServices;
using AppointmentScheduler.Core.Services.JWTServices.Interface;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddTransient<IAppointmentAdderService, AppointmentAdderService>();
            services.AddTransient<IAppointmentGetterService, AppointmentGetterService>();
            services.AddTransient<IJWTGenerator, JWTGenerator>();
            services.AddTransient<IJWTGetterService,  JWTGetterService>();
        }
    }
}
