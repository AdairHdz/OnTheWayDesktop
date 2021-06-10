using System;

namespace Utils.CustomExceptions
{
    public class NetworkRequestException : Exception
    {
        public int? StatusCode { get; }
        public NetworkRequestException() { }

        public NetworkRequestException(int? statusCode)
        {
            StatusCode = statusCode;
        }
        public NetworkRequestException(int? statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
