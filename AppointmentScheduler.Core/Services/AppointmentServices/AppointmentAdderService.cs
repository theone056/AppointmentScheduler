using AppointmentScheduler.Core.Domain.Entities;
using AppointmentScheduler.Core.Domain.Interface;
using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Core.Services.AppointmentServices.Interfaces;
using AutoMapper;

namespace AppointmentScheduler.Core.Services.AppointmentServices
{
    public class AppointmentAdderService : IAppointmentAdderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AppointmentAdderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(CreateAppointmentDTO createAppointmentDTO)
        {
            if(createAppointmentDTO == null) { throw new ArgumentNullException(nameof(createAppointmentDTO)); }
            try
            {
                var appointment = _mapper.Map<Appointment>(createAppointmentDTO);
                var result = await _unitOfWork.Appointments.Add(appointment);
                if(result)
                {
                    await _unitOfWork.Save();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
