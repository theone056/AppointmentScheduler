using AppointmentScheduler.Core.Domain.Interface;
using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Core.Services.AppointmentServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.Services.AppointmentServices
{
    public class AppointmentAdderService : IAppointmentAdderService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentAdderService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<bool> CreateAsync(CreateAppointmentDTO createAppointmentDTO)
        {
            if(createAppointmentDTO == null) { throw new ArgumentNullException(nameof(createAppointmentDTO)); }

            return await _appointmentRepository.CreateAsync(createAppointmentDTO);
        }
    }
}
