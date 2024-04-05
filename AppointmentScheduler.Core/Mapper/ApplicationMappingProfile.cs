using AppointmentScheduler.Core.Domain.IdentityEntities;
using AppointmentScheduler.Core.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.Mapper
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<ApplicationUser, UserRegistrationDTO>().ReverseMap();
        }
    }
}
