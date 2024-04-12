using AppointmentScheduler.Core.Domain.IdentityEntities;
using AppointmentScheduler.Core.Domain.Interface;
using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Infrasturcture.Context;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Infrasturcture.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IMapper _mapper;
        public UserRepository(UserManager<ApplicationUser> userManager, 
                              SignInManager<ApplicationUser> signInManager,
                              IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<bool> LoginAsync(UserLoginDTO user)
        {
            try
            {
                var userdetails = await _userManager.FindByEmailAsync(user.Username) ?? await _userManager.FindByNameAsync(user.Username);
                if(userdetails is not null)
                {
                    var result = await _userManager.CheckPasswordAsync(userdetails, user.Password);
                    if (result)
                    {
                        await _signInManager.PasswordSignInAsync(userdetails, user.Password ,isPersistent:false, lockoutOnFailure:false);
                        return true;
                    }
                }

                return false;
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
               await _signInManager.SignOutAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IdentityResult> RegisterAsync(UserRegistrationDTO user)
        {
            try
            {
                var usermapped = _mapper.Map<ApplicationUser>(user);
                return await _userManager.CreateAsync(usermapped,user.Password);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
