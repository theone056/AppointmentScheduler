using AppointmentScheduler.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.Services.AppointmentServices.Interfaces
{
    public interface IAppointmentAdderService
    {
        Task<bool> CreateAsync(CreateAppointmentDTO createAppointmentDTO);
    }
}
