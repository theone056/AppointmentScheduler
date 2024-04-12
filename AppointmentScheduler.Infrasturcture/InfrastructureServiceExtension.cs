using AppointmentScheduler.Core.Domain.IdentityEntities;
using AppointmentScheduler.Core.Domain.Interface;
using AppointmentScheduler.Infrasturcture.Context;
using AppointmentScheduler.Infrasturcture.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Infrasturcture
{
    public static class InfrastructureServiceExtension
    {
        public static void ConfigureInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("connectionString"));
            });

            services.AddIdentity<ApplicationUser, ApplicationUserRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthorizationCore(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
        }
    }
}
