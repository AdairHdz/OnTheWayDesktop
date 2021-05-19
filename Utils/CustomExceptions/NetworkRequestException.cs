using System;
using System.Net;

namespace Utils.CustomExceptions
{
    public class NetworkRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public NetworkRequestException() { }
        public NetworkRequestException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
