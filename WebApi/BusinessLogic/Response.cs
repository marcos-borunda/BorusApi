using System;

namespace WebApi.BusinessLogic
{
    public class Response
    {
        public StatusResponse Status { get; }
        public string? Message { get; }

        public Response(StatusResponse status, string? message = null)
        {
            Status = status;
            Message = message;
        }
    }
}