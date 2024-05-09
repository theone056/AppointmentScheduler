using AppointmentScheduler.Core.Domain.IdentityEntities;
using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Core.Services.JWTServices.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppointmentScheduler.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJWTGenerator _jwtGenerator;
        private readonly IJWTGetterService _jwtGetter;
        private readonly IMapper _mapper;
        public UserController(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager,
                              IJWTGenerator jWTGenerator,
                              IJWTGetterService jwtGetter,
                              IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jWTGenerator;
            _jwtGetter = jwtGetter;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegistrationDTO userRegistrationDTO)
        {
            if(ModelState.IsValid)
            {
                var userRegister = _mapper.Map<ApplicationUser>(userRegistrationDTO);
                var result = await _userManager.CreateAsync(userRegister, userRegistrationDTO.Password);
                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(userRegister, isPersistent: false);
                    var authenticationResponse = _jwtGenerator.CreateJWTToken(userRegister);
                    userRegister.RefreshToken = authenticationResponse.RefreshToken;
                    userRegister.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTime;
                    await _userManager.UpdateAsync(userRegister);
                    return Ok(authenticationResponse);
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginDTO.Username) ?? await _userManager.FindByNameAsync(loginDTO.Username);
                if(user is not null)
                {
                    var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                    if(result)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        var authenticationResponse = _jwtGenerator.CreateJWTToken(user);
                        user.RefreshToken = authenticationResponse.RefreshToken;
                        user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTime;
                        await _userManager.UpdateAsync(user);
                        return Ok(authenticationResponse);
                    }
                }
                else
                {
                    return NoContent();
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("GenerateNewAccessToken")]
        public async Task<IActionResult> GenerateNewAccessToken(TokenModel tokenModel)
        {
            if (ModelState.IsValid)
            {
                ClaimsPrincipal? principal = _jwtGetter.GetClaimsPrincipalFromJWTToken(tokenModel.Token);
                if(principal is null) 
                {
                    return BadRequest(principal);
                }

                string email = principal.FindFirstValue(ClaimTypes.NameIdentifier);

                ApplicationUser user = await _userManager.FindByEmailAsync(email);

                if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpirationDateTime <= DateTime.Now)
                {
                    return BadRequest("Invalid refresh token");
                }

                AuthenticationResponse authenticationResponse = _jwtGenerator.CreateJWTToken(user);
                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpirationDateTime = authenticationResponse.RefreshTokenExpirationDateTime;
                await _userManager.UpdateAsync(user);

                return Ok(authenticationResponse);
            }

            return BadRequest(tokenModel);
        }
    }
}
