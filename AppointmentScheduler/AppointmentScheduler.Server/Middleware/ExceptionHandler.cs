using AppointmentScheduler.Server.Model;

namespace AppointmentScheduler.Server.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await _next(context);
            }
            catch(Exception ex) { 
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new ApiResponse<object> { Success = false, Message = "An error occured while processing your request." });
            }
        }
    }
}
