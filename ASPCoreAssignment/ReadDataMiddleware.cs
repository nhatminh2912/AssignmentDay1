using Serilog;
using Serilog.AspNetCore;

namespace ASPCoreAssignment
{
    public class ReadDataMiddleware
    {
        private readonly RequestDelegate _next;
        public ReadDataMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILoggingMessageWriter svc)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File("log.txt", rollingInterval: RollingInterval.Day).CreateLogger();
            Log.Information(context.Request.Scheme);
            Log.Information(context.Request.Path);
            Log.Information(context.Request.Host.ToString());
            Log.Information(context.Request.QueryString.ToString());
            Log.Information(context.Request.Body.ToString());
            svc.Write("Day la scheme" + " : " + context.Request.Scheme);
            svc.Write("Day la path" + " : " + context.Request.Path);
            svc.Write("Day la host" + " : " + context.Request.Host);
            svc.Write("Day la query" + " : " + context.Request.QueryString);
            svc.Write("Day la request body" + " : " + context.Request.Body);
            await _next(context);
        }
    }

    public static class ReadDataMiddlewareExtensions
    {
        public static IApplicationBuilder UseReadData(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ReadDataMiddleware>();
        }
    }
}
