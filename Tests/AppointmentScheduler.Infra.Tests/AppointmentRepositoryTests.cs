using AppointmentScheduler.Core.Domain.Entities;
using AppointmentScheduler.Core.Domain.Interface;
using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Infrasturcture.Context;
using AppointmentScheduler.Infrasturcture.Repositories;
using AutoMapper;
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
        private readonly Mock<ApplicationDbContext> _moqDbContext;
        private readonly Mock<IMapper> _moqMapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentRepositoryTests()
        {
            _moqDbContext = new Mock<ApplicationDbContext>();
            _moqMapper = new Mock<IMapper>();

            _appointmentRepository = new AppointmentRepository(_moqDbContext.Object, _moqMapper.Object);
        }

        [Fact]
        public async void CreateAsync_Returns_Boolean()
        {
            Appointment appointment = new Appointment();
            _moqMapper.Setup(x => x.Map<Appointment>(It.IsAny<CreateAppointmentDTO>()));
            _moqDbContext.Setup(x => x.Set<Appointment>()).Returns()
            _moqDbContext.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);
        }
    }
}
