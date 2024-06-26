﻿using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Core.Services.AppointmentServices.Interfaces;
using AppointmentScheduler.Server.Model;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AppointmentScheduler.Server.Controllers.v1
{
    [ApiVersion(1.0)]
    public class AppointmentController : CustomControllerBase
    {
        private readonly IAppointmentAdderService _appointmentAdderService;
        private readonly IAppointmentGetterService _appointmentGetterService;
        public AppointmentController(IAppointmentAdderService appointmentAdderService,
                                     IAppointmentGetterService appointmentGetterService)
        {
            _appointmentAdderService = appointmentAdderService;
            _appointmentGetterService = appointmentGetterService;

        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateAppointmentDTO createAppointment)
        {
            if (ModelState.IsValid)
            {
                var result = await _appointmentAdderService.CreateAsync(createAppointment);
                if (result)
                {
                    return Created();
                }
                else
                {
                    return Problem(detail: "Unable to create appointment, please try again.", statusCode: 400, title: "Create Appointment");
                }
            }
            return ValidationProblem(ModelState);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _appointmentGetterService.GetAllAppointmentsAsync();
            if (result is not null)
            {
                return Ok(new ApiResponse<List<GetAllAppointmentsDTO>>
                {
                    Success = true,
                    Message = "Successfully retrived the data!",
                    Data = result
                });
            }

            return NotFound();
        }
    }
}
