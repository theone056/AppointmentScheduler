using AppointmentScheduler.Core.Domain.Entities;
using AppointmentScheduler.Core.Domain.Interface;
using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Infrasturcture.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AppointmentScheduler.Infrasturcture.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AppointmentRepository(ApplicationDbContext context,
                                     IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(CreateAppointmentDTO appointmentDTO)
        {
            try
            {
                var appointmentDTOMapped = _mapper.Map<Appointment>(appointmentDTO);
                await _context.Appointments.AddAsync(appointmentDTOMapped);
                var result = await _context.SaveChangesAsync();

                if(result > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<GetAllAppointmentsDTO>> GetAllAppointmentsAsync()
        {
            try
            {
                var result = await _context.Appointments.ToListAsync();
                return _mapper.Map<List<GetAllAppointmentsDTO>>(result);
            }catch
            {
                throw;
            }
        }

        
    }
}
