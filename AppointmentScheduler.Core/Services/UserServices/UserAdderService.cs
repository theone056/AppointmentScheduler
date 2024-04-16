using AppointmentScheduler.Core.Domain.Interface;
using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Core.Services.UserServices.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.Services.UserServices
{
    public class UserAdderService : IUserAdderService
    {
        private readonly IUserRepository _userRepository;
        public UserAdderService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RegisterUserResponse> RegisterAsync(UserRegistrationDTO userRegistration)
        {
            if(userRegistration == null) throw new ArgumentNullException(nameof(userRegistration));
            try
            {
                return await _userRepository.RegisterAsync(userRegistration);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
