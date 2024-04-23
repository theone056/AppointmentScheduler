
using System.Collections.Generic;
using System.Diagnostics;

namespace AppointmentScheduler.Server.Middleware
{
    public class MeasureRequestProcessingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var sw = new Stopwatch();
            sw.Start();
            await next.Invoke(context);
            sw.Stop();
            Console.WriteLine(String.Format("<!-- {0} ms -->", sw.ElapsedMilliseconds));
        }
    }
}
