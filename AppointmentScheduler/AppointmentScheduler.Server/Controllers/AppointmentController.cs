using AppointmentScheduler.Core.DTO;
using AppointmentScheduler.Core.Services.AppointmentServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AppointmentScheduler.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentAdderService _appointmentAdderService;
        public AppointmentController(IAppointmentAdderService appointmentAdderService)
        {
            _appointmentAdderService = appointmentAdderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentDTO createAppointment)
        {
            if (ModelState.IsValid)
            {
                var result = await _appointmentAdderService.CreateAsync(createAppointment);
                if(result)
                {
                    return Created();
                }
                else
                {
                    return BadRequest();
                }
            }

            return BadRequest(ModelState);
        }
    }
}
