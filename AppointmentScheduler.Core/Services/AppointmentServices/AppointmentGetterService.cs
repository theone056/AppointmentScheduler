using AppointmentScheduler.Core.Domain.Interface;
using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Core.Services.AppointmentServices.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.Services.AppointmentServices
{
    public class AppointmentGetterService : IAppointmentGetterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AppointmentGetterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetAllAppointmentsDTO>> GetAllAppointmentsAsync()
        {
            try
            {
                var result = await _unitOfWork.Appointments.GetAll();
                var appointments = _mapper.Map<List<GetAllAppointmentsDTO>>(result);
                return appointments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }
        }
    }
}
