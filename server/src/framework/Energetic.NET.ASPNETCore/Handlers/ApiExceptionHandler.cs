using Energetic.NET.SharedKernel.BaseExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using EnergeticCms.WebApi.Filters;

namespace Energetic.NET.ASPNETCore.Handlers
{
    public class ApiExceptionHandler : IExceptionHandler
    {
        private readonly IWebHostEnvironment _environment;

        private readonly ILogger<ApiExceptionFilter> _logger;

        private readonly Dictionary<Type, Action<ExceptionContext>> _exceptionHandles;

        public ApiExceptionHandler(IWebHostEnvironment environment, ILogger<ApiExceptionFilter> logger)
        {
            _environment = environment;
            _logger = logger;
            _exceptionHandles = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
                { typeof(NotFoundException), HandleNotFoundException }
            };
        }

        public ValueTask<bool> TryHandleAsync(HttpContext httpContext,
            Exception exception, CancellationToken cancellationToken)
        {
            var res = exception;
            var http = httpContext;
            return ValueTask.FromResult(false);
        }

        private void HandleException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            if (_exceptionHandles.TryGetValue(exceptionType, out Action<ExceptionContext>? value))
            {
                value.Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            UnHandleException(context);
        }

        private void HandleValidationException(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(GetErrorResponseResult(context.Exception));
            context.ExceptionHandled = true;
        }

        private void HandleForbiddenAccessException(ExceptionContext context)
        {
            context.Result = new ObjectResult(GetErrorResponseResult(context.Exception))
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
            context.ExceptionHandled = true;
        }

        private void HandleUnauthorizedAccessException(ExceptionContext context)
        {
            context.Result = new UnauthorizedObjectResult(GetErrorResponseResult(context.Exception));
            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            context.Result = new NotFoundObjectResult(GetErrorResponseResult(context.Exception));
            context.ExceptionHandled = true;
        }

        private static void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void UnHandleException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "UnhandledException occurred");
            var message = _environment.IsDevelopment() ? context.Exception.ToString() : "UnhandledException occurred";
            context.Result = new ObjectResult(new ErrorResponseResult(message))
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.ExceptionHandled = true;
        }

        private static ErrorResponseResult GetErrorResponseResult(Exception exception)
        {
            if (exception is BaseException baseException)
                return new ErrorResponseResult(baseException.ErrorCode, baseException.ErrorMessage);
            else
                return new ErrorResponseResult(exception.Message);
        }
    }
}
