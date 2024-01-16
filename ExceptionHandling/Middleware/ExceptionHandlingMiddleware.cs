using Common.Model.DTO;
using Common.Model.DTO.Const;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;

namespace ExceptionHandling.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            IHttpContextAccessor contextAccessor,
            ILogger<ExceptionHandlerMiddleware> logger
            )
        {
            _next = next;
            _httpContextAccessor = contextAccessor;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var bodyObject = new ApiResponseError
            {
                TraceId = _httpContextAccessor?.HttpContext?.TraceIdentifier?.ToString() ?? string.Empty,
                Errors = new List<ErrorModel> { }
            };

            try
            {
                if (context != null)
                {
                    await _next(context);
                }
            }
            catch (BaseException error)
            {
                _logger.LogError(error.MessageCode, error);
                response.StatusCode = (int)error.StatusCode;
                bodyObject.HttpStatus = response.StatusCode;
                bodyObject.MessageCode = error.MessageCode;
                if (error.Errors != null)
                {
                    foreach (var errorModel in error.Errors)
                    {
                        errorModel.Message = errorModel.Message;
                    }
                    bodyObject.Errors = error.Errors;
                }
                var result = JsonConvert.SerializeObject(bodyObject);
                await response.WriteAsync(result);
            }
            catch (Exception error)
            {
                _logger.LogError(error.Message, error);
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                bodyObject.HttpStatus = response.StatusCode;
                bodyObject.MessageCode = Messages.UnexpectedError;
                bodyObject.Errors = new List<ErrorModel>
                {
                    new ErrorModel
                    {
                        Code = error.Source ?? String.Empty,
                        Message = error.Message,
                        Value = JsonConvert.SerializeObject(error.Data)
                    }
                };

                var result = JsonConvert.SerializeObject(bodyObject);
                await response.WriteAsync(result);
            }
        }
    }
}
