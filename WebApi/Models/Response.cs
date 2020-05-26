using System;

namespace WebApi.Models
{
    public class Response
    {
        public string Result { get; }
        public string? Message { get; }

        public Response(string result, string? message)
        {
            Result = result ?? throw new ArgumentNullException(nameof(result));
            Message = message;
        }
    }
}