using AppointmentScheduler.Core.Domain.Entities;
using AppointmentScheduler.Core.Domain.Interface;
using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Infrasturcture.Context;
using AppointmentScheduler.Infrasturcture.GenericRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppointmentScheduler.Infrasturcture.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
