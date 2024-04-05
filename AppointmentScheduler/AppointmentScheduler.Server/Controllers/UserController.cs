using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Core.Services.UserServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduler.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IUserAdderService _userAdderService;
        private readonly IUserAuthenticationService _userAuthenticationService;
        public UserController(IUserAdderService userAdderService,
                              IUserAuthenticationService userAuthenticationService)
        {
            _userAdderService = userAdderService;
            _userAuthenticationService = userAuthenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationDTO userRegistrationDTO)
        {
            if(ModelState.IsValid)
            {
                var result = await _userAdderService.RegisterAsync(userRegistrationDTO);
                if(result.Succeeded)
                {
                    return Ok(result);
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("Register", error.Description);
                    }
                }
            }

            return BadRequest(ModelState);
        }
    }
}
