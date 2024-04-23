using AppointmentScheduler.Server.Model;

namespace AppointmentScheduler.Server.Middleware
{
    public class ExceptionHandler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new ApiResponse<object> { Success = false, Message = "An error occured while processing your request." });
            }
        }
    }
}
