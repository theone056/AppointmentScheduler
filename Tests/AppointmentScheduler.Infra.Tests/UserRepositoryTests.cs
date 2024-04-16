using AppointmentScheduler.Core.Domain.IdentityEntities;
using AppointmentScheduler.Core.Domain.Interface;
using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Infrasturcture.Repositories;
using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace AppointmentScheduler.Infra.Tests
{
    public class UserRepositoryTests
    {

        private readonly Mock<UserManager<ApplicationUser>> _moqUserManager;
        private readonly Mock<SignInManager<ApplicationUser>> _moqsignInManager;
        private readonly Mock<IMapper> _moqMapper;
        private readonly IUserRepository _userRepository;

        public UserRepositoryTests()
        {
            _moqUserManager = new Mock<UserManager<ApplicationUser>>(
                              new Mock<IUserStore<ApplicationUser>>().Object,
                              new Mock<IOptions<IdentityOptions>>().Object,
                              new Mock<IPasswordHasher<ApplicationUser>>().Object,
                              new IUserValidator<ApplicationUser>[0],
                              new IPasswordValidator<ApplicationUser>[0],
                              new Mock<ILookupNormalizer>().Object,
                              new Mock<IdentityErrorDescriber>().Object,
                              new Mock<IServiceProvider>().Object,
                              new Mock<ILogger<UserManager<ApplicationUser>>>().Object);
            _moqsignInManager = new Mock<SignInManager<ApplicationUser>>(
                                _moqUserManager.Object,
                                new Mock<IHttpContextAccessor>().Object,
                                new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                                new Mock<IOptions<IdentityOptions>>().Object,
                                new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
                                new Mock<IAuthenticationSchemeProvider>().Object);
            _moqMapper = new Mock<IMapper>();
            _userRepository = new UserRepository(_moqUserManager.Object, _moqsignInManager.Object, _moqMapper.Object);
        }

        [Fact]
        public async Task LoginAsync_Returns_Boolean()
        {
            var result = await _userRepository.LoginAsync(new UserLoginDTO() { Username = "user@example.com"});
            _moqUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser());
            Assert.IsType<bool>(result);
        }

        [Fact]
        public void LoginAsync_Throws_NullException_If_UserLoginDTO_isNull()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _userRepository.LoginAsync(null));
        }

        [Fact]
        public async Task LoginAsync_Returns_False_If_Password_isInvalid()
        {
            UserLoginDTO userLoginDTO = new UserLoginDTO() { Username = "user@example.com", Password = "12345"};
            _moqUserManager.Setup(x => x.FindByEmailAsync(userLoginDTO.Username)).ReturnsAsync(It.IsAny<ApplicationUser>());
            _moqUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), "1234")).ReturnsAsync(false);
            var result = await _userRepository.LoginAsync(userLoginDTO);

            Assert.False(result);
        }

        [Fact]
        public async Task LoginAsync_Returns_True_If_Password_isValid()
        {
            UserLoginDTO userLoginDTO = new UserLoginDTO() { Username = "user@example.com", Password = "12345" };
            _moqUserManager.Setup(x => x.FindByEmailAsync(userLoginDTO.Username)).ReturnsAsync(new ApplicationUser() { Email = "user@example.com" , FirstName = "Derick", LastName = "Colon"});
            _moqUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), "12345")).ReturnsAsync(true);
            _moqsignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(),"12345",false,false)).Returns(Task.FromResult(SignInResult.Success));
            var result = await _userRepository.LoginAsync(userLoginDTO);

            Assert.True(result);
        }


        [Fact]
        public void RegisterAsync_Throws_Null_Exception()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _userRepository.RegisterAsync(null));
        }

        [Fact]
        public async void RegisterAsync_Returns_IdentityResult()
        {
            UserRegistrationDTO userRegistrationDTO = new UserRegistrationDTO() { Email = "user@example.com", Password = "123456" };
            _moqMapper.Setup(x => x.Map(userRegistrationDTO, typeof(UserRegistrationDTO), typeof(ApplicationUser))).Returns(new ApplicationUser() { Email = "user@example.com"});
            _moqUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), userRegistrationDTO.Password)).ReturnsAsync(IdentityResult.Success);

            var result = await _userRepository.RegisterAsync(userRegistrationDTO);
           
            Assert.IsType<RegisterUserResponse>(result);
            Assert.NotNull(result);
            Assert.True(result.Succeeded);
        }
    }
}
