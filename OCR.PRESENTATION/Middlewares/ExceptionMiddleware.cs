using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OCR.Application;
using OCR.Application.Exceptions;
using OCR.Presentation.Errors;
using System.Net;
using System.Text.Json;

namespace OCR.Presentation.Middlewares
{
    internal class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment hostEnvironment, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                GlobalConstants.SetWebApiURL($"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}/");
                GlobalConstants.SetWebRootPath($"{_webHostEnvironment.WebRootPath}");
                await _next(httpContext);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Application Error Exception: {e}", e.Message);
                httpContext.Response.ContentType = "application/json";
                int statusCode = (int)HttpStatusCode.InternalServerError;
                dynamic exceptionResponse;

                switch (e)
                {
                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        exceptionResponse = new CodeError(statusCode, notFoundException.Message, e.StackTrace);
                        break;
                    case BadRequestException badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        exceptionResponse = new CodeError(statusCode, badRequestException.Message, e.StackTrace);
                        break;
                    default:
                        exceptionResponse = new CodeError(statusCode, e.Message, e.StackTrace);
                        break;
                }

                if (!_hostEnvironment.IsDevelopment())
                {
                    exceptionResponse = new CodeErrorResponse(statusCode, exceptionResponse.Message);
                }

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                string result = JsonSerializer.Serialize(exceptionResponse, options);

                httpContext.Response.StatusCode = statusCode;

                await httpContext.Response.WriteAsync(result);
            }
        }
    }
}
