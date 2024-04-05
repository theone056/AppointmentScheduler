using AppointmentScheduler.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.Services.UserServices.Interfaces
{
    public interface IUserAuthenticationService
    {
        Task<bool> LoginAsync(UserLoginDTO userLogin);
        Task LogoutAsync();
    }
}
