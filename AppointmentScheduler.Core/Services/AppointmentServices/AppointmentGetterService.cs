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
    public class AppointmentGetterService : IAppointmentGetterService
    {
        private readonly IAppointmentRepository _repository;
        public AppointmentGetterService(IAppointmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<GetAllAppointmentsDTO>> GetAllAppointmentsAsync()
        {
            try
            {
                return await _repository.GetAllAppointmentsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }
        }
    }
}
