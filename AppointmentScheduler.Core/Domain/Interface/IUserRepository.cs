using AppointmentScheduler.Core.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.Domain.Interface
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterAsync(UserRegistrationDTO user);
        Task<bool> LoginAsync(UserLoginDTO user);
        Task LogoutAsync();
    }
}
