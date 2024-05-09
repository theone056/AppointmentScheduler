using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.Services.JWTServices.Interface
{
    public interface IJWTGetterService
    {
        ClaimsPrincipal GetClaimsPrincipalFromJWTToken(string? token);
    }
}
