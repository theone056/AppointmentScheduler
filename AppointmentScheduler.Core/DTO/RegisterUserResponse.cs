using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.DTO
{
    public class RegisterUserResponse
    {
        public IEnumerable<IdentityError> Errors { get; set; }
        public bool Succeeded { get; set; }
    }
}
