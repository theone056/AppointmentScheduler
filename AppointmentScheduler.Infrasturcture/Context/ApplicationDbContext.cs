using AppointmentScheduler.Core.Domain.Entities;
using AppointmentScheduler.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppointmentScheduler.Infrasturcture.Context
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser,ApplicationUserRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
    }
}
