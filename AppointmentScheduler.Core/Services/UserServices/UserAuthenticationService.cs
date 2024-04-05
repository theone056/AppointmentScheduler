using AppointmentScheduler.Core.Domain.Interface;
using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Core.Services.UserServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Core.Services.UserServices
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        public UserAuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> LoginAsync(UserLoginDTO userLogin)
        {
            if(userLogin == null) throw new ArgumentNullException(nameof(userLogin));
            try
            {
                return await _userRepository.LoginAsync(userLogin);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _userRepository.LogoutAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
