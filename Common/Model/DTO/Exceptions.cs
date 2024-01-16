using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Model.DTO
{
    public class ApiResponseError
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
        public string MessageCode { get; set; } = string.Empty;
        [JsonIgnore]
        public int HttpStatus { get; set; }
        public string TraceId { get; set; } = string.Empty;
    }

    public class ErrorModel
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
