using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.DTO
{
    public class TokenModel
    {
        public string? Token { get; set; }
        public String? RefreshToken { get; set; }
    }
}
