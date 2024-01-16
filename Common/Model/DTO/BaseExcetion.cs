using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.DTO
{
    public class BaseException : Exception
    {
        public Exception? ExceptionTracking { get; set; }
        public HttpStatusCode StatusCode { get; protected set; }
        public string MessageCode { get; protected set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public List<ErrorModel>? Errors { get; protected set; }

        public BaseException()

        {

        }

        public BaseException(string messageCode, Exception? exceptionTracking = null) : base(messageCode)

        {
            MessageCode = messageCode;
            ExceptionTracking = exceptionTracking;
        }

        public BaseException(ErrorModel error, Exception? exceptionTracking = null) : base(error.Message)
        {
            MessageCode = error.Message;
            Errors = new List<ErrorModel>() { error };
            ExceptionTracking = exceptionTracking;
        }

        public BaseException(List<ErrorModel>? errors, string message, Exception? exceptionTracking = null) : base(message)
        {
            MessageCode = message;
            Errors = errors;
            ExceptionTracking = exceptionTracking;
        }
    }
}
