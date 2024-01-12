using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;
using System.Diagnostics;

namespace Energetic.NET.Middleware.Logger
{
    public class DbLoggingMiddleware(RequestDelegate next, ILogger logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var stopWatch = Stopwatch.StartNew();
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await next(context);

            stopWatch.Stop();

            responseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();
            responseBody.Seek(0, SeekOrigin.Begin);
            LogEventLevel level = GetLogLevel(context.Response.StatusCode);
            logger.Write(level, "Request {RequestMethod} {RequestPath} processed in {ElapsedMilliseconds} ms. Response status code: {StatusCode}. Response body: {ResponseBody}",
                context.Request.Method,
                context.Request.Path,
                stopWatch.ElapsedMilliseconds,
                context.Response.StatusCode,
                responseBodyText);

            await responseBody.CopyToAsync(originalBodyStream);
        }

        private static LogEventLevel GetLogLevel(int statusCode)
        {
            return statusCode >= 500 ? LogEventLevel.Error : LogEventLevel.Information;
        }
    }
}
