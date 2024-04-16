using AppointmentScheduler.Core.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.Services.UserServices.Interfaces
{
    public interface IUserAdderService
    {
        Task<RegisterUserResponse> RegisterAsync(UserRegistrationDTO userRegistration);
    }
}
