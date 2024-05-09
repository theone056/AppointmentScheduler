using AppointmentScheduler.Core.Domain.IdentityEntities;
using AppointmentScheduler.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.Services.JWTServices.Interface
{
    public interface IJWTGenerator
    {
        AuthenticationResponse CreateJWTToken(ApplicationUser user);
    }
}
