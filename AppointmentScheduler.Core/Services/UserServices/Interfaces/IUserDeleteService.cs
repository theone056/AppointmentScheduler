using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.Services.UserServices.Interfaces
{
    public interface IUserDeleteService
    {
        Task<bool> DeleteUserAsync(Guid userId);
    }
}
