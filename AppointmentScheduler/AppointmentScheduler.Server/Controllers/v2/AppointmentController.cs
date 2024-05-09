using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Core.Services.AppointmentServices.Interfaces;
using AppointmentScheduler.Server.Model;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AppointmentScheduler.Server.Controllers.v2
{
    [ApiVersion(2.0)]
    public class AppointmentController : CustomControllerBase
    {
        private readonly IAppointmentGetterService _appointmentGetterService;
        public AppointmentController(IAppointmentGetterService appointmentGetterService)
        {
            _appointmentGetterService = appointmentGetterService;

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _appointmentGetterService.GetAllAppointmentsAsync();
            if(result is not null)
            {
                return Ok(new ApiResponse<List<GetAllAppointmentsDTO>> { 
                    Success = true, 
                    Message = "Successfully retrived the data! V2",
                    Data = result
                });
            }

            return NotFound();
        }
    }
}
