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

        private readonly IUnitOfWork _appointmentRepository;
        private readonly DbContextMock<ApplicationDbContext> dbContextMock;
        public AppointmentRepositoryTests()
        {
            dbContextMock = new DbContextMock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().Options);
            _moqDbContext = dbContextMock.Object;
            dbContextMock.CreateDbSetMock(temp=>temp.Appointments, new List<Appointment>());
            _appointmentRepository = new UnitOfWork(_moqDbContext);
        }

        [Fact]
        public async void CreateAsync_Returns_Boolean()
        {
            dbContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
            var result = await _appointmentRepository.Appointments.Add(new Appointment() { Title ="Test 1234"});

            Assert.IsType<Boolean>(result);
        }


        [Fact]
        public async void CreateAsync_Returns_False()
        {
            var result = await _appointmentRepository.Appointments.Add(new Appointment() { Title = "Test 1234" });
            await _appointmentRepository.Save();

            dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async void CreateAsync_Returns_True()
        {
            dbContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            var result = await _appointmentRepository.Appointments.Add(new Appointment() { Title = "Test 1234" });

            Assert.IsType<Boolean>(result);
            Assert.True(result);
        }

        [Fact]
        public void CreateAsync_ArgumentNullException_If_Value_isNull()
        {
            dbContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new ArgumentNullException());

            Assert.ThrowsAsync<ArgumentNullException>(() => _appointmentRepository.Appointments.Add(new Appointment() { Title = "Test 1234" }));
        }

        [Fact]
        public async void GetAllAppointmentsAsync_Returns_List_of_GetAllAppointmentsDTO()
        {
            var result = await _appointmentRepository.Appointments.GetAll();
            Assert.IsType<List<Appointment>>(result);
        }
    }
}
