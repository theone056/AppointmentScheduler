using AppointmentScheduler.Core.Domain.Entities;
using AppointmentScheduler.Core.Domain.Interface;
using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Infrasturcture.Context;
using AppointmentScheduler.Infrasturcture.Repositories;
using AutoMapper;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentScheduler.Infra.Tests
{
    public class AppointmentRepositoryTests
    {
        private readonly ApplicationDbContext _moqDbContext;
        private readonly Mock<IMapper> _moqMapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly DbContextMock<ApplicationDbContext> dbContextMock;
        public AppointmentRepositoryTests()
        {
            _moqMapper = new Mock<IMapper>();

            dbContextMock = new DbContextMock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().Options);
            _moqDbContext = dbContextMock.Object;
            dbContextMock.CreateDbSetMock(temp=>temp.Appointments, new List<Appointment>());
            _appointmentRepository = new AppointmentRepository(_moqDbContext, _moqMapper.Object);
        }

        [Fact]
        public async void CreateAsync_Returns_Boolean()
        {
            _moqMapper.Setup(x => x.Map<Appointment>(It.IsAny<CreateAppointmentDTO>())).Returns(new Appointment() { Id=Guid.NewGuid(), Title = "Test 1234"});
            dbContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
            var result = await _appointmentRepository.CreateAsync(new CreateAppointmentDTO() { Title ="Test 1234"});

            Assert.IsType<Boolean>(result);
        }


        [Fact]
        public async void CreateAsync_Returns_False()
        {
            _moqMapper.Setup(x => x.Map<Appointment>(It.IsAny<CreateAppointmentDTO>())).Returns(new Appointment() { Id = Guid.NewGuid(), Title = "Test 1234" });
            dbContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
            var result = await _appointmentRepository.CreateAsync(new CreateAppointmentDTO() { Title = "Test 1234" });

            Assert.IsType<Boolean>(result);
            Assert.False(result);
        }

        [Fact]
        public async void CreateAsync_Returns_True()
        {
            _moqMapper.Setup(x => x.Map<Appointment>(It.IsAny<CreateAppointmentDTO>())).Returns(new Appointment() { Id = Guid.NewGuid(), Title = "Test 1234" });
            dbContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            var result = await _appointmentRepository.CreateAsync(new CreateAppointmentDTO() { Title = "Test 1234" });

            Assert.IsType<Boolean>(result);
            Assert.True(result);
        }

        [Fact]
        public void CreateAsync_ArgumentNullException_If_Value_isNull()
        {
            dbContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new ArgumentNullException());

            Assert.ThrowsAsync<ArgumentNullException>(() => _appointmentRepository.CreateAsync(new CreateAppointmentDTO() { Title = "Test 1234" }));
        }

        [Fact]
        public async void GetAllAppointmentsAsync_Returns_List_of_GetAllAppointmentsDTO()
        {
            dbContextMock.Setup(x => x.Appointments.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<Appointment>() { new Appointment() { Title = "Test" } });
            List<GetAllAppointmentsDTO> result = await _appointmentRepository.GetAllAppointmentsAsync();

            Assert.IsType<List<Appointment>>(result);
        }
    }
}
