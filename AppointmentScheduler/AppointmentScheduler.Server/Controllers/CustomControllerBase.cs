using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduler.Server.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {

    }
}
